using System.Net;
using System.Threading;
using System.Threading.Tasks;
using C3.Infrastructure;
using C3.Infrastructure.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace C3.Features.Comments
{
    public class List
    {
        public record Query(string Slug) : IRequest<CommentsEnvelope>;

        public class QueryHandler : IRequestHandler<Query, CommentsEnvelope>
        {
            private readonly C3Context _context;

            public QueryHandler(C3Context context)
            {
                _context = context;
            }

            public async Task<CommentsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var article = await _context.Articles
                    .Include(x => x.Comments)
                        .ThenInclude(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (article == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Article = Constants.NOT_FOUND });
                }

                return new CommentsEnvelope(article.Comments);
            }
        }
    }
}
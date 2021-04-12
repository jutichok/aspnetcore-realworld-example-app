using System.Net;
using System.Threading;
using System.Threading.Tasks;
using C3.Infrastructure;
using C3.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace C3.Features.Articles
{
    public class Details
    {
        public record Query(string Slug) : IRequest<ArticleEnvelope>;

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, ArticleEnvelope>
        {
            private readonly C3Context _context;

            public QueryHandler(C3Context context)
            {
                _context = context;
            }

            public async Task<ArticleEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var article = await _context.Articles.GetAllData()
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (article == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Article = Constants.NOT_FOUND });
                }
                return new ArticleEnvelope(article);
            }
        }
    }
}
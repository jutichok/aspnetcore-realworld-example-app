using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using C3.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace C3.Features.Tags
{
    public class List
    {
        public record Query : IRequest<TagsEnvelope>;

        public class QueryHandler : IRequestHandler<Query, TagsEnvelope>
        {
            private readonly C3Context _context;

            public QueryHandler(C3Context context)
            {
                _context = context;
            }

            public async Task<TagsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var tags = await _context.Tags.OrderBy(x => x.TagId).AsNoTracking().ToListAsync(cancellationToken);
                return new TagsEnvelope()
                {
                    Tags = tags.Select(x => x.TagId).ToList()
                };
            }
        }
    }
}
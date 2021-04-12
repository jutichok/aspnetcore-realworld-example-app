using System.Threading;
using System.Threading.Tasks;
using C3.Features.Articles;
using C3.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C3.Features.Favorites
{
    [Route("articles")]
    public class FavoritesController : Controller
    {
        private readonly IMediator _mediator;

        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{slug}/favorite")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public Task<ArticleEnvelope> FavoriteAdd(string slug, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Add.Command(slug), cancellationToken);
        }

        [HttpDelete("{slug}/favorite")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public Task<ArticleEnvelope> FavoriteDelete(string slug, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Delete.Command(slug), cancellationToken);
        }
    }
}
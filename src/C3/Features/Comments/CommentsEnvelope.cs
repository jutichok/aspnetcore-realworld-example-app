using System.Collections.Generic;
using C3.Domain;

namespace C3.Features.Comments
{
    public record CommentsEnvelope(List<Comment> Comments);
}
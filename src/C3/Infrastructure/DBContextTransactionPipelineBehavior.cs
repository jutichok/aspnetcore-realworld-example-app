using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace C3.Infrastructure
{
    /// <summary>
    /// Adds transaction to the processing pipeline
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class DBContextTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly C3Context _context;

        public DBContextTransactionPipelineBehavior(C3Context context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse result = default(TResponse);

            try
            {
                _context.BeginTransaction();

                result = await next();

                _context.CommitTransaction();
            }
            catch (Exception)
            {
                _context.RollbackTransaction();
                throw;
            }

            return result;
        }
    }
}

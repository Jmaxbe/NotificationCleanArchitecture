using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Saves changes async
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken</param>
    /// <returns></returns>
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
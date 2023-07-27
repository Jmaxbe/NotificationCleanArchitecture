using System.Linq.Expressions;
using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Persistence.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _entities;

    protected BaseRepository(DbContext context)
    {
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _entities.FirstOrDefaultAsync(_=>_.UniqueId == id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _entities.Where(predicate).AsQueryable();
    }

    public void Add(T entity)
    {
        _entities.Attach(entity);
        _entities.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        var baseEntities = entities.ToList();
        _entities.AttachRange(baseEntities);
        _entities.AddRange(baseEntities);
    }
    
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _entities.Attach(entity);
        await _entities.AddAsync(entity, cancellationToken);
    }

    public async Task AddWithDomainEventAsync(T entity, BaseEvent @event, CancellationToken cancellationToken = default)
    {
        _entities.Attach(entity);
        entity.AddDomainEvent(@event);
        await _entities.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var baseEntities = entities.ToList();
        _entities.AttachRange(baseEntities);    
        await _entities.AddRangeAsync(baseEntities, cancellationToken);
    }

    public void Update(T entity)
    {
        _entities.Attach(entity);
        _entities.Update(entity);
    }
    
    public void UpdateWithDomainEvent(T entity, BaseEvent @event)
    {
        _entities.Attach(entity);
        entity.AddDomainEvent(@event);
        _entities.Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        var baseEntities = entities.ToList();
        _entities.AttachRange(baseEntities);
        _entities.UpdateRange(baseEntities);
    }
    
    public void Remove(T entity)
    {
        _entities.Attach(entity);
        _entities.Remove(entity);
    }
    
    public void RemoveWithDomainEvent(T entity, BaseEvent @event)
    {
        _entities.Attach(entity);
        entity.AddDomainEvent(@event);
        _entities.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        var baseEntities = entities.ToList();
        _entities.AttachRange(baseEntities);
        _entities.RemoveRange(baseEntities);
    }
}
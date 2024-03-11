namespace SpecificationPatternHelperApp.Base;

public abstract class BaseSpecificationService<TEntity, TContext> where TEntity
    : class
    where TContext : DbContext
{
    protected readonly TContext _context;

    protected BaseSpecificationService(TContext context)
    {
        _context = context;
    }

    protected ISpecification<TEntity> Specification { get; set; } =
        null!;

    protected IQueryable<TEntity> GetQuery()
    {
        return SpecificationBuilder<TEntity>
            .GetQuery(_context.Set<TEntity>().AsQueryable(),
                Specification);
    }
}
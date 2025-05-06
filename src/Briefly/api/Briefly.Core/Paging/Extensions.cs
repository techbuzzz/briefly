using Ardalis.Specification;

namespace Briefly.Core.Paging;

public static class Extensions
{
    public static async Task<PagedList<TDestination>> PaginatedListAsync<T, TDestination>(
        this IReadRepositoryBase<T> repository, ISpecification<T, TDestination> spec, PaginationFilter filter,
        CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class
    {
        ArgumentNullException.ThrowIfNull(repository);

        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, cancellationToken).ConfigureAwait(false);

        return new PagedList<TDestination>(items, filter.PageNumber, filter.PageSize, totalCount);
    }
}
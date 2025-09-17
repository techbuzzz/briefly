using Ardalis.Specification;
using Briefly.Core.Paging;

namespace Briefly.Core.Specifications;

public class EntitiesByBaseFilterSpec<T, TResult> : Specification<T, TResult>
{
    public EntitiesByBaseFilterSpec(BaseFilter filter)
    {
        Query.SearchBy(filter);
    }
}

public class EntitiesByBaseFilterSpec<T> : Specification<T>
{
    public EntitiesByBaseFilterSpec(BaseFilter filter)
    {
        Query.SearchBy(filter);
    }
}
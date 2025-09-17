using Ardalis.Specification;

namespace Briefly.Core.Specifications;

public class OrderedSpecificationBuilder<T> : IOrderedSpecificationBuilder<T>
{
    public OrderedSpecificationBuilder(Specification<T> specification)
    {
        Specification = specification;
    }

    public Specification<T> Specification { get; }
}
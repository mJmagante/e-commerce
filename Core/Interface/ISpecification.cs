using System.Linq.Expressions;

namespace Core.Interface;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
    bool IsDistinct { get; }
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}


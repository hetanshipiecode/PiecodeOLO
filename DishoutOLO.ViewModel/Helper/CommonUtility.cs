using System.Linq.Expressions;

namespace DishoutOLO.ViewModel.Helper
{
    public static class CommonUtility
    {
        public static IQueryable<TSource> WhereIf<TSource>(
    this IQueryable<TSource> source,
    bool condition,
    Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
    }
}

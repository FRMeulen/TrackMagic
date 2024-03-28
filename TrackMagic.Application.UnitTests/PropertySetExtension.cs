using System.Linq.Expressions;
using System.Reflection;

namespace TrackMagic.Application.UnitTests
{
    public static class PropertySetExtension
    {
        public static T With<T, TValue>(this T target, Expression<Func<T, TValue>> memberLambda, TValue value)
        {
            var memberSelector = memberLambda.Body as MemberExpression;
            if (memberSelector != null)
            {
                var property = memberSelector.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                }
            }

            return target;
        }
    }
}

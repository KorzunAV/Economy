using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Economy.Common
{
    /// <summary>
    /// 	Represents helper class for types.
    /// </summary>
    public class TypeHelper
    {

        private const string PROPERY_ACCESSOR = ".";

        /// <summary>
        /// 	Get the property info.
        /// </summary>
        private static PropertyInfo GetPropertyInternal(LambdaExpression p)
        {
            MemberExpression memberExpression;

            if (p.Body is UnaryExpression)
            {
                var ue = (UnaryExpression)p.Body;
                memberExpression = (MemberExpression)ue.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)p.Body;
            }

            return (PropertyInfo)(memberExpression).Member;
        }

        /// <summary>
        /// 	Gets name of property.
        /// </summary>
        public static string PropertyName<T>(Expression<Func<T>> expression)
        {
            return GetPropertyInternal(expression).Name;
        }

        /// <summary>
        /// 	Gets name of property.
        /// </summary>
        public static string PropertyName<TP,T>(Expression<Func<TP>> pe, Expression<Func<T>> expression)
        {
            return string.Join(PROPERY_ACCESSOR,
                        new[]
                            {
                                GetPropertyInternal(pe).Name,
                                GetPropertyInternal(expression).Name
                            });

        }

        /// <summary>
        /// 	Gets name of property.
        /// </summary>
        public static string PropertyName<TP1, TP2, T>(Expression<Func<TP1>> ppe, Expression<Func<TP2>> pe, Expression<Func<T>> expression)
        {
            return string.Join(PROPERY_ACCESSOR,
                        new[]
                            {
                                GetPropertyInternal(ppe).Name, 
                                GetPropertyInternal(pe).Name,
                                GetPropertyInternal(expression).Name
                            });

        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace CodeKnight.Core
{
    public static class PInfo<T>
    {
        public static PropertyInfo Info<TProperty>(Expression<Func<T, TProperty>> propertyLambda, bool checkType = true)
        {
            Type type = typeof(T);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (checkType && type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }

        public static string Name<TProperty>(Expression<Func<T, TProperty>> propertyLambda)
        {
            return Info<TProperty>(propertyLambda).Name;
        }
    }
}

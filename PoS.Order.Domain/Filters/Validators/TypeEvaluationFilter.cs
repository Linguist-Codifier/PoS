using System;
using System.Linq;
using System.Reflection;
using PoS.Order.Domain.Models;

namespace PoS.Order.Domain.Filters.Validators
{
    /// <summary>
    /// Provides common type-evaluations for runtime <see cref="Type"/> checking.
    /// </summary>
    public static class TypeEvaluationFilter
    {
        /// <summary>
        /// Evaluates whether this current <see cref="MemberInfo"/> has the attribute <paramref name="targetType"/>.
        /// </summary>
        /// <param name="_">This current <see cref="MemberInfo"/>.</param>
        /// <param name="targetType">The target attribute <see cref="Type"/> to be searched for.</param>
        /// <returns><see langword="true"/> whether this current <see cref="MemberInfo"/> has the target specified attribute <see cref="Type"/>; otherwise <see langword="false"/>.</returns>
        public static Boolean HasAttribute(this MemberInfo _, Type targetType) => _.GetCustomAttribute(targetType) is not null;

        /// <summary>
        /// Evaluates whether this current <see cref="Type"/> is equal to <typeparamref name="RuntimeType"/>.
        /// </summary>
        /// <typeparam name="RuntimeType">The target <see cref="Type"/> to compared to.</typeparam>
        /// <param name="_">This current <see cref="Type"/>.</param>
        /// <returns><see langword="true"></see> whether this current <see cref="Type"/> is equal to <typeparamref name="RuntimeType"/>; otherwise <see langword="false"></see>.</returns>
        public static Boolean Equals<RuntimeType>(this System.Type _) where RuntimeType : class => _ == typeof(RuntimeType);

        /// <summary>
        /// Evaluates whether this current <see cref="Type"/> has the <typeparamref name="RuntimeType"/> generic type.
        /// </summary>
        /// <typeparam name="RuntimeType">The target generic type argument to be compared to.</typeparam>
        /// <param name="_">This current <see cref="Type"/>.</param>
        /// <returns><see langword="true"/> whether this current <see cref="Type"/> has the <typeparamref name="RuntimeType"/> generic type argument; otherwise <see langword="false"></see>.</returns>
        public static Boolean HasGenericTypeArgument<RuntimeType>(this Type _) where RuntimeType : OrderType => _.GenericTypeArguments.Any<Type>(type => type.Name == typeof(RuntimeType).Name);
    }
}
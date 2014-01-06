using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RyanConrad.AttachToAny.Extensions {
	public static partial class AttachToAnyExtensions {

		/// <summary>
		/// Gets the custom attribute.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static T GetCustomAttribute<T> ( this Type type ) where T : Attribute {
			var attr = type.GetCustomAttributes<T> ( ).FirstOrDefault ( );
			return attr;
		}

		/// <summary>
		/// Gets the custom attribute value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="Expected">The type of the Expected.</typeparam>
		/// <param name="type">The type.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static Expected GetCustomAttributeValue<T, Expected> ( this Type type, Func<T, Expected> expression ) where T : Attribute {
			var attribute = type.GetCustomAttribute<T> ( );
			if ( attribute == null )
				return default ( Expected );
			return expression ( attribute );
		}

		/// <summary>
		/// Determines whether [is] [the specified attribute].
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="t">The type.</param>
		/// <returns></returns>
		public static bool Is<TType> ( this TType t ) where TType : class {
			return t.GetType ( ).Is<TType> ( );
		}

		/// <summary>
		/// Determines whether [is] [the specified attribute].
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="t">The type.</param>
		/// <returns></returns>
		public static bool Is<TType> ( this object t ) {
			return t is TType;
		}

		/// <summary>
		/// Determines whether [is] [the specified attribute].
		/// </summary>
		/// <typeparam name="TType">The type of the type.</typeparam>
		/// <param name="t">The attribute.</param>
		/// <returns></returns>
		public static bool Is<TType> ( this Type t ) where TType : class {
			return typeof ( TType ).IsAssignableFrom ( t );
		}
	}
}

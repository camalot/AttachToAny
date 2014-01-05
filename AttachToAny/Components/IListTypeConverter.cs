using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanConrad.AttachToAny.Components {
	internal class IListTypeConverter : TypeConverter {
		/// <summary>
		/// Returns whether this converter can convert the object to the specified type, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		/// true if this converter can perform the conversion; otherwise, false.
		/// </returns>
		public override bool CanConvertTo ( ITypeDescriptorContext context, Type destinationType ) {
			if ( destinationType == typeof ( String ) )
				return true;
			else
				return false;
		}

		public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType ) {
			if ( destinationType == typeof ( String ) && value != null ) {
				int count = ( (IList)value ).Count;
				return string.Format ( "({0} Item{1})", count, count == 1 ? string.Empty : "s" );
			} else
				return null;
		}

	}
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyanConrad.AttachToAny.Components {
	public class CollectionEditor : System.ComponentModel.Design.CollectionEditor {
		public CollectionEditor ( Type type )
			: base ( type ) {

		}

		private CollectionForm EditorForm { get; set; }


		/// <summary>
		/// Edits the value.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="provider">The provider.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public override object EditValue ( ITypeDescriptorContext context, IServiceProvider provider, object value ) {
			return base.EditValue ( context, provider, value );
		}

		/// <summary>
		/// Creates a new form to display and edit the current collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.ComponentModel.Design.CollectionEditor.CollectionForm"></see> to provide as the user interface for editing the collection.
		/// </returns>
		protected override System.ComponentModel.Design.CollectionEditor.CollectionForm CreateCollectionForm ( ) {
			this.EditorForm = base.CreateCollectionForm ( ); ;
			return this.EditorForm;
		}

		protected override Type[] CreateNewItemTypes ( ) {
			return new Type[] { this.CollectionItemType };
		}

	}

	public class CollectionEditor<T> : CollectionEditor {
		public CollectionEditor ( ) : base(typeof(T)) {

		}

	}
}
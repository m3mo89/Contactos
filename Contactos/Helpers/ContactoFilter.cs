using System;
using System.Collections.Generic;
using System.Linq;
using Android.Widget;
using Contactos.Adapters;
using Contactos.Data;
using Java.Lang;
using Object = Java.Lang.Object;

namespace Contactos.Helpers
{
    public class ContactoFilter : Filter
    {
        private ContactosAdapter _adapter;

        public ContactoFilter(ContactosAdapter adapter)
        {
            this._adapter = adapter;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            var returnObj = new FilterResults();
            var results = new List<Contacto>();
            if (_adapter.originalData == null)
                _adapter.originalData = _adapter.contactos;

            if (constraint == null) return returnObj;

            if (_adapter.originalData != null && _adapter.originalData.Any())
            {
                // Compare constraint to all names lowercased. 
                // It they are contained they are added to results.
                results.AddRange(
                    _adapter.originalData.Where(
                        chemical => chemical.Name.ToLower().Contains(constraint.ToString())));
            }

            // Nasty piece of .NET to Java wrapping, be careful with this!
            returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
            returnObj.Count = results.Count;

            constraint.Dispose();

            return returnObj;
        }

        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            using (var values = results.Values)
                _adapter.contactos = values.ToArray<Object>()
                    .Select(r => r.ToNetObject<Contacto>()).ToList();

            _adapter.NotifyDataSetChanged();

            // Don't do this and see GREF counts rising
            constraint.Dispose();
            results.Dispose();
        }
    }
}

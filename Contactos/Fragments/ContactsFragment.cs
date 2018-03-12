using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Contactos.Adapters;
using Contactos.Data;

namespace Contactos.Fragments
{
    public class ContactsFragment: Android.Support.V4.App.Fragment
    {
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            var view = inflater.Inflate(Resource.Layout.Contacto, container, false);

            var contactoList = view.FindViewById<ListView>(Resource.Id.contactoListView);
            //contactoList.FastScrollEnabled = true;
            contactoList.ItemClick += OnItemClick;

            contactoList.Adapter = new ContactosAdapter(ContactoData.Contacts);

            return view;
		}

        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int contacto = e.Position;

            var intent = new Intent(this.Activity.ApplicationContext, typeof(ContactoDetalleActivity));

            intent.PutExtra("Contacto", contacto);

            StartActivity(intent);  
        }
	}
}

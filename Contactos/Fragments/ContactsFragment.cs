using System;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Contactos.Adapters;
using Contactos.Data;
using Contactos.Helpers;

namespace Contactos.Fragments
{
    public class ContactsFragment: Android.Support.V4.App.Fragment
    {
        ContactosAdapter adapter;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            var view = inflater.Inflate(Resource.Layout.Contacto, container, false);

            var contactoList = view.FindViewById<ListView>(Resource.Id.contactoListView);
            //contactoList.FastScrollEnabled = true;
            contactoList.ItemClick += OnItemClick;

            adapter = new ContactosAdapter(ContactoData.Contacts);
            contactoList.Adapter = adapter;

            return view;
		}

        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int contacto = e.Position;

            var realPosition = adapter?.contactos[contacto];

            contacto = ContactoData.Contacts.IndexOf(realPosition);

            var intent = new Intent(this.Activity.ApplicationContext, typeof(ContactoDetalleActivity));

            intent.PutExtra("Contacto", contacto);

            StartActivity(intent);  
        }

		public override void OnCreate(Bundle savedInstanceState)
		{
            base.OnCreate(savedInstanceState);

            HasOptionsMenu = true;
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
            inflater.Inflate(Resource.Menu.search,menu);

            var search = menu.FindItem(Resource.Id.action_search);
            var searchView = search.ActionView.JavaCast<Android.Support.V7.Widget.SearchView>();

            searchView.QueryHint = "Buscar contacto";

            searchView.QueryTextChange += OnQueryTextChange;

            searchView.QueryTextSubmit += OnQueryTextSubmit;

            MenuItemCompat.SetOnActionExpandListener(search, new SearchViewExpandListener(adapter));

            base.OnCreateOptionsMenu(menu, inflater);
		}

        private void OnQueryTextChange(object s, Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs e)
        {
            adapter.Filter.InvokeFilter(e.NewText);
        }

        private void OnQueryTextSubmit(object s, Android.Support.V7.Widget.SearchView.QueryTextSubmitEventArgs e)
        {
            //TODO: Do something fancy when search button on keyboard is pressed
            //Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
            e.Handled = true;
        }
	}
}

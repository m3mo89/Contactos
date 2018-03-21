using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Contactos.Adapters;
using Android.Support.V4.View;
using Contactos.Helpers;
using Contactos.Data;
using Android.Support.V4.App;
using static Contactos.Fragments.ContactsFragment;
using Android.Support.Design.Widget;
using System;
using Android.Content;

namespace Contactos
{
    [Activity(Label = "Contactos")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        HomeAdapter adapter;
        Fragments.ContactsFragment fragmentContactos;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            base.SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            fragmentContactos = new Fragments.ContactsFragment(false);

            var fragments = new Android.Support.V4.App.Fragment[]
            {
                fragmentContactos,
                new Fragments.ContactsFragment(true),
                new Fragments.AboutFragment()
            };

            var titles = CharSequence.ArrayFromStringArray(new [] { "Contactos", "Favoritos", "Acerca de" }); 

            adapter = new HomeAdapter(base.SupportFragmentManager, fragments, titles);

            var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewPager);

            viewPager.Adapter = adapter;
            viewPager.OffscreenPageLimit = 3;

            var btnFab = (FloatingActionButton)FindViewById(Resource.Id.fab);
            btnFab.Click += OnAddButtonClick;

            var tabLayout = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tabLayout);
            tabLayout.SetupWithViewPager(viewPager);

            tabLayout.GetTabAt(0).SetIcon(Resource.Mipmap.ic_contacts_white_24dp);
            tabLayout.GetTabAt(1).SetIcon(Resource.Mipmap.ic_favorite_white_24dp);
            tabLayout.GetTabAt(2).SetIcon(Resource.Mipmap.ic_info_outline_white_24dp);
        }

        private void OnAddButtonClick(object s, EventArgs e)
        {
            StartActivityForResult(typeof(AgregarContactoActivity), 200);

        }

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
            base.OnActivityResult(requestCode, resultCode, data);

            if(resultCode == Result.Ok && requestCode == 200)
            {
                fragmentContactos.NotifyAdapter();
            }
		}
	}
}


using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Contactos.Adapters;
using Android.Support.V4.View;
using Contactos.Helpers;
using Contactos.Data;

namespace Contactos
{
    [Activity(Label = "Contactos")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        private Android.Support.V7.Widget.SearchView _searchView;
        private Android.Support.V7.Widget.Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            base.SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
           

            var fragments = new Android.Support.V4.App.Fragment[]
            {
                new Fragments.ContactsFragment(),
                new Fragments.AboutFragment()
            };

            var titles = CharSequence.ArrayFromStringArray(new [] { "Contactos", "Acerca de" }); 

            var adapter = new HomeAdapter(base.SupportFragmentManager, fragments, titles);

            var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewPager);

            viewPager.Adapter = adapter;
            viewPager.OffscreenPageLimit = 2;

            var tabLayout = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tabLayout);
            tabLayout.SetupWithViewPager(viewPager);

            tabLayout.GetTabAt(0).SetIcon(Resource.Mipmap.ic_contacts_white_24dp);
            tabLayout.GetTabAt(1).SetIcon(Resource.Mipmap.ic_info_outline_white_24dp);
        }


    }
}


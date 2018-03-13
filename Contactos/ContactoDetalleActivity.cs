
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Contactos.Data;
using Contactos.Helpers;

namespace Contactos
{
    [Activity(Label = "ContactoDetalleActivity")]
    public class ContactoDetalleActivity : Android.Support.V7.App.AppCompatActivity
    {
        Contacto contactoSelected;
        TextView phone;
        Android.Support.V7.Widget.Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ContactoDetalle);

            int contacto = Intent.GetIntExtra("Contacto", -1);

            contactoSelected = ContactoData.Contacts[contacto];

            var image = FindViewById<ImageView>(Resource.Id.contactoImageView);
            var name = FindViewById<TextView>(Resource.Id.contactoNameTextView);
            var call = FindViewById<ImageButton>(Resource.Id.contactoCallImageButton);

            phone = FindViewById<TextView>(Resource.Id.contactoPhoneTextVIEW);

            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);


            base.SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_arrow_back_white_24dp);

            name.Text = contactoSelected.Name;
            phone.Text = contactoSelected.Phone;
            image.SetImageDrawable(ImageAssetManager.Get(this, contactoSelected.ImageUrl));

            call.Click += OnCallClick;
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            base.MenuInflater.Inflate(Resource.Menu.actions, menu);
            SetFavoriteDrawable(contactoSelected.IsFavorite);
            return true;
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.addToFavorites:
                    contactoSelected.IsFavorite = !contactoSelected.IsFavorite;
                    SetFavoriteDrawable(contactoSelected.IsFavorite);
                    break;

                case Android.Resource.Id.Home:
                    Finish();
                    break;
            }

            return true;
        }

        private void SetFavoriteDrawable(bool isFavorite)
        {
            if (isFavorite)
                toolbar.Menu.FindItem(Resource.Id.addToFavorites).SetIcon(Resource.Mipmap.ic_favorite_white_24dp); // filled in 'heart' image
            else
                toolbar.Menu.FindItem(Resource.Id.addToFavorites).SetIcon(Resource.Mipmap.ic_favorite_border_white_24dp); // 'heart' image border only
        }

        private void OnCallClick(object s, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetAction(Intent.ActionCall);

            var uri = $"tel: {phone.Text}";
            intent.SetData(Android.Net.Uri.Parse(uri));

            StartActivity(intent);
        }
    }
}


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
    public class ContactoDetalleActivity : Activity
    {
        TextView phone;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ContactoDetalle);

            int contacto = Intent.GetIntExtra("Contacto", -1);

            var item = ContactoData.Contacts[contacto];

            var image = FindViewById<ImageView>(Resource.Id.contactoImageView);
            var name = FindViewById<TextView>(Resource.Id.contactoNameTextView);
            var call = FindViewById<ImageButton>(Resource.Id.contactoCallImageButton);

            phone = FindViewById<TextView>(Resource.Id.contactoPhoneTextVIEW);

            name.Text = item.Name;
            phone.Text = item.Phone;
            image.SetImageDrawable(ImageAssetManager.Get(this, item.ImageUrl));

            call.Click += OnCallClick;
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

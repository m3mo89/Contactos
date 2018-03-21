
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Contactos.Data;
using Contactos.Helpers;
using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace Contactos
{
    [Activity(Label = "AgregarContactoActivity")]
    public class AgregarContactoActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static readonly Int32 REQUEST_CAMERA = 0;
        private static readonly Int32 SELECT_FILE = 1;
        private Button SaveButton;
        private TextInputLayout NameTextInputLayout;
        private TextInputLayout PhoneTextInputLayout;
        private string Path = string.Empty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AgregarContacto);

            //App bar
            var toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.appbar);
            base.SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_arrow_back_white_24dp);

            //CollapsingToolbarLayout
            var ctlLayout = (Android.Support.Design.Widget.CollapsingToolbarLayout)FindViewById(Resource.Id.ctlLayout);

            var btnFab = (Android.Support.Design.Widget.FloatingActionButton)FindViewById(Resource.Id.btnFab);
            btnFab.Click += OnCameraButtonClick;

            SaveButton = FindViewById<Button>(Resource.Id.saveButton);
            SaveButton.Click += OnSaveButtonClick;

            NameTextInputLayout = FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.nameTextInputLayout);
            PhoneTextInputLayout = FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.phoneTextInputLayout);
       
            CreateDirectoryForPictures();
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    break;
            }

            return true;
        }


        private void OnSaveButtonClick(object s, EventArgs e)
        { 
            var name = FindViewById<EditText>(Resource.Id.nameEditText).Text;
            var phone = FindViewById<EditText>(Resource.Id.phoneEditText).Text;


            bool nombreValido = IsNombreValido(name);
            bool telefonoValido = IsTelefonoValido(phone);

            if(nombreValido && telefonoValido)
            {
                Contacto contacto = new Contacto();
                contacto.Name = name;
                contacto.Phone = phone;
                contacto.ImageUrl = string.IsNullOrEmpty(Path) ? "images/noimage.png": Path;

                this.RunOnUiThread(() => 
                {
                    Data.ContactoData.Contacts.Add(contacto);
                });

                SetResult(Result.Ok);

                Finish();
            }
        }

        private bool IsNombreValido(String nombre)
        {
            Java.Util.Regex.Pattern patron = Java.Util.Regex.Pattern.Compile("^[a-zA-Z ]+$");
            if (!patron.Matcher(nombre).Matches() || nombre.Length > 30)
            {
                NameTextInputLayout.Error = "Nombre invalido";
                return false;
            }
            else
            {
                int existe = Data.ContactoData.Contacts.Where(x => x.Name.ToLower().Trim() == nombre.ToLower().Trim()).Count();
                if(existe > 0)
                {
                    NameTextInputLayout.Error = "Nombre existente";
                    return false;
                }
                else
                {
                    NameTextInputLayout.Error = null;
                }
            }

            return true;
        }

        private bool IsTelefonoValido(String telefono)
        {
            if (!Android.Util.Patterns.Phone.Matcher(telefono).Matches())
            {
                PhoneTextInputLayout.Error = "Telefono invalido";
                return false;
            }
            else
            {
                PhoneTextInputLayout.Error = null;
            }

            return true;
        }

        private void OnCameraButtonClick(object s, EventArgs e)
        {
            String[] items = { "Tomar foto", "Elegir de la galeria", "Cancelar" };

            using (var dialogBuilder = new AlertDialog.Builder(this))
            {
                dialogBuilder.SetTitle("Agregar foto");
                dialogBuilder.SetItems(items, (d, args) => {
                    //Take photo
                    if (args.Which == 0)
                    {
                        if (IsThereAnAppToTakePictures())
                        {
                            var intent = new Intent(MediaStore.ActionImageCapture);
                            CamaraHelper._file = new File(CamaraHelper._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

                            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(CamaraHelper._file));
                            this.StartActivityForResult(intent, REQUEST_CAMERA);
                        }else
                        {
                            Toast.MakeText(this, "No hay una app para tomar fotos",ToastLength.Long).Show();
                        }
                    }
                    //Choose from gallery
                    else if (args.Which == 1)
                    {
                        var intent = new Intent();//Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
                        intent.SetType("image/*");
                        intent.SetAction(Intent.ActionGetContent);
                        this.StartActivityForResult(Intent.CreateChooser(intent, "Seleccionar imagen"), SELECT_FILE);
                    }
                });

                dialogBuilder.Show();
            }
        }

        private void CreateDirectoryForPictures()
        {
            CamaraHelper._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "CameraAppDemo");
            if (!CamaraHelper._dir.Exists())
            {
                CamaraHelper._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var imageView = FindViewById<ImageView>(Resource.Id.expandedImage);

                if (requestCode == REQUEST_CAMERA)
                {
                    Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                    Uri contentUri = Uri.FromFile(CamaraHelper._file);
                    mediaScanIntent.SetData(contentUri);
                    SendBroadcast(mediaScanIntent);

                    int height = Resources.DisplayMetrics.HeightPixels;
                    int width = imageView.Height;
                    CamaraHelper.bitmap = CamaraHelper._file.Path.LoadAndResizeBitmap(width, height);
                    Path = CamaraHelper._file.Path;

                    if (CamaraHelper.bitmap != null)
                    {
                        imageView.SetImageBitmap(CamaraHelper.bitmap);
                        CamaraHelper.bitmap = null;
                    }

                    // Dispose of the Java side bitmap.
                    GC.Collect();

                }
                else if (requestCode == SELECT_FILE)
                {
                    imageView.SetImageURI(data.Data);

                    Path = GetPathToImage(data.Data);
                }
            }
		}

        private string GetPathToImage(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ManagedQuery(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;
        }
	}
}

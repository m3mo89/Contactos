using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;
using Android.Views;
using Android.Widget;
using Contactos.Data;
using Contactos.Fragments;
using Contactos.Helpers;

namespace Contactos.Adapters
{
    public class ContactosAdapter: BaseAdapter<Contacto>, ISectionIndexer
    {
        List<Contacto> contactos;
        Java.Lang.Object[] sectionHeaders;
        Dictionary<int, int> positionForSectionMap;
        Dictionary<int, int> sectionForPositionMap;

        public ContactosAdapter(List<Contacto> contactos)
        {
            this.contactos = contactos;

            sectionHeaders = SectionIndexerBuilder.BuildSectionHeaders(contactos);
            positionForSectionMap = SectionIndexerBuilder.BuildPositionForSectionMap(contactos);
            sectionForPositionMap = SectionIndexerBuilder.BuildSectionForPositionMap(contactos);
        }

        public override Contacto this[int position]
        {
            get
            {
                return contactos[position];
            }
        }

        public override int Count
        {
            get
            {
                return contactos.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;

            if (convertView == null)
            {
                convertView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ContactoRow, parent, false);

                holder = new ViewHolder() 
                { 
                    Photo = convertView.FindViewById<ImageView>(Resource.Id.photoImageView), 
                    Name = convertView.FindViewById<TextView>(Resource.Id.nameTextView), 
                    Phone = convertView.FindViewById<TextView>(Resource.Id.phoneTextView) 
                };
                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }


            var photoDrawable = ImageAssetManager.Get(parent.Context, contactos[position].ImageUrl);

            holder.Photo.SetImageDrawable(photoDrawable);
            holder.Name.Text = contactos[position].Name;
            holder.Phone.Text = contactos[position].Phone;

            return convertView;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionHeaders;
        }

        public int GetPositionForSection(int section)
        {
            return positionForSectionMap[section];
        }

        public int GetSectionForPosition(int position)
        {
            return sectionForPositionMap[position];
        }
    }
}

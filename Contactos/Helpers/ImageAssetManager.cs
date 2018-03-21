using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;

namespace Contactos.Helpers
{
    public static class ImageAssetManager
    {
        static Dictionary<string, Drawable> cache = new Dictionary<string, Drawable>();

        public static Drawable Get(Context context, string url)
        {
            if (!cache.ContainsKey(url))
            {
                Drawable drawable;

                try
                {
                    drawable = Drawable.CreateFromStream(context.Assets.Open(url), null);
                }catch
                {
                    drawable = Drawable.CreateFromPath(url);
                }

                Android.Graphics.Bitmap originalBitmap = ((BitmapDrawable)drawable).Bitmap;
                RoundedBitmapDrawable roundedDrawable = RoundedBitmapDrawableFactory.Create(context.Resources, originalBitmap);
                roundedDrawable.CornerRadius = originalBitmap.Height;

                cache.Add(url, roundedDrawable);
            }

            return cache[url];
        }
    }
}

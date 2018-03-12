using System;
using Android.OS;
using Android.Views;

namespace Contactos.Fragments
{
    public class AboutFragment: Android.Support.V4.App.Fragment
    {
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            return inflater.Inflate(Resource.Layout.AcercaDe, container, false);
		}
	}
}

using System;
using Android.Support.V4.App;
using Java.Lang;

namespace Contactos.Adapters
{
    public class HomeAdapter: Android.Support.V4.App.FragmentPagerAdapter
    {
        Android.Support.V4.App.Fragment[] fragments;
        ICharSequence[] titles;


        public HomeAdapter(Android.Support.V4.App.FragmentManager fm, Android.Support.V4.App.Fragment[] fragments, ICharSequence[] titles)
            :base(fm)
        {
            this.fragments = fragments;
            this.titles = titles;
        }

        public override int Count => fragments.Length;

        public override Fragment GetItem(int position)
        {
            return fragments[position];
        }

		public override ICharSequence GetPageTitleFormatted(int position)
		{
            return titles[position];
		}
	}
}

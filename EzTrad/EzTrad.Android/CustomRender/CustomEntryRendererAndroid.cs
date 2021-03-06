using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using EzTrad.CustomLayout;
using EzTrad.Droid.CustomRender;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendererAndroid))]
namespace EzTrad.Droid.CustomRender
{
    [Obsolete]
    public class CustomEntryRendererAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                this.Control.SetPadding(20, 0, 0, 0);

                CustomEntry customEntry = (CustomEntry)e.NewElement;
                if (customEntry.IsPasswordFlag)
                {
                    this.Control.InputType = InputTypes.TextVariationVisiblePassword;
                }

            }
        }
        public CustomEntryRendererAndroid(Context context) : base(context)
        {
        }
    }
}
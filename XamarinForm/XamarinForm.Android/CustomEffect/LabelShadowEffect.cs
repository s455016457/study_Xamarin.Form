using System;
using System.ComponentModel;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForm.CustomEffect;

[assembly: ResolutionGroupName("CustomEffect")]
[assembly:ExportEffect(typeof(XamarinForm.Droid.CustomEffect.LabelShadowEffect), "LabelShadowEffect")]
namespace XamarinForm.Droid.CustomEffect
{
    /// <summary>
    /// Label阴影效果
    /// </summary>
    public class LabelShadowEffect : PlatformEffect
    {
        TextView control;
        Android.Graphics.Color color;
        float radius, distanceX, distanceY;
        protected override void OnAttached()
        {
            try
            {
                control = Control as TextView;
                UpdateRadius();
                UpdateOffset();
                UpdateColor();
                UpdateControl();
            }
            catch (Exception ex)
            {
                Console.WriteLine("不能在附加控件上设置属性，错误信息：{0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName.Equals(ShadowEffect.colorProperty))
            {
                UpdateColor();
                UpdateControl();
            }
            else if (args.PropertyName.Equals(ShadowEffect.radiusProperty))
            {
                UpdateRadius();
                UpdateControl();
            }
            else if (args.PropertyName.Equals(ShadowEffect.distanceXProperty)
                || args.PropertyName.Equals(ShadowEffect.distanceYProperty))
            {
                UpdateOffset();
                UpdateControl();
            }
        }

        void UpdateControl()
        {
            if (control != null)
            {
                control.SetShadowLayer(radius, distanceX, distanceY, color);
            }
        }

        void UpdateColor()
        {
            color = ShadowEffect.GetColor(Element).ToAndroid();
        }

        void UpdateRadius()
        {
            radius = (float)ShadowEffect.GetRadius(Element);
        }

        void UpdateOffset()
        {
            distanceX = (float)ShadowEffect.GetDistanceX(Element);
            distanceY = (float)ShadowEffect.GetDistanceY(Element);
        }
    }
}
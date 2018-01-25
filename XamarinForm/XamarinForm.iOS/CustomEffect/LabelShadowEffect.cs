using CoreGraphics;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForm.CustomEffect;

[assembly:ResolutionGroupName("CustomEffect")]
[assembly:ExportEffect(typeof(XamarinForm.iOS.CustomEffect.LabelShadowEffect), "LabelShadowEffect")]
namespace XamarinForm.iOS.CustomEffect
{
    /// <summary>
    /// Label阴影效果
    /// </summary>
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                UpdateRadius();
                UpdateColor();
                UpdateOffset();
                Control.Layer.ShadowOpacity = 1.0f;
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
            }
            else if (args.PropertyName.Equals(ShadowEffect.radiusProperty))
            {
                UpdateRadius();
            }
            else if (args.PropertyName.Equals(ShadowEffect.distanceXProperty)
                || args.PropertyName.Equals(ShadowEffect.distanceYProperty))
            {
                UpdateOffset();
            }
        }

        void UpdateRadius()
        {
            Control.Layer.CornerRadius = (nfloat)ShadowEffect.GetRadius(Element);
        }

        void UpdateColor()
        {
            Control.Layer.ShadowColor = ShadowEffect.GetColor(Element).ToCGColor();
        }
        void UpdateOffset()
        {
            Control.Layer.ShadowOffset = new CGSize
            {
                Width=(nfloat)ShadowEffect.GetDistanceX(Element),
                Height=(nfloat)ShadowEffect.GetDistanceY(Element)
            };
        }
    }
}

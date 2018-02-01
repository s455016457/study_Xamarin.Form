using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.CustomEffect;

namespace XamarinForm.Pages.Effect
{
	public class TestLabelShadowEffetPage : ContentPage
	{
		public TestLabelShadowEffetPage ()
		{
            Title = "Label阴影效果";
            StackLayout layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            };

            ScrollView scrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Start,
            };

            setCodeText(scrollView);

            Label label = new Label
            {
                Text = "Label Shadow Effect Label阴影效果"
            };

            Color color = Color.Default;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    color = Color.Yellow;
                    break;
                case Device.Android:
                    color = Color.Green;
                    break;
                case Device.UWP:
                    color = Color.Red;
                    break;
            }

            ShadowEffect.SetHasShadow(label, true);
            ShadowEffect.SetRadius(label, 5);
            ShadowEffect.SetDistanceX(label, 5);
            ShadowEffect.SetDistanceY(label, 5);
            ShadowEffect.SetColor(label, color);

            layout.Children.Add(label);
            layout.Children.Add(new Label { Text = "代码如下：", FontAttributes = FontAttributes.Bold });
            layout.Children.Add(scrollView);

            Content = layout;
        }
        private void setCodeText(ScrollView scrollView)
        {
            scrollView.Content = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                Text = @"
 Label label = new Label
 {
     Text = ""Label Shadow Effect Label阴影效果""
 };
 
 Color color = Color.Default;
 switch (Device.RuntimePlatform)
 {
     case Device.iOS:
         color = Color.Yellow;
         break;
     case Device.Android:
         color = Color.Green;
         break;
     case Device.UWP:
         color = Color.Red;
         break;
 }
 
 ShadowEffect.SetHasShadow(label, true);
 ShadowEffect.SetRadius(label, 5);
 ShadowEffect.SetDistanceX(label, 5);
 ShadowEffect.SetDistanceY(label, 5);
 ShadowEffect.SetColor(label, color);

public static class ShadowEffect
{
    #region BindableProperty
    /// <summary>
    /// 是否显示阴影
    /// </summary>
    public static readonly BindableProperty hasShadowProperty = BindableProperty.Create(""HasShadow"", typeof(Boolean), typeof(ShadowEffect), false, propertyChanged: OnHasShadowChanged);
    /// <summary>
    /// 阴影颜色
    /// </summary>
    public static readonly BindableProperty colorProperty = BindableProperty.Create(""Color"", typeof(Color), typeof(ShadowEffect), Color.Default);
    /// <summary>
    /// 阴影半径
    /// </summary>
    public static readonly BindableProperty radiusProperty = BindableProperty.CreateAttached(""Radius"", typeof(double), typeof(ShadowEffect), 1.0);
    /// <summary>
    /// X轴偏移量
    /// </summary>
    public static readonly BindableProperty distanceXProperty = BindableProperty.Create(""DistanceX"", typeof(double), typeof(ShadowEffect), 0.0);
    /// <summary>
    /// Y轴偏移量
    /// </summary>
    public static readonly BindableProperty distanceYProperty = BindableProperty.Create(""DistanceY"", typeof(double), typeof(ShadowEffect), 0.0);
    #endregion

    #region getter setter
    public static Boolean GetHasShadow(BindableObject view)
    {
        return (Boolean)view.GetValue(hasShadowProperty);
    }
    public static void SetHasShadow(BindableObject view, Boolean value)
    {
        view.SetValue(hasShadowProperty, value);
    }

    public static Color GetColor(BindableObject view)
    {
        return (Color)view.GetValue(colorProperty);
    }
    public static void SetColor(BindableObject view, Color value)
    {
        view.SetValue(colorProperty, value);
    }

    public static double GetRadius(BindableObject view)
    {
        return (double)view.GetValue(radiusProperty);
    }
    public static void SetRadius(BindableObject view, double value)
    {
        view.SetValue(radiusProperty, value);
    }

    public static double GetDistanceX(BindableObject view)
    {
        return (double)view.GetValue(distanceXProperty);
    }
    public static void SetDistanceX(BindableObject view, double value)
    {
        view.SetValue(distanceXProperty, value);
    }

    public static double GetDistanceY(BindableObject view)
    {
        return (double)view.GetValue(distanceYProperty);
    }
    public static void SetDistanceY(BindableObject view, double value)
    {
        view.SetValue(distanceYProperty, value);
    }
    #endregion

    static void OnHasShadowChanged(BindableObject bindable, object oldValue, object newValue)
    {
        View view = bindable as View;
        if (view == null) return;

        Boolean hasShadow = (Boolean)newValue;
        if (hasShadow)
        {
            view.Effects.Add(new LabelShadowEffect());
        }
        else
        {
            Effect effect = view.Effects.FirstOrDefault(p => p is LabelShadowEffect);
            if (effect == null) return;
            view.Effects.Remove(effect);
        }
    }
}
class LabelShadowEffect : RoutingEffect
{
    public LabelShadowEffect() : base(""CustomEffect.LabelShadowEffect"") { }
}

/**
IOS中阴影效果实现
**/
[assembly:ResolutionGroupName(""CustomEffect"")]
[assembly: ExportEffect(typeof(XamarinForm.iOS.CustomEffect.LabelShadowEffect), ""LabelShadowEffect"")]
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
                Console.WriteLine(""不能在附加控件上设置属性，错误信息：{ 0}"", ex.Message);
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
                Width = (nfloat)ShadowEffect.GetDistanceX(Element),
                Height = (nfloat)ShadowEffect.GetDistanceY(Element)
            };
        }
    }
}
    

/**
Android中阴影效果实现
**/
[assembly:ResolutionGroupName (""CustomEffect"")]
[assembly: ExportEffect(typeof(XamarinForm.Droid.CustomEffect.LabelShadowEffect), ""LabelShadowEffect"")]
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
                Console.WriteLine(""不能在附加控件上设置属性，错误信息：{0}"", ex.Message);
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
"
            };
        }
    }
}
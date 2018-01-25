using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace XamarinForm.CustomEffect
{
    public static class ShadowEffect
    {
        #region BindableProperty
        /// <summary>
        /// 是否显示阴影
        /// </summary>
        public static readonly BindableProperty hasShadowProperty = BindableProperty.Create("HasShadow", typeof(Boolean), typeof(ShadowEffect), false, propertyChanged: OnHasShadowChanged);
        /// <summary>
        /// 阴影颜色
        /// </summary>
        public static readonly BindableProperty colorProperty = BindableProperty.Create("Color", typeof(Color), typeof(ShadowEffect), Color.Gray);
        /// <summary>
        /// 阴影半径
        /// </summary>
        public static readonly BindableProperty radiusProperty= BindableProperty.CreateAttached("Radius", typeof(double), typeof(ShadowEffect), 5d);
        /// <summary>
        /// X轴偏移量
        /// </summary>
        public static readonly BindableProperty distanceXProperty = BindableProperty.Create("DistanceX", typeof(double), typeof(ShadowEffect), 3d);
        /// <summary>
        /// Y轴偏移量
        /// </summary>
        public static readonly BindableProperty distanceYProperty = BindableProperty.Create("DistanceY", typeof(double), typeof(ShadowEffect), 3d);
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
        public LabelShadowEffect() : base("CustomEffect.LabelShadowEffect")
        {
        }
    }
}

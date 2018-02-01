using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForm.Extensions
{
    public static class ViewExtensions
    {

        public static Task<Boolean> ColorTo(this VisualElement element, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Color> transform = (t) =>
                Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                               fromColor.G + t * (toColor.G - fromColor.G),
                               fromColor.B + t * (toColor.B - fromColor.B),
                               fromColor.A + t * (toColor.A - fromColor.A));
            return ColorAnimation(element, "ColorTo", transform, callback, length, easing);
        }

        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }


        /// <summary>
        /// 颜色动画
        /// </summary>
        /// <param name="element">View</param>
        /// <param name="name">动画名称</param>
        /// <param name="transform">动画效果方法</param>
        /// <param name="callback">动画执行完调用方法</param>
        /// <param name="length">动画执行事件</param>
        /// <param name="easing">动画执行模式</param>
        /// <returns></returns>
        static Task<Boolean> ColorAnimation(
            VisualElement element, 
            string name, 
            Func<double, Color> transform, 
            Action<Color> callback, 
            uint length, 
            Easing easing)
        {
            easing = easing ?? Easing.Linear;
            TaskCompletionSource<Boolean> tkc = new TaskCompletionSource<bool>();
            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => tkc.SetResult(c));
            return tkc.Task;
        }
    }
}

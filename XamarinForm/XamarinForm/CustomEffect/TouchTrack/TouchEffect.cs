using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.CustomEffect.TouchTrack
{
    public class TouchEffect : RoutingEffect
    {
        public TouchEffect() : base("CustomEffect.TouchEffect")
        {
        }

        public event TouchActionEventHandler TouchAction;
        /// <summary>
        /// 捕获
        /// </summary>
        public bool Capture { set; get; }

        public void OnTouchAction(Element element, TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}

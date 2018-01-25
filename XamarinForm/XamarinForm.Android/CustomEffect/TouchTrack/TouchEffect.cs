using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForm.CustomEffect.TouchTrack;

//[assembly: ResolutionGroupName("CustomEffect")]
[assembly: ExportEffect(typeof(XamarinForm.Droid.CustomEffect.TouchTrack.TouchEffect), "TouchEffect")]
namespace XamarinForm.Droid.CustomEffect.TouchTrack
{
    public class TouchEffect : PlatformEffect
    {
        /// <summary>
        /// 当前视图
        /// </summary>
        Android.Views.View view;
        Element formsElement;
        bool capture;
        /// <summary>
        /// 触摸效果
        /// </summary>
        XamarinForm.CustomEffect.TouchTrack.TouchEffect pclTouchEffect;
        Func<double, double> fromPixels;
        int[] twoIntArray = new int[2];

        /// <summary>
        /// 视图和触摸跟踪键值对集合
        /// </summary>
        static Dictionary<Android.Views.View, TouchEffect> viewDictionary =
            new Dictionary<Android.Views.View, TouchEffect>();
        /// <summary>
        /// 触摸手指和触摸跟踪键值对集合
        /// </summary>
        static Dictionary<int, TouchEffect> idToEffectDictionary =
            new Dictionary<int, TouchEffect>();

        protected override void OnAttached()
        {
            view = Control == null ? Container : Control;
            XamarinForm.CustomEffect.TouchTrack.TouchEffect touchEffect =
                (XamarinForm.CustomEffect.TouchTrack.TouchEffect)
                Element.Effects.FirstOrDefault(p => p is XamarinForm.CustomEffect.TouchTrack.TouchEffect);
            if (touchEffect != null && view != null)
            {
                viewDictionary.Add(view, this);
                formsElement = Element;
                pclTouchEffect = touchEffect;
                //保存fromPixels方法
                fromPixels = view.Context.FromPixels;
                //注册触摸事件
                view.Touch += View_Touch;
            }
        }

        protected override void OnDetached()
        {
            if (viewDictionary.ContainsKey(view))
            {
                viewDictionary.Remove(view);
                view.Touch -= View_Touch;
            }
        }

        private void View_Touch(object sender, Android.Views.View.TouchEventArgs e)
        {
            Android.Views.View senderView = sender as Android.Views.View;
            MotionEvent motionEvent = e.Event;

            // Get the pointer index
            int pointerIndex = motionEvent.ActionIndex;
            // 获取事件ID
            int id = motionEvent.GetPointerId(pointerIndex);
            //获取View的位置坐标
            senderView.GetLocationOnScreen(twoIntArray);
            //屏幕指针坐标
            Point screenPointerCoords = new Point(twoIntArray[0] + motionEvent.GetX(pointerIndex),
                                                  twoIntArray[1] + motionEvent.GetY(pointerIndex));
            switch (e.Event.ActionMasked)
            {
                case MotionEventActions.Down:
                case MotionEventActions.PointerDown:    //指针降下
                    FireEvent(this, id, TouchActionType.Pressed, screenPointerCoords, true);
                    idToEffectDictionary.Add(id, this);
                    capture = pclTouchEffect.Capture;
                    break;
                case MotionEventActions.Move:   //移动
                    //多个移动事件被捆绑在一起，所以要在一个循环中处理它们。
                    for (pointerIndex = 0; pointerIndex < motionEvent.PointerCount; pointerIndex++)
                    {
                        id = motionEvent.GetPointerId(pointerIndex);
                        if (capture)
                        {
                            //获取View的位置坐标
                            senderView.GetLocationOnScreen(twoIntArray);
                            //屏幕指针坐标
                            screenPointerCoords = new Point(twoIntArray[0] + motionEvent.GetX(pointerIndex),
                                                            twoIntArray[1] + motionEvent.GetY(pointerIndex));

                            FireEvent(this, id, TouchActionType.Moved, screenPointerCoords, true);
                        }
                        else
                        {
                            CheckForBoundaryHop(id, screenPointerCoords);

                            if (idToEffectDictionary[id] != null)
                            {
                                FireEvent(idToEffectDictionary[id], id, TouchActionType.Moved, screenPointerCoords, true);
                            }
                        }
                    }
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Pointer1Up: //指针抬起
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Released, screenPointerCoords, false);
                    }
                    else
                    {
                        CheckForBoundaryHop(id, screenPointerCoords);

                        if (idToEffectDictionary[id] != null)
                        {
                            FireEvent(idToEffectDictionary[id], id, TouchActionType.Released, screenPointerCoords, false);
                        }
                    }
                    idToEffectDictionary.Remove(id);
                    break;
                case MotionEventActions.Cancel: //取消
                    if (capture)
                    {
                        FireEvent(this, id, TouchActionType.Cancelled, screenPointerCoords, false);
                    }
                    else
                    {
                        if (idToEffectDictionary[id] != null)
                        {
                            FireEvent(idToEffectDictionary[id], id, TouchActionType.Cancelled, screenPointerCoords, false);
                        }
                    }
                    idToEffectDictionary.Remove(id);
                    break;
            }
        }

        /// <summary>
        /// 检查边界
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="pointerLocation">屏幕指针坐标</param>
        void CheckForBoundaryHop(int id, Point pointerLocation)
        {
            TouchEffect touchEffectHit = null;
            foreach (Android.Views.View view in viewDictionary.Keys)
            {
                try
                {
                    //获取视图在屏幕上的坐标
                    view.GetLocationOnScreen(twoIntArray);
                }
                catch   //系统无法访问已被处理的对象
                {
                    continue;
                }
                //创建矩形
                Rectangle rectangle = new Rectangle(twoIntArray[0], twoIntArray[1], view.Width, view.Height);
                //如果屏幕指针坐标存在矩形中
                if (rectangle.Contains(pointerLocation))
                {
                    //获取该视图的TouchEffect
                    touchEffectHit = viewDictionary[view];
                }
            }
            if (touchEffectHit != idToEffectDictionary[id])
            {
                if (idToEffectDictionary[id] != null)
                {
                    FireEvent(idToEffectDictionary[id], id, TouchActionType.Exited, pointerLocation, true);
                }
                if (touchEffectHit != null)
                {
                    FireEvent(touchEffectHit, id, TouchActionType.Entered, pointerLocation, true);
                }
                idToEffectDictionary[id] = touchEffectHit;
            }
        }

        /// <summary>
        /// 激活事件
        /// </summary>
        /// <param name="touchEffect"></param>
        /// <param name="id">触摸手指事件ID</param>
        /// <param name="actionType">事件类别</param>
        /// <param name="pointerLocation">屏幕上手指的坐标</param>
        /// <param name="isInContact"></param>
        void FireEvent(TouchEffect touchEffect, int id, TouchActionType actionType, Point pointerLocation, bool isInContact)
        {
            // 获取调用触发事件的方法。
            Action<Element, TouchActionEventArgs> onTouchAction = touchEffect.pclTouchEffect.OnTouchAction;

            // 获取视图中指针的位置。
            touchEffect.view.GetLocationOnScreen(twoIntArray);

            double x = pointerLocation.X - twoIntArray[0];
            double y = pointerLocation.Y - twoIntArray[1];
            Point point = new Point(fromPixels(x), fromPixels(y));

            // 触发事件
            onTouchAction(touchEffect.formsElement,new TouchActionEventArgs(id, actionType, point, isInContact));
        }
    }
}
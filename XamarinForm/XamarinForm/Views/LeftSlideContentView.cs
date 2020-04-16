using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using XamarinForm.CustomEffect.TouchTrack;
using XamarinForm.EventArg;
using static Xamarin.Forms.AbsoluteLayout;

namespace XamarinForm.Views
{
    public class LeftSlideContentView : ContentView
    {

        public event EventHandler<SlideViewReleasedEventArg> OnReleased;
        /// <summary>
        /// 是否已开始移动
        /// </summary>
        bool isBeingDragged;
        /// <summary>
        /// 触摸手指事件ID
        /// </summary>
        long touchId;
        /// <summary>
        /// 屏幕上手指的位置
        /// </summary>
        Point pressPoint;
        
        public LeftSlideContentView()
        {
            TouchEffect touchEffect = new TouchEffect
            {
                Capture = true
            };

            touchEffect.TouchAction += OnTouchEffectAction;
            Effects.Add(touchEffect);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals("Content", StringComparison.CurrentCultureIgnoreCase) && Content != null)
            {
                TouchEffect touchEffect = new TouchEffect
                {
                    Capture = true
                };

                touchEffect.TouchAction += OnTouchEffectAction;
                Content.Effects.Add(touchEffect);
                Content.ChildAdded += Children_ChildAdded;
                Content.PropertyChanged += Children_PropertyChanged;
                SetChildrenViewEffects(Content);
            }
        }

        /// <summary>
        /// 设置子视图的效果
        /// </summary>
        /// <param name="obj">视图</param>
        private void SetChildrenViewEffects(object obj)
        {
            if (!(obj is Layout<View>)) return;

            var view = obj as Layout<View>;

            if (view == null) return;

            foreach (var childView in view.Children)
            {
                TouchEffect touchEffect = new TouchEffect
                {
                    Capture = true
                };

                touchEffect.TouchAction += OnTouchEffectAction;
                childView.Effects.Add(touchEffect);
                childView.PropertyChanged += Children_PropertyChanged;
            }
            #region old Code
            //var childrenProperty = obj.GetType().GetProperty("Children",typeof(IAbsoluteList<View>));

            //if (childrenProperty == null) return;

            //var views = childrenProperty.GetValue(obj) as IEnumerable<View>;
            //if (views == null) return;

            //foreach (var view in views)
            //{
            //    TouchEffect touchEffect = new TouchEffect
            //    {
            //        Capture = true
            //    };

            //    touchEffect.TouchAction += OnTouchEffectAction;
            //    view.Effects.Add(touchEffect);
            //    view.PropertyChanged += Children_PropertyChanged;
            //}
            #endregion
        }

        /// <summary>
        /// 当Children View 的属性改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Children_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Children", StringComparison.CurrentCultureIgnoreCase) && Content != null)
            {
                SetChildrenViewEffects(sender);
            }
        }

        private void Children_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is View)
            {
                var view = e.Element as View;
                if (view != null)
                {
                    TouchEffect touchEffect = new TouchEffect
                    {
                        Capture = true
                    };

                    touchEffect.TouchAction += OnTouchEffectAction;
                    view.Effects.Add(touchEffect);
                    view.ChildAdded += Children_ChildAdded;
                    view.PropertyChanged += Children_PropertyChanged;
                    SetChildrenViewEffects(view);
                }
            }
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            View paentView = Parent as View;
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!isBeingDragged)
                    {
                        isBeingDragged = true;
                        touchId = args.Id;
                        pressPoint = args.Location;
                    }
                    break;

                case TouchActionType.Moved:
                    if (isBeingDragged && touchId == args.Id)
                    {
                        double tranlationX = TranslationX + args.Location.X - pressPoint.X;

                        //矩形坐标不能超出父控件
                        if (paentView != null)
                        {
                            if (X + tranlationX < -1 * Width *2/3-5)
                            {
                                tranlationX = -1 * Width * 2 / 3;
                            }
                            else if (X + tranlationX + Width > paentView.Width)
                            {
                                tranlationX = paentView.Width - Width;
                            }
                        }

                        TranslationX = tranlationX;
                    }
                    break;

                case TouchActionType.Cancelled:
                case TouchActionType.Released:
                    if (isBeingDragged && touchId == args.Id)
                    {
                        isBeingDragged = false;
                        if (OnReleased != null)
                        {
                            SlideViewReleasedEventArg eventArg = new SlideViewReleasedEventArg(TranslationX, TranslationY);
                            OnReleased.Invoke(this, eventArg);
                        }
                    }
                    break;
            }
        }
    }
}

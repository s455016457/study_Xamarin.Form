using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.CustomEffect.TouchTrack;

namespace XamarinForm.Views
{
    public class DraggableBoxView:BoxView
    {
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
        public DraggableBoxView()
        {
            TouchEffect touchEffect = new TouchEffect
            {
                Capture=true
            };

            touchEffect.TouchAction += OnTouchEffectAction;

            Effects.Add(touchEffect);
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
                        double tranlationY = TranslationY + args.Location.Y - pressPoint.Y;
                        //矩形坐标不能超出父控件
                        if (paentView != null)
                        {
                            if (X + tranlationX < 0)
                            {
                                tranlationX = 0;
                            }
                            else if (X + tranlationX + Width > paentView.Width)
                            {
                                tranlationX = paentView.Width-Width;
                            }

                            if (Y + tranlationY < 0)
                            {
                                tranlationY = 0;
                            }
                            else if (Y + tranlationY + Height > paentView.Height)
                            {
                                tranlationY = paentView.Height-Height;
                            }
                        }

                        TranslationX = tranlationX;
                        TranslationY = tranlationY;

                        //TranslationX += args.Location.X - pressPoint.X;
                        //TranslationY += args.Location.Y - pressPoint.Y;
                    }
                    break;

                case TouchActionType.Cancelled:
                case TouchActionType.Released:
                    if (isBeingDragged && touchId == args.Id)
                    {
                        isBeingDragged = false;
                    }
                    break;
            }
        }
    }
}

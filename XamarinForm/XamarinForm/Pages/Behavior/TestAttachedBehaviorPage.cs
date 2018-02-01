using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Behavior
{
    public class TestAttachedBehaviorPage:ContentPage
    {
        public TestAttachedBehaviorPage()
        {
            Title = "附加行为";
            StackLayout stackLayout = new StackLayout
            {
                Orientation=StackOrientation.Vertical,
                HorizontalOptions=LayoutOptions.Fill,
                VerticalOptions=LayoutOptions.Fill,
                Margin=new Thickness(5,0,5,0),
            };

            Entry entry = new Entry
            {
                Keyboard=Keyboard.Default,
                FontSize=Device.GetNamedSize(NamedSize.Micro,typeof(Entry)),
                Placeholder="请输入数字",
                PlaceholderColor=Color.Gray,
            };
            NumericValidationBehavior.SetAttachBehavior(entry, true);
            stackLayout.Children.Add(entry);
            stackLayout.Children.Add(new Label { Text="代码如下："});

            Label label = new Label
            {
                FontSize=Device.GetNamedSize(NamedSize.Micro,typeof(Label)),
                Text = @"Entry entry = new Entry
 {
     Keyboard=Keyboard.Default,
     FontSize=Device.GetNamedSize(NamedSize.Micro,typeof(Entry)),
     Placeholder=""请输入数字"",
     PlaceholderColor=Color.Gray,
 };
 //绑定到 entry 上
 NumericValidationBehavior.SetAttachBehavior(entry, true);
 public static class NumericValidationBehavior
 {
     //创建附加行为属性
     //附加行为的名称
     //返回值类型
     //声明对象的类型
     //默认值
     //当属性发生更改时要运行的委托
     public static BindableProperty AttachBehaviorProperty = BindableProperty.Create(
         ""AttachBehavior"", 
         typeof(bool),
         typeof(NumericValidationBehavior),
         false,
         propertyChanged: OnAttachBehaviorChanged);

     public static bool GetAttachBehavior(BindableObject view)
     {
         return (bool)view.GetValue(AttachBehaviorProperty);
     }

     public static void SetAttachBehavior(BindableObject view, bool value)
     {
        //设置指定的属性值
        //AttachBehaviorProperty 用来赋值的绑定属性
         view.SetValue(AttachBehaviorProperty, value);
     }

    /// <summary>
    /// 当附加行为改变时触发
    /// </summary>
    /// <param name=""view"">附加行为绑定的对象</param>
    /// <param name=""oldValue"" > 附加行为属性的旧值</param> 
    /// <param name=""newValue"" > 附加行为属性的新值</param> 
    static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue){
         var entry = view as Entry;
         if (entry == null)
         {
             return;
         }

         bool attachBehavior = (bool)newValue;
         if (attachBehavior)
         {
             entry.TextChanged += OnEntryTextChanged;
         }
         else
         {
             entry.TextChanged -= OnEntryTextChanged;
         }
     }
     static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
     {
         double result;
         bool isValid = double.TryParse(args.NewTextValue, out result);
         ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
     }
 }"
            };

            ScrollView scrollView = new ScrollView {
                Content = label,
                VerticalOptions=LayoutOptions.StartAndExpand,
                HorizontalOptions=LayoutOptions.StartAndExpand
            };

            stackLayout.Children.Add(scrollView);
            Content = stackLayout;
        }

        public static class NumericValidationBehavior
        {
            //创建附加行为属性
            //附加行为的名称
            //返回值类型
            //声明对象的类型
            //默认值
            //当属性发生更改时要运行的委托
            public static BindableProperty AttachBehaviorProperty = BindableProperty.Create(
                "AttachBehavior", 
                typeof(bool), 
                typeof(NumericValidationBehavior), 
                false,
                propertyChanged: OnAttachBehaviorChanged);

            public static bool GetAttachBehavior(BindableObject view)
            {
                return (bool)view.GetValue(AttachBehaviorProperty);
            }

            public static void SetAttachBehavior(BindableObject view, bool value)
            {
                //设置指定的属性值
                //AttachBehaviorProperty 用来赋值的绑定属性
                view.SetValue(AttachBehaviorProperty, value);
            }
            
            /// <summary>
            /// 当附加行为改变时触发
            /// </summary>
            /// <param name="view">附加行为绑定的对象</param>
            /// <param name="oldValue">附加行为属性的旧值</param>
            /// <param name="newValue">附加行为属性的新值</param>
            static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
            {
                var entry = view as Entry;
                if (entry == null)
                {
                    return;
                }

                bool attachBehavior = (bool)newValue;
                if (attachBehavior)
                {
                    entry.TextChanged += OnEntryTextChanged;
                }
                else
                {
                    entry.TextChanged -= OnEntryTextChanged;
                }
            }
            static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
            {
                double result;
                bool isValid = double.TryParse(args.NewTextValue, out result);
                ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
            }
        }
    }
}

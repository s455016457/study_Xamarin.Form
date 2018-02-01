using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace XamarinForm.Pages.Behavior
{
    public class TestBehaviorsPage2:ContentPage
    {
        public TestBehaviorsPage2()
        {
            Title = "行为2";
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

            Entry emailEntry = new Entry
            {
                Placeholder = "Email",
                PlaceholderColor = Color.Silver,
                Keyboard = Keyboard.Email,
            };

            emailEntry.Behaviors.Add(new EntryEmailBehavior2());
            //emailEntry.Style.Setters.Add(EntryEmailBehavior2.AttchedBehaviorProperty,true);

            Entry emailEntry2 = new Entry
            {
                Placeholder = "Email",
                PlaceholderColor = Color.Silver,
                Keyboard = Keyboard.Email,
                TextColor = Color.Blue
            };
            emailEntry2.Behaviors.Add(new EntryEmailBehavior2());
            //emailEntry2.Style.Setters.Add(EntryEmailBehavior2.AttchedBehaviorProperty, false);

            layout.Children.Add(emailEntry);
            layout.Children.Add(emailEntry2);
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
Entry emailEntry = new Entry
{
    Placeholder=""Email"",
    PlaceholderColor = Color.Silver,
    Keyboard = Keyboard.Email,
};
emailEntry.Behaviors.Add(new EntryEmailBehavior2());

Entry emailEntry2 = new Entry
{
    Placeholder = ""Email"",
    PlaceholderColor = Color.Silver,
    Keyboard = Keyboard.Email,
    TextColor = Color.Blue
};
emailEntry2.Behaviors.Add(new EntryEmailBehavior2());

/// <summary>
/// 可以以style的方式在Xamarin上使用
/// 使用方法如下：
/// <Style x:Key=""EmailValidationStyle"" TargetType=""Entry"">
///     <Style.Setters>
///         <Setter Property = ""local:EntryEmailBehavior2.EntryEmailBechavior"" Value =""true"" />
///     </Style.Setters>
/// </Style>
/// 
/// <Entry Placeholder=""Email"" Style =""{ StaticResource EmailValidationStyle}"" >
/// </summary>
public class EntryEmailBehavior2 : Behavior<Entry>
{
    static System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("" ^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$"");
    static Color oldEntryColo;
    /// <summary>
    /// 创建附加行为属性
    /// </summary>
    public static readonly BindableProperty AttchedBehaviorProperty = BindableProperty.CreateAttached(
        ""EntryEmailBechavior""           //属性名称
        , typeof(Boolean)               //属性返回值类型
        , typeof(EntryEmailBehavior2)   //属性申明类型
        , false                         //属性默认值
        , propertyChanged: OnAttachBehaviorChanged);

    public static Boolean GetAttachBehavior(BindableObject view)
    {
        return (Boolean)view.GetValue(AttchedBehaviorProperty);
    }

    public static void SetAttachBehavior(BindableObject view, bool value)
    {
        view.SetValue(AttchedBehaviorProperty, value);
    }

    static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
    {
        Entry entry = view as Entry;
        if (entry == null) return;

        Boolean attcheBehavior = (Boolean)newValue;

        if (attcheBehavior)
        {
            entry.Behaviors.Add(new EntryEmailBehavior2());
        }
        else
        {
            var toRemove = entry.Behaviors.FirstOrDefault(p => p is EntryEmailBehavior2);
            if (toRemove != null)
                entry.Behaviors.Remove(toRemove);
        }
    }

    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += Bindable_TextChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= Bindable_TextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = sender as Entry;
        if (entry == null) return;
        if (regex.IsMatch(e.NewTextValue))
        {
            if (oldEntryColo != null)
                entry.TextColor = oldEntryColo;
        }
        else
        {
            if (entry.TextColor != Color.Red)
                oldEntryColo = entry.TextColor;
            entry.TextColor = Color.Red;
        }
    }
}
",
            };
        }
    }

    /// <summary>
    /// 可以以style的方式在Xamarin上使用
    /// 使用方法如下：
    /// <Style x:Key="EmailValidationStyle" TargetType="Entry">
    ///     <Style.Setters>
    ///         <Setter Property = "local:EntryEmailBehavior2.AttchedBehaviorProperty" Value="true" />
    ///     </Style.Setters>
    /// </Style>
    /// 
    /// <Entry Placeholder="Email" Style="{StaticResource EmailValidationStyle}">
    /// </summary>
    public class EntryEmailBehavior2 : Behavior<Entry>
    {
        static System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
        static Color oldEntryColo;
        /// <summary>
        /// 创建附加行为属性
        /// </summary>
        public static readonly BindableProperty AttchedBehaviorProperty = BindableProperty.CreateAttached(
            "EntryEmailBechavior"           //属性名称
            , typeof(Boolean)               //属性返回值类型
            , typeof(EntryEmailBehavior2)   //属性申明类型
            , false                         //属性默认值
            , propertyChanged: OnAttachBehaviorChanged);

        public static Boolean GetAttachBehavior(BindableObject view)
        {
            return (Boolean) view.GetValue(AttchedBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttchedBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            Entry entry = view as Entry;
            if (entry == null) return;

            Boolean attcheBehavior = (Boolean)newValue;

            if (attcheBehavior)
            {
                entry.Behaviors.Add(new EntryEmailBehavior2());
            }
            else
            {
                var toRemove = entry.Behaviors.FirstOrDefault(p => p is EntryEmailBehavior2);
                if (toRemove != null)
                    entry.Behaviors.Remove(toRemove);
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry == null) return;
            if (regex.IsMatch(e.NewTextValue))
            {
                if (oldEntryColo != null)
                    entry.TextColor = oldEntryColo;
            }
            else
            {
                if (entry.TextColor != Color.Red)
                    oldEntryColo = entry.TextColor;
                entry.TextColor = Color.Red;
            }
        }
    }
}

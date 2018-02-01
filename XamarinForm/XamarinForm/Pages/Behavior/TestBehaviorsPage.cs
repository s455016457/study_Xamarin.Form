using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Behavior
{
    public class TestBehaviorsPage:ContentPage
    {
        public TestBehaviorsPage()
        {
            Title = "行为";
            StackLayout layout = new StackLayout
            {
                Orientation=StackOrientation.Vertical,
                VerticalOptions=LayoutOptions.Fill,
                HorizontalOptions=LayoutOptions.Fill,
            };

            ScrollView scrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                VerticalOptions=LayoutOptions.StartAndExpand,
                HorizontalOptions=LayoutOptions.Start,
            };

            setCodeText(scrollView);

            Entry emailEntry = new Entry
            {
                Placeholder = "Email",
                PlaceholderColor = Color.Silver,
                Keyboard = Keyboard.Email,
            };

            emailEntry.Behaviors.Add(new EntryEmailBehavior());

            Entry emailEntry2 = new Entry
            {
                Placeholder = "Email",
                PlaceholderColor = Color.Silver,
                Keyboard = Keyboard.Email,
                TextColor=Color.Blue
            };
            emailEntry2.Behaviors.Add(new EntryEmailBehavior());

            layout.Children.Add(emailEntry);
            layout.Children.Add(emailEntry2);
            layout.Children.Add(new Label { Text="代码如下：",FontAttributes=FontAttributes.Bold});
            layout.Children.Add(scrollView);

            Content = layout;
        }

        private void setCodeText(ScrollView scrollView)
        {
            scrollView.Content = new Label
            {
                FontSize=Device.GetNamedSize(NamedSize.Micro,typeof(Label)),
                VerticalOptions=LayoutOptions.StartAndExpand,
                HorizontalOptions=LayoutOptions.Start,
                Text= @"
Entry emailEntry = new Entry
{
    Placeholder=""Email"",
    PlaceholderColor = Color.Silver,
    Keyboard = Keyboard.Email,
};
emailEntry.Behaviors.Add(new EntryEmailBehavior());

Entry emailEntry2 = new Entry
{
    Placeholder = ""Email"",
    PlaceholderColor = Color.Silver,
    Keyboard = Keyboard.Email,
    TextColor = Color.Blue
};
emailEntry2.Behaviors.Add(new EntryEmailBehavior());

public class EntryEmailBehavior :Behavior<Entry>
{
    static System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("" ^\\s * ([A - Za - z0 - 9_ -] + (\\.\\w +)*@(\\w +\\.)+\\w{ 2, 5 })\\s * $"");
    static Color oldEntryColo;
    /// <summary>
    /// 设置属性时触发
    /// </summary>
    /// <param name=""bindable""></param>
     protected override void OnAttachedTo(Entry bindable)
     {
         bindable.TextChanged += Bindable_TextChanged;
         base.OnAttachedTo(bindable);
     }

    /// <summary>
    /// 删除属性时触发
    /// </summary>
    /// <param name=""bindable""></param>
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

    public class EntryEmailBehavior :Behavior<Entry>
    {
        static System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
        static Color oldEntryColo;
        /// <summary>
        /// 设置属性时触发
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }
        /// <summary>
        /// 删除属性时触发
        /// </summary>
        /// <param name="bindable"></param>
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

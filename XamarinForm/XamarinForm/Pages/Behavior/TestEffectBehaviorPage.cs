using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Pages.Behavior
{
    public class TestEffectBehaviorPage:ContentPage
    {
        public TestEffectBehaviorPage()
        {
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
                Text= "Label Shadow Effect"
            };

            label.Behaviors.Add(new EffectBehavior
            {
                Group = "CustomEffect",
                Name = "LabelShadowEffect"
            });

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
    Text=""Label Shadow Effect""
};

label.Behaviors.Add(new EffectBehavior
{
    Group = ""CustomEffect"",
    Name = ""LabelShadowEffect""
});

 public class EffectBehavior : Behavior<View>
 {
     public static readonly BindableProperty GroupProperty = BindableProperty.Create(""Group"", typeof(String), typeof(EffectBehavior), String.Empty);
     public static readonly BindableProperty NameProperty = BindableProperty.Create(""Name"", typeof(String), typeof(EffectBehavior), String.Empty);

    public String Group
    {
        get { return (String)GetValue(GroupProperty); }

        set { SetValue(GroupProperty, value); }
    }
    public String Name
    {
        get { return (String)GetValue(NameProperty); }

        set { SetValue(NameProperty, value); }
    }

    protected override void OnAttachedTo(View bindable)
    {
        base.OnAttachedTo(bindable);
        AddEffect(bindable);
    }

    protected override void OnDetachingFrom(View bindable)
    {
        RemoveEffect(bindable);
        base.OnDetachingFrom(bindable);
    }

    void AddEffect(View view)
    {
        Effect effect = GetEffect();
        if (effect != null)
            view.Effects.Add(effect);
    }

    void RemoveEffect(View view)
    {
        Effect effect = GetEffect();
        if (effect != null)
            view.Effects.Remove(effect);
    }

    Effect GetEffect()
    {
        if (!string.IsNullOrWhiteSpace(Group) && !String.IsNullOrWhiteSpace(Name))
        {
            return Effect.Resolve(String.Format(""{0}.{1}"", Group, Name));
        }

        return null;
    }
}
"
            };
        }
    }

    public class EffectBehavior : Behavior<View>
    {
        public static readonly BindableProperty GroupProperty = BindableProperty.Create("Group", typeof(String), typeof(EffectBehavior), String.Empty);
        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(String), typeof(EffectBehavior), String.Empty);

        public String Group
        {
            get { return (String)GetValue(GroupProperty); }

            set { SetValue(GroupProperty, value); }
        }
        public String Name
        {
            get { return (String)GetValue(NameProperty); }

            set { SetValue(NameProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            AddEffect(bindable);
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            RemoveEffect(bindable);
            base.OnDetachingFrom(bindable);
        }

        void AddEffect(View view)
        {
            Xamarin.Forms.Effect effect = GetEffect();
            if (effect != null)
            view.Effects.Add(effect);
        }

        void RemoveEffect(View view)
        {
            Xamarin.Forms.Effect effect = GetEffect();
            if (effect != null)
                view.Effects.Remove(effect);
        }

        Xamarin.Forms.Effect GetEffect()
        {
            if (!string.IsNullOrWhiteSpace(Group) && !String.IsNullOrWhiteSpace(Name))
            {
                return Xamarin.Forms.Effect.Resolve(string.Format("{0}.{1}", Group, Name));
            }

            return null;
        }
    }
}

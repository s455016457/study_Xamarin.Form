using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XamarinForm.Utilities;

namespace XamarinForm.Pages
{
	public class ClassAndSubclassesPage : ContentPage
	{
        StackLayout stackLayout = new StackLayout
        {
            Orientation=StackOrientation.Vertical,
            HorizontalOptions=LayoutOptions.Fill,
            VerticalOptions=LayoutOptions.FillAndExpand,
        };
		public ClassAndSubclassesPage ()
		{
            ClassAndSubclasses classAndSubclass = ClassAndSubclassesFactory.Create(typeof(View));
            AddItemToStackLayout(classAndSubclass, 0);
            Content = new ScrollView
            {
                HorizontalOptions=LayoutOptions.Fill,
                VerticalOptions=LayoutOptions.Fill,
                Content= stackLayout,
            };
        }

        void AddItemToStackLayout(ClassAndSubclasses parentClass, int level)
        {
            Label label = new Label
            {
                Text = String.Format("{0}{1}", new string(' ', 4 * level), parentClass.ShowName),
                TextColor = parentClass.Type.IsAbstract ? Color.Blue : Color.Default
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Micro,typeof(Label));
            stackLayout.Children.Add(label);
            foreach (ClassAndSubclasses childClass in parentClass.Subclasses)
            {
                AddItemToStackLayout(childClass, level + 1);
            }
        }
	}
}
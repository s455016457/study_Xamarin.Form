using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinForm.Services;
using XamarinForm.Views;

namespace XamarinForm.Pages.Effect
{
    public class TestLeftSlideViewCellPage : ContentPage
    {
        public TestLeftSlideViewCellPage()
        {

            var liftSlide_DataTemplate = new DataTemplate(typeof(LeftSlideViewCell1));
            liftSlide_DataTemplate.SetBinding(LeftSlideViewCell1.IconProperty, "Icon");
            liftSlide_DataTemplate.SetBinding(LeftSlideViewCell1.TitleProperty, "Title");

            var listView = new ListView()
            {
                ItemTemplate = liftSlide_DataTemplate,
                RowHeight=200,
            };

            listView.ItemsSource = new ListMenuDataStore().GetMenuItem().ChildrenMenu;

            listView.ItemAppearing += ListView_ItemAppearing; ;
            Content = listView;
        }

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listView = sender as ListView;
            foreach (var item in listView.TemplatedItems)
            {
                var LeftSlideViewCell = item as LeftSlideViewCell1;
                if (LeftSlideViewCell != null && LeftSlideViewCell.IsSlide)
                    LeftSlideViewCell.UnSlideAsync();
            }
        }
    }

    class LeftSlideViewCell1 : LeftSlideViewCell
    {
        Image image1;
        Label label;
        Image image2;

        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(ImageSource), typeof(ListMenuCell));
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(String), typeof(ListMenuCell), "");
        public static readonly BindableProperty HasChildProperty = BindableProperty.Create("HasChild", typeof(Boolean), typeof(ListMenuCell), false);

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public Boolean HasChild
        {
            get { return (Boolean)GetValue(HasChildProperty); }
            set { SetValue(HasChildProperty, value); }
        }

        
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            image1.Source = Icon;
            label.Text = Title;
            if (HasChild)
            {
                image2.Source = ImageSource.FromFile("folder.png");
            }
            else
            {
                image2.Source = ImageSource.FromFile("page_white_text.png");
            }
        }

        public override View CreateRightView()
        {
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.End,
            };

            stackLayout.Children.Add(new Button { Text = "标记", HorizontalOptions = LayoutOptions.End, });
            stackLayout.Children.Add(new Button { Text = "批准", HorizontalOptions = LayoutOptions.End, });
            stackLayout.Children.Add(new Button { Text = "拒绝", HorizontalOptions = LayoutOptions.End, });
            stackLayout.Children.Add(new Button { Text = "删除", HorizontalOptions = LayoutOptions.End, });

            return stackLayout;
        }

        public override LeftSlideContentView CreateLeftView()
        {
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.White,
            };

            image1 = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            label = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            label.FontSize = Device.GetNamedSize(NamedSize.Micro, label);

            image2 = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            stackLayout.Children.Add(image1);
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(image2);

            return new LeftSlideContentView { Content = stackLayout, HeightRequest = 440,VerticalOptions=LayoutOptions.FillAndExpand, };
        }
    }
}

using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.Pages.Animation.Custom;

namespace XamarinForm.Pages.Animation
{
    public class TestCustomAnimationPage : ContentPage
    {
        public TestCustomAnimationPage()
        {
            Title = "测试自定义动画效果";

            var scaleButton = new Button { Text = "图片循环放大效果" };
            scaleButton.Clicked += OnImageScaleAnimationButtonClicked;
            var scaleButton2 = new Button { Text = "图片循环放大效果2" };
            scaleButton2.Clicked += OnImageScaleAnimation2ButtonClicked;
            var childButton = new Button { Text = "子动画效果" };
            childButton.Clicked += OnImageChildAnimationButtonClicked;
            var child2Button = new Button { Text = "子动画效果2" };
            child2Button.Clicked += OnImageChildAnimation2ButtonClicked;
            var colorButton = new Button { Text = "颜色画效果" };
            colorButton.Clicked += OnColorAnimation2ButtonClicked;

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = {
                        scaleButton,
                        scaleButton2,
                        childButton,
                        child2Button,
                        colorButton,
                    }
                }
            };
        }
        async void OnImageScaleAnimationButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageScaleAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnImageScaleAnimation2ButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageScaleAnimationPage2();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnImageChildAnimationButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageChildAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnImageChildAnimation2ButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageChildAnimationPage2();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnColorAnimation2ButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ColorAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
    }
}

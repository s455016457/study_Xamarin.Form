using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.Pages.Animation.Basic;

namespace XamarinForm.Pages.Animation
{
    public class TestBasicAnimationPage:ContentPage
    {
        public TestBasicAnimationPage()
        {
            Title = "测试基础动画效果";

            var fadeButton = new Button { Text = "图片淡入效果" };
            fadeButton.Clicked += OnFadeAnimationButtonClicked;

            var imageScaleButton = new Button { Text = "图片缩放效果" };
            imageScaleButton.Clicked += OnImageScaleAnimationButtonClicked;

            var imageRelativeScaleButton = new Button { Text = "图片缩放效果2" };
            imageRelativeScaleButton.Clicked += OnImageRelativeScaleAnimationButtonClicked;

            var labelFadeButton = new Button { Text = "文字淡入效果" };
            labelFadeButton.Clicked += OnLabelFadeAnimationButtonClicked;

            var imageRotateButton = new Button { Text = "图片旋转效果" };
            imageRotateButton.Clicked += OnImageRotateButtonClicked;

            var imageRotateXButton = new Button { Text = "图片沿X轴旋转效果" };
            imageRotateXButton.Clicked += OnImageRotateXButtonClicked;

            var imageRotateYButton = new Button { Text = "图片沿Y轴旋转效果" };
            imageRotateYButton.Clicked += OnImageRotateYButtonClicked;

            var imageMultipleRotateYButton = new Button { Text = "图片多轴旋转效果" };
            imageMultipleRotateYButton.Clicked += OnImageMultipleRotateYButtonClicked;

            var imageRelRotateButton = new Button { Text = "图片旋转效果2" };
            imageRelRotateButton.Clicked += OnImageRelRotateButtonClicked;

            var labelRotateButton = new Button { Text = "文字旋转效果2" };
            labelRotateButton.Clicked += OnLabelRotateButtonClicked;

            var imageTranslateButton = new Button { Text = "图片移动效果" };
            imageTranslateButton.Clicked += OnImageTranslateButtonClicked;

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children = {
                        fadeButton,
                        imageScaleButton,
                        imageRelativeScaleButton,
                        labelFadeButton,
                        imageRotateButton,
                        imageRotateXButton,
                        imageRotateYButton,
                        imageMultipleRotateYButton,
                        imageRelRotateButton,
                        labelRotateButton,
                        imageTranslateButton,
                    }
                }
            };
        }
        async void OnFadeAnimationButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageFadeAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500)
                );
            }
        }
        async void OnImageScaleAnimationButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageScaleAnimationPage();
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.ScaleTo(2, 500)
                );
                await animationPage.ScaleTo(1, 500);
            }
        }
        async void OnImageRelativeScaleAnimationButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageRelativeScaleAnimationPage();
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.ScaleTo(2, 500)
                );
                await animationPage.ScaleTo(1, 500);
            }
        }
        async void OnLabelFadeAnimationButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new LabelFadeAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500)
                );
            }
        }
        async void OnImageRotateButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageRotateAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnImageRotateXButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageRotateXAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateXTo(360, 500)
                );
                animationPage.RotationX = 0;
            }
        }
        async void OnImageRotateYButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageRotateYAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateYTo(360, 500)
                );
                animationPage.RotationY = 0;
            }
        }
        async void OnImageMultipleRotateYButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageMultipleRotationAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500),
                    animationPage.RotateXTo(360, 500),
                    animationPage.RotateYTo(360, 500)
                );
                animationPage.Rotation = 0;
                animationPage.RotationX = 0;
                animationPage.RotationY = 0;
            }
        }
        async void OnImageRelRotateButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageRelativeRotateAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnLabelRotateButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new LabelRotateAnimationPage();
                animationPage.Opacity = 0;
                await Task.WhenAll(
                    Navigation.PushAsync(animationPage, true),
                    animationPage.FadeTo(1, 500),
                    animationPage.RotateTo(360, 500)
                );
                animationPage.Rotation = 0;
            }
        }
        async void OnImageTranslateButtonClicked(object sender, EventArgs e)
        {
            NavigationPage navigation = Parent as NavigationPage;
            if (navigation != null)
            {
                var animationPage = new ImageTranslateAnimationPage();
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

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public class TestProgressBarPage : ContentPage
    {
        public TestProgressBarPage()
        {
            Title = "进度条";
            StackLayout layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding=new Thickness(20,10),
            };

            ScrollView scrollView = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Start,
            };

            setCodeText(scrollView);

            ProgressBar progressBar = new ProgressBar
            {
                Progress=0,
            };
            double progress = 0;

            Device.StartTimer(TimeSpan.FromSeconds(.02), () =>
            {
                progress += 0.003;
                progress = progress > 1 ? 1 : progress;
                progressBar.ProgressTo(progress, 0, Easing.Linear);
                if (progress >= 1)
                {
                    DisplayAlert("结果", "下载完成！", "确定");
                    Debug.WriteLine("进度条执行完成！");
                    return false;
                }
                return true;
            });

            layout.Children.Add(progressBar);
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
ProgressBar progressBar = new ProgressBar
{
    Progress=0,
};

double progress = 0;

var autoEvent = new AutoResetEvent(false);

Device.StartTimer(TimeSpan.FromSeconds(.02), () =>
{
    progress += 0.01;
    progressBar.ProgressTo(progress, 0, Easing.Linear);
    if (progress >= 1)
    {
        DisplayAlert(""结果"", ""下载完成！"", ""确定"");
        Debug.WriteLine(""进度条执行完成！"");
        return false;
    }
    return true;
});
"
            };
        }
    }
}
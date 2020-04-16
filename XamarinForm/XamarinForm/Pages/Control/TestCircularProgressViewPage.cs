using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForm.Views;

namespace XamarinForm.Pages.Control
{
    public class TestCircularProgressViewPage : ContentPage
    {
        public TestCircularProgressViewPage()
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

            CircularProgressView circularProgress = new CircularProgressView
            {
                //WidthRequest=30,
                //HeightRequest=30,
                Progress=0,
            };

            Boolean isAdd = true;

            Device.StartTimer(TimeSpan.FromSeconds(.02), () =>
            {
                //if (isAdd)
                //{
                //    var progress = (circularProgress.Progress + .01);
                //    circularProgress.Progress = progress;
                //    if (progress >= 1) isAdd = false;
                //}
                //else
                //{
                //    var progress = (circularProgress.Progress - .01);
                //    circularProgress.Progress = progress;
                //    if (progress <= 0) isAdd = true;
                //}
                var progress = (circularProgress.Progress + .01);
                if (progress > 1) progress = 0;
                circularProgress.Progress = progress;
                return true;
            });

            layout.Children.Add(circularProgress);
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
/// <summary>
/// 环形进度条
/// </summary>
public class CircularProgressView:Grid
{
    View progress1;
    View progress2;
    View background1;
    View background2;
    public static BindableProperty ProgressProperty = BindableProperty.Create(""Progress"", typeof(double), typeof(CircularProgressView), 0d, propertyChanged: ProgressChanged);

    public CircularProgressView()
    {
        WidthRequest = 30;
        HeightRequest = 30;
        progress1 = CreateImage(""progress_done"");
        background1 = CreateImage(""progress_pending"");
        background2 = CreateImage(""progress_pending"");
        progress2 = CreateImage(""progress_done"");
        HandleProgressChanged(1, 0);
    }
    private View CreateImage(string v1)
    {
        var img = new Image();
        img.Source = ImageSource.FromFile(v1 + "".png"");
        this.Children.Add(img);
        return img;
    }

    private static void ProgressChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var c = bindable as CircularProgressView;
        c.HandleProgressChanged(Clamp((double)oldValue, 0, 1), Clamp((double)newValue, 0, 1));
    }

    static double Clamp(double value, double min, double max)
    {
        if (value <= max && value >= min) return value;
        else if (value > max) return max;
        else return min;
    }

    private void HandleProgressChanged(double oldValue, double p)
    {
        if (p < .5)
        {
            if (oldValue >= .5)
            {
                background1.IsVisible = true;
                progress2.IsVisible = false;
                background2.Rotation = 180;
                progress1.Rotation = 0;
            }
            double rotation = 360 * p;
            background1.Rotation = rotation;
        }
        else
        {
            if (oldValue < .5)
            {
                background1.IsVisible = false;
                progress2.IsVisible = true;
                progress1.Rotation = 180;
            }
            double rotation = 360 * p;
            background2.Rotation = rotation;
        }
    }
            
    /// <summary>
    /// 进度属性
    /// </summary>
    public double Progress
    {
        get { return (double)this.GetValue(ProgressProperty); }
        set { SetValue(ProgressProperty, value); }
    }
}

CircularProgressView circularProgress = new CircularProgressView
{
    WidthRequest=30,
    HeightRequest=30,
    Progress=0,
};

Device.StartTimer(TimeSpan.FromSeconds(.02), () =>
{
    var progress = (circularProgress.Progress + .01);
    if (progress > 1) progress = 0;
    circularProgress.Progress = progress;
    return true;
});
"
            };
        }
    }
}
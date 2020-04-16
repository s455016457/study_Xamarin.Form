using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForm.Pages.Control
{
    public class TestUserDialogsPage : ContentPage
    {
        public TestUserDialogsPage()
        {
            StackLayout layout = new StackLayout();


            var LoginButton = new Button { Text = "登陆" };
            var LoadingButton = new Button { Text = "加载" };
            var ActionSheetButton = new Button { Text = "Action Sheet" };
            var ConfirmButton = new Button { Text = "Confirm" };
            var DateButton = new Button { Text = "Date" };
            var ProgressButton = new Button { Text = "Progress" };
            var PromptButton = new Button { Text = "Prompt" };
            var TimeButton = new Button { Text = "Time" };

            LoginButton.Clicked += LoginButton_Clicked;
            LoadingButton.Clicked += LoadingButton_ClickedAsync;
            ActionSheetButton.Clicked += ActionSheetButton_Clicked;
            ConfirmButton.Clicked += ConfirmButton_Clicked;
            DateButton.Clicked += DateButton_Clicked;
            ProgressButton.Clicked += ProgressButton_Clicked;
            PromptButton.Clicked += PromptButton_Clicked;
            TimeButton.Clicked += TimeButton_Clicked;

            layout.Children.Add(LoginButton);
            layout.Children.Add(LoadingButton);
            layout.Children.Add(ActionSheetButton);
            layout.Children.Add(ConfirmButton);
            layout.Children.Add(DateButton);
            layout.Children.Add(ProgressButton);
            layout.Children.Add(PromptButton);
            layout.Children.Add(TimeButton);

            Content = layout;
        }

        private void ActionSheetButton_Clicked(object sender, EventArgs e)
        {
            var config = new ActionSheetConfig
            {
                Title = "这里是Title",
                Message = "Message",
                UseBottomSheet = true,
                ItemIcon = "page_white_text",
                Destructive=new ActionSheetOption("Destructive", ()=>
                {
                    UserDialogs.Instance.Alert("Destructive Action");
                }, "icon"),
                Cancel=new ActionSheetOption("Cancel", () => 
                {
                    UserDialogs.Instance.Alert("Cancel Action");
                }, "icon"),
                Options ={
                    new ActionSheetOption("Options1",() => 
                    {
                        UserDialogs.Instance.Alert("Options1 Action");
                    }, "icon"),
                    new ActionSheetOption("Options2",() =>
                    {
                        UserDialogs.Instance.Alert("Options2 Action");
                    }, "icon"),
                    new ActionSheetOption("Options3",() =>
                    {
                        UserDialogs.Instance.Alert("Options3 Action");
                    }, "icon"),
                    new ActionSheetOption("Options4",() =>
                    {
                        UserDialogs.Instance.Alert("Options4 Action");
                    }, "icon"),
                    new ActionSheetOption("Options5",() =>
                    {
                        UserDialogs.Instance.Alert("Options5 Action");
                    }, "icon"),
                    new ActionSheetOption("Options6",() =>
                    {
                        UserDialogs.Instance.Alert("Options6 Action");
                    }, "icon"),
                    new ActionSheetOption("Options7",() =>
                    {
                        UserDialogs.Instance.Alert("Options7 Action");
                    }, "icon"),
                },
            };
            var ActionSheet = UserDialogs.Instance.ActionSheet(config);            
        }

        private void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            var config = new ConfirmConfig {
                CancelText="取消",
                Message="确定删除吗？",
                OkText="确定",
                Title= "温馨提示",
                OnAction = p => {
                    if (p)
                    {
                        UserDialogs.Instance.Alert("确定");
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("取消");
                    }
                }
            };

            var confirm = UserDialogs.Instance.Confirm(config);
        }
        private void DateButton_Clicked(object sender, EventArgs e)
        {
            var config = new DatePromptConfig
            {
                CancelText="取消",
                OkText="确定",
                Title= "温馨提示",
                IsCancellable=true,
                SelectedDate=DateTime.Now,
                UnspecifiedDateTimeKindReplacement=DateTimeKind.Utc,
                MinimumDate=DateTime.MinValue,
                MaximumDate=DateTime.MaxValue,
                OnAction = p => {
                    if (p.Ok)
                    {                       
                        UserDialogs.Instance.Alert("确定" + p.Value ==null? String.Empty:  p.Value.ToString("R"));
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("取消" + p.Value == null ? String.Empty : p.Value.ToString("R"));
                    }
                }
            };

            var datePrompt = UserDialogs.Instance.DatePrompt(config);
        }
        private void PromptButton_Clicked(object sender, EventArgs e)
        {
            var config = new PromptConfig
            {
                CancelText="取消",
                OkText="确定",
                Title= "温馨提示",
                IsCancellable=true,
                InputType=InputType.Email,
                Placeholder="请输入电子邮件",
                Message="这里是Message",
                OnTextChanged = p =>
                {
                    UserDialogs.Instance.Alert(String.Format("有效性{0}，值为：{1}",p.IsValid, p.Value));
                },
                OnAction = p => {
                    if (p.Ok)
                    {                       
                        UserDialogs.Instance.Alert("确定" + p.Value);
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("取消" + p.Value);
                    }
                }
            };

            var datePrompt = UserDialogs.Instance.Prompt(config);
        }
        private void TimeButton_Clicked(object sender, EventArgs e)
        {
            var config = new TimePromptConfig
            {
                CancelText="取消",
                OkText="确定",
                Title= "温馨提示",
                IsCancellable=true,
                Use24HourClock=true,
                SelectedTime=TimeSpan.FromHours(2),
                OnAction = p => {
                    if (p.Ok)
                    {                       
                        UserDialogs.Instance.Alert("确定" + p.Value);
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("取消" + p.Value);
                    }
                }
            };

            var datePrompt = UserDialogs.Instance.TimePrompt(config);
        }
        private async void LoadingButton_ClickedAsync(object sender, EventArgs e)
        {
            var loading = UserDialogs.Instance.Loading("数据加载中，请稍后...", maskType:MaskType.Gradient);
            loading.Show();
            await Task.Delay(5000);
            loading.Hide();
        }
        private void ProgressButton_Clicked(object sender, EventArgs e)
        {
            var progress = UserDialogs.Instance.Progress("数据加载中，请稍后...", maskType:MaskType.Gradient);            
            progress.Show();

            //while (progress.PercentComplete < 100)
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(.1));
            //    progress.PercentComplete += 1;
            //}
            //progress.Hide();
            //progress.Dispose();

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                progress.PercentComplete += 10;
                if (progress.PercentComplete >= 100)
                {
                    progress.Hide();
                    progress.Dispose();
                    return false;
                }
                return true;
            });
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            var config = new LoginConfig
            {
                Title = "请登陆",
                LoginPlaceholder="请输入账号",
                PasswordPlaceholder="请输入密码",
                Message="这里是Message",
                OkText="登陆",
                CancelText = "取消",
            };
            config.OnAction += p =>
            {
                if (p.Ok)
                {
                    UserDialogs.Instance.Alert(string.Format("登陆，登陆账号：{0}，密码：{1}", p.LoginText, p.Password));
                }
                else
                {
                    UserDialogs.Instance.Alert(string.Format("取消，登陆账号：{0}，密码：{1}", p.LoginText, p.Password));
                }
            };
            UserDialogs.Instance.Login(config);
        }
    }
}

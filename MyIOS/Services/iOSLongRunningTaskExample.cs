using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using UserNotifications;

namespace MyIOS.Services
{
    public class iOSLongRunningTaskExample
    {
        nint _taskId;
        CancellationTokenSource _cts;

        public void Start()
        {
            _cts = new CancellationTokenSource();

            _taskId = UIApplication.SharedApplication.BeginBackgroundTask("LongRunningTask", OnExpiration);
            Console.WriteLine("开启后台应用程序");

            try
            {
                //INVOKE THE SHARED CODE
                int count = 1;
                Timer timer = new Timer((stateInfo) =>
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    count++;

                    var content = new UNMutableNotificationContent();
                    content.Title = "服务通知标题" + count;
                    content.Subtitle = "服务通知副标题" + count;
                    content.Body = "服务通知类容,这里可以有好多的内容" + count;
                    content.Badge = count;

                    //1秒后发送通知，不重复
                    var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

                    // 创建通知发送请求
                    var requestID = "sampleRequest1";
                    var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                    // 添加通知发送请求
                    UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
                    {
                        if (err != null)
                        {
                                // 处理异常
                        }
                    });

                }, _cts, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(30));

            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    Stop();
                }
            }

        }

        public void Stop()
        {
            _cts.Cancel();
            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
        }

        void OnExpiration()
        {
            Console.WriteLine("后台应用程序时间到了");
            _cts.Cancel();
            UIApplication.SharedApplication.EndBackgroundTask(_taskId);
            Start();
        }
    }    
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForm.DependencyServices
{
    public abstract class AbstractNotificationsImpl : INotificationService
    {        
        public abstract Task<bool> RequestPermission();
        public abstract Task<int> Send(Notification notification);
        public abstract void Vibrate(int ms = 300);
    }
}

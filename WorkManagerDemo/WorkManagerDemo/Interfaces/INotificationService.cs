using System;
using System.Collections.Generic;
using System.Text;

namespace WorkManagerDemo.Interfaces
{
    public interface INotificationService
    {
        void CreateNotification(string title, string message);
    }
}

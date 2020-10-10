using CelebrationRegister.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CelebrationRegister.Core.Services.Interfaces
{
    public interface INotificationServices
    {
        List<Notification> GetAllNotification(int take = 10);
        Notification GetNotificationById(int notificationId);
        void UpdateNotification(Notification notification);
        void DeleteNotification(int notificationId);
        void DeleteNotification(Notification notification);
        void AddNotfication(Notification notification);
        void Save();
    }
}

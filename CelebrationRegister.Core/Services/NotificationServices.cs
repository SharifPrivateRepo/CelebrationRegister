using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities;

namespace CelebrationRegister.Core.Services
{
    public class NotificationServices:INotificationServices
    {
        #region Injection

        private CelebrationRegister_Context db;

        public NotificationServices(CelebrationRegister_Context db)
        {
            this.db = db;
        }

        #endregion

        public List<Notification> GetAllNotification(int take = 10)
        {
            return db.Notfications
                .OrderByDescending(n=>n.CreateDate)
                .Take(take).ToList();
        }

        public Notification GetNotificationById(int notificationId)
        {
            return db.Notfications.Find(notificationId);
        }

        public void UpdateNotification(Notification notification)
        {
            db.Notfications.Update(notification);
        }

        public void DeleteNotification(int notificationId)
        {
            var notification = GetNotificationById(notificationId);
            DeleteNotification(notification);
        }

        public void DeleteNotification(Notification notification)
        {
            db.Notfications.Remove(notification);
        }

        public void AddNotfication(Notification notification)
        {
            db.Notfications.Add(notification);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

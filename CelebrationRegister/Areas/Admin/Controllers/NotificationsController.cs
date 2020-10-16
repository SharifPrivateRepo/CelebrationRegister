using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CelebrationRegister.Core.Security;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Core.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CelebrationRegister.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(1)]
    public class NotificationsController : Controller
    {
        #region Injection

        private INotificationServices _notificationServices;

        public NotificationsController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        #endregion

        // GET: Admin/Notifications
        public IActionResult Index()
        {
            return View(_notificationServices.GetAllNotification());
        }

        // GET: Admin/Notifications/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = _notificationServices.GetNotificationById(id.Value);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // GET: Admin/Notifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Notifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NotificationId,Title,ShortDescription,Text,Image,CreateDate")] Notification notification, IFormFile notifImage)
        {
            if (ModelState.IsValid)
            {
                if (notifImage != null)
                {
                    notification.Image = ImageTools.SaveImage(notifImage, "NotificationImages");
                }
                else
                {
                    notification.Image = "default-notification.jpg";
                }
                notification.CreateDate = DateTime.Now;
                _notificationServices.AddNotfication(notification);
                _notificationServices.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(notification);
        }

        // GET: Admin/Notifications/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = _notificationServices.GetNotificationById(id.Value);
            if (notification == null)
            {
                return NotFound();
            }
            return View(notification);
        }

        // POST: Admin/Notifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("NotificationId,Title,ShortDescription,Text,Image,CreateDate")] Notification notification, IFormFile notifImage)
        {
            if (id != notification.NotificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (notifImage != null)
                    {
                        notification.Image = ImageTools.UpdateImage(notifImage, "NotificationImages", "default-notification.jpg",
                             notification.Image);
                    }
                    _notificationServices.UpdateNotification(notification);
                    _notificationServices.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notification);
        }

        // GET: Admin/Notifications/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = _notificationServices.GetNotificationById(id.Value);
            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        // POST: Admin/Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _notificationServices.DeleteNotification(id);
            _notificationServices.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}

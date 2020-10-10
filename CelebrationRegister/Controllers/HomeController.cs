using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CelebrationRegister.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CelebrationRegister.Models;

namespace CelebrationRegister.Controllers
{
    public class HomeController : Controller
    {
        #region Injection

        private INotificationServices _notificationServices;

        public HomeController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        #endregion

        public IActionResult Index()
        {
            return View(_notificationServices.GetAllNotification(8));
        }

    }
}

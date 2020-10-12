using CelebrationRegister.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CelebrationRegister.Web.Controllers
{
    public class NewsController : Controller
    {
        #region Injection

        private INotificationServices _notificationServices;

        public NewsController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        #endregion

        public IActionResult Index(int id)
        {
           
            return View(_notificationServices.GetNotificationById(id));
        }
    }
}

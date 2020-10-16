using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CelebrationRegister.Core.Services.Interfaces;
using CelebrationRegister.Data.Context;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.AdditionalOptions;
using CelebrationRegister.Data.Entities.DynamicSettings;

namespace CelebrationRegister.Core.Services
{
    public class SettingServices : ISettingServices
    {
        #region Injection

        private CelebrationRegister_Context db;

        public SettingServices(CelebrationRegister_Context db)
        {
            this.db = db;
        }

        #endregion


        #region BirthDayLimitation

        public void ChangeBirthDayLimitation(Setting setting)
        {
            db.Settings.Update(setting);
            db.SaveChanges();
        }

        public DateTime GetBirthDayLimitation()
        {
            var setting = db.Settings.Find(1);
            return setting.BirthDayLimitation.Value;
        }

        public void SetBirthDayLimitation(DateTime date)
        {
            var setting = db.Settings.Find(1);
            setting.BirthDayLimitation = date;
            db.Update(setting);
            db.SaveChanges();
        }

        public List<City> GetCityList()
        {
            var items = db.City.ToList();
            return items;
        }

        public List<AdditionalOptions> GetAllAdditionalOptions()
        {
            return db.AdditionalOptions.ToList();
        }

        public void AddOption(string optionTitle)
        {
            db.AdditionalOptions.Add(new AdditionalOptions()
            {
                OptionTitle = optionTitle
            });

            db.SaveChanges();
        }

        public void DeleteOption(int optionId)
        {
            var option = db.AdditionalOptions.SingleOrDefault(a => a.OptionId == optionId);
            if (option != null && !option.IsDelete)
            {
                option.IsDelete = true;

                db.AdditionalOptions.Update(option);
                db.SaveChanges();
            }
        }

        public Setting GetAverageNotification()
        {
            return db.Settings.Find(2);
        }

        public void SetAverageNotification(string text)
        {
           var notification = db.Settings.Find(2);
           notification.Notification = text;
           db.Settings.Update(notification);
           db.SaveChanges();
        }

        #endregion
    }
}

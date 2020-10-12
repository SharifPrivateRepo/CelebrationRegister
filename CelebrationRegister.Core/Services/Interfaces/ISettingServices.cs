﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CelebrationRegister.Data.Entities;
using CelebrationRegister.Data.Entities.DynamicSettings;

namespace CelebrationRegister.Core.Services.Interfaces
{
    public interface ISettingServices
    {
        #region BirthDayLimitation

        void ChangeBirthDayLimitation(Setting setting);

        DateTime GetBirthDayLimitation();

        void SetBirthDayLimitation(DateTime date);

        List<City> GetCityList();

        

        #endregion
    }
}

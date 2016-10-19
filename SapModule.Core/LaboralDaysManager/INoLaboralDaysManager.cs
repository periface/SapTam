using System;
using System.Collections.Generic;
using Abp.Domain.Services;

namespace SapModule.Core.LaboralDaysManager
{
    public interface INoLaboralDaysManager : IDomainService
    {
        void AddNoLaboralDays(List<DateTime> datesList);
        int GetYearLaboralDaysCount(int year);
        int GetYearLaboralDaysCount(DateTime date);
        int GetRangeLaboralDaysCount(DateTime starTime,DateTime endTime);
        bool IsLaboralDay(DateTime date);
        bool IsTodayLaboralDay();
    }
}

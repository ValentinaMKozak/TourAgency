using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;

namespace TourAgency.BLL.Interfaces
{
    public interface IDailyProgramService
    {
        IEnumerable<DailyProgramDTO> GetAllDailyPrograms(int? tourId);
        DailyProgramDTO GetDailyProgram(int? id);
        bool CreateDailyProgram(DailyProgramDTO dailyProgramDTO);
        bool UpdateDailyProgram(int? id, DailyProgramDTO dailyProgramDTO);
        bool DeleteDailyProgram(int? id);
        void Dispose();
    }
}

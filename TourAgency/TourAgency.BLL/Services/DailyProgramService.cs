using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;
using TourAgency.BLL.Infrastructure;
using TourAgency.BLL.Interfaces;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;
using TourAgency.DAL.Repositories;

namespace TourAgency.BLL.Services
{
    public class DailyProgramService : IDailyProgramService
    {
        IUnitOfWork Database { get; set; }

        public DailyProgramService()
        {
            Database = new EFUnitOfWork(); 
        }

        public IEnumerable<DailyProgramDTO> GetAllDailyPrograms(int? tourId)
        {
            var dailyPrograms = Database.DailyPrograms.Find(i => i.TourId == tourId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DailyProgram, DailyProgramDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<DailyProgram>, List<DailyProgramDTO>>(dailyPrograms);
        }

        public DailyProgramDTO GetDailyProgram(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var dailyProgramDb = Database.DailyPrograms.Get(id.Value);
            if (dailyProgramDb == null)
                throw new ValidationException("Daily program wasn't found", "");
            DailyProgramDTO dailyProgramDTO = new DailyProgramDTO
            {
                DailyProgramId = dailyProgramDb.DailyProgramId,
                Theme = dailyProgramDb.Theme,
                Description = dailyProgramDb.Description,
                TourId = dailyProgramDb.TourId
            };

            return dailyProgramDTO;
        }

        public bool CreateDailyProgram(DailyProgramDTO dailyProgramDTO)
        {
            var dailyProgramDb = new DailyProgram
            {
                Theme = dailyProgramDTO.Theme,
                Description  = dailyProgramDTO.Description,
                TourId = dailyProgramDTO.TourId
            };
            Database.DailyPrograms.Create(dailyProgramDb);

            return Database.Save();
        }

        public bool UpdateDailyProgram(int? id, DailyProgramDTO dailyProgramDTO)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var dailyProgramDb = Database.DailyPrograms.Get(id.Value);
            if (dailyProgramDb == null)
                throw new ValidationException("Daily program wasn't found", "");
            dailyProgramDb.Theme = dailyProgramDTO.Theme;
            dailyProgramDb.Description = dailyProgramDTO.Description;
            Database.DailyPrograms.Update(dailyProgramDb);

            return Database.Save();
        }

        public bool DeleteDailyProgram(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var dailyProgramDb = Database.DailyPrograms.Get(id.Value);
            if (dailyProgramDb == null)
                throw new ValidationException("Daily program wasn't found", "");
            Database.DailyPrograms.Delete(dailyProgramDb.DailyProgramId);

            return Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

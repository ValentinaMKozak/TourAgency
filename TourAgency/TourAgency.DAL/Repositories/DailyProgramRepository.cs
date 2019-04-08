using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourAgency.DAL.EF;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;

namespace TourAgency.DAL.Repositories
{
    public class DailyProgramRepository : IRepository<DailyProgram>
    {
        private readonly TourAgencyContext db;
         
        public DailyProgramRepository(TourAgencyContext context)
        {
            db = context;
        }

        public IEnumerable<DailyProgram> GetAll()
        {
            return db.DailyPrograms;
        }

        public DailyProgram Get(int? id)
        {
            return db.DailyPrograms.FirstOrDefault(dp => dp.DailyProgramId == id);
        }

        public IEnumerable<DailyProgram> Find(Func<DailyProgram, bool> predicate)
        {
            return db.DailyPrograms.Where(predicate).ToList();
        }

        public void Create(DailyProgram dailyProgram)
        {
            db.DailyPrograms.Add(dailyProgram);
        }

        public void Update(DailyProgram dailyProgram)
        {
            db.Entry(dailyProgram).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            DailyProgram dailyProgram = db.DailyPrograms.Find(id);
            if (dailyProgram != null)
                db.DailyPrograms.Remove(dailyProgram);
        }
    }
}

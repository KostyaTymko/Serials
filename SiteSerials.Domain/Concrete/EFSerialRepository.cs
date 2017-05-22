using SiteSerials.Domain.Abstract;
using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Concrete
{
    public class EFSerialRepository : ISerialRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Serial> Serials
        {
            get { return context.Serials; }
        }

        public void SaveSerial(Serial serial)
        {
            if (serial.Id == 0)
                context.Serials.Add(serial);
            else
            {
                Serial dbEntry = context.Serials.Find(serial.Id);
                if (dbEntry != null)
                {
                    dbEntry.Serial_title = serial.Serial_title;
                    dbEntry.SerialDescription = serial.SerialDescription;
                    dbEntry.Rating = serial.Rating;
                    dbEntry.Category = serial.Category;
                    dbEntry.Date = serial.Date;
                    dbEntry.ImageData = serial.ImageData;
                    dbEntry.ImageMimeType = serial.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public void SaveSeason(Season season)
        {
            if (season.Id == 0)
                context.Seasons.Add(season);
            else
            {
                Season dbEntry = context.Seasons.Find(season.Id);
                if (dbEntry != null)
                {
                    dbEntry.Season_title = season.Season_title;
                    dbEntry.Serial = season.Serial;
                }
            }
            context.SaveChanges();
        }

        public void CreateSeason(Season season)
        {
            context.Seasons.Add(season);
            context.SaveChanges();
        }


        public Serial DeleteSerial(int serialId)
        {
            Serial dbEntry = context.Serials.Find(serialId);
            if (dbEntry != null)
            {
                context.Serials.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Season DeleteSeason(int seasonId)
        {
            Season dbEntry = context.Seasons.Find(seasonId);
            if (dbEntry != null)
            {
                context.Seasons.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}

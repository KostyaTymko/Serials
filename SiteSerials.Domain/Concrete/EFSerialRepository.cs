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
    }
}

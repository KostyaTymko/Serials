using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Entities
{
    public class Favorite
    {

        public ICollection<Serial> FavSerials { get; set; }

        //public void AddItem(Serial serial)
        //{
        //    Line line = lineCollection
        //        .Where(g => g.Serial.Id == serial.Id)
        //        .FirstOrDefault();

        //    if (line == null)
        //    {
        //        lineCollection.Add(new Line
        //        {
        //            Serial = serial
        //        });
        //    }
        //}

        public void RemoveLine(Serial serial)
        {
            FavSerials.Remove(serial);
        }

        public void Clear()
        {
            FavSerials.Clear();
        }


    }
}

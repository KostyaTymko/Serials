using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Abstract
{
    public interface ISerialRepository
    {
        IEnumerable<Serial> Serials { get; }
        IEnumerable<Season> Seasons { get; }
        IEnumerable<Serie> Series { get; }
        void SaveSerial(Serial serial);
        void SaveSeason(Season season);
        void SaveSerie(Serie serie);
        void CreateSeason(Season season);
        void CreateSerie(Serie serie);
        Serial DeleteSerial(int serialId);
        Season DeleteSeason(int seasonId);
        Serie DeleteSerie(int serieId);
    }
}

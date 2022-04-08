using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class Brommer
    {
        public long ID;
        public string Merk;
        public string Kenteken;
        public int KilometerStand;

        public Brommer(long iD, string merk, string kenteken, int kilometerStand)
        {
            ID = iD;
            Merk = merk;
            Kenteken = kenteken;
            KilometerStand = kilometerStand;
        }

        public Brommer(BrommerDTO dto)
        { 
            ID = dto.ID;
            Merk = dto.Merk;
            Kenteken = dto.Kenteken;
            KilometerStand = dto.KilometerStand;
        }

        internal BrommerDTO GetDTO()
        {
            BrommerDTO dto = new BrommerDTO(ID, Merk, Kenteken, KilometerStand);
            return dto;
        }
    }
}

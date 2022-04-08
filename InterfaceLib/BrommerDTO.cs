using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class BrommerDTO
    {
        public readonly long ID;
        public readonly string Merk;
        public readonly string Kenteken;
        public readonly int KilometerStand;

        public BrommerDTO(long iD, string merk, string kenteken, int kilometerStand)
        {
            ID = iD;
            Merk = merk;
            Kenteken = kenteken;
            KilometerStand = kilometerStand;
        }
    }
}

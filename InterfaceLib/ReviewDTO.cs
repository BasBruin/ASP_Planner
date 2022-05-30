using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class ReviewDTO
    {
        public readonly int? ID;
        public readonly string Teamreview;
        public readonly int TeamID;

        public ReviewDTO(string teamreview, int teamID, int? iD = null)
        {
            Teamreview = teamreview;
            TeamID = teamID;
            ID = iD;
        }
    }
}

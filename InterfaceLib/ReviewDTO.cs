using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class ReviewDTO
    {
        public readonly int ID;
        public readonly string Teamreview;
        public readonly int TeamID;

        public ReviewDTO(int iD, string teamreview, int teamID)
        {
            ID = iD;
            Teamreview = teamreview;
            TeamID = teamID;
        }
    }
}

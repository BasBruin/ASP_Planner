using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class Review
    {
        public readonly int ID;
        public readonly string Teamreview;
        public readonly int TeamID;

        public Review(int iD, string teamreview, int teamID)
        {
            ID = iD;
            Teamreview = teamreview;
            TeamID = teamID;
        }

    }
}

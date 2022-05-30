using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class Review
    {
        public readonly int? ID;
        public readonly string Teamreview;
        public readonly int TeamID;

        public Review(string teamreview, int teamID, int? iD = null)
        {
            Teamreview = teamreview;
            TeamID = teamID;
            ID = iD;
        }

        public Review(ReviewDTO dto)
        {
            this.ID = dto.ID;
            this.Teamreview = dto.Teamreview;
            this.TeamID = dto.TeamID;
        }

        public ReviewDTO GetDTO()
        {
            ReviewDTO dto = new(Teamreview, TeamID ,ID);
            return dto;
        }
    }
}

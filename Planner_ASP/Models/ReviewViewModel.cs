using InterfaceLib;

namespace Planner_ASP.Models
{
    public class ReviewViewModel
    {
        public readonly int? ID;
        public readonly string Teamreview;
        public readonly int TeamID;

        public ReviewViewModel(string teamreview, int teamID, int? iD = null)
        {
            Teamreview = teamreview;
            TeamID = teamID;
            ID = iD;
        }

        public ReviewViewModel(ReviewDTO dto)
        {
            this.ID = dto.ID;
            this.Teamreview = dto.Teamreview;
            this.TeamID = dto.TeamID;
        }
    }
}

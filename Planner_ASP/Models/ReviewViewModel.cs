using InterfaceLib;

namespace Planner_ASP.Models
{
    public class ReviewViewModel
    {
        public int? ID { get; set; }
        public string Teamreview { get; set; }
        public int TeamID{ get; set; }

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

        public ReviewViewModel()
        {

        }
    }
}

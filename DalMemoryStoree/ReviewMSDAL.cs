using InterfaceLib;

namespace DalMemoryStoree
{
    public class ReviewMSDAL : IReviewContainer
    {
        List<ReviewDTO> reviews = new();

        public void Create(ReviewDTO dTO)
        {
            reviews.Add(dTO);
        }

        public List<ReviewDTO> GetTeamReviews(int ID)
        {
            TeamDTO t1 = new(ID, "Avans", "Kaasje");
            ReviewDTO r1 = new("Leuk", t1.ID);
            
            reviews.Add(r1);
            return reviews;
        }
    }
}
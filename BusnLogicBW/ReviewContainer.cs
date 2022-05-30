using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class ReviewContainer
    {
        private readonly IReviewContainer container;

        public ReviewContainer(IReviewContainer container)
        {
            this.container = container;
        }

        public void Create(Review r)
        {
            ReviewDTO dto = r.GetDTO();
            container.Create(dto);
        }

        public List<Review> GetTeamReviews(int ID)
        {
            List<ReviewDTO> dtos = container.GetTeamReviews(ID);
            List<Review> reviews = new();
            foreach (ReviewDTO dto in dtos)
            {
                reviews.Add(new Review(dto));
            }
            return reviews;
        }
    }
}

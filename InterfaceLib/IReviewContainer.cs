using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public interface IReviewContainer
    {
        public List<ReviewDTO> GetTeamReviews(int ID);
        public void Create(ReviewDTO dTO);
    }
}

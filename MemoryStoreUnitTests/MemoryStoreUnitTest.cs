using BusnLogicBW;
using DalMemoryStoree;
using InterfaceLib;

namespace MemoryStoreUnitTests
{
    [TestClass]
    public class MemoryStoreUnitTest
    {
        ReviewContainer container = new ReviewContainer(new ReviewMSDAL());


        [TestMethod]
        public void GetTeamReviewsTest(int ID)
        {
            //Arrange

            //Act
            List<Review> dtos = container.GetTeamReviews(1);
            //Assert
            Assert.AreEqual(1, dtos[0].TeamID);
            Assert.AreEqual(1, dtos.Count);
        }

    }
}
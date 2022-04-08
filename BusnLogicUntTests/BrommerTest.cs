using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusnLogicBW;
using DALFakeUnitTests;


namespace BusnLogicUntTests
{
    [TestClass]
    public class BrommerTest
    {
        private readonly BrommerContainer bc = new(new BrommerFakeUTDAL());

        [TestMethod]
        public void ZoekHenk()
        {
            // Arrange.
            Brommer b;
            // Act.
            b = bc.FindByID(1);
            // Assert.
            Assert.AreEqual("Henk", b.Merk);
            Assert.AreEqual(1, b.ID);
        }

        [TestMethod]
        public void VindVerkoperNiet()
        {
            // Arrange.
            // Act.
            // Assert.
            Assert.ThrowsException<Exception>(() => bc.FindByID(2));
        }
    }
}

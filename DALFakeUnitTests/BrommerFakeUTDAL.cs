using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALFakeUnitTests
{
    public class BrommerFakeUTDAL : IBrommerContainer
    {
        public void Create(BrommerDTO brommer)
        {
            if (brommer.ID == 2)
            {
                throw new Exception("Bestond al");
            }
        }

        public BrommerDTO FindByID(long ID)
        {
            if (ID == 1)
            {
                return new BrommerDTO(1, "Audi", "DBB-11-B", 5000);
            }
            throw new Exception("Bestaat niet");
        }

        public BrommerDTO FindByKenteken(string kenteken)
        {
            BrommerDTO brommer = new(1, "Audi", "DBB-11-B", 5000);

            if (kenteken == "DBB-11-B")
            {
                return brommer;
            }
            throw new Exception("Bestaat niet");

        }

        public List<BrommerDTO> FindByMerk(string merk)
        {
            List<BrommerDTO> brommers = new();
            BrommerDTO brommer = new(1, "Audi", "DBB-11-B", 5000);

            if (merk == "Audi")
            {
                brommers.Add(brommer);
            }
            else if (merk == "Riva")
            {
                throw new Exception("Tyf op met je Riva");
            }
            return brommers;
        }

        public void Update(BrommerDTO b)
        {

        }
        public void Delete(BrommerDTO b)
        {

        }
    }
}

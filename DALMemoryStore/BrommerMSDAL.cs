using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALMemoryStore
{
    public class BrommerMSDAL : IBrommerContainer
    {
        private readonly List<BrommerDTO> brommerstore = new();

        public void Create(BrommerDTO brommer)
        {
            brommerstore.Add(brommer);
        }

        public void Delete(BrommerDTO brommer)
        {
            BrommerDTO dto = FindByID(brommer.ID);
            brommerstore.Remove(dto);
        }

        public BrommerDTO FindByID(long ID)
        {
            foreach (BrommerDTO v in brommerstore)
            {
                if (v.ID == ID)
                {
                    return v;
                }
            }
            throw new Exception("Bestaat niet");
        }

        public BrommerDTO FindByKenteken(string kenteken)
        {
            foreach (BrommerDTO v in brommerstore)
            {
                if (v.Kenteken == kenteken)
                {
                    return v;
                }
            }
            throw new Exception("Bestaat niet");
        }

        public List<BrommerDTO> FindByMerk(string merk)
        {
            List<BrommerDTO> lijst = new List<BrommerDTO>();

            foreach (BrommerDTO v in brommerstore)
            {
                if (v.Merk == merk)
                {
                    lijst.Add(v);
                }
            }
            return lijst;
        }

        public void Update(BrommerDTO b)
        {
            BrommerDTO dto = FindByID(b.ID);
            int positie = brommerstore.FindIndex(d => d == dto);
            brommerstore[positie] = b;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLib;

namespace BusnLogicBW
{
    public class BrommerContainer
    {
        private readonly IBrommerContainer container;

        public BrommerContainer(IBrommerContainer container)
        {
            this.container = container;
        }
        public Brommer FindByID(long ID)
        {
            BrommerDTO dto = container.FindByID(ID);
            return new Brommer(dto);
        }

        public Brommer FindByKenteken(string Kenteken)
        {
            BrommerDTO dto = container.FindByKenteken(Kenteken);
            return new Brommer(dto);
        }

        public List<Brommer> FindByMerk(string merk)
        {
            List<Brommer> brommers = new List<Brommer>();
            List<BrommerDTO> dtos = container.FindByMerk(merk);

            foreach (BrommerDTO dto in dtos)
            {
                brommers.Add(new Brommer(dto));
            }

            return brommers;
        }

        public void Create(Brommer b)
        {
            BrommerDTO dto = b.GetDTO();
            container.Create(dto);
        }

        public void Update(Brommer b)
        {
            BrommerDTO dto = b.GetDTO();
            container.Update(dto);
        }

        public void Delete(Brommer b)
        {
            BrommerDTO dto = b.GetDTO();
            container.Delete(dto);
        }
    }
}

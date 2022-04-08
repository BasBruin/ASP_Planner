using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public interface IBrommerContainer
    {
        public BrommerDTO FindByID(long ID);
        public void Create(BrommerDTO brommer);
        public BrommerDTO FindByKenteken(string kenteken);
        public List<BrommerDTO> FindByMerk(string merk);
        public void Update(BrommerDTO brommer);
        public void Delete(BrommerDTO brommer);
    }
}

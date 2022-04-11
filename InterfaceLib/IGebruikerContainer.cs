using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public interface IGebruikerContainer
    {
        public GebruikerDTO FindByID(int ID);
        public int Create(GebruikerDTO gebruiker);
        public void Update(GebruikerDTO gebruiker);
        public void Delete(GebruikerDTO gebruiker);
        public List<GebruikerDTO> GetAll();
    }
}

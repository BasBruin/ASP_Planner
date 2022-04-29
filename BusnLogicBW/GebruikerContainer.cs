using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusnLogicBW
{
    public class GebruikerContainer
    {
        private readonly IGebruikerContainer container;

        public GebruikerContainer(IGebruikerContainer container)
        {
            this.container = container;
        }

        public Gebruiker FindByID(int ID)
        {
            GebruikerDTO dto = container.FindByID(ID);
            return new Gebruiker(dto);
        }

        public void Create(Gebruiker g, string wachtwoord)
        {
            GebruikerDTO dto = g.GetDTO();
            container.Create(dto, wachtwoord);
        }

        public void Update(Gebruiker g)
        {
            GebruikerDTO dto = g.GetDTO();
            container.Update(dto);
        }

        public void Delete(Gebruiker g)
        {
            GebruikerDTO dto = g.GetDTO();
            container.Delete(dto);
        }

        public List<Gebruiker> GetAll()
        {
            List<GebruikerDTO> dtos = container.GetAll();
            List<Gebruiker> gebruikers = new List<Gebruiker>();
            foreach (GebruikerDTO dto in dtos)
            {
                gebruikers.Add(new Gebruiker(dto));
            }
            return gebruikers;
        }

        public Gebruiker FindByUsernameAndPassword(string gebruikersnaam, string wachtwoord)
        {
            GebruikerDTO dto = container.FindByUsernameAndPassword(gebruikersnaam, wachtwoord);
            if (dto == null)
            {
                return null;
            }
            return new Gebruiker(dto);
        }
    }
}

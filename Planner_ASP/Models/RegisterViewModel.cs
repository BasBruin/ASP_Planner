using System.ComponentModel.DataAnnotations;

namespace Planner_ASP.Models
{
    public class RegisterViewModel
    {
        public string Naam { get; set; }
        public string GameNaam { get; set; }
        public string PlannerNaam { get; set; }
        [Required(ErrorMessage = "Vul dit veld in!")]
        public string Wachtwoord { get; set; }
        public string WachtwoordHerhalen { get; set; }
        public string Email { get; set; }
        public string Rank1s { get; set; }
        public string Rank2s { get; set; }
        public string Rank3s { get; set; }

        public enum Ranks
        {
            Unranked,
            BronzeI,
            BronzeII,
            BronzeIII,
            SilverI,
            SilverII,
            SilverIII,
            GoldI,
            GoldII,
            GoldIII,
            PlatinumI,
            PlatinumII,
            PlatinumIII,
            DiamondI,
            DiamondII,
            DiamondIII,
            ChampionI,
            ChampionII,
            ChampionIII,
            GrandChampionI,
            GrandChampionII,
            GrandChampionIII,
            SuperSonicLegend
        }
    }
}

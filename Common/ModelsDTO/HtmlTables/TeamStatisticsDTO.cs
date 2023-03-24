namespace Common.ModelsDTO.HtmlTables
{
    public class TeamStatisticsDTO
    {
        //it is important to put the properties in the correct order
        public string Lp { get; set; }
        public string Name { get; set; }
        public string Matches { get; set; }
        public string Points { get; set; }

        //total
        public string TotalWins { get; set; }
        public string TotalDraws { get; set; }
        public string TotalLosts { get; set; }
        public string TotalGoals { get; set; }

        //home
        public string HomeWins { get; set; }
        public string HomeDraws { get; set; }
        public string HomeLosts { get; set; }
        public string HomeGoals { get; set; }

        //away
        public string AwayWins { get; set; }
        public string AwayDraws { get; set; }
        public string AwayLosts { get; set; }
        public string AwayGoals { get; set; }

        //direct
        public string DirectMatches { get; set; }
        public string DirectMatchesPoints { get; set; }
        public string DirectWins { get; set; }
        public string DirectDraws { get; set; }
        public string DirectLosts { get; set; }
        public string DirectGoals { get; set; }
    }
}

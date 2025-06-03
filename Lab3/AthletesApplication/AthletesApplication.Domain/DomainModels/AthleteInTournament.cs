namespace AthletesApplication.Domain.DomainModels
{
    public class AthleteInTournament : BaseEntity
    {
        public Athlete Athlete { get; set; }
        public Guid AthleteId { get; set; }
        public Tournament Tournament { get; set; }
        public Guid TournamentId { get; set; }
    }
}

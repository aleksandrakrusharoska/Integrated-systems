using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Repository.Interface;
using AthletesApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AthletesApplication.Service.Implementation
{
    public class TournamentService : ITournamentService
    {
        private readonly IRepository<Tournament> _tournamentRepository;
        private readonly IRepository<AthleteInTournament> _athleteInTournamentRepository;
        private readonly IParticipationService _participationService;

        public TournamentService(IRepository<Tournament> tournamentRepository, IRepository<AthleteInTournament> athleteInTournamentRepository, IParticipationService participationService)
        {
            _tournamentRepository = tournamentRepository;
            _athleteInTournamentRepository = athleteInTournamentRepository;
            _participationService = participationService;
        }

        public Tournament CreateTournament(string userId)
        {
            // Implement method
            // Hint: Look at Auditory exercises - OrderProducts method in ShoppingCartService 

            // Get all Participations by current user
            var participations = _participationService.GetAllByCurrentUser(userId);

            // Create new Tournament and insert in database         
            var tournament = new Tournament
            {
                DateCreated = DateTime.Now,
                UserId = userId
            };
            _tournamentRepository.Insert(tournament);

            foreach (var participation in participations)
            {
                // Create new AthletesInTournament using Athletes from the Participations and insert in database
                var athletesInTournament = new AthleteInTournament
                {
                    Athlete = participation.Athlete,
                    AthleteId = participation.AthleteId,
                    Tournament = tournament,
                    TournamentId = tournament.Id
                };
                _athleteInTournamentRepository.Insert(athletesInTournament);

                // Delete all Participations
                _participationService.DeleteById(participation.Id);
            }

            // Return Tournament
            return tournament;
        }

        // Bonus task
        public Tournament GetTournamentDetails(Guid id)
        {
            // Implement method
            throw new NotImplementedException();
        }
    }
}

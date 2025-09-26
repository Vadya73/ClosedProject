using Meta;
using VContainer;

namespace Core
{
    public class AiPresentationController
    {
        private readonly AiPresentationView _aiPresentationView;
        private readonly LeaderboardView _leaderboardView;

        [Inject]
        public AiPresentationController(AiPresentationView aiPresentationView, LeaderboardView leaderboardView)
        {
            _aiPresentationView = aiPresentationView;
            _leaderboardView = leaderboardView;
        }
        
        public void ClosePresentationView()
        {
            _aiPresentationView.Hide();
            
            if (_leaderboardView.IsActive)
                _leaderboardView.Hide();
        }
    }
}
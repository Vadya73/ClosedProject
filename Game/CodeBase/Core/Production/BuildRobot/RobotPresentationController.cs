using Meta;
using VContainer;

namespace Core
{
    public class RobotPresentationController
    {
        private readonly RobotPresentationView _robotPresentationView;
        private readonly LeaderboardView _leaderboardView;

        [Inject]
        public RobotPresentationController(RobotPresentationView robotPresentationView, LeaderboardView leaderboardView)
        {
            _robotPresentationView = robotPresentationView;
            _leaderboardView = leaderboardView;
        }
        
        public void ContinueGame()
        {
            _robotPresentationView.Hide();
            
            if (_leaderboardView.IsActive)
                _leaderboardView.Hide();
        }
    }
}
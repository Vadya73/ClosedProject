using System;
using Meta.AllLeaderboards;
using VContainer.Unity;

namespace Meta
{
    public class LeaderboardObserver : IInitializable, IDisposable
    {
        private readonly AllLeaderboardsView _allLeaderboardsView;
        private readonly LeaderboardController _leaderboardController;

        public LeaderboardObserver(AllLeaderboardsView allLeaderboardsView, LeaderboardController leaderboardController)
        {
            _allLeaderboardsView = allLeaderboardsView;
            _leaderboardController = leaderboardController;
        }
        
        public void Initialize()
        {
            _allLeaderboardsView.ExitButton.onClick.AddListener(HideAllLeaderboardsView);
        }

        public void Dispose()
        {
            _allLeaderboardsView.ExitButton.onClick.RemoveListener(HideAllLeaderboardsView);
        }

        private void HideAllLeaderboardsView()
        {
            _leaderboardController.HideAllLeaderboard();
        }
    }
}
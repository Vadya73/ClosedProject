using Meta.AllLeaderboards;
using VContainer;
using VContainer.Unity;

namespace Meta
{
    public class LeaderboardController : IInitializable
    {
        private readonly LeaderboardView _leaderboardView;
        private readonly AllLeaderboardsView _allLeaderboardsView;
        private readonly LeaderboardData _aiLeaderboardData;
        private readonly LeaderboardData _robotsLeaderboardData;
        private readonly LeaderboardEntryUI[] _allEntryUis;
        
        private LeaderboardEntry _aILastEntry;
        private LeaderboardEntry _robotLastEntry;
        
        public LeaderboardData AiLeaderboardData => _aiLeaderboardData;

        [Inject]
        public LeaderboardController(PlayerConfig playerConfig, LeaderboardView leaderboardView, AllLeaderboardsView allLeaderboardsView)
        { 
            _aiLeaderboardData = playerConfig.FirstLevelLeaderboardConfig.AiLeaderboardData; 
            _robotsLeaderboardData = playerConfig.FirstLevelLeaderboardConfig.RobotsLeaderboardData;

            _allEntryUis = leaderboardView.LeaderboardEntries;

            _leaderboardView = leaderboardView;
            _allLeaderboardsView = allLeaderboardsView;
        }

        public void Initialize()
        {
            _leaderboardView.ForcedHide();
            _allLeaderboardsView.ForcedHide();
        }

        public void ShowAllLeaderboards()
        {
            if (_allLeaderboardsView.IsAnimated)
                return;

            if (_allLeaderboardsView.IsActive)
            {
                _allLeaderboardsView.Hide();
                return;
            }
            
            _allLeaderboardsView.Show();
            _allLeaderboardsView.UpdateLeaderBoards(_aiLeaderboardData, _robotsLeaderboardData, _aILastEntry, _robotLastEntry);
        }

        public void ShowLeaderboardView(LeaderboardType leaderboardType)
        {
            UpdateLeaderboardView(leaderboardType);
            _leaderboardView.Show();
        }

        public void AddToLeaderboard(LeaderboardType leaderboardType,LeaderboardEntry leaderboardEntry, bool isPlayerEntry)
        {
            
            switch (leaderboardType)
            {
                case LeaderboardType.Ai:
                    _aILastEntry = leaderboardEntry;
                    _aiLeaderboardData.AddEntry(leaderboardEntry, isPlayerEntry);
                    break;
                case LeaderboardType.Robot:
                    _robotLastEntry = leaderboardEntry;
                    _robotsLeaderboardData.AddEntry(leaderboardEntry, isPlayerEntry);
                    break;
            }
        }

        private void UpdateLeaderboardView(LeaderboardType leaderboardType)
        {
            if (leaderboardType == LeaderboardType.Ai)
            {
                _aiLeaderboardData.SortLeaderboardEntries();
                for (var i = 0; i < _allEntryUis.Length; i++)
                {
                    _allEntryUis[i].UpdateData(_aiLeaderboardData.Entrys[i]);
                }
                
                foreach (var entry in _aiLeaderboardData.Entrys)
                {
                    if (entry.entryName == _aILastEntry.entryName)
                    {
                        _aILastEntry = entry;
                        break;
                    }
                }

                if (_aILastEntry.positionInLeaderboard >= 10)
                {
                    _leaderboardView.PlayerEntry.UpdateData(_aILastEntry);
                }
            }

            if (leaderboardType == LeaderboardType.Robot)
            {
                _robotsLeaderboardData.SortLeaderboardEntries();
                for (var i = 0; i < _allEntryUis.Length; i++)
                {
                    _allEntryUis[i].UpdateData(_robotsLeaderboardData.Entrys[i]);
                }
                
                foreach (var entry in _robotsLeaderboardData.Entrys)
                {
                    if (entry.entryName == _robotLastEntry.entryName)
                    {
                        _robotLastEntry = entry;
                        break;
                    }
                }

                if (_robotLastEntry.positionInLeaderboard >= 10)
                    _allEntryUis[^1].UpdateData(_robotLastEntry);
            }
        }

        public void HideAllLeaderboard()
        {
            if (_allLeaderboardsView.IsAnimated || !_allLeaderboardsView.IsActive)
                return;
            
            _allLeaderboardsView.Hide();
        }
    }
    
    public enum LeaderboardType
    {
        Ai = 0,
        Robot = 1,
    }
}
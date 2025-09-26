using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.AllLeaderboards
{
    public class AllLeaderboardsView : BaseView<IData>
    {
        [SerializeField] private Button _exitButton;
        [Header("AI Leaderboard")]
        [SerializeField] private LeaderboardEntryUI[] _aILeaderboardEntries;
        [SerializeField] private LeaderboardEntryUI _aIPlayerEntry;
        [Header("Robots Leaderboard")]
        [SerializeField] private LeaderboardEntryUI[] _robotsLeaderboardEntries;
        [SerializeField] private LeaderboardEntryUI _robotsPlayerEntry;
        
        public Button ExitButton => _exitButton;

        public void UpdateLeaderBoards(LeaderboardData aiLeaderboardData, LeaderboardData robotsLeaderboardData,
            LeaderboardEntry aILastEntry, LeaderboardEntry robotLastEntry)
        {
            // AI
            aiLeaderboardData.SortLeaderboardEntries();
            
            for (var i = 0; i < _aILeaderboardEntries.Length; i++)
            {
                _aILeaderboardEntries[i].UpdateData(aiLeaderboardData.Entrys[i]);
            }

            foreach (var aiLeaderboardEntry in aiLeaderboardData.Entrys)
            {
                if (aiLeaderboardEntry.isPlayerEntrys)
                {
                    if (aiLeaderboardEntry.positionInLeaderboard <= 9)
                        break;
                    
                    _aIPlayerEntry.UpdateData(aiLeaderboardEntry);
                    break;
                }
            }
            // top or bot func is work
            if (aILastEntry.positionInLeaderboard >= 10)
            {
                _aIPlayerEntry.UpdateData(aILastEntry);
            }
            
            // Robot
            robotsLeaderboardData.SortLeaderboardEntries();
            
            for (var i = 0; i < _robotsLeaderboardEntries.Length; i++)
            {
                _robotsLeaderboardEntries[i].UpdateData(robotsLeaderboardData.Entrys[i]);
            }

            foreach (var robotLeaderboardEntry in robotsLeaderboardData.Entrys)
            {
                if (robotLeaderboardEntry.isPlayerEntrys)
                {
                    if (robotLeaderboardEntry.positionInLeaderboard <= 9)
                        break;

                    _robotsPlayerEntry.UpdateData(robotLeaderboardEntry);
                    break;
                }
            }
            // top or bot func is work
            if (robotLastEntry.positionInLeaderboard >= 10)
            {
                _robotsPlayerEntry.UpdateData(robotLastEntry);
            }
        }
        
    }
}

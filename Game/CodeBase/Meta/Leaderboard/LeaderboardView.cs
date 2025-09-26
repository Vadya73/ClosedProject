using UI;
using UnityEngine;

namespace Meta
{
    public class LeaderboardView : BaseView<IData>
    {
        [SerializeField] private LeaderboardEntryUI[] _leaderboardEntries;
        [SerializeField] private LeaderboardEntryUI _playerEntry;
        public LeaderboardEntryUI[] LeaderboardEntries => _leaderboardEntries;
        public LeaderboardEntryUI PlayerEntry => _playerEntry;
    }
}

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Meta
{
    [CreateAssetMenu(menuName = "Leaderboard/Config", fileName = "LeaderboardConfig", order = 0)]
    public class LeaderboardConfig : ScriptableObject
    {
        [SerializeField] private LeaderboardData _aiLeaderboardData;
        [SerializeField] private LeaderboardData _robotsLeaderboardData;
        
        public LeaderboardData AiLeaderboardData => _aiLeaderboardData;
        public LeaderboardData RobotsLeaderboardData => _robotsLeaderboardData;
    }
    
    [System.Serializable]
    public class LeaderboardData
    {
        [SerializeField] private List<LeaderboardEntry> _entrys;
        
        public event Action<LeaderboardData> OnEntryAdd;
        
        public List<LeaderboardEntry> Entrys => _entrys;

        [Button("Sort Leaderboard Data")]
        public void SortLeaderboardEntries()
        {
            for (int i = 0; i < _entrys.Count; i++)
            {
                for (int j = i + 1; j < _entrys.Count; j++)
                {
                    if (_entrys[i].score < _entrys[j].score)
                    {
                        (_entrys[i], _entrys[j]) = (_entrys[j], _entrys[i]);
                    }                   
                }
                var ent = _entrys[i];
                ent.positionInLeaderboard = i + 1;
                _entrys[i] = ent;
            }
        }

        [Button("Set Random Score")]
        private void SetRandomScore()
        {
            for (var i = 0; i < _entrys.Count; i++)
            {
                var range = Random.Range(1, 100);

                var leaderboardEntry = _entrys[i];
                leaderboardEntry.score = range;
                _entrys[i] = leaderboardEntry;
            }
        }

        public void AddEntry( LeaderboardEntry entry, bool isPlayerEntry = false)
        {
            entry.isPlayerEntrys = isPlayerEntry;
            _entrys.Add(entry);
                
            SortLeaderboardEntries();
            OnEntryAdd?.Invoke(this);
        }
    }
    
    [System.Serializable]
    public struct LeaderboardEntry
    {
        public int positionInLeaderboard;
        public string entryName;
        public int score;
        public Sprite avatar;
        public bool isPlayerEntrys;
    }
}
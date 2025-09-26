using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
    public class LeaderboardEntryUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image _avatarImage;
        [SerializeField] private Image _backgroundImage;

        public void UpdateData(LeaderboardEntry leaderboardEntry)
        {
            _positionText.text = leaderboardEntry.positionInLeaderboard.ToString();
            _nameText.text = leaderboardEntry.entryName;
            _scoreText.text = leaderboardEntry.score.ToString();
            _avatarImage.sprite = leaderboardEntry.avatar;

            if (leaderboardEntry.isPlayerEntrys)
            {
                _backgroundImage.color = Color.yellow;
            }
        }
    }
}
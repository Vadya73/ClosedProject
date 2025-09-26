using Core;
using UnityEngine;

namespace OtherSO
{
    [CreateAssetMenu(menuName = "Level Config", fileName = "LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject, ISkillPoint
    {
        [SerializeField] private string _levelName;
        [SerializeField] private int _levelIndex;
    }
}
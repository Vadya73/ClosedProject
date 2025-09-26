using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Progress/Progress Config", fileName = "ProgressConfig", order = 0)]
    public class ProgressBarConfig : ScriptableObject
    {
        [SerializeField, PreviewField(Height = 150)] private Sprite _robotResearchSprite;
        [SerializeField, PreviewField(Height = 150)] private Sprite _aiResearchSprite;
        [SerializeField, PreviewField(Height = 150)] private Sprite _skillTreeSprite;
        
        public Sprite RobotResearchSprite => _robotResearchSprite;
        public Sprite AiResearchSprite => _aiResearchSprite;
        public Sprite SkillTreeSprite =>_skillTreeSprite;
    }
}
using Services;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UI;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Research/SkillData")]
    public class SkillData : SerializedScriptableObject, IData
    {
        [OdinSerialize, ShowInInspector] private ISkillPoint _skillPointData;
        [SerializeField] private SkillType _skillType;
        [SerializeField] private ProductionAddSkillType _addSkillType;
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        [SerializeField, HorizontalGroup] private WalletType _walletType;
        [SerializeField, HorizontalGroup] private int _cost;
        [SerializeField] private int _requiredResearchBubbles;
        [SerializeField] private Sprite _skillSprite;

        public ISkillPoint SkillPointData => _skillPointData;
        public SkillType SkillType => _skillType;
        public ProductionAddSkillType AddSkillType => _addSkillType;
        public string Name => _name;
        public string Description => _description;
        public WalletType WalletType => _walletType;
        public int Cost => _cost;
        public int RequiredResearchBubbles => _requiredResearchBubbles;
        public Sprite SkillSprite => _skillSprite;
    }

    public enum SkillType
    {
        Material = 0,
        Chip = 1,
        Battery = 2,
        Engine = 3,
        RobotAddon = 4,
        DataSet = 5,
        NetworkArchitecture = 6,
        ModelSize = 7,
        TrainingMethod = 8,
        AIAddon = 9,
        Location = 10,
        DataCenterLevel = 11,
        RobotSubtype = 12,
    }
}
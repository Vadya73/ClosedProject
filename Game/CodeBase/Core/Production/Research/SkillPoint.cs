using System;
using Infrastructure;
using Meta;
using OtherSO;
using Sirenix.OdinInspector;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace Core
{
    public class SkillPoint : BaseView<SkillData>, IPointerClickHandler
    {
        [SerializeField] private SkillData _skillData;
        [SerializeField] private GameObject _lockedObject;
        [SerializeField] private Image _backGroundImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private RectTransform _lineToNext;
        [SerializeField] private SkillPoint _nextSkillPoint;
        [SerializeField] private bool _isFirstSkillPoint;
        [SerializeField,ReadOnly] private bool _isOpen;
        [SerializeField,ReadOnly] private bool _hasBuy;
        
        private ResearchController _researchController;
        private PlayerConfig _playerConfig;
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;
        public SkillData SkillData => _skillData;

        [Inject]
        private void Construct(ResearchController researchController, PlayerConfig playerConfig, AudioController audioController, 
            SoundConfigs soundConfigs)
        {
            _researchController = researchController;
            _playerConfig = playerConfig;
            _audioController = audioController;
            _soundConfigs = soundConfigs;
        }

        protected override void Awake()
        {
            base.Awake();
            
            if (_isFirstSkillPoint)
                _isOpen = true;

            _lockedObject.SetActive(_isOpen == false);
        }

        private void Start()
        {
            if (_skillData == null)
                return;
            
            SetupLine();
            _nameText.text = _skillData.Name; 
            switch (_skillData.SkillType)
            {
                case SkillType.Material:
                    foreach (var component in _playerConfig.BuildRobotData.AllHaveMaterials)
                    {
                        if (component == (RobotComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.Chip:
                    foreach (var component in _playerConfig.BuildRobotData.AllHaveChip)
                    {
                        if (component == (RobotComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.Battery:
                    foreach (var component in _playerConfig.BuildRobotData.AllHaveBattery)
                    {
                        if (component == (RobotComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.Engine:
                    foreach (var component in _playerConfig.BuildRobotData.AllHaveEngine)
                    {
                        if (component == (RobotComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.RobotAddon:
                    foreach (var component in _playerConfig.BuildRobotData.Addons)
                    {
                        if (component == (RobotAddonConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.DataSet:
                    foreach (var component in _playerConfig.TrainingAIData.AllHaveDatasets)
                    {
                        if (component == (AiComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.NetworkArchitecture:
                    foreach (var component in _playerConfig.TrainingAIData.AllHaveNetworkArchitecture)
                    {
                        if (component == (AiComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.ModelSize:
                    foreach (var component in _playerConfig.TrainingAIData.AllHaveModelSize)
                    {
                        if (component == (AiComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.TrainingMethod:
                    foreach (var component in _playerConfig.TrainingAIData.AllHaveTrainingMethods)
                    {
                        if (component == (AiComponentConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.AIAddon:
                    foreach (var component in _playerConfig.TrainingAIData.Addons)
                    {
                        if (component == (AiAddonConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.Location:
                    foreach (var component in _playerConfig.LocationData.AllHaveLocations)
                    {
                        if (component == (LevelConfig)_skillData.SkillPointData)
                        { 
                            OpenSkill();
                            _hasBuy = true;
                            _nextSkillPoint?.OpenSkill();
                        }
                    }
                    break;
                case SkillType.DataCenterLevel:
                    switch (SceneManager.GetActiveScene().buildIndex)
                    {
                        case 3: // 3 lvl
                            foreach (var component in _playerConfig.DataCenterData.AllHaveDataCenterLevelsThirdLevel)
                            {
                                if (component == (DataCenterConfig)_skillData.SkillPointData)
                                { 
                                    OpenSkill();
                                    _hasBuy = true;
                                    _nextSkillPoint?.OpenSkill();
                                }
                            }
                            break;
                        case 4:
                            foreach (var component in _playerConfig.DataCenterData.AllHaveDataCenterLevelsFourLevel)
                            {
                                if (component == (DataCenterConfig)_skillData.SkillPointData)
                                { 
                                    OpenSkill();
                                    _hasBuy = true;
                                    _nextSkillPoint?.OpenSkill();
                                }
                            }
                            break;
                    }
                    break;
                case SkillType.RobotSubtype:
                    var subType = (RobotSubtypeResearch)_skillData.SkillPointData;
                    
                    if (_playerConfig.BuildRobotData.PlayerHaveRobotSubtypes.HasFlag(subType.RobotSubtype))
                    {
                        OpenSkill();
                        _hasBuy = true;
                        _nextSkillPoint?.OpenSkill();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetupLine()
        {
            if (_nextSkillPoint == null || _lineToNext == null)
                return;

            Vector3 startPos = transform.position;
            Vector3 endPos = _nextSkillPoint.transform.position;

            Vector3 dir = endPos - startPos;
            float distance = dir.magnitude;

            _lineToNext.gameObject.SetActive(true);

            _lineToNext.position = (startPos + endPos) / 2f;

            _lineToNext.sizeDelta = new Vector2(distance, _lineToNext.sizeDelta.y);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _lineToNext.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isOpen)
                return;
            
            _audioController.PlayEffects(_soundConfigs.DefaultClickSound);
            
            if (_researchController.SkillDescriptionView.IsAnimated)
            {
                if (!_researchController.SkillDescriptionView.IsActive)
                {
                    _researchController.ShowSkillDescriptionView(this);
                    _researchController.HighlightSkillPoint(transform.position);
                }
                return;
            }
            
            if (_isOpen && !_hasBuy)
                _researchController.ShowResearchButton();
            else
                _researchController.HideResearchButton();
            
            _researchController.ShowSkillDescriptionView(this);
            _researchController.HighlightSkillPoint(transform.position);
        }

        public void OpenNextSkillPoint()
        {
            _hasBuy = true;
            if (_nextSkillPoint == null)
                return;
            
            _nextSkillPoint.OpenSkill();
        }

        private void OpenSkill()
        {
            _isOpen = true;
            _lockedObject.SetActive(false);
        }
    }
}
using DG.Tweening;
using Infrastructure;
using Meta;
using Services;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ResearchController : IInitializable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly Wallet _wallet;
        private readonly SkillDescriptionView _skillDescriptionView;
        private readonly ResearchView _researchSkillsView;
        private readonly ProductionProgressController _progressController;
        private readonly PlayerConfig _playerConfig;
        private readonly NotificationController _notificationController;
        private readonly LoadScreen _loadScreen;

        private ProductionController _productionController;
        private Vector3 _defaultDescriptionPosition;
        private SkillData _currentSkillData;
        private SkillPoint _currentSkillPoint;
        private SceneDataCenter _sceneDataCenter;
        
        private SkillData _researchSkillData;
        private SkillPoint _researchSkillPoint;

        public SkillDescriptionView SkillDescriptionView => _skillDescriptionView;
        public ResearchView ResearchView => _researchSkillsView;
        
        [Inject]
        public ResearchController(IObjectResolver objectResolver,Wallet wallet, SkillDescriptionView skillDescriptionView, ResearchView researchSkillsView,
            ProductionProgressController progressController, PlayerConfig playerConfig, NotificationController notificationController,
            LoadScreen loadScreen)
        {
            _objectResolver = objectResolver;
            _wallet = wallet;
            _skillDescriptionView = skillDescriptionView;
            _researchSkillsView = researchSkillsView;
            _progressController = progressController;
            _playerConfig = playerConfig;
            _notificationController = notificationController;
            _loadScreen = loadScreen;
        }

        public void Initialize()
        {
            _productionController = _objectResolver.Resolve<ProductionController>();
        }

        public void SetDataCenter(SceneDataCenter sceneDataCenter)
        {
            _sceneDataCenter = sceneDataCenter;
        }

        public void ShowSkillDescriptionView(SkillPoint skillPoint)
        {
            if (skillPoint.SkillData == _currentSkillData)
            {
                if (!_skillDescriptionView.IsActive)
                {
                    _currentSkillPoint = skillPoint;
                    _currentSkillData = skillPoint.SkillData;
                    _skillDescriptionView.Show();
                    _skillDescriptionView.UpdateView(skillPoint.SkillData);
                }

                return;
            }
            
            if (_skillDescriptionView.IsActive)
            {
                DOTween.Sequence()
                    .AppendCallback(_skillDescriptionView.Hide)
                    .AppendInterval(.6f)
                    .AppendCallback(() =>
                    {
                        _currentSkillPoint = skillPoint;
                        _currentSkillData = skillPoint.SkillData;
                        _skillDescriptionView.UpdateView(skillPoint.SkillData);
                    })
                    .AppendCallback(_skillDescriptionView.Show);
                
                return;
            }

            _currentSkillPoint = skillPoint;
            _currentSkillData = skillPoint.SkillData;
            _skillDescriptionView.Show();
            _skillDescriptionView.UpdateView(skillPoint.SkillData);
        }

        public void HideSkillDescriptionView()
        {
            _skillDescriptionView.Hide();
        }

        public void HideForcedView() => _researchSkillsView.ForcedHide();

        public void ShowForcedView()
        {
            _researchSkillsView.ForcedShow();
            _skillDescriptionView.ForcedHide();
        }

        public void HighlightSkillPoint(Vector3 transformPosition)
        {
            _researchSkillsView.HighlightObject.gameObject.SetActive(true);
            _researchSkillsView.HighlightObject.position = transformPosition;
        }

        public void OnViewEnabled()
        {
            _currentSkillData = null;
            _skillDescriptionView.ForcedHide();
        }

        public void TryStartResearch()
        {
            if (CheckValidate() == false)
                return;
            
            if (_wallet.TrySpend(_currentSkillData.WalletType, _currentSkillData.Cost))
            {
                _progressController.StartProgressWithBubbles(ProductionType.Research, _currentSkillData);
                _productionController.HideView();
                _productionController.LockResearch(ProductionType.Research);
                
                _researchSkillData = _currentSkillData;
                _researchSkillPoint = _currentSkillPoint;
            }
            else
                _notificationController.ShowErrorMessage("No Money");
        }

        public void EndResearch()
        {
            _productionController.UnlockResearch();
            _researchSkillPoint.OpenNextSkillPoint();
            
            switch (_researchSkillData.AddSkillType)
            {
                case ProductionAddSkillType.RobotComponent:
                    _playerConfig.BuildRobotData.AddRobotComponent(_researchSkillData.SkillPointData);
                    break;
                case ProductionAddSkillType.RobotAddon:
                    _playerConfig.BuildRobotData.AddRobotAddon(_researchSkillData.SkillPointData);
                    break;
                case ProductionAddSkillType.AiComponent:
                    _playerConfig.TrainingAIData.AddAiComponent(_researchSkillData.SkillPointData);
                    break;
                case ProductionAddSkillType.AiAddon:
                    _playerConfig.TrainingAIData.AddAiAddon(_researchSkillData.SkillPointData);
                    break;
                case ProductionAddSkillType.Location:
                    _playerConfig.LocationData.AddLocation(_researchSkillData.SkillPointData);
                    switch (_researchSkillData.Name)
                    {
                        case "Office":
                            _loadScreen.LoadScene(2);
                            break;
                        case "Laboratory":
                            _loadScreen.LoadScene(3);
                            break;
                        case "Fabric":
                            _loadScreen.LoadScene(4);
                            break;
                    }
                    break;
                case ProductionAddSkillType.Skill:
                    _playerConfig.SkillsData.AddSkill(_researchSkillData.SkillPointData);
                    break;
                case ProductionAddSkillType.DataCenter:
                    _playerConfig.DataCenterData.AddLevel(_researchSkillData.SkillPointData);
                    _sceneDataCenter.AddDataCenter();
                    break;
                case ProductionAddSkillType.RobotSubtype:
                    _playerConfig.AddRobotSubtype(_researchSkillData.SkillPointData);
                    _productionController.UpdateBuildRobotSubtypes();
                    break;
            }
        }

        public void HideResearchButton()
        {
            _skillDescriptionView.ResearchButton.gameObject.SetActive(false);
        }

        public void ShowResearchButton()
        {
            _skillDescriptionView.ResearchButton.gameObject.SetActive(true);
        }

        private bool CheckValidate()
        {
            if (_productionController.IsResearching)
            {
                _notificationController.ShowErrorMessage("You are already researching");
                return false;
            }

            return true;
        }
    }

    public enum ProductionAddSkillType
    {
        RobotComponent = 0,
        RobotAddon = 1,
        AiComponent = 2,
        AiAddon = 3,
        Location = 4,
        Skill = 5,
        DataCenter = 6,
        RobotSubtype = 7,
    }
}
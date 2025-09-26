using System;
using System.Linq;
using Infrastructure;
using Meta;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public sealed class BuildRobotController : IInitializable
    {
        private readonly BuildRobotView _buildRobotView;
        private readonly BuildRobotComponent[] _components;
        private readonly BuildRobotAddon[] _addons;
        private readonly IObjectResolver _objectResolver;
        private readonly ChooseRobotTypeView[] _chooseRobotTypeViews;
        private readonly PlayerConfig _playerConfig;
        private readonly ProductionProgressController _progressController;
        private readonly RobotPresentationView _robotPresentationView;
        private readonly ProductHolder _productHolder;
        private readonly NotificationController _notificationController;
        private readonly ProductSaleController _productSaleController;
        private readonly LeaderboardController _leaderboardController;
        private readonly AudioController _audioController;

        private ProductionController _productionController;

        private RobotTypes _robotType;
        private RobotSubtypes _robotSubtype;
        private string _robotName;
        private GameObject _highlightObject;
        private TouchScreenKeyboard _touchscreenKeyboard; 
        private Robot _currentRobotConfig;
        private int _researchDays;
        
        public event Action<RobotSubtypes> OnRobotCreateFinished;
        public BuildRobotView BuildRobotView => _buildRobotView;
        
        [Inject]
        public BuildRobotController(IObjectResolver objectResolver,BuildRobotView buildRobotView, PlayerConfig playerConfig, 
            ProductionProgressController progressController, RobotPresentationView robotPresentationView, ProductHolder productHolder,
            NotificationController notificationController, ProductSaleController productSaleController, LeaderboardController leaderboardController,
            AudioController audioController)
        {
            _objectResolver = objectResolver;
            _buildRobotView = buildRobotView;
            _playerConfig = playerConfig;
            _progressController = progressController;
            _robotPresentationView = robotPresentationView;
            _productHolder = productHolder;
            _notificationController = notificationController;
            _productSaleController = productSaleController;
            _leaderboardController = leaderboardController;
            _audioController = audioController;

            _components = buildRobotView.Components;
            _addons = buildRobotView.Addons;
            
            _components = buildRobotView.Components;
            _addons = buildRobotView.Addons;
            _chooseRobotTypeViews = buildRobotView.ChooseRobotTypeViews;
            _highlightObject = buildRobotView.HighlightObject;
        }

        public void Initialize()
        {
            _productionController = _objectResolver.Resolve<ProductionController>();
        }

        public void StartBuildRobot()
        {
            if (CheckValidate() == false)
                return;
            
            _buildRobotView.StartBuildButton.interactable = false;
            _researchDays = 0;

            foreach (var component in _components)
            {
                if (component.CurrentComponentConfig != null)
                    _researchDays += component.CurrentComponentConfig.AdditionalDaysToCreateRobot;
            }
            
            foreach (var addon in _addons)
            {
                if (addon.IsActive)
                    _researchDays += addon.RobotAddonConfig.AddingDaysToCreate;
            }
            
            if (_researchDays <= 0)
                return;

            _productionController.HideView();
            _productionController.LockResearch(ProductionType.BuildRobot);
            _currentRobotConfig = CreateRobot();
            ResetChooses();
            _progressController.StartProgress(productionType: ProductionType.BuildRobot, _researchDays, 
                _currentRobotConfig.RequiredBubblesScore.TechnologyRequiredBubbles, 
                _currentRobotConfig.RequiredBubblesScore.DesignRequiredBubbles);
        }

        private void ResetChooses()
        {
            foreach (var addon in _addons)
                addon.Toggle.isOn = false;

            foreach (var component in _components)
            {
                component.Reset();
                
                if (component.ComponentType == RobotComponentType.AI)
                    component.ResetAi();
            }
            
            _robotName = string.Empty;
            _robotType = RobotTypes.None;
            _robotSubtype = RobotSubtypes.None;

            foreach (var chooseRobotType in _chooseRobotTypeViews)
                chooseRobotType.ResetDropdown();
            
            _highlightObject.SetActive(false);
            _buildRobotView.NameInputField.text = _robotName;
        }

        private bool CheckValidate()
        {
            if (_productionController.IsResearching)
            {
                _notificationController.ShowErrorMessage("You are already researching");
                return false;
            }
            
            if (_robotType == RobotTypes.None || _robotSubtype == RobotSubtypes.None)
            {
                _notificationController.ShowErrorMessage("Choose Type or SubType");
                return false;
            }

            if (string.IsNullOrEmpty(_robotName))
            {
                _notificationController.ShowErrorMessage("Set robot Name");
                return false;
            }

            if (_components.Any(component => component.ComponentType == RobotComponentType.Material && component.CurrentComponentConfig == null))
            {
                _notificationController.ShowErrorMessage("Choose Material");
                return false;
            }

            return true;
        }

        public void SetRobotType(RobotTypes robotType, RobotSubtypes subtype)
        {
            ResetOtherDropdowns(robotType);

            _robotType = robotType;
            _robotSubtype = subtype;
        }
        
        public void HighLightRobotType(Vector3 position)
        {
            _highlightObject.SetActive(true);
            _highlightObject.transform.position = position;
        }

        public void SetRandomName()
        {
            var randomName = _playerConfig.RobotNamesConfig.GetRandomName();
            var randomLastName = _playerConfig.RobotNamesConfig.GetRandomLastName();
            _robotName = $"{randomName} {randomLastName}";
            _buildRobotView.NameInputField.text = _robotName;
        }

        public void UpdateRobotName(string name) => 
            _robotName = name;

        public void ShowForcedView() => _buildRobotView.ForcedShow();

        public void HideForcedView()
        {
            _buildRobotView.ForcedHide();
        }

        public void EndBuildRobot()
        {
            OnRobotCreateFinished?.Invoke(_currentRobotConfig.RobotSubType);
            
            _buildRobotView.StartBuildButton.interactable = true;
            
            _productHolder.AddRobot(_currentRobotConfig);
            _robotPresentationView.Show();
            _robotPresentationView.UpdateView(_currentRobotConfig);

            if (_currentRobotConfig.RobotSubType == RobotSubtypes.Humanoid)
            {
                _leaderboardController.AddToLeaderboard(LeaderboardType.Robot, new LeaderboardEntry()
                {
                    entryName = _currentRobotConfig.Name,
                    score = _currentRobotConfig.Score,
                    avatar = _currentRobotConfig.Image,
                }, true);
                _leaderboardController.ShowLeaderboardView(LeaderboardType.Robot);
            }
            
            _productSaleController.CreateSaleProduct(_currentRobotConfig);
            
            _productionController.UnlockResearch();
        }

        private Robot CreateRobot()
        {
            Robot robot = new();

            robot.SetTypeAndSubType(_robotType, _robotSubtype);
            robot.CreateProductSaleData();
            robot.SetName(_robotName);
            robot.SetDescription("Робот — это бездушный механизм, преобразующий электричество в движение, алгоритмы — в действия, а данные — в иллюзию осознанного выбора.");
            
            foreach (var component in _components)
            {
                if (component == null) continue;
    
                if (component.CurrentAI != null && 
                    !string.IsNullOrWhiteSpace(component.CurrentAI.Name))
                {
                    robot.SetAI(component.CurrentAI);
                }

                if (component.CurrentComponentConfig != null)
                {
                    robot.SetComponent(component.CurrentComponentConfig);
                }
            }

            foreach (var addon in _addons)
            {
                if (addon.IsActive)
                    robot.SetAddons(addon.RobotAddonConfig);
            }
            
            robot.CalculateParams();
            robot.CalculateScore();
            robot.CalculateBubblesScore();
            
            return robot;
        }
        
        private void ResetOtherDropdowns(RobotTypes robotType)
        {
            foreach (var chooseRobotType in _chooseRobotTypeViews)
            {
                if (chooseRobotType.RobotType != robotType)
                    chooseRobotType.ResetDropdown();
            }
        }

        public void ShowKeyboard(string text)
        {
            bool keyboardIsActive = false;
            try
            {
                keyboardIsActive = _touchscreenKeyboard != null && _touchscreenKeyboard.active;
            }
            catch (NullReferenceException)
            {
                keyboardIsActive = false;
                _touchscreenKeyboard = null;
            }

            if (keyboardIsActive) return;

            _touchscreenKeyboard = TouchScreenKeyboard.Open(
                text, 
                TouchScreenKeyboardType.Default,
                autocorrection: true,
                multiline: false,
                secure: false
            );
        }

        public void HideKeyboard()
        {
            if (_touchscreenKeyboard != null)
            {
                try
                {
                    _touchscreenKeyboard.active = false;
                }
                catch (NullReferenceException)
                {
                }
                finally
                {
                    _touchscreenKeyboard = null;
                }
            }
        }

        public void SetBubbleCount(int firstBubble, int secondBubble)
        {
            _currentRobotConfig.SetBubbleCurrentCount(firstBubble, secondBubble);
        }

        public void UpdateSubtypes()
        {
            foreach (var chooseRobotTypeView in _chooseRobotTypeViews)
            {
                chooseRobotTypeView.UpdateAvailableSubtypes();
                chooseRobotTypeView.UpdateDropDown();
            }
        }
    }
}
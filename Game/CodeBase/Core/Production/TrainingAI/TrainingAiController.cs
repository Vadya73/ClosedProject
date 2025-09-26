using System.Linq;
using Meta;
using UI;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class TrainingAiController : IInitializable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly TrainingAiView _trainingAiView;
        private readonly ProductionProgressController _progressController;
        private readonly PlayerConfig _playerConfig;
        private readonly TrainingAiComponent[] _components;
        private readonly TrainingAiAddon[] _addons;
        private readonly NotificationController _notificationController;
        private readonly AiPresentationView _aiPresentationView;
        private readonly LeaderboardController _leaderboardController;

        private string _aiName;
        private int _researchDays;
        private AI _currentAiConfig;
        private ProductionController _productionController;

        public TrainingAiView TrainingAiView => _trainingAiView;
        
        [Inject]
        public TrainingAiController(IObjectResolver objectResolver,TrainingAiView trainingAiView, ProductionProgressController progressController,
            PlayerConfig playerConfig, NotificationController notificationController, AiPresentationView aiPresentationView, 
            LeaderboardController leaderboardController)
        {
            _objectResolver = objectResolver;
            _trainingAiView = trainingAiView;
            _progressController = progressController;
            _playerConfig = playerConfig;
            _notificationController = notificationController;
            _aiPresentationView = aiPresentationView;
            _leaderboardController = leaderboardController;

            _components = _trainingAiView.Components;
            _addons = _trainingAiView.Addons;
        }

        public void Initialize()
        {
            _productionController = _objectResolver.Resolve<ProductionController>();
        }

        public void StartAiResearch()
        {
            if (CheckValidate() == false)
                return;
            
            _trainingAiView.StartResearchButton.interactable = false;
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
            
            _currentAiConfig = CreateAI();
            _productionController.LockResearch(ProductionType.TrainingAI);
            _progressController.StartProgress(ProductionType.TrainingAI, _researchDays, _currentAiConfig.RequiredToCreateBubbleScore);
            _productionController.HideView();
        }

        private bool CheckValidate()
        {
            if (_productionController.IsResearching)
            {
                _notificationController.ShowErrorMessage("You are already researching");
                return false;
            }
            
            if (string.IsNullOrEmpty(_aiName))
            {
                _notificationController.ShowErrorMessage("Set Ai Name");
                return false;
            }

            if (_components.Any(component => component.CurrentComponentConfig == null))
            {
                _notificationController.ShowErrorMessage("Set All Components");
                return false;
            }

            return true;
        }

        public void HideForcedView()
        {
            _trainingAiView.ForcedHide();
        }

        public void ShowForcedView()
        {
            _trainingAiView.ForcedShow();
        }

        public void SetRandomName()
        {
            var randomName = _playerConfig.AiDataConfig.GetRandomName();
            var randomLastName = _playerConfig.AiDataConfig.GetRandomLastName();
            _aiName = $"{randomName} {randomLastName}";
            _trainingAiView.NameInputField.text = _aiName;
        }

        public void UpdateRobotName(string name) => 
            _aiName = name;

        public void OnProgressResearchEnd()
        {
            _trainingAiView.StartResearchButton.interactable = true;
            
            _playerConfig.AiDataConfig.AiData.AddAI(_currentAiConfig);
            _aiPresentationView.Show();
            _aiPresentationView.UpdateView(_currentAiConfig);
            _productionController.UnlockResearch();

            _leaderboardController.AddToLeaderboard(LeaderboardType.Ai, new LeaderboardEntry()
            {
                entryName = _currentAiConfig.Name,
                score = _currentAiConfig.Score,
                avatar = _currentAiConfig.Image,
            }, true);
            _leaderboardController.ShowLeaderboardView(LeaderboardType.Ai);
        }

        private AI CreateAI()
        {
            AI ai = new();

            ai.SetName(_aiName);
            ai.SetDescription("Нейросеть — это цифровая тень разума, где слои чисел, переплетаясь в тишине матричных вычислений, учатся находить смысл в хаосе данных, подражая мерцающим искрам биологического сознания, но без его тайны.");
            ai.SetImage(_playerConfig.AiDataConfig.GetRandomSprite());
            
            foreach (var component in _components)
            {
                if (component.CurrentComponentConfig != null)
                    ai.SetComponent(component.CurrentComponentConfig);
            }

            foreach (var addon in _addons)
            {
                if (addon.IsActive)
                    ai.SetAddons(addon.RobotAddonConfig);
            }
            
            ai.CalculateParams();
            ai.CalculateScore();
            ai.CalculateBubbleScore();
            
            return ai;
        }

        public void SetBubbleCount(int firstBubble)
        {
            _currentAiConfig.SetBubbleCurrentCount(firstBubble);
        }
    }
}
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ProductionController : IInitializable
    {
        private readonly ProductionView _productionView;
        private readonly BuildRobotController _buildRobotController;
        private readonly TrainingAiController _trainingAiController;
        private readonly ResearchController _researchController;
        private readonly RobotPresentationView _robotPresentationView;
        private readonly AiPresentationView _aiPresentationView;

        private bool _isResearching;
        private ProductionType _currentProductionType;
        
        public bool IsResearching => _isResearching;
        public ProductionType CurrentProductionType => _currentProductionType;

        [Inject]
        public ProductionController(ProductionView productionView, BuildRobotController buildRobotController, 
            TrainingAiController trainingAiController, ResearchController researchController, RobotPresentationView robotPresentationView, 
            AiPresentationView aiPresentationView)
        {
            _productionView = productionView;
            _buildRobotController = buildRobotController;
            _trainingAiController = trainingAiController;
            _researchController = researchController;
            _robotPresentationView = robotPresentationView;
            _aiPresentationView = aiPresentationView;
        }

        public void Initialize()
        {
            _productionView.ForcedHide();
            _robotPresentationView.ForcedHide();
            _aiPresentationView.ForcedHide();
            _isResearching = false;
        }

        public void ShowMainAndBRView()
        {
            if (_productionView.IsActive)
                _productionView.Hide();
            
            _trainingAiController.HideForcedView();
            _researchController.HideForcedView();
            _productionView.TrainingAIButtonImage.color = _productionView.DefaultButtonColor;
            _productionView.ResearchButtonImage.color = _productionView.DefaultButtonColor;
            
            _productionView.Show();
            _buildRobotController.ShowForcedView();
            _productionView.BuildRobotButtonImage.color = _productionView.SelectedButtonColor;
        }
        
        public void ShowMainAndResearchView()
        {
            if (_productionView.IsActive)
                _productionView.Hide();
            
            _trainingAiController.HideForcedView();
            _buildRobotController.HideForcedView();
            _productionView.BuildRobotButtonImage.color = _productionView.DefaultButtonColor;
            _productionView.TrainingAIButtonImage.color = _productionView.DefaultButtonColor;
            
            _productionView.Show();
            _researchController.ShowForcedView();
            _productionView.ResearchButtonImage.color = _productionView.SelectedButtonColor;
        }

        public void HideView() => _productionView.Hide();

        public void ShowBuildRobotView()
        {
            if (_buildRobotController.BuildRobotView.IsActive)
                return;
            
            _trainingAiController.HideForcedView();
            _researchController.HideForcedView();
            _buildRobotController.ShowForcedView();
        }

        public void ShowTrainingAIView()
        {
            if (_trainingAiController.TrainingAiView.IsActive)
                return;
            
            _trainingAiController.ShowForcedView();
            _researchController.HideForcedView();
            _buildRobotController.HideForcedView();
        }

        public void ShowResearchView()
        {
            if (_researchController.ResearchView.IsActive)
                return;
            
            _trainingAiController.HideForcedView();
            _researchController.ShowForcedView();
            _buildRobotController.HideForcedView();
        }

        public void LockResearch(ProductionType productionType)
        {
            _isResearching = true;
            _currentProductionType = productionType;
        }

        public void UnlockResearch()
        {
            _isResearching = false;
        }

        public void UpdateBuildRobotSubtypes()
        {
            _buildRobotController.UpdateSubtypes();
        }
    }
}
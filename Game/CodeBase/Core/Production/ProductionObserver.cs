using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ProductionObserver : IInitializable, IDisposable
    {
        private readonly ProductionView _productionView;
        private readonly ProductionController _productionController;
        
        [Inject]
        public ProductionObserver(ProductionView productionView, ProductionController productionController)
        {
            _productionView = productionView;
            _productionController = productionController;
        }
        
        public void Initialize()
        {
            _productionView.BuildRobotButton.onClick.AddListener(ShowBuildRobot);
            _productionView.TrainingAIButton.onClick.AddListener(ShowTrainingAI);
            _productionView.ResearchButton.onClick.AddListener(ShowResearch);
            _productionView.ExitButton.onClick.AddListener(HideProductionView);
        }

        public void Dispose()
        {
            _productionView.BuildRobotButton.onClick.RemoveListener(ShowBuildRobot);
            _productionView.TrainingAIButton.onClick.RemoveListener(ShowTrainingAI);
            _productionView.ResearchButton.onClick.RemoveListener(ShowResearch);
            _productionView.ExitButton.onClick.RemoveListener(HideProductionView);
        }

        private void ShowBuildRobot()
        {
            _productionController.ShowBuildRobotView();
            _productionView.BuildRobotButtonImage.color = _productionView.SelectedButtonColor;
            
            _productionView.TrainingAIButtonImage.color = _productionView.DefaultButtonColor;
            _productionView.ResearchButtonImage.color = _productionView.DefaultButtonColor;
        }

        private void ShowTrainingAI()
        {
            _productionController.ShowTrainingAIView();
            _productionView.TrainingAIButtonImage.color = _productionView.SelectedButtonColor;
            
            _productionView.BuildRobotButtonImage.color = _productionView.DefaultButtonColor;
            _productionView.ResearchButtonImage.color = _productionView.DefaultButtonColor;
        }

        private void ShowResearch()
        {
            _productionController.ShowResearchView();
            _productionView.ResearchButtonImage.color = _productionView.SelectedButtonColor;
            
            _productionView.BuildRobotButtonImage.color = _productionView.DefaultButtonColor;
            _productionView.TrainingAIButtonImage.color = _productionView.DefaultButtonColor;
        }

        private void HideProductionView() => _productionController.HideView();
    }
}
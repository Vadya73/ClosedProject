using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class TrainingAiObserver : IInitializable, IDisposable
    {
        private readonly TrainingAiView _trainingAiView;
        private readonly TrainingAiController _trainingAiController;
        private readonly ProductionProgressController _productionProgressController;

        [Inject]
        public TrainingAiObserver(TrainingAiView trainingAiView, TrainingAiController trainingAiController, 
            ProductionProgressController productionProgressController)
        {
            _trainingAiView = trainingAiView;
            _trainingAiController = trainingAiController;
            _productionProgressController = productionProgressController;
        }
        
        public void Initialize()
        {
            _trainingAiView.StartResearchButton.onClick.AddListener(StartResearch);
            _trainingAiView.NameInputField.onValueChanged.AddListener(SetName);
            _trainingAiView.RandomNameButton.onClick.AddListener(SetRandomName);

            _productionProgressController.OnProgressEnd += ProgressEnd;
        }

        public void Dispose()
        {
            _trainingAiView.StartResearchButton.onClick.RemoveListener(StartResearch);
            _trainingAiView.NameInputField.onValueChanged.RemoveListener(SetName);
            _trainingAiView.RandomNameButton.onClick.RemoveListener(SetRandomName);
            
            _productionProgressController.OnProgressEnd -= ProgressEnd;
        }

        private void ProgressEnd(ProductionType productionType, int firstBubble, int secondBubble)
        {
            if (productionType != ProductionType.TrainingAI)
                return;

            _trainingAiController.SetBubbleCount(firstBubble);
            _trainingAiController.OnProgressResearchEnd();
        }

        private void SetRandomName()
        {
            _trainingAiController.SetRandomName();
        }

        private void SetName(string name)
        {
            _trainingAiController.UpdateRobotName(name);
        }

        private void StartResearch()
        {
            _trainingAiController.StartAiResearch();
        }
    }
}
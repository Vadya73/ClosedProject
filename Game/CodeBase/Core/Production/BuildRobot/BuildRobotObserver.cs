using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public sealed class BuildRobotObserver : IInitializable, IDisposable
    {
        private readonly BuildRobotView _buildRobotView;
        private readonly BuildRobotController _buildRobotController;
        private readonly ProductionProgressController _progressController;
        private readonly ProductionController _productionController;

        [Inject]
        public BuildRobotObserver(BuildRobotView buildRobotView, BuildRobotController buildRobotController, 
            ProductionProgressController progressController, ProductionController productionController)
        {
            _buildRobotView = buildRobotView;
            _buildRobotController = buildRobotController;
            _progressController = progressController;
            _productionController = productionController;
        }

        public void Initialize()
        {
            _buildRobotView.StartBuildButton.onClick.AddListener(StartBuildRobot);
            _buildRobotView.RandomNameButton.onClick.AddListener(SetRandomName);
            _buildRobotView.NameInputField.onValueChanged.AddListener(UpdateRobotName);
            _buildRobotView.NameInputField.onSelect.AddListener(ShowKeyboard);

            _progressController.OnProgressEnd += BuildRobotProgressEnd;
        }

        public void Dispose()
        {
            _buildRobotView.StartBuildButton.onClick.RemoveListener(StartBuildRobot);
            _buildRobotView.RandomNameButton.onClick.RemoveListener(SetRandomName);
            _buildRobotView.NameInputField.onValueChanged.RemoveListener(UpdateRobotName);
            
            _buildRobotView.NameInputField.onSelect.RemoveListener(ShowKeyboard);
            _buildRobotController.HideKeyboard();
            _buildRobotView.NameInputField.onDeselect.RemoveListener(HideKeyboard);

            _progressController.OnProgressEnd -= BuildRobotProgressEnd;
        }

        private void ShowKeyboard(string arg0)
        {
            _buildRobotController.ShowKeyboard(arg0);
        }

        private void HideKeyboard(string arg0)
        {
            _buildRobotController.HideKeyboard();
        }

        private void BuildRobotProgressEnd(ProductionType productionType, int firstBubble, int secondBubble)
        {
            if (productionType != ProductionType.BuildRobot)
                return;

            _buildRobotController.SetBubbleCount(firstBubble, secondBubble);
            _buildRobotController.EndBuildRobot();
        }

        private void StartBuildRobot()
        {
            _buildRobotController.StartBuildRobot();
        }

        private void SetRandomName()
        {
            _buildRobotController.SetRandomName();
        }

        private void UpdateRobotName(string name)
        {
            _buildRobotController.UpdateRobotName(name);
        }
    }
}
using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class RobotPresentationObserver : IInitializable, IDisposable
    {
        private readonly RobotPresentationView _robotPresentationView;
        private readonly RobotPresentationController _robotPresentationController;

        [Inject]
        public RobotPresentationObserver(RobotPresentationView robotPresentationView, RobotPresentationController robotPresentationController)
        {
            _robotPresentationView = robotPresentationView;
            _robotPresentationController = robotPresentationController;
        }

        public void Initialize()
        {
            _robotPresentationView.ContinueButton.onClick.AddListener(Continue);
        }

        public void Dispose()
        {
            _robotPresentationView.ContinueButton.onClick.RemoveListener(Continue);
        }

        private void Continue()
        {
            _robotPresentationController.ContinueGame();
        }
    }
}
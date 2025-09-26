using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class AiPresentationObserver : IInitializable, IDisposable
    {
        private readonly AiPresentationView _aiPresentationView;
        private readonly AiPresentationController _aiPresentationController;
        
        [Inject]
        public AiPresentationObserver(AiPresentationView aiPresentationView, AiPresentationController aiPresentationController)
        {
            _aiPresentationView = aiPresentationView;
            _aiPresentationController = aiPresentationController;
        }

        public void Initialize()
        {
            _aiPresentationView.ContinueButton.onClick.AddListener(ClosePresentationView);
        }

        public void Dispose()
        {
            _aiPresentationView.ContinueButton.onClick.RemoveListener(ClosePresentationView);
        }

        private void ClosePresentationView()
        {
            _aiPresentationController.ClosePresentationView();
        }
    }
}
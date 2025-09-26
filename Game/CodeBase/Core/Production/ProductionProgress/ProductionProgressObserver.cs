using System;
using VContainer.Unity;

namespace Core
{
    public class ProductionProgressObserver : IInitializable, IDisposable
    {
        private readonly ProductionProgressBarView _progressBarView;
        private readonly ProductionProgressController _progressController;

        public ProductionProgressObserver(ProductionProgressBarView progressBarView, ProductionProgressController progressController)
        {
            _progressBarView = progressBarView;
            _progressController = progressController;
        }

        public void Initialize()
        {
            _progressBarView.CancelButton.onClick.AddListener(CancelResearch);
        }

        public void Dispose()
        {
            _progressBarView.CancelButton.onClick.RemoveListener(CancelResearch);
        }

        private void CancelResearch()
        {
            _progressController.CancelProgress();
        }
    }
}
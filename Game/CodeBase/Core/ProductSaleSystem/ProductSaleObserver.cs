using System;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ProductSaleObserver : IInitializable, IDisposable
    {
        private readonly ProductSaleController _productSaleController;
        private readonly ProductSaleView _productSaleView;
        private readonly TimeSystem _timeSystem;

        [Inject]
        public ProductSaleObserver(ProductSaleController productSaleController, ProductSaleView productSaleView, TimeSystem timeSystem)
        {
            _productSaleController = productSaleController;
            _productSaleView = productSaleView;
            _timeSystem = timeSystem;
        }

        public void Initialize()
        {
            _timeSystem.TimeData.OnWeekChange += OnWeekChange;
            
            _productSaleView.BackGraphButton.onClick.AddListener(ShowBackGraph);
            _productSaleView.NextGraphButton.onClick.AddListener(ShowNextGraph);
        }

        public void Dispose()
        {
            _timeSystem.TimeData.OnWeekChange -= OnWeekChange;
            
            _productSaleView.BackGraphButton.onClick.RemoveListener(ShowBackGraph);
            _productSaleView.NextGraphButton.onClick.RemoveListener(ShowNextGraph);
        }

        private void ShowNextGraph()
        {
            _productSaleController.ShowNextGraph();
        }

        private void ShowBackGraph()
        {
            _productSaleController.ShowBackGraph();
        }

        private void OnWeekChange()
        {
            _productSaleController.TickProductCost();
        }
    }
}
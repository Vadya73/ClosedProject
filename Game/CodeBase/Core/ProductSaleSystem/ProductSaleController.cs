using System.Linq;
using Services;
using Sirenix.Utilities;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class ProductSaleController : IStartable
    {
        private readonly ProductSaleView _productSaleView;
        private readonly IObjectResolver _objectResolver;
        private readonly ProductSaleConfig _productSaleConfig;
        private readonly ProductSaleController _productSaleController;
        private readonly Wallet _wallet;
        private readonly Transform _createTransform;
        private readonly SaleGraph _saleGraphPrefab;

        private int _currentGraph;

        [Inject]
        public ProductSaleController(IObjectResolver objectResolver, ProductSaleConfig productSaleConfig, Wallet wallet,
             ProductSaleView productSaleView, PlayerConfig playerConfig)
        {
            _objectResolver = objectResolver;
            _productSaleConfig = productSaleConfig;
            _wallet = wallet;
            _productSaleView = productSaleView;
            
            _saleGraphPrefab = playerConfig.SaleGraph;
        }

        public void Start()
        {
            if (_productSaleConfig.CurrentSaleProducts == null || _productSaleConfig.CurrentSaleProducts.Count == 0)
            {
                _productSaleView.ForcedHide();
                return;
            }

            foreach (var product in _productSaleConfig.CurrentSaleProducts)
                CreateSaleProduct(product);
        }

        public void TickProductCost()
        {
            if (_productSaleConfig.CurrentSaleProducts.Count == 0)
                return;
            
            if (_productSaleView.IsActive == false)
                _productSaleView.Show();
            

            for (int i = _productSaleConfig.CurrentSaleProducts.Count - 1; i >= 0; i--)
            {
                var robot = _productSaleConfig.CurrentSaleProducts[i];
        
                robot.ProductSaleData.AddPassedWeeks();
                long addCost = CalculateCost(robot);
                float onePercent = addCost / 100f;
                float minusCost = onePercent * robot.ProductSaleData.PassedWeeks;
                addCost -= (long)minusCost;
        
                _wallet.AddWalletCount(WalletType.Cash, addCost);
                robot.ProductSaleData.SetLastSaleCost(addCost);
        
                RobotTick(robot, addCost);
                
                if (minusCost >= onePercent * 10)
                {
                    var robotToRemove = _productSaleConfig.CurrentSaleProducts[i];
                    _productSaleConfig.CurrentSaleProducts.RemoveAt(i);

                    foreach (var graph in _productSaleView.SaleGraphs.ToList())
                    {
                        if (graph.Robot == robotToRemove)
                        {
                            Object.Destroy(graph.gameObject);
                            _productSaleView.SaleGraphs.Remove(graph);

                            if (_productSaleView.SaleGraphs.IsNullOrEmpty())
                                _productSaleView.ForcedHide();
                        }
                    }
                }
                
            }
        }

        private void RobotTick(Robot robot, float addCost)
        {
            if (_productSaleView.SaleGraphs.IsNullOrEmpty())
                return;
            
            foreach (var saleGraph in _productSaleView.SaleGraphs)
            {
                if (saleGraph.Robot == robot)
                    saleGraph.AddNewValue(addCost);
            }
        }

        public void CreateSaleProduct(Robot robot)
        {
            if (!_productSaleConfig.CurrentSaleProducts.Contains(robot))
                _productSaleConfig.AddToCurrentSaleProducts(robot);
            
            var saleGraph = _objectResolver.Instantiate(_saleGraphPrefab, _productSaleView.SaleGraphsContainer.transform);
            var rect = saleGraph.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(500,250);
            
            if (_productSaleView.IsActive == false)
                _productSaleView.Show();
            saleGraph.ResetGraph();
            _productSaleView.SaleGraphs.Add(saleGraph);
            
            saleGraph.SetRobot(robot);
        }

        private long CalculateCost(Robot robot)
        {
            return robot.Score switch
            {
                < 30 => Random.Range(_productSaleConfig.GetCountLess30(robot.RobotSubType).x, _productSaleConfig.GetCountLess30(robot.RobotSubType).y),
                > 30 and <= 60 => Random.Range(_productSaleConfig.GetCountLess60(robot.RobotSubType).x, _productSaleConfig.GetCountLess60(robot.RobotSubType).y),
                > 60 and < 90 => Random.Range(_productSaleConfig.GetCountLess90(robot.RobotSubType).x, _productSaleConfig.GetCountLess90(robot.RobotSubType).y),
                >= 90 => Random.Range(_productSaleConfig.GetCountMore90(robot.RobotSubType).x, _productSaleConfig.GetCountMore90(robot.RobotSubType).y),
                _ => 0
            };
        }

        public void ShowNextGraph()
        {
            if (_currentGraph + 1 >= _productSaleView.SaleGraphs.Count)
                return;
            
            _currentGraph++;
            var a = _productSaleView.SaleGraphsContainer.transform.position.x - 670f;
            _productSaleView.SaleGraphsContainer.transform.position = new Vector3(a, _productSaleView.SaleGraphsContainer.transform.position.y, 
                _productSaleView.SaleGraphsContainer.transform.position.z);
        }

        public void ShowBackGraph()
        {
            if (_currentGraph == 0)
                return;
            
            _currentGraph--;
            var a = _productSaleView.SaleGraphsContainer.transform.position.x + 670f;
            _productSaleView.SaleGraphsContainer.transform.position = new Vector3(a, _productSaleView.SaleGraphsContainer.transform.position.y, 
                _productSaleView.SaleGraphsContainer.transform.position.z);
        }
    }
}


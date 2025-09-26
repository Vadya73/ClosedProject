using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ProductSaleView : BaseView<Robot>
    {
        [SerializeField] private List<SaleGraph> _saleGraphs = new List<SaleGraph>();
        [SerializeField] private Transform _saleGraphsContainer;
        [SerializeField] private Button _backGraphButton;
        [SerializeField] private Button _nextGraphButton;
        
        public List<SaleGraph> SaleGraphs => _saleGraphs;
        public Transform SaleGraphsContainer => _saleGraphsContainer;
        public Button BackGraphButton => _backGraphButton;
        public Button NextGraphButton => _nextGraphButton;

        public override void UpdateView(Robot taskData)
        {
            
        }

        public void AddSaleGraph(SaleGraph saleGraph)
        {
            _saleGraphs.Add(saleGraph);
        }
    }
}
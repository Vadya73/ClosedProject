using UI;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class ProductSaleData : IData
    {
        [SerializeField] private int _passedWeeks;
        [SerializeField] private float _minSaleCost;
        [SerializeField] private float _maxSaleCost;
        [SerializeField] private float _lastSaleCost;
        [SerializeField] private float _totalSell;
        
        public int PassedWeeks => _passedWeeks;
        public float MinSaleCost => _minSaleCost;
        public float MaxSaleCost => _maxSaleCost;
        public float LastSaleCost => _lastSaleCost;
        public float TotalSell => _totalSell;
        
        public void AddPassedWeeks() => _passedWeeks++;

        public void SetLastSaleCost(float lastSaleCost)
        {
            if (lastSaleCost > _maxSaleCost)
                _maxSaleCost = lastSaleCost;
            
            if (lastSaleCost < _minSaleCost || _minSaleCost == 0)
                _minSaleCost = lastSaleCost;
            
            _lastSaleCost = lastSaleCost;
            _totalSell += lastSaleCost;
        }
    }
}
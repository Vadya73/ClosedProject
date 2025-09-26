using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Product Sale Config", fileName = "ProductSaleConfig", order = 0)]
    public class ProductSaleConfig : ScriptableObject
    {
        [SerializeField] private List<Robot> _currentSaleProducts;

        [SerializeField] private ProductSaleSettingData[] _productSaleSettingData;
        
        public List<Robot> CurrentSaleProducts => _currentSaleProducts;

        public void AddToCurrentSaleProducts(Robot productSaleData)
        {
            _currentSaleProducts.Add(productSaleData);
        }

        public int2 GetCountLess30(RobotSubtypes subType)
        {
            foreach (var data in _productSaleSettingData)
            {
                if(data.RobotSubtype == subType)
                    return data.CountLess30;
            }
            
            return int2.zero;
        }

        public int2 GetCountLess60(RobotSubtypes subType)
        {
            foreach (var data in _productSaleSettingData)
            {
                if(data.RobotSubtype == subType)
                    return data.CountLess60;
            }
            
            return int2.zero;
        }

        public int2 GetCountLess90(RobotSubtypes subType)
        {
            foreach (var data in _productSaleSettingData)
            {
                if(data.RobotSubtype == subType)
                    return data.CountLess90;
            }
            
            return int2.zero;
        }

        public int2 GetCountMore90(RobotSubtypes subType)
        {
            foreach (var data in _productSaleSettingData)
            {
                if(data.RobotSubtype == subType)
                    return data.CountMore90;
            }
            
            return int2.zero;
        }
    }

    [Serializable]
    public struct ProductSaleSettingData
    {
        public RobotSubtypes RobotSubtype;
        public int2 CountLess30;
        public int2 CountLess60;
        public int2 CountLess90;
        public int2 CountMore90;
    }
}
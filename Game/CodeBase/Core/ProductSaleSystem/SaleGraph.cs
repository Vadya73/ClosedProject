using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class SaleGraph : MonoBehaviour
    {
        [SerializeField] private TMP_Text _robotNameText;
        [SerializeField] private List<Slider> _sliders = new List<Slider>();
        [SerializeField] private float _animationSpeed = 5f;
        [SerializeField, Sirenix.OdinInspector.ReadOnly] private Robot _robot;

        private Queue<float> _values = new Queue<float>();
        private float _currentMaxValue = 1f;
        
        public Robot Robot => _robot; 

        void Start()
        {
            foreach (var slider in _sliders)
            {
                slider.maxValue = _currentMaxValue;
                _values.Enqueue(0f);
            }
        }

        public void AddNewValue(float newValue)
        {
            _currentMaxValue = Mathf.Max(_currentMaxValue, newValue);
        
            _values.Enqueue(newValue);
        
            if (_values.Count > _sliders.Count)
            {
                _values.Dequeue();
            }
        
            UpdateSlidersWithShift();
        }

        public void SetRobot(Robot robot)
        {
            _robot = robot;
            _robotNameText.text = robot.Name;
        }

        private void UpdateSlidersWithShift()
        {
            float[] currentValues = _values.ToArray();
        
            for (int i = 0; i < _sliders.Count; i++)
            {
                int valueIndex = Mathf.Max(0, currentValues.Length - _sliders.Count + i);
                float targetValue = valueIndex < currentValues.Length ? currentValues[valueIndex] : 0f;
            
                Slider slider = _sliders[i];
                slider.maxValue = _currentMaxValue;
            
                if (gameObject.activeInHierarchy)
                    StartCoroutine(AnimateSlider(slider, targetValue));
            }
        }

        private System.Collections.IEnumerator AnimateSlider(Slider slider, float targetValue)
        {
            float startValue = slider.value;
            float t = 0f;
        
            while (t < 1f)
            {
                t += UnityEngine.Time.deltaTime * _animationSpeed;
                slider.value = Mathf.Lerp(startValue, targetValue, t);
                yield return null;
            }
        }

        public void ResetGraph()
        {
            foreach (var slider in _sliders)
            {
                slider.maxValue = _currentMaxValue;
                slider.value = 0f;
                _values.Enqueue(0f);
            }
        }
    }
}
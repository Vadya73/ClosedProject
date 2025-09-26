using TMPro;
using UnityEngine;
using VContainer;

namespace Core
{
    public abstract class ProductionComponent<TComponent> : MonoBehaviour where TComponent : ScriptableObject
    {
        protected abstract void FillDropDown();
        protected abstract void SetCurrentComponent(int componentIndex);
        
        [SerializeField] protected TMP_Dropdown _dropdown;
        [SerializeField] protected TComponent _currentComponentConfig;
        
        protected PlayerConfig _playerConfig;
        public TComponent CurrentComponentConfig => _currentComponentConfig;

        [Inject]
        private void Construct(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }
        
        private void Awake()
        {
            if (_dropdown == null)
                _dropdown = GetComponent<TMP_Dropdown>();
            
            _dropdown.onValueChanged.AddListener(SetCurrentComponent);
        }

        private void OnEnable()
        {
            _dropdown.ClearOptions();
            FillDropDown();
        }

        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(SetCurrentComponent);
        }

        public void Reset()
        {
            _currentComponentConfig = null;
            _dropdown.value = -1;
        }
    }
}
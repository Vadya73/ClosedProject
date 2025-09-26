using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    [RequireComponent(typeof(Toggle))]
    public abstract class ProductionAddon<TConfig> : MonoBehaviour where TConfig : ScriptableObject 
    {
        [SerializeField] private bool _isActive;
        [SerializeField] protected TConfig _robotAddonConfig;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private GameObject _lockObject;
        public Toggle Toggle => _toggle;
        public bool IsActive => _isActive;
        public TConfig RobotAddonConfig => _robotAddonConfig;

        protected void Awake()
        {
            if (!_toggle)
                _toggle = GetComponent<Toggle>();
            
            _toggle.onValueChanged.AddListener(SetActiveBool);
            SetActiveBool(_toggle.isOn);
        }

        private void OnEnable()
        {
            _lockObject.SetActive(!_toggle.interactable);
        }

        protected void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(SetActiveBool);
        }

        private void SetActiveBool(bool b) => _isActive = b;
    }
}
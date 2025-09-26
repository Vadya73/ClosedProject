using Core.CameraControl;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Bootstrap
{
    public class BootstrapUI : BaseView<IData>
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _button;
        private LoadScreen _loadScreen;
        private CameraController _cameraController;

        public Toggle Toggle => _toggle;
        public Button Button => _button;

        [Inject]
        private void Construct(LoadScreen loadScreen, CameraController cameraController)
        {
            _loadScreen = loadScreen;
            _cameraController = cameraController;
        }

        protected override void Awake()
        {
            base.Awake();
            _toggle.onValueChanged.AddListener(ToggleValueChanged);
            _button.onClick.AddListener(GoToLevel);
            _toggle.isOn = false;
            _button.interactable = _toggle.isOn;
            _cameraController.SetActiveStateCamera(false);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(ToggleValueChanged);
            _button.onClick.RemoveListener(GoToLevel);
        }

        private void ToggleValueChanged(bool arg0)
        {
            _button.interactable = arg0;
        }

        private void GoToLevel()
        {
            _loadScreen.LoadScene(1);
        }
    }
}
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ProductionView : BaseView<IData>
    {
        [Header("Build Robot")]
        [SerializeField] private Button _buildRobotButton;
        [SerializeField] private Image _buildRobotButtonImage;
        [Header("Training AI")]
        [SerializeField] private Button _trainingAIButton;
        [SerializeField] private Image _trainingAIButtonImage;
        [Header("Research")]
        [SerializeField] private Button _researchButton;
        [SerializeField] private Image _researchButtonImage;
        [Header("Exit")]
        [SerializeField] private Button _exitButton;
        [Header("Colors")]
        [SerializeField] private Color _defaultButtonColor;
        [SerializeField] private Color _selectedButtonColor;

        private CanvasGroup _canvasGroup;
        
        public Button BuildRobotButton => _buildRobotButton;
        public Image BuildRobotButtonImage => _buildRobotButtonImage;
        public Button TrainingAIButton => _trainingAIButton;
        public Image TrainingAIButtonImage => _trainingAIButtonImage;
        public Button ResearchButton => _researchButton;
        public Image ResearchButtonImage => _researchButtonImage;
        public Button ExitButton => _exitButton;
        public Color DefaultButtonColor => _defaultButtonColor;
        public Color SelectedButtonColor => _selectedButtonColor;

        protected override void Awake()
        {
            base.Awake();
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            SetViewActivity(true);

            _canvasGroup.DOFade(1f, 0.5f);
        }

        public override void Hide()
        {
            _canvasGroup.DOFade(0f, 0.5f)
                .OnComplete(() => gameObject.SetActive(false));
            SetViewActivity(false);
        }
    }
}

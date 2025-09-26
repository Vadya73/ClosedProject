using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core
{
    public class ProductionProgressBarView : BaseView<IData>
    {
        [SerializeField] private ProgressBarConfig _progressBarConfig;
        [SerializeField] private Image _icon;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _cancelButton;
        [Header("Bubble 1")]
        [SerializeField] private Transform _bubble1Transform;
        [SerializeField] private TMP_Text _bubble1ProgressText;
        [SerializeField] private Image _bubble1ProgressImage;
        [Header("Bubble 2")]
        [SerializeField] private Transform _bubble2Transform;
        [SerializeField] private TMP_Text _bubble2ProgressText;
        [SerializeField] private Image _bubble2ProgressImage;
        
        public Button CancelButton => _cancelButton;
        private PlayerConfig _playerConfig;

        [Inject]
        private void Construct(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }
        
        protected override void Awake()
        {
            base.Awake();
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show(ProductionType productionType, int firstBubbleProgress, int secondBubbleProgress = 0)
        {
            _progressSlider.value = 0f;
            gameObject.SetActive(true);
            SetViewActivity(true);
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, .3f);

            _icon.sprite = productionType switch
            {
                ProductionType.BuildRobot => _progressBarConfig.RobotResearchSprite,
                ProductionType.TrainingAI => _progressBarConfig.AiResearchSprite,
                ProductionType.Research => _progressBarConfig.SkillTreeSprite,
                _ => _icon.sprite
            };

            switch (productionType)
            {
                case ProductionType.BuildRobot:
                    _bubble1Transform.gameObject.SetActive(true);
                    _bubble2Transform.gameObject.SetActive(true);
                    _cancelButton.gameObject.SetActive(false);
                    
                    _bubble1ProgressImage.color = _playerConfig.BubbleConfig.BuildRobotTechnologyColor;
                    _bubble2ProgressImage.color = _playerConfig.BubbleConfig.BuildRobotDesignColor;
                    
                    _bubble1ProgressText.text = "0";
                    _bubble2ProgressText.text = "0";
                    break;
                case ProductionType.TrainingAI:
                    _bubble1Transform.gameObject.SetActive(true);
                    _bubble2Transform.gameObject.SetActive(false);
                    _cancelButton.gameObject.SetActive(false);
                    
                    _bubble1ProgressImage.color = _playerConfig.BubbleConfig.AITrainingDataScienceColor;
                    _bubble1ProgressText.text = "0";
                    break;
                case ProductionType.Research:
                    _bubble1Transform.gameObject.SetActive(true);
                    _bubble2Transform.gameObject.SetActive(false);
                    _cancelButton.gameObject.SetActive(true);
                    
                    _bubble1ProgressImage.color = _playerConfig.BubbleConfig.ResearchColor;
                    _bubble1ProgressText.text = "0";
                    break;
            }
        }

        public override void Hide()
        {
            SetViewActivity(false);
            _canvasGroup.DOFade(0f, .3f).OnComplete(() => gameObject.SetActive(false));
        }

        public void UpdateSliderValue(float percent)
        {
            _progressSlider.value = percent;
        }

        public void UpdateBubbles(int firstBubbleCountCurrent, int firstBubbleCountRequired, int secondBubbleCountCurrent, int secondBubbleCountRequired)
        {
            _bubble1ProgressText.text = $"{firstBubbleCountCurrent}";
            _bubble2ProgressText.text = $"{secondBubbleCountCurrent}";
        }
    }

    public enum ProductionType
    {
        BuildRobot = 0,
        TrainingAI = 1,
        Research = 2,
    }
}
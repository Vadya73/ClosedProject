using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class AiPresentationView : BaseView<AI>
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _image;
        [SerializeField] private Button _continueButton;
        [Header("Sliders")]
        [SerializeField] private Slider _agiSlider;
        [SerializeField] private TMP_Text _agiText;
        [SerializeField] private Slider _accuracySlider;
        [SerializeField] private TMP_Text _accuracyText;
        [SerializeField] private Slider _flexibilitySlider;
        [SerializeField] private TMP_Text _flexibilityText;
        [SerializeField] private Slider _stabilitySlider;
        [SerializeField] private TMP_Text _stabilityText;
        [SerializeField] private Slider _adaptivenessSlider;
        [SerializeField] private TMP_Text _adaptivenessText;
        public Button ContinueButton => _continueButton;

        public override void UpdateView(AI taskData)
        {
            _agiSlider.value = taskData.Score;
            _agiText.text = taskData.Score.ToString();
            
            _nameText.text = taskData.Name;
            _descriptionText.text = taskData.Description;
            _image.sprite = taskData.Image;

            _accuracySlider.value = taskData.Characters.Accuracy;
            _accuracyText.text = taskData.Characters.Accuracy.ToString();
            
            _flexibilitySlider.value = taskData.Characters.Flexibility;
            _flexibilityText.text = taskData.Characters.Flexibility.ToString();
            
            _stabilitySlider.value = taskData.Characters.Stability;
            _stabilityText.text = taskData.Characters.Stability.ToString();
            
            _adaptivenessSlider.value = taskData.Characters.Adaptiveness;
            _adaptivenessText.text = taskData.Characters.Adaptiveness.ToString();
            
            SetColor(_accuracySlider);
            SetColor(_flexibilitySlider);
            SetColor(_stabilitySlider);
            SetColor(_adaptivenessSlider);
        }
        
        private void SetColor(Slider slider)
        {
            var sliderImage = slider.fillRect.GetComponent<Image>();
            
            switch (slider.value)
            {
                case <= 25:
                    sliderImage.color = Color.red;
                    return;
                case <= 50:
                    sliderImage.color = Color.yellow;
                    return;
                case <= 100:
                    sliderImage.color = Color.green;
                    return;
            }
        }
    }
}
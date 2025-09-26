using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class RobotPresentationView : BaseView<Robot>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _image;
        [SerializeField] private Button _continueButton;
        [Header("Sliders and more")]
        [SerializeField] private Slider _intellectSlider;
        [SerializeField] private TMP_Text _intellectText;
        [SerializeField] private Slider _responsivenessSlider;
        [SerializeField] private TMP_Text _responsivenessText;
        [SerializeField] private Slider _longevitySlider;
        [SerializeField] private TMP_Text _longevityText;
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private TMP_Text _speedText;
        [SerializeField] private Slider _weightSlider;
        [SerializeField] private TMP_Text _weightText;
        
        public Button ContinueButton => _continueButton;

        public override void UpdateView(Robot taskData)
        {
            if (taskData == null)
                return;
            
            _name.text = taskData.Name;
            _description.text = taskData.Description;
            
            _intellectSlider.value = taskData.RobotСharacters.Intellect;
            _intellectText.text = taskData.RobotСharacters.Intellect.ToString();
            
            _responsivenessSlider.value = taskData.RobotСharacters.Responsiveness;
            _responsivenessText.text = taskData.RobotСharacters.Responsiveness.ToString();

            _longevitySlider.value = taskData.RobotСharacters.Longevity;
            _longevityText.text = taskData.RobotСharacters.Longevity.ToString();

            _speedSlider.value = taskData.RobotСharacters.Speed;
            _speedText.text = taskData.RobotСharacters.Speed.ToString();

            _weightSlider.value = taskData.RobotСharacters.Weight;
            _weightText.text = taskData.RobotСharacters.Weight.ToString();

            SetColor(_intellectSlider);
            SetColor(_responsivenessSlider);
            SetColor(_longevitySlider);
            SetColor(_speedSlider);
            SetColor(_weightSlider);
            
        }
        
        private void SetColor(Slider slider)
        {
            if (slider == null || slider.fillRect == null) 
                return;

            Image img = slider.fillRect.GetComponent<Image>();
            
            if (img == null) 
                return;

            float val = slider.value;

            if (val >= 75f)
                img.color = Color.green;
            else if (val > 50f)
                img.color = Color.yellow;
            else
                img.color = Color.red;
        }
    }
}

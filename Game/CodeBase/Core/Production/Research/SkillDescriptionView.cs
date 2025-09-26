using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class SkillDescriptionView : BaseView<SkillData>
    {
        [SerializeField] private Image _bgImageComponent;
        [SerializeField] private TMP_Text _nameTextComponent;
        [SerializeField] private TMP_Text _costTextComponent;
        [SerializeField] private TMP_Text _descriptionTextComponent;
        [Header("Buttons")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _researchButton;
        [Header("Transforms")]
        [SerializeField] private Transform _defaultPosTransform;
        [SerializeField] private Transform _outPosTransform;

        public Button CloseButton => _closeButton;
        public Button ResearchButton => _researchButton;
        
        public override void UpdateView(SkillData taskData)
        {
            _costTextComponent.text = "Cost is: " + taskData.Cost.ToString();
            _nameTextComponent.text = taskData.Name;
            _descriptionTextComponent.text = taskData.Description;
        }

        public override void Show()
        {
            if (_isAnimated)
                return;
            
            SetAnimatedState(true);
            gameObject.SetActive(true);
            SetViewActivity(true);
            
            transform.position = _outPosTransform.position;
            
            transform.DOMove(_defaultPosTransform.position, 0.5f)
                .OnComplete(() => SetAnimatedState(false));
        }

        public override void Hide()
        {
            if (_isAnimated)
                return;
            
            SetAnimatedState(true);
            
            DOTween.Sequence()
                .Append(transform.DOMove(_outPosTransform.position, 0.5f))
                .OnComplete(() =>
                {
                    SetViewActivity(false);
                    gameObject.SetActive(false);
                    SetAnimatedState(false);
                });
        }
    }
}
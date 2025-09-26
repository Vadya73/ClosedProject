using System;
using DG.Tweening;
using Infrastructure;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class UnhiredWorkerCard : MonoBehaviour, IPoolable
    {
        [Header("Tech Components")]
        [SerializeField] private GameObject _frontSide;
        [SerializeField] private GameObject _backSide;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _informationButton;
        [Header("UI Components")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _salary;
        [SerializeField] private Image _avatar;
        [Header("Sliders")]
        [SerializeField] private Slider _dataScienceSlider;
        [SerializeField] private Slider _technologySlider;
        [SerializeField] private Slider _designSlider;
        [SerializeField] private Slider _researchSlider;
        [Header("Data")]
        [SerializeField, ReadOnly] private WorkerConfig _cardConfig;
        
        public WorkerConfig CardConfig => _cardConfig;

        private void Awake()
        {
            _informationButton.onClick.AddListener(FlipCard);
        }

        private void OnEnable()
        {
            _frontSide.SetActive(true);
            _backSide.SetActive(false);
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        public void UpdateData(WorkerConfig workerData, Action<UnhiredWorkerCard> onPurchase)
        {
            _cardConfig = workerData;
            
            _name.text = workerData.WorkerData.Name;
            _description.text = workerData.WorkerData.Description;
            _salary.text = "Salary: " + workerData.WorkerData.CurrentSalary.ToString();
            _avatar.sprite = workerData.WorkerData.Avatar;

            _dataScienceSlider.value = workerData.WorkerData.Stats.DataScienceLevel;
            _technologySlider.value = workerData.WorkerData.Stats.TechnologyLevel;
            _designSlider.value = workerData.WorkerData.Stats.DesignLevel;
            _researchSlider.value = workerData.WorkerData.Stats.ResearchLevel;
            
            _purchaseButton.onClick.AddListener(() => onPurchase(this));
        }

        private void FlipCard()
        {
            bool isFrontActive = _frontSide.activeSelf;
    
            DOTween.Sequence()
                .Append(transform.DOLocalRotate(new Vector3(0, 90f, 0), 0.25f).SetEase(Ease.Linear))
        
                .AppendCallback(() => {
                    _frontSide.SetActive(!isFrontActive);
                    _backSide.SetActive(isFrontActive);
                    transform.localEulerAngles = new Vector3(0, -90f, 0);
                })
        
                .Append(transform.DOLocalRotate(new Vector3(0, 0f, 0), 0.25f).SetEase(Ease.OutBack, 0.5f));
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            _purchaseButton.onClick.RemoveAllListeners();
            _informationButton.onClick.RemoveAllListeners();
        }

    }
}
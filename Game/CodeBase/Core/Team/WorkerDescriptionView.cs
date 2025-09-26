using System;
using Services;
using Sirenix.OdinInspector;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core
{
    public class WorkerDescriptionView : BaseView<WorkerData>
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _salaryText;
        [SerializeField] private Image _avatarImage;
        [Header("Buttons chars")]
        [SerializeField] private Image _dataScienceCostImage;
        [SerializeField] private TMP_Text _dataScienceCostText;
        [SerializeField] private Image _technologyCostImage;
        [SerializeField] private TMP_Text _technologyCostText;
        [SerializeField] private Image _designCostImage;
        [SerializeField] private TMP_Text _designCostText;
        [SerializeField] private Image _researchCostImage;
        [SerializeField] private TMP_Text _researchCostText;
        [Header("Sliders")]
        [SerializeField] private Slider _dataScienceSlider;
        [SerializeField] private Slider _technologySlider;
        [SerializeField] private Slider _designSlider;
        [SerializeField] private Slider _researchSlider;
        [Header("Buttons")]
        [SerializeField] private Button _dataScienceButton;
        [SerializeField] private Button _technologyButton;
        [SerializeField] private Button _designButton;
        [SerializeField] private Button _researchButton;
        [SerializeField] private Button _dismissButton;
        [SerializeField] private Button _exitButton;
        [SerializeField, ReadOnly] private WorkerData _currentWorkerData;
        
        private TeamSystemController _teamSystemController;
        private IObjectResolver _objectResolver;
        private WalletConfig _walletConfig;
        private HiredTeamController _hiredTeamController;
        private HiredWorkerCard _workerCardMono;

        [Inject]
        private void Construct(IObjectResolver objectResolver, Wallet wallet)
        {
            _objectResolver = objectResolver;
            _walletConfig = wallet.WalletConfig;
        }
        
        protected override void Awake()
        {
            base.Awake();
            _dataScienceButton.onClick.AddListener(() => UpdateCharacteristic(WorkerCharsEnum.DataScience));
            _technologyButton.onClick.AddListener(() => UpdateCharacteristic(WorkerCharsEnum.Technology));
            _designButton.onClick.AddListener(() => UpdateCharacteristic(WorkerCharsEnum.Design));
            _researchButton.onClick.AddListener(() => UpdateCharacteristic(WorkerCharsEnum.Research));
            _exitButton.onClick.AddListener(Hide);
            _dismissButton.onClick.AddListener(DismissWorker);
            
            _teamSystemController = _objectResolver.Resolve<TeamSystemController>();
            _hiredTeamController = _objectResolver.Resolve<HiredTeamController>();
        }

        public override void Hide()
        {
            _dataScienceButton.interactable = false;
            _technologyButton.interactable = false;
            _designButton.interactable = false;
            _researchButton.interactable = false;
            _dismissButton.interactable = false;
            _exitButton.interactable = false;
            base.Hide();
        }

        public override void Show()
        {
            base.Show();
            _dataScienceButton.interactable = true;
            _technologyButton.interactable = true;
            _designButton.interactable = true;
            _researchButton.interactable = true;
            _dismissButton.interactable = true;
            _exitButton.interactable = true;
        }

        public override void UpdateView(WorkerData taskData)
        {
            _currentWorkerData = taskData;
            
            _nameText.text = taskData.Name;
            _salaryText.text = "Salary: " + taskData.CurrentSalary.ToString();
            _avatarImage.sprite = taskData.Avatar;

            UpdateSliders(taskData);
            
            _dataScienceCostText.text = taskData.Stats.DataScienceDefaultUpgradeCost.ToString();
            _technologyCostText.text = taskData.Stats.TechnologyDefaultUpgradeCost.ToString();
            _designCostText.text = taskData.Stats.DesignDefaultUpgradeCost.ToString();
            _researchCostText.text = taskData.Stats.ResearchDefaultUpgradeCost.ToString();

            _dataScienceCostImage.sprite = _walletConfig.WalletData.GetIcon(taskData.Stats.DataScienceWalletType);
            _technologyCostImage.sprite = _walletConfig.WalletData.GetIcon(taskData.Stats.TechnologyWalletType);
            _designCostImage.sprite = _walletConfig.WalletData.GetIcon(taskData.Stats.DesignWalletType);
            _researchCostImage.sprite = _walletConfig.WalletData.GetIcon(taskData.Stats.ResearchWalletType);

            _dismissButton.interactable = taskData != _teamSystemController.TeamSystemConfig.PlayerConfig.WorkerData;
        }

        private void DismissWorker()
        {
            _teamSystemController.DismissWorker(_workerCardMono);

            Hide();
        }
        
        private void UpdateCharacteristic(WorkerCharsEnum workerCharsEnum)
        {
            _teamSystemController.TryUpgradeCharacteristic(workerCharsEnum, _currentWorkerData);
            UpdateSalary();
        }

        private void UpdateSalary()
        {
            _salaryText.text = "Salary: " + _currentWorkerData.CurrentSalary;
        }

        public void UpdateSliders(WorkerData currentWorkerData)
        {
            _dataScienceSlider.value = currentWorkerData.Stats.DataScienceLevel;
            _technologySlider.value = currentWorkerData.Stats.TechnologyLevel;
            _designSlider.value = currentWorkerData.Stats.DesignLevel;
            _researchSlider.value = currentWorkerData.Stats.ResearchLevel;
        }

        public void SetCard(HiredWorkerCard workerCard) => _workerCardMono = workerCard;

        public void UpdateCostUpgrades(WorkerData currentWorkerData, Func<long, int, long> calculateCost)
        {
            _dataScienceCostText.text = calculateCost(currentWorkerData.Stats.DataScienceDefaultUpgradeCost, 
                    currentWorkerData.Stats.DataScienceLevel).ToString();
            
            _technologyCostText.text = calculateCost(currentWorkerData.Stats.TechnologyDefaultUpgradeCost,
                    currentWorkerData.Stats.TechnologyLevel).ToString();
            
            _designCostText.text = calculateCost(currentWorkerData.Stats.DesignDefaultUpgradeCost, 
                currentWorkerData.Stats.DesignLevel).ToString();
            
            _researchCostText.text = calculateCost(currentWorkerData.Stats.ResearchDefaultUpgradeCost,
                currentWorkerData.Stats.ResearchLevel).ToString();
        }

        public void CheckMaxLevels(WorkerData currentWorkerData)
        {
            UpdateUpgradeUI(currentWorkerData.Stats.DataScienceLevel, currentWorkerData.Stats.MaxLevel, 
                _dataScienceButton, _dataScienceCostText, _dataScienceCostImage);

            UpdateUpgradeUI(currentWorkerData.Stats.TechnologyLevel, currentWorkerData.Stats.MaxLevel, 
                _technologyButton, _technologyCostText, _technologyCostImage);

            UpdateUpgradeUI(currentWorkerData.Stats.DesignLevel, currentWorkerData.Stats.MaxLevel, 
                _designButton, _designCostText, _designCostImage);

            UpdateUpgradeUI(currentWorkerData.Stats.ResearchLevel, currentWorkerData.Stats.MaxLevel, 
                _researchButton, _researchCostText, _researchCostImage);
        }

        
        private void UpdateUpgradeUI(int level, int maxLevel, Button button, TMP_Text costText, Image costImage)
        {
            bool isMax = level >= maxLevel;

            button.interactable = !isMax;
            costImage.gameObject.SetActive(!isMax);
            costText.text = isMax ? "MAX LEVEL" : costText.text;
        }

    }
    
    public enum WorkerCharsEnum
    {
        DataScience = 0,
        Technology = 1,
        Design = 2,
        Research = 3
    }
}
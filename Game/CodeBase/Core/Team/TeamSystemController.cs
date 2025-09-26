using System;
using System.Linq;
using Infrastructure;
using Services;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core
{
    public class TeamSystemController : IInitializable, IStartable, IDisposable
    {
        private readonly TeamSystemView _teamSystemView;
        private readonly TeamSystemConfig _teamSystemConfig;
        private readonly Wallet _wallet;
        private readonly NotificationController _notificationController;
        private readonly UnhiredTeamController _unhiredTeamController;
        private readonly HiredTeamController _hiredTeamController;
        private readonly WorkerDescriptionView _workerDescriptionView;
        private readonly TimeSystem _timeSystem;
        private readonly AudioController _audioController;
        private readonly WorkerPlace[] _workerPlaces;
        
        public TeamSystemConfig TeamSystemConfig => _teamSystemConfig;
        public WorkerPlace[] WorkerPlaces => _workerPlaces; 

        [Inject]
        public TeamSystemController(TeamSystemView teamSystemView,TeamSystemConfig teamSystemConfig, Wallet wallet, 
            NotificationController notificationController, UnhiredTeamController unhiredTeamController, HiredTeamController hiredTeamController,
            WorkerDescriptionView workerDescriptionView, TimeSystem timeSystem, AudioController audioController)
        { 
            _teamSystemView = teamSystemView;
            _teamSystemConfig = teamSystemConfig;
            _wallet = wallet;
            _notificationController = notificationController;
            _unhiredTeamController = unhiredTeamController;
            _hiredTeamController = hiredTeamController;
            _workerDescriptionView = workerDescriptionView;
            _timeSystem = timeSystem;
            _audioController = audioController;
            _workerPlaces = _teamSystemView.WorkerPlaces;
        }

        public void Initialize()
        {
            ForceHideView();
            SpawnPlayer();
            
            _timeSystem.TimeData.OnMonthChanged += PaySalary;
        }

        public void Dispose()
        {
            _timeSystem.TimeData.OnMonthChanged -= PaySalary;
        }

        void IStartable.Start()
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) // 2 lvl
            {
                if (_teamSystemConfig.TwoLvlTeamConfig == null)
                    return;

                foreach (WorkerConfig workerConf in _teamSystemConfig.TwoLvlTeamConfig)
                {
                    SpawnWorker(workerConf.WorkerPrefab);
                }
            }
            
            _workerDescriptionView.ForcedHide();
        }

        private void ForceHideView()
        {
            _teamSystemView.ForcedHide();
        }

        private void PaySalary()
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    var workersToProcess = _teamSystemConfig.TwoLvlTeamConfig.ToList();
            
                    foreach (var workerConfig in workersToProcess)
                    {
                        if (!_wallet.TrySpend(WalletType.Cash, workerConfig.WorkerData.CalculateSalary((long)_teamSystemConfig.SalaryMultiplier)))
                            DismissWorker(workerConfig.WorkerData);
                    }
                    break;
                case 3:
                    var workersToProcess3 = _teamSystemConfig.ThreeLvlTeamConfig.ToList();
            
                    foreach (var workerConfig in workersToProcess3)
                    {
                        if (!_wallet.TrySpend(WalletType.Cash, workerConfig.WorkerData.CalculateSalary((long)_teamSystemConfig.SalaryMultiplier)))
                            DismissWorker(workerConfig.WorkerData);
                    }
                    break;
            }
        }

        private void SpawnPlayer()
        {
            foreach (var workerPlace in _workerPlaces)
            {
                workerPlace.SetWorker(_teamSystemConfig.PlayerPrefab);
                return;
            }
        }

        private void SpawnWorker(Worker worker)
        {
            foreach (var workerPlace in _workerPlaces)
            {
                if (workerPlace.IsFree)
                {
                    workerPlace.SetWorker(worker);
                    return;
                }
            }
        }

        public void ShowView()
        {
            if (_teamSystemView.IsActive || _teamSystemView.IsAnimated)
                return;
            
            _teamSystemView.Show();
        }

        public void HideView()
        {
            if (!_teamSystemView.IsActive || _teamSystemView.IsAnimated)
                return;
            
            _teamSystemView.Hide();
        }

        public void AddTeammate(WorkerConfig workerCardData)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    _teamSystemConfig.TwoLvlTeamConfig.Add(workerCardData);
                    _teamSystemConfig.SecondLvlUnhiredTeamConfig.Remove(workerCardData);
                    _hiredTeamController.AddTeammate(workerCardData);
                    break;
                case 3:
                    _teamSystemConfig.ThreeLvlTeamConfig.Add(workerCardData);
                    _teamSystemConfig.ThirdLvlUnhiredTeamConfig.Remove(workerCardData);
                    _hiredTeamController.AddTeammate(workerCardData);
                    break;
                case 4:
                    _teamSystemConfig.FourLvlTeamConfig.Add(workerCardData);
                    _teamSystemConfig.FourLvlUnhiredTeamConfig.Remove(workerCardData);
                    _hiredTeamController.AddTeammate(workerCardData);
                    break;
            }
            
            SpawnWorker(workerCardData.WorkerPrefab);
        }

        // maybe to HiredTeamController

        public void TryUpgradeCharacteristic(WorkerCharsEnum workerCharsEnum, WorkerData currentWorkerData)
        {
            switch (workerCharsEnum)
            {
                case WorkerCharsEnum.DataScience:
                    if (currentWorkerData.Stats.DataScienceLevel == 30) 
                        return;
                    
                    var dataScienceUpgradeCost = CalculateCost(currentWorkerData.Stats.DataScienceDefaultUpgradeCost,
                        currentWorkerData.Stats.DataScienceLevel);

                    if (_wallet.TrySpend(currentWorkerData.Stats.DataScienceWalletType, dataScienceUpgradeCost))
                    {
                        currentWorkerData.Stats.AddDataScience(1);
                        _audioController.PlayEffects(_audioController.SoundConfigs.TeammateUpgradeSound);
                    }
                    else
                        _notificationController.ShowErrorMessage("You do not have enough coins to upgrade your worker");
                    break;
                case WorkerCharsEnum.Technology:
                    if (currentWorkerData.Stats.TechnologyLevel == 30)
                        return;
                    
                    var technologyUpgradeCost = CalculateCost(currentWorkerData.Stats.TechnologyDefaultUpgradeCost,
                        currentWorkerData.Stats.TechnologyLevel);

                    if (_wallet.TrySpend(currentWorkerData.Stats.TechnologyWalletType, technologyUpgradeCost))
                    {
                        currentWorkerData.Stats.AddTechnology(1);
                        _audioController.PlayEffects(_audioController.SoundConfigs.TeammateUpgradeSound);
                    }
                    else
                        _notificationController.ShowErrorMessage("You do not have enough coins to upgrade your worker");
                    break;
                case WorkerCharsEnum.Design:
                    if (currentWorkerData.Stats.DesignLevel == 30)
                        return;
                    
                    var designUpgradeCost = CalculateCost(currentWorkerData.Stats.DesignDefaultUpgradeCost,
                        currentWorkerData.Stats.DesignLevel);

                    if (_wallet.TrySpend(currentWorkerData.Stats.DesignWalletType, designUpgradeCost))
                    {
                        _audioController.PlayEffects(_audioController.SoundConfigs.TeammateUpgradeSound);
                        currentWorkerData.Stats.AddDesign(1);
                    }
                    else
                        _notificationController.ShowErrorMessage("You do not have enough coins to upgrade your worker");
                    break;
                case WorkerCharsEnum.Research:
                    if (currentWorkerData.Stats.ResearchLevel == 30)
                        return;
                    
                    var researchUpgradeCost = CalculateCost(currentWorkerData.Stats.ResearchDefaultUpgradeCost,
                        currentWorkerData.Stats.ResearchLevel);

                    if (_wallet.TrySpend(currentWorkerData.Stats.ResearchWalletType, researchUpgradeCost))
                    {
                        currentWorkerData.Stats.AddResearch(1);
                        _audioController.PlayEffects(_audioController.SoundConfigs.TeammateUpgradeSound);
                    }
                    else
                        _notificationController.ShowErrorMessage("You do not have enough coins to upgrade your worker");
                    break;
            }
            
            _workerDescriptionView.UpdateSliders(currentWorkerData);
            _workerDescriptionView.UpdateCostUpgrades(currentWorkerData, CalculateCost);
            _workerDescriptionView.CheckMaxLevels(currentWorkerData);

            currentWorkerData.CalculateSalary((long)_teamSystemConfig.SalaryMultiplier);
            
            foreach (HiredWorkerCard currentHiredWorker in _hiredTeamController.CardPool.AllPoolObjects)
            {
                if (currentHiredWorker.CardConfig.WorkerData == currentWorkerData)
                {
                    currentHiredWorker.UpdateSalary(currentWorkerData.CurrentSalary);
                    break;
                }
            }
        }

        public long CalculateCost(long statsDataScienceDefaultUpgradeCost, int statsDataScienceLevel)
        {
            if (statsDataScienceLevel == 1)
                return statsDataScienceDefaultUpgradeCost;

            var percentToDel = statsDataScienceLevel switch
            {
                <= 10 => 10f,
                > 10 and <= 20 => 20f,
                > 20 and <= 30 => 30f,
                _ => 30f
            };

            var percent = statsDataScienceLevel * percentToDel;
            var onePercent = statsDataScienceDefaultUpgradeCost / 100;
            return (long)((onePercent * percent) + statsDataScienceDefaultUpgradeCost);
        }

        public void DismissWorker(HiredWorkerCard currentWorkerData)
        {
            if (currentWorkerData == null) 
            {
                Debug.LogError("Trying to dismiss null worker data");
                return;
            }

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    var workerConfig = _teamSystemConfig.TwoLvlTeamConfig
                        .FirstOrDefault(w => w?.WorkerData == currentWorkerData.CardConfig.WorkerData);
            
                    if (workerConfig == null) 
                        return;

                    foreach (var place in _workerPlaces)
                    {
                        if (place?.Worker?.WorkerConfig == workerConfig)
                        {
                            place.SetIsFree(true);
                            
                            if (place.Worker.gameObject != null)
                                Object.Destroy(place.Worker.gameObject);
                            
                            place.ClearWorker();
                            break;
                        }
                    }

                    _teamSystemConfig.TwoLvlTeamConfig.Remove(workerConfig);
            
                    _teamSystemConfig.SecondLvlUnhiredTeamConfig.Add(workerConfig);
            
                    _hiredTeamController?.DeleteCard(currentWorkerData);
                    _unhiredTeamController?.AddToPool(workerConfig);
            
                    _notificationController.ShowErrorMessage($"{workerConfig.WorkerData.Name} has dismissed");
                    break;
                case 3:
                    var workerConfig3 = _teamSystemConfig.ThreeLvlTeamConfig
                        .FirstOrDefault(w => w?.WorkerData == currentWorkerData.CardConfig.WorkerData);
            
                    if (workerConfig3 == null) 
                        return;

                    foreach (var place in _workerPlaces)
                    {
                        if (place?.Worker?.WorkerConfig == workerConfig3)
                        {
                            place.SetIsFree(true);
                            
                            if (place.Worker.gameObject != null)
                                Object.Destroy(place.Worker.gameObject);
                            
                            place.ClearWorker();
                            break;
                        }
                    }

                    _teamSystemConfig.ThreeLvlTeamConfig.Remove(workerConfig3);
            
                    _teamSystemConfig.ThirdLvlUnhiredTeamConfig.Add(workerConfig3);
            
                    _hiredTeamController?.DeleteCard(currentWorkerData);
                    _unhiredTeamController?.AddToPool(workerConfig3);
            
                    _notificationController.ShowErrorMessage($"{workerConfig3.WorkerData.Name} has dismissed");
                    break;
            }
            
            _workerDescriptionView.ForcedHide();
        }

        private void DismissWorker(WorkerData currentWorkerData)
        {
            if (currentWorkerData == null) 
            {
                Debug.LogError("Trying to dismiss null worker data");
                return;
            }

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    var workerConfig = _teamSystemConfig.TwoLvlTeamConfig
                        .FirstOrDefault(w => w?.WorkerData == currentWorkerData);
            
                    if (workerConfig == null) 
                        return;

                    foreach (var place in _workerPlaces)
                    {
                        if (place?.Worker?.WorkerConfig == workerConfig)
                        {
                            place.SetIsFree(true);
                            
                            if (place.Worker.gameObject != null)
                                Object.Destroy(place.Worker.gameObject);
                            
                            place.ClearWorker();
                            break;
                        }
                    }
                    _teamSystemConfig.TwoLvlTeamConfig.Remove(workerConfig);
            
                    _teamSystemConfig.SecondLvlUnhiredTeamConfig.Add(workerConfig);
            
                    _hiredTeamController?.DeleteCard(currentWorkerData);
                    _unhiredTeamController?.AddToPool(workerConfig);
            
                    _notificationController.ShowErrorMessage($"{workerConfig.WorkerData.Name} has dismissed");
                    break;
                case 3:
                    var workerConfig3 = _teamSystemConfig.ThreeLvlTeamConfig
                        .FirstOrDefault(w => w?.WorkerData == currentWorkerData);
            
                    if (workerConfig3 == null) 
                        return;

                    foreach (var place in _workerPlaces)
                    {
                        if (place?.Worker?.WorkerConfig == workerConfig3)
                        {
                            place.SetIsFree(true);
                            
                            if (place.Worker.gameObject != null)
                                Object.Destroy(place.Worker.gameObject);
                            
                            place.ClearWorker();
                            break;
                        }
                    }
                    _teamSystemConfig.ThreeLvlTeamConfig.Remove(workerConfig3);
            
                    _teamSystemConfig.ThirdLvlUnhiredTeamConfig.Add(workerConfig3);
            
                    _hiredTeamController?.DeleteCard(currentWorkerData);
                    _unhiredTeamController?.AddToPool(workerConfig3);
            
                    _notificationController.ShowErrorMessage($"{workerConfig3.WorkerData.Name} has dismissed");
                    break;
            }
            
            _workerDescriptionView.Hide();
        }

        public void HideDescriptionView()
        {
            _workerDescriptionView.ForcedHide();
        }
    }
}
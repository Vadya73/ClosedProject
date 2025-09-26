using System.Collections.Generic;
using Infrastructure;
using Services;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class UnhiredTeamController : IInitializable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly UnhiredTeamView _unhiredTeamView;
        private readonly TeamSystemConfig _teamSystemConfig;
        private readonly Wallet _wallet;
        private readonly NotificationController _notificationController;
        private List<WorkerConfig> _currentUnhiredWorkers;
        private ObjectPool<UnhiredWorkerCard> _cardPool;
        private TeamSystemController _teamSystemController;
        
        private const float CardWidth = 600f;

        [Inject]
        public UnhiredTeamController(IObjectResolver objectResolver,UnhiredTeamView unhiredTeamView, TeamSystemConfig teamSystemConfig, 
            Wallet wallet, NotificationController notificationController)
        {
            _objectResolver = objectResolver;
            _unhiredTeamView = unhiredTeamView;
            _teamSystemConfig = teamSystemConfig;
            _wallet = wallet;
            _notificationController = notificationController;
        }
        
        public void Initialize()
        {
            _teamSystemController = _objectResolver.Resolve<TeamSystemController>();
            
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    return;
                case 2:
                    _currentUnhiredWorkers = _teamSystemConfig.SecondLvlUnhiredTeamConfig;
                    
                    _cardPool = new ObjectPool<UnhiredWorkerCard>(_teamSystemConfig.UnhiredWorkerCardPrefab, 
                        _currentUnhiredWorkers.Count, _unhiredTeamView.WorkersContainer);
                    break;
                case 3:
                    _currentUnhiredWorkers = _teamSystemConfig.ThirdLvlUnhiredTeamConfig;

                    _cardPool = new ObjectPool<UnhiredWorkerCard>(_teamSystemConfig.UnhiredWorkerCardPrefab, 
                        _currentUnhiredWorkers.Count, _unhiredTeamView.WorkersContainer);
                    break;
                case 4:
                    _currentUnhiredWorkers = _teamSystemConfig.FourLvlUnhiredTeamConfig;

                    _cardPool = new ObjectPool<UnhiredWorkerCard>(_teamSystemConfig.UnhiredWorkerCardPrefab, 
                        _currentUnhiredWorkers.Count, _unhiredTeamView.WorkersContainer);
                    break;
            }
            
            _cardPool.Initialize();
            
            foreach (WorkerConfig unhiredWorker in _currentUnhiredWorkers)
            {
                UnhiredWorkerCard card = _cardPool.Spawn();
                card.UpdateData(unhiredWorker, TryPurchaseCard);
            }

            _unhiredTeamView.RectTransform.sizeDelta = new Vector2(CardWidth *_currentUnhiredWorkers.Count, _unhiredTeamView.RectTransform.sizeDelta.y);
        }
        
        private void TryPurchaseCard(UnhiredWorkerCard unhiredWorkerCard)
        {
            int freeWorkerPlaces = 0;
            foreach (var workerPlace in _teamSystemController.WorkerPlaces)
            {
                if (workerPlace.IsFree)
                    freeWorkerPlaces++;
            }

            if (freeWorkerPlaces <= 0)
            {
                _notificationController.ShowErrorMessage("No free worker places were found");
                return;
            }
            
            if (_wallet.HasMoney(walletType: WalletType.Cash, unhiredWorkerCard.CardConfig.WorkerData.CurrentSalary))
            {
                _teamSystemController.AddTeammate(unhiredWorkerCard.CardConfig);
                _cardPool.Despawn(unhiredWorkerCard);
                _unhiredTeamView.RectTransform.sizeDelta = new Vector2(CardWidth *_currentUnhiredWorkers.Count, _unhiredTeamView.RectTransform.sizeDelta.y);
            }
            else
            {
                _notificationController.ShowErrorMessage("No money");
            }
        }

        public void AddToPool(WorkerConfig workerConfig)
        {
            var inst = _cardPool.Spawn();
            inst.UpdateData(workerConfig, TryPurchaseCard);
            _unhiredTeamView.RectTransform.sizeDelta = new Vector2(CardWidth *_currentUnhiredWorkers.Count, _unhiredTeamView.RectTransform.sizeDelta.y);
        }

        public void ShowView()
        {
            _unhiredTeamView.ForcedShow();
        }

        public void HideView()
        {
            _unhiredTeamView.ForcedHide();
        }
    }
}
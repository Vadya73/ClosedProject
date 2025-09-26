using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class HiredTeamController : IInitializable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly HiredTeamView _hiredTeamView;
        private readonly TeamSystemConfig _teamSystemConfig;
        private readonly WorkerDescriptionView _workerDescriptionView;
        private TeamSystemController _teamSystemController;
        private List<WorkerConfig> _currentHiredWorkers;
        private ObjectPool<HiredWorkerCard> _cardPool;
        private const float CardWidth = 600f;

        public ObjectPool<HiredWorkerCard> CardPool => _cardPool;


        [Inject]
        public HiredTeamController(IObjectResolver objectResolver,HiredTeamView hiredTeamView, TeamSystemConfig teamSystemConfig, 
            WorkerDescriptionView workerDescriptionView)
        {
            _objectResolver = objectResolver;
            _hiredTeamView = hiredTeamView;
            _teamSystemConfig = teamSystemConfig;
            _workerDescriptionView = workerDescriptionView;
        }

        public void Initialize()
        {
            _teamSystemController = _objectResolver.Resolve<TeamSystemController>();
            
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    return;
                case 2:
                    _currentHiredWorkers = _teamSystemConfig.TwoLvlTeamConfig;
                    
                    _cardPool = new ObjectPool<HiredWorkerCard>(_teamSystemConfig.HiredWorkerCardPrefab, 
                        _currentHiredWorkers.Count, _hiredTeamView.WorkersContainer);
                    break;
                case 3:
                    _currentHiredWorkers = _teamSystemConfig.ThreeLvlTeamConfig;

                    _cardPool = new ObjectPool<HiredWorkerCard>(_teamSystemConfig.HiredWorkerCardPrefab, 
                        _currentHiredWorkers.Count, _hiredTeamView.WorkersContainer);
                    break;
                case 4:
                    _currentHiredWorkers = _teamSystemConfig.FourLvlTeamConfig;

                    _cardPool = new ObjectPool<HiredWorkerCard>(_teamSystemConfig.HiredWorkerCardPrefab, 
                        _currentHiredWorkers.Count, _hiredTeamView.WorkersContainer);
                    break;
            }
            
            _cardPool.Initialize();
            
            foreach (WorkerConfig hiredWorker in _currentHiredWorkers)
            {
                HiredWorkerCard card = _cardPool.Spawn();
                card.UpdateData(hiredWorker,  ShowWorkerDescription);
            }
            
            HiredWorkerCard playerCard = _cardPool.Spawn();
            playerCard.UpdateData(_teamSystemConfig.PlayerPrefab.WorkerConfig,  ShowWorkerDescription);

            _hiredTeamView.ContainerRectTransform.sizeDelta = new Vector2(CardWidth * (_currentHiredWorkers.Count + 1), _hiredTeamView.ContainerRectTransform.sizeDelta.y);
        }

        private void ShowWorkerDescription(HiredWorkerCard workerCard)
        {
            _workerDescriptionView.Show();
            _workerDescriptionView.UpdateView(workerCard.CardConfig.WorkerData);
            _workerDescriptionView.UpdateCostUpgrades(workerCard.CardConfig.WorkerData, _teamSystemController.CalculateCost);
            _workerDescriptionView.SetCard(workerCard);
            _workerDescriptionView.CheckMaxLevels(workerCard.CardConfig.WorkerData);
        }

        public void ShowView()
        {
            _hiredTeamView.ForcedShow();
        }

        public void HideView()
        {
            _hiredTeamView.ForcedHide();
        }

        public void AddTeammate(WorkerConfig workerCardData)
        {
            var inst = _cardPool.Spawn();
            inst.UpdateData(workerCardData, ShowWorkerDescription);
            _hiredTeamView.ContainerRectTransform.sizeDelta = new Vector2(CardWidth * (_currentHiredWorkers.Count + 1), _hiredTeamView.ContainerRectTransform.sizeDelta.y);
        }

        public void DeleteCard(HiredWorkerCard worker)
        {
            _cardPool.Despawn(worker);
            _hiredTeamView.ContainerRectTransform.sizeDelta = new Vector2(CardWidth * (_currentHiredWorkers.Count + 1), _hiredTeamView.ContainerRectTransform.sizeDelta.y);
            Debug.Log("delete card");
        }
        
        public void DeleteCard(WorkerData worker)
        {
            _hiredTeamView.ContainerRectTransform.sizeDelta = new Vector2(CardWidth * (_currentHiredWorkers.Count + 1), _hiredTeamView.ContainerRectTransform.sizeDelta.y);
            
            var poolItems = _cardPool.AllPoolObjects.ToList();
    
            foreach (var poolObject in poolItems)
            {
                if (poolObject.CardConfig.WorkerData.Name == worker.Name)
                {
                    _cardPool.Despawn(poolObject);
                    Debug.Log($"Despawned card: {worker.Name}");
                    return;
                }
            }
    
            Debug.LogWarning($"Card with worker {worker.Name} not found in pool");
        }
    }
}
using System;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Core
{
    public class BubbleSystem : IInitializable, IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly TeamSystemController _teamSystemController;
        private readonly Camera _camera;
        private readonly BubbleSystemView _bubbleSystemView;
        private readonly TimeSystem _timeSystem;
        private readonly Bubble _bubblePrefab;
        private readonly BubblesConfig _bubbleConfig;

        private ProductionController _productionController;
        private ProductionProgressBarView _progressBarView;
        private ObjectPool<Bubble> _bubblePool;
        private ProductionProgressController _productionProgressController;
        private List<Sequence> _sequence;

        [Inject]
        public BubbleSystem(IObjectResolver objectResolver, TeamSystemController teamSystemController, Camera camera, BubbleSystemView bubbleSystemView, 
            PlayerConfig playerConfig, TimeSystem timeSystem)
        {
            _objectResolver = objectResolver;
            _teamSystemController = teamSystemController;
            _camera = camera;
            _bubbleSystemView = bubbleSystemView;
            _timeSystem = timeSystem;
            _bubblePrefab = playerConfig.BubblePrefab;
            _bubbleConfig = playerConfig.BubbleConfig;
        }

        public void Initialize()
        {
            _progressBarView = _objectResolver.Resolve<ProductionProgressBarView>();
            _productionProgressController = _objectResolver.Resolve<ProductionProgressController>();
            _productionController = _objectResolver.Resolve<ProductionController>();
            
            _bubblePool = new ObjectPool<Bubble>(_bubblePrefab, 10,_bubbleSystemView.BubbleContainer);
            
            _bubblePool.Initialize();
            _sequence = new List<Sequence>();
        }

        public void SpawnBubble(Transform bubbleSpawnPosition)
        {
            Vector3 spawnPos = _camera.WorldToScreenPoint(bubbleSpawnPosition.position);
            Bubble spawnBubble = _bubblePool.Spawn();
            spawnBubble.transform.position = spawnPos;
            spawnBubble.transform.localScale = Vector3.zero;
            SetBubbleType(spawnBubble);

            spawnBubble.transform.DOMoveY(spawnPos.y + 50f, .3f);
            spawnBubble.ShowSequence = DOTween.Sequence()
                .Append(spawnBubble.transform.DOScale(Vector3.one, .3f))
                .OnComplete(() => spawnBubble.Move(_progressBarView.transform.position, DespawnBubble));
        }


        public void SpawnWorkerBubbles()
        {
            var places = _teamSystemController.WorkerPlaces;
            
            for (int i = 0; i < places.Length; i++)
            {
                var place = places[i];
                if (place?.Worker == null)
                    continue;

                if (HasWorkerCooldown(place))
                    continue;

                SpawnBubble(place.Worker.BubbleSpawnPoint);
                
                place.Worker.WorkerConfig?.WorkerData?
                    .ResetBubbleCooldown(_productionController.CurrentProductionType, _timeSystem.TimeData);
            }
        }
        
        private void SetBubbleType(Bubble spawnBubble)
        {
            switch (_productionController.CurrentProductionType)
            {
                case ProductionType.BuildRobot:
                    if (Random.Range(0, 2) == 0)
                        spawnBubble.SetBubbleType(BubbleType.BRYellow, _bubbleConfig);
                    else
                        spawnBubble.SetBubbleType(BubbleType.BROrange, _bubbleConfig);
                    break;
                case ProductionType.TrainingAI:
                    spawnBubble.SetBubbleType(BubbleType.AIBlue, _bubbleConfig);
                    break;
                case ProductionType.Research:
                    spawnBubble.SetBubbleType(BubbleType.ResearchGreen, _bubbleConfig);
                    break;
            }
        }

        private bool HasWorkerCooldown(WorkerPlace workerPlace)
        {
            var data = workerPlace?.Worker?.WorkerConfig?.WorkerData;
            if (data == null)
                return true;

            float cooldown = _productionController.CurrentProductionType switch
            {
                ProductionType.BuildRobot => data.BuildRobotBubbleCooldown,
                ProductionType.TrainingAI => data.TrainingAiBubbleCooldown,
                ProductionType.Research => data.ResearchBubbleCooldown,
                _ => 0f
            };

            if (cooldown > 0f)
            {
                data.MinusCooldown(Time.deltaTime,
                    _productionController.CurrentProductionType);
                return true;
            }

            return false;

        }

        private void DespawnBubble(Bubble bubble)
        {
            _bubblePool.Despawn(bubble);

            _productionProgressController.AddBubble(bubble.BubbleType);
        }

        public void Dispose()
        {
            _sequence.ForEach(x => x.Kill());
        }
    }

    public enum BubbleType
    {
        AIBlue = 0, // AI Training
        ResearchGreen = 1, // Research
        BRYellow = 2, // BuildRobot
        BROrange = 3, // BuildRobot
    }
}
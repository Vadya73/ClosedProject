using Core;
using UnityEngine;
using VContainer;

namespace Meta
{
    public class SceneDataCenter : MonoBehaviour
    {
        [SerializeField] private DataCenterLevel[] _levels;
        
        private ProductionProgressController _productionProgressController;
        private BubbleSystem _bubbleSystem;
        private ResearchController _researchController;
        private bool _isActive;
        private int _currentLevel;

        [Inject]
        private void Construct(ProductionProgressController productionProgressController, BubbleSystem bubbleSystem, 
            ResearchController researchController)
        {
            _productionProgressController = productionProgressController;
            _bubbleSystem = bubbleSystem;
            _researchController = researchController;
        }

        private void Awake()
        {
            _productionProgressController.OnProgressStart += StartWork;
            _productionProgressController.OnProgressEnd += StopWork;

            foreach (var level in _levels)
            {
                level.LevelConfig.SetCurrentSpawnCooldown(level.LevelConfig.BubbleSpawnCooldown);
                
                if (!level.LevelConfig.HasBuy)
                    level.VisualGameobject.SetActive(false);
            }
            
            _researchController.SetDataCenter(this);
        }

        private void Update()
        {
            if (!_isActive)
                return;

            SpawnBubbles();
        }

        private void OnDestroy()
        {
            _productionProgressController.OnProgressStart -= StartWork;
            _productionProgressController.OnProgressEnd -= StopWork;
        }

        private void SpawnBubbles()
        {
            foreach (var level in _levels)
            {
                if (!level.LevelConfig.HasBuy) 
                    continue;
                
                level.LevelConfig.SubtractCurrentBubbleSpawnCooldown(Time.deltaTime);

                if (level.LevelConfig.CurrentBubbleSpawnCooldown <= 0)
                {
                    _bubbleSystem.SpawnBubble(level.BubbleSpawnPosition);
                    level.LevelConfig.SetCurrentSpawnCooldown(level.LevelConfig.BubbleSpawnCooldown);
                    return;
                }
            }
        }

        private void StartWork() => _isActive = true;
        
        private void StopWork(ProductionType arg1, int arg2, int arg3) => _isActive = false;

        public void AddDataCenter()
        {
            if (_currentLevel >= _levels.Length)
                return;

            _levels[_currentLevel].SetAcivateLevel(true);
            _currentLevel++;
        }
    }
}
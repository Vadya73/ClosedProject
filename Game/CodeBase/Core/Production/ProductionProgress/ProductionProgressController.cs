using System;
using DG.Tweening;
using Infrastructure;
using Services;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public sealed class ProductionProgressController : IInitializable, ITickable, IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ProductionProgressBarView _progressView;
        private readonly TimeSystem _timeSystem;
        private readonly BubbleSystem _bubbleSystem;
        private readonly AudioController _audioController;
        private readonly Wallet _wallet;

        private TeamSystemController _teamSystemController;
        private int _daysToResearch;
        private int _daysRemainingToResearch;
        private bool _isActive = false;
        private ProductionType _productionType;
        private bool _researchTypeActive;
        
        private int _firstBubbleCountRequired; // Technology BUILD ROBOT
        private int _firstBubbleCountCurrent;
        private int _secondBubbleCountRequired; // DESIGN BUILD ROBOT
        private int _secondBubbleCountCurrent;
        
        private int _cashSpend;
        private WalletType _walletTypeSpend;

        private Sequence _progressSequence;

        public event Action OnProgressStart;
        public event Action<ProductionType, int, int> OnProgressEnd;
        public event Action OnProgressCancel;

        [Inject]
        public ProductionProgressController(IObjectResolver objectResolver,ProductionProgressBarView progressView, 
            TimeSystem timeSystem, BubbleSystem bubbleSystem, AudioController audioController, Wallet wallet)
        {
            _objectResolver = objectResolver;
            _progressView = progressView;
            _timeSystem = timeSystem;
            _bubbleSystem = bubbleSystem;
            _audioController = audioController;
            _wallet = wallet;

            _timeSystem.TimeData.OnDayChanged += DayChanged;
        }

        public void Initialize()
        {
            _teamSystemController = _objectResolver.Resolve<TeamSystemController>();
            if (_isActive == false)
                _progressView.ForcedHide();

            foreach (var workerPlace in _teamSystemController.WorkerPlaces)
            {
                if (workerPlace.Worker == null)
                    return;
                
                workerPlace.Worker.WorkerConfig.WorkerData.ResetAllBubbleCooldown(_timeSystem.TimeData);
            }
        }

        public void Tick()
        {
            if (!_isActive)
                return;

            _bubbleSystem.SpawnWorkerBubbles();
        }

        private void DayChanged()
        {
            if (!_isActive)
                return;

            if (_researchTypeActive)
            {
                if (_firstBubbleCountCurrent >= _firstBubbleCountRequired)
                {
                    FinishProgress();
                    return;
                }
                
                float percent = _firstBubbleCountCurrent / _firstBubbleCountRequired; 
                _progressView.UpdateSliderValue(percent);

                
                return;
            }
            
            _daysRemainingToResearch -= 1;
            ProgressUpdate();
            if (_daysRemainingToResearch == 0)
                DOVirtual.DelayedCall(2f, FinishProgress);
        }
        
        public void StartProgress(ProductionType productionType, int researchDays, int firstBubbleCount, int secondBubbleCount = 0)
        {
            _isActive = true;

            _productionType = productionType;
            _daysToResearch = researchDays;
            _daysRemainingToResearch = researchDays;

            _firstBubbleCountRequired = firstBubbleCount;
            _firstBubbleCountCurrent = 0;

            _secondBubbleCountRequired = secondBubbleCount;
            _secondBubbleCountCurrent = 0;

            _progressView.Show(_productionType, firstBubbleCount, secondBubbleCount);

            OnProgressStart?.Invoke();
        }

        
        public void StartProgressWithBubbles(ProductionType productionType, SkillData bubbleCount)
        {
            OnProgressStart?.Invoke();

            _cashSpend = bubbleCount.Cost;
            _walletTypeSpend = bubbleCount.WalletType;
            _progressView.CancelButton.interactable = true;
            
            _firstBubbleCountRequired = bubbleCount.RequiredResearchBubbles;
            _firstBubbleCountCurrent = 0;
            
            _researchTypeActive = true;
            _isActive = true;
            _progressView.Show(productionType, bubbleCount.RequiredResearchBubbles);
            _productionType = productionType;
        }

        private void FinishProgress()
        {
            OnProgressEnd?.Invoke(_productionType, _firstBubbleCountCurrent, _secondBubbleCountCurrent);
            
            _audioController.PlayEffects(_audioController.SoundConfigs.CompleteProduction);
            
            _isActive = false;
            _researchTypeActive = false;
            _progressSequence = DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() => _progressView.Hide());
        }

        private void ProgressUpdate()
        {
            float passedDays = _daysToResearch - _daysRemainingToResearch;
            float onePercent = _daysToResearch / 100f;
            float percent = passedDays / onePercent;

            _progressView.UpdateSliderValue(percent / 100f);
        }

        public void AddBubble(BubbleType bubbleBubbleType)
        {
            switch (bubbleBubbleType)
            {
                case BubbleType.AIBlue:
                    _firstBubbleCountCurrent += 1;
                    break;
                case BubbleType.ResearchGreen:
                    _firstBubbleCountCurrent += 1;
                    break;
                case BubbleType.BRYellow:
                    _secondBubbleCountCurrent += 1;
                    break;
                case BubbleType.BROrange:
                    _firstBubbleCountCurrent += 1;
                    break;
            }

            _progressView.UpdateBubbles(_firstBubbleCountCurrent, _firstBubbleCountRequired, 
                _secondBubbleCountCurrent, _secondBubbleCountRequired);
        }

        public void Dispose()
        {
            _progressSequence.Kill();
        }

        public void CancelProgress()
        {
            _progressView.CancelButton.interactable = false;
            
            OnProgressCancel?.Invoke();
            
            _isActive = false;
            _researchTypeActive = false;
            _progressSequence = DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() => _progressView.Hide());
            
            _wallet.AddWalletCount(_walletTypeSpend, _cashSpend);
        }
    }
}
using System.Collections;
using DG.Tweening;
using Infrastructure;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Meta.RouletteWheel
{
    public class WheelOfFortuneController : IStartable, ITickable
    {
        private readonly WheelOfFortuneView _wheelOfFortuneView;
        private readonly Wallet _wallet;
        private readonly MonoHelper _monoHelper;
        private readonly WheelOfFortune _wheelOfFortuneMono;
        
        private bool _isSpinning;
        private float _cooldownRemaining;
        
        [Inject]
        public WheelOfFortuneController( WheelOfFortuneView wheelOfFortuneView, Wallet wallet, MonoHelper monoHelper, 
            WheelOfFortune wheelOfFortuneMono)
        {
            _wheelOfFortuneView = wheelOfFortuneView;
            _wallet = wallet;
            _monoHelper = monoHelper;
            _wheelOfFortuneMono = wheelOfFortuneMono;
        }
        
        public void Start()
        {
            _wheelOfFortuneView.ForcedHide();
            _wheelOfFortuneMono.CooldownText.gameObject.SetActive(false);
            _wheelOfFortuneView.CooldownTextUI.gameObject.SetActive(false);
        }

        public void Tick()
        {
            if (_cooldownRemaining > 0)
            {
                _cooldownRemaining -= Time.deltaTime;
                _wheelOfFortuneMono.UpdateCooldownText(Mathf.CeilToInt(_cooldownRemaining).ToString());
                _wheelOfFortuneView.UpdateCooldownTextUI(Mathf.CeilToInt(_cooldownRemaining).ToString());
                
                if (_cooldownRemaining <= 0)
                {
                    _wheelOfFortuneView.SpinButton.interactable = true;
                    _wheelOfFortuneMono.CooldownText.gameObject.SetActive(false);
                    _wheelOfFortuneView.CooldownTextUI.gameObject.SetActive(false);
                }
            }
        }

        public void HideView()
        {
            _wheelOfFortuneView.Hide();
        }

        public void OnSpinClicked()
        {
            if (_isSpinning || _cooldownRemaining > 0) return;

            _wheelOfFortuneView.SpinButton.interactable = false;
            Spin();
        }

        public void GiveReward(WheelReward reward)
        {
            Debug.Log($"Выдана награда: {reward.RewardType} x{reward.Amount}");
            switch (reward.RewardType)
            {
                case RewardType.None:
                    break;
                case RewardType.Cash:
                    _wallet.AddWalletCount(WalletType.Cash, reward.Amount);
                    break;
                case RewardType.Tokens:
                    _wallet.AddWalletCount(WalletType.Tokens, reward.Amount);
                    break;
                case RewardType.GreenCard:
                    _wallet.AddWalletCount(WalletType.GreenCard, reward.Amount);
                    break;
                case RewardType.PurpleCard:
                    _wallet.AddWalletCount(WalletType.PurpleCard, reward.Amount);
                    break;
            }
        }

        private void Spin()
        {
            _monoHelper.StartCoroutine(SpinRoutine());
        }

        private IEnumerator SpinRoutine()
        {
            _isSpinning = true;
            
            int sectorIndex = Random.Range(0, _wheelOfFortuneMono.Config.rewards.Length);

            float sectorAngle = 360f / _wheelOfFortuneMono.Config.rewards.Length;
            float targetAngle = 360f * _wheelOfFortuneMono.Config.extraRounds + sectorIndex * sectorAngle + sectorAngle / 2;

            yield return _wheelOfFortuneMono.WheelTransform
                .DORotate(new Vector3(0, 0, -targetAngle), _wheelOfFortuneMono.Config.spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .WaitForCompletion();

            var reward = _wheelOfFortuneMono.Config.rewards[sectorIndex];
            GiveReward(reward);

            _cooldownRemaining = _wheelOfFortuneMono.Config.cooldownSeconds;
            _wheelOfFortuneMono.CooldownText.gameObject.SetActive(true);
            _wheelOfFortuneView.CooldownTextUI.gameObject.SetActive(true);

            _isSpinning = false;
        }
    }
}

using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Infrastructure.AdMob
{
    public class RewardedAds
    {
        private RewardedAd _rewarded;
        private string _adUnitId = "ca-app-pub-xxx/aaa";

        public void LoadRewarded()
        {
            var request = new AdRequest();

            RewardedAd.Load(_adUnitId, request, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError($"Ошибка загрузки Rewarded: {error}");
                    return;
                }

                _rewarded = ad;
                Debug.Log("Rewarded загружен");

                _rewarded.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Rewarded закрыт");
                    LoadRewarded(); // перезагрузка
                };
            });
        }

        public void ShowRewarded(Action<Reward> onRewardEarned)
        {
            if (_rewarded != null && _rewarded.CanShowAd())
            {
                _rewarded.Show(reward =>
                {
                    Debug.Log($"Игрок получил награду: {reward.Amount} {reward.Type}");
                    onRewardEarned?.Invoke(reward);
                });
            }
            else
            {
                Debug.LogWarning("Rewarded не готов");
            }
        }
    }
}
using GoogleMobileAds.Api;
using UnityEngine;

namespace Infrastructure.AdMob
{
    public class InterstitialAds
    {
        private InterstitialAd _interstitial;
        private string _adUnitId = "ca-app-pub-xxx/zzz";

        public void LoadAd()
        {
            var request = new AdRequest();

            InterstitialAd.Load(_adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError($"Ошибка загрузки Interstitial: {error}");
                    return;
                }

                _interstitial = ad;
                Debug.Log("Interstitial загружен");

                _interstitial.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Interstitial закрыт");
                    LoadAd(); // Подгружаем новый после закрытия
                };
            });
        }

        public void ShowAd()
        {
            if (_interstitial != null && _interstitial.CanShowAd())
            {
                _interstitial.Show();
            }
            else
            {
                Debug.LogWarning("Interstitial не готов");
            }
        }
    }
}
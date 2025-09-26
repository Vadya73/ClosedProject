using GoogleMobileAds.Api;
using UnityEngine;

namespace Infrastructure.AdMob
{
    public class BannerAds
    {
        private BannerView _banner;

        public void ShowBanner(string adUnitId = "ca-app-pub-xxx/yyy")
        {
            if (_banner != null)
            {
                _banner.Destroy();
                _banner = null;
            }

            _banner = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

            _banner.OnBannerAdLoaded += () => Debug.Log("Баннер загружен");
            _banner.OnBannerAdLoadFailed += (err) => Debug.LogError($"Ошибка загрузки баннера: {err}");

            var request = new AdRequest();
            _banner.LoadAd(request);
        }

        public void HideBanner() => _banner?.Hide();

        public void DestroyBanner()
        {
            _banner?.Destroy();
            _banner = null;
        }
    }
}
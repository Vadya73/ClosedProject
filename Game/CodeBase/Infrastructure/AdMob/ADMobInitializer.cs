using UnityEngine;
using GoogleMobileAds.Api;

namespace Infrastructure.AdMob
{
    public class ADMobInitializer : MonoBehaviour
    {
        private void Start()
        {
            MobileAds.Initialize(initStatus =>
            {
                Debug.Log("AdMob инициализирован!");
            });
        }
    }
}

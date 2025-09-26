using UnityEngine;
using UnityEngine.Rendering;

namespace Infrastructure
{
    public class MonoHelper : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
    
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Physics.autoSyncTransforms = false;
    
            Graphics.activeTier = GraphicsTier.Tier2;
        }
    }
}
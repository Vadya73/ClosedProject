using UnityEngine;

[CreateAssetMenu(menuName = "Sound Configs", fileName = "SoundConfigs", order = 0)]
public class SoundConfigs : ScriptableObject
{
    [SerializeField] private AudioClip _defaultClickSound;
    [SerializeField] private AudioClip _shopPurchaseSound;
    [SerializeField] private AudioClip _teammateUpgradeSound;
    [SerializeField] private AudioClip _completeProduction;
    [SerializeField] private AudioClip _notificationSound;
    [SerializeField] private AudioClip _addCashSound;
    [SerializeField] private AudioClip _addTokensSound;
    
    public AudioClip DefaultClickSound => _defaultClickSound;
    public AudioClip ShopPurchaseSound => _shopPurchaseSound;
    public AudioClip TeammateUpgradeSound => _teammateUpgradeSound;
    public AudioClip CompleteProduction => _completeProduction;
    public AudioClip NotificationSound => _notificationSound;
    public AudioClip AddCashSound => _addCashSound;
    public AudioClip AddTokensSound => _addTokensSound;
}
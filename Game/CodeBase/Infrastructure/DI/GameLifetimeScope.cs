using Core;
using Core.CameraControl;
using DEV;
using Services;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private TimeData _timeData;
        [SerializeField] private WalletConfig _walletConfig;
        [SerializeField] private SoundConfigs _soundConfig;

        protected override void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_soundConfig);
            builder.RegisterInstance(_playerConfig);
            
            // builder.RegisterEntryPoint<FirebaseManager>().AsSelf();

            builder.RegisterComponentInHierarchy<MonoHelper>();
            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterComponentInHierarchy<CameraController>();
            builder.RegisterComponentInHierarchy<AudioController>(); 
            builder.RegisterComponentInHierarchy<LoadScreen>(); 
            
            RegisterNotification(builder);
            RegisterWalletService(builder);
            TimeSystemRegister(builder);
            
            RegisterDevTools(builder);
        }

        private void RegisterDevTools(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DevToolsView>();
            builder.Register<DevToolsController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<DevToolsObserver>().AsSelf();
        }

        private void RegisterWalletService(IContainerBuilder builder)
        {
            builder.Register<Wallet>(Lifetime.Singleton).WithParameter(_walletConfig);
        }

        private void RegisterNotification(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<NotificationView>();
            
            builder.RegisterEntryPoint<NotificationController>().AsSelf();
        }
        
        private void TimeSystemRegister(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TimeSystem>().WithParameter(_timeData).AsSelf();
        }
        

    }
}

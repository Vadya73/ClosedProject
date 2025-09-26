using Core;
using Meta;
using Meta.AllLeaderboards;
using Meta.RouletteWheel;
using Meta.Shop;
using Services;
using UI;
using UI.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    public class LevelLifetimeScope : LifetimeScope
    {
        [SerializeField] private ProductSaleConfig _productSaleConfig;
        [SerializeField] private RobotsDataConfig _robotsDataConfig;
        [SerializeField] private AiDataConfig _aiDataConfig;
        [SerializeField] private AllTaskConfigs _allTasksConfig;
        [SerializeField] private TeamSystemConfig _teamSystemConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterShop(builder);
            RegisterRoulette(builder);
            RegisterTeamSystem(builder);
            TaskSystemRegister(builder);
            RegisterDialogueService(builder);
            RegisterLeaderboards(builder);
            RegisterRobotPresentationView(builder);
            RegisterAiPresentationView(builder);
            RegisterRobotHolder(builder);
            RegisterProductSale(builder);
            RegisterWalletViewService(builder);
            BubbleSystemRegister(builder);
            ProgressBarRegister(builder);   // ok -> TimeSystem
            
            BuildRobotRegister(builder);    // ok -> Production, Progressbar
            TrainingAiRegister(builder);    // -> Progress, Production
            ResearchRegister(builder);
                
            ProductionRegister(builder);    // ok -> buildRobot, trainingAI, research
            SettingsUIRegister(builder);
            LevelUIRegister(builder);       // ok -> Production
        }

        private void SettingsUIRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<SettingsView>();
            builder.RegisterEntryPoint<SettingsController>().AsSelf();

            builder.RegisterEntryPoint<SettingsObserver>().AsSelf();
        }

        private void RegisterShop(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ShopView>();
            builder.RegisterEntryPoint<ShopController>().AsSelf();

            builder.RegisterEntryPoint<ShopObserver>().AsSelf();
        }

        private void RegisterRoulette(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<WheelOfFortuneView>();
            builder.RegisterComponentInHierarchy<WheelOfFortune>();
            
            builder.RegisterEntryPoint<WheelOfFortuneController>().AsSelf();
            builder.RegisterEntryPoint<WheelOfFortuneObserver>().AsSelf();
        }

        private void RegisterTeamSystem(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<WorkerDescriptionView>();
            // Hired
            builder.RegisterComponentInHierarchy<HiredTeamView>();
            builder.RegisterEntryPoint<HiredTeamController>().WithParameter(_teamSystemConfig).AsSelf();
            // Unhired
            builder.RegisterComponentInHierarchy<UnhiredTeamView>();
            builder.RegisterEntryPoint<UnhiredTeamController>().WithParameter(_teamSystemConfig).AsSelf();
            // Humanoids
            builder.RegisterComponentInHierarchy<HumanoidsTeamView>();
            builder.RegisterEntryPoint<HumanoidsTeamController>().WithParameter(_teamSystemConfig).AsSelf();
            // Main Team
            builder.RegisterComponentInHierarchy<TeamSystemView>();
            builder.RegisterEntryPoint<TeamSystemController>().WithParameter(_teamSystemConfig).AsSelf();

            builder.RegisterEntryPoint<TeamSystemObserver>().AsSelf();
        }

        private void RegisterLeaderboards(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LeaderboardView>();
            builder.RegisterComponentInHierarchy<AllLeaderboardsView>();
            
            builder.RegisterEntryPoint<LeaderboardController>().AsSelf();
            builder.RegisterEntryPoint<LeaderboardObserver>().AsSelf();
        }
        
        private void RegisterAiPresentationView(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<AiPresentationView>();
            builder.Register<AiPresentationController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<AiPresentationObserver>().AsSelf();
        }

        private void RegisterRobotPresentationView(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<RobotPresentationView>();
            builder.Register<RobotPresentationController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<RobotPresentationObserver>().AsSelf();
        }

        private void RegisterRobotHolder(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ProductHolder>()
                .WithParameter(_robotsDataConfig.RobotsData)
                .WithParameter(_aiDataConfig.AiData)
                .AsSelf();
        }

        private void RegisterProductSale(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ProductSaleView>();
            
            builder.RegisterEntryPoint<ProductSaleController>().WithParameter(_productSaleConfig).AsSelf();
            builder.RegisterEntryPoint<ProductSaleObserver>().AsSelf();
        }

        private void RegisterWalletViewService(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<WalletView>();
            
            builder.RegisterEntryPoint<WalletObserver>().AsSelf();
        }

        private void TrainingAiRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TrainingAiView>();
            
            builder.RegisterEntryPoint<TrainingAiController>().AsSelf();
            builder.RegisterEntryPoint<TrainingAiObserver>().AsSelf();
        }

        private void ResearchRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ResearchView>();
            builder.RegisterComponentInHierarchy<SkillDescriptionView>();
            
            builder.RegisterEntryPoint<ResearchController>().AsSelf();
            builder.RegisterEntryPoint<ResearchObserver>().AsSelf();
        }

        private void BubbleSystemRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BubbleSystemView>();

            builder.RegisterEntryPoint<BubbleSystem>().AsSelf();
        }

        private void LevelUIRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LevelUI>();
            
            builder.Register<LevelUIController>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LevelUIObserver>().AsSelf();
        }

        private void ProductionRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ProductionView>();
            
            builder.RegisterEntryPoint<ProductionController>().AsSelf();
            builder.RegisterEntryPoint<ProductionObserver>().AsSelf();
        }

        private void ProgressBarRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ProductionProgressBarView>();
            
            builder.RegisterEntryPoint<ProductionProgressController>().AsSelf();
            builder.RegisterEntryPoint<ProductionProgressObserver>().AsSelf();
        }

        private void BuildRobotRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BuildRobotView>();
            
            builder.RegisterEntryPoint<BuildRobotController>().AsSelf();
            builder.RegisterEntryPoint<BuildRobotObserver>().AsSelf();
        }
        
        private void TaskSystemRegister(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TaskSystemView>();
            builder.RegisterEntryPoint<TaskSystemController>().WithParameter(_allTasksConfig).AsSelf();

            builder.RegisterEntryPoint<TaskSystemObserver>().AsSelf();
        }
        
        private void RegisterDialogueService(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DialogueView>();
            
            builder.RegisterEntryPoint<DialogueController>().AsSelf();
            builder.RegisterEntryPoint<DialogueObserver>().AsSelf();
        }
    }
}
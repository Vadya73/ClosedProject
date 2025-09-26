using System;
using VContainer.Unity;

namespace Core
{
    public class ResearchObserver : IInitializable, IDisposable
    {
        private readonly SkillDescriptionView _skillDescriptionView;
        private readonly ResearchView _researchSkillsView;
        private readonly ResearchController _researchController;
        private readonly ProductionProgressController _progressController;

        public ResearchObserver(SkillDescriptionView skillDescriptionView, ResearchView researchSkillsView, 
            ResearchController researchController, ProductionProgressController progressController)
        {
            _skillDescriptionView = skillDescriptionView;
            _researchSkillsView = researchSkillsView;
            _researchController = researchController;
            _progressController = progressController;
        }

        void IInitializable.Initialize()
        {
            _researchSkillsView.OnEnabled += OnViewEnabled;
            _progressController.OnProgressEnd += OnProgressEnd;
                
            _skillDescriptionView.CloseButton.onClick.AddListener(HideDescriptionView);
            _skillDescriptionView.ResearchButton.onClick.AddListener(TryStartResearch);
        }

        void IDisposable.Dispose()
        {
            _researchSkillsView.OnEnabled -= OnViewEnabled;
            _progressController.OnProgressEnd -= OnProgressEnd;

            _skillDescriptionView.CloseButton.onClick.RemoveListener(HideDescriptionView);
            _skillDescriptionView.ResearchButton.onClick.RemoveListener(TryStartResearch);
        }

        private void OnProgressEnd(ProductionType productType, int firstBubble, int secondBubble)
        {
            if (productType != ProductionType.Research)
                return;
            
            _researchController.EndResearch();
        }

        private void TryStartResearch()
        {
            _researchController.TryStartResearch();
        }

        private void OnViewEnabled()
        {
            _researchController.OnViewEnabled();
        }

        private void HideDescriptionView()
        {
            _researchController.HideSkillDescriptionView();
        }
    }
}
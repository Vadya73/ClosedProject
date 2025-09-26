using System;
using VContainer;
using VContainer.Unity;

namespace Meta
{
    public class DialogueObserver : IInitializable, IDisposable
    {
        private readonly DialogueView _dialogueView;
        private readonly DialogueController _dialogueController;
            
        [Inject]
        public DialogueObserver(DialogueView dialogueView, DialogueController dialogueController)
        {
            _dialogueView = dialogueView;
            _dialogueController = dialogueController;
        }
        
        public void Initialize()
        {
            _dialogueView.ClickedAreaButton.onClick.AddListener(SetNextMessage);
        }

        public void Dispose()
        {
            _dialogueView.ClickedAreaButton.onClick.RemoveListener(SetNextMessage);
        }

        private void SetNextMessage()
        {
            _dialogueController.OnClickArea();
        }
    }
}
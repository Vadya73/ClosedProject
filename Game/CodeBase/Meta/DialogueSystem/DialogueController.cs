using System;
using Core.CameraControl;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Meta
{
    public class DialogueController : IInitializable
    {
        private readonly DialogueView _dialogueView;
        private readonly TaskSystemController _taskSystemController;
        private readonly CameraController _cameraController;

        private DialogueConfig _currentConfig;
        private string _currentIdMessage;
        private bool _dialogueIsActive;
        public event Action OnDialogueEnd;
        public event Action OnDialogueStart;

        [Inject]
        public DialogueController(DialogueView dialogueView, TaskSystemController taskSystemController, 
            CameraController cameraController)
        {
            _dialogueView = dialogueView;
            _taskSystemController = taskSystemController;
            _cameraController = cameraController;
        }

        public void Initialize()
        {
            _currentIdMessage = "";
            _currentConfig = null;
            
            if (!_dialogueIsActive)
                _dialogueView.ForcedHide();
            else
                _dialogueView.ForcedShow();
        }

        public void StartDialogue(DialogueConfig config)
        {
            OnDialogueStart?.Invoke();
            
            _currentConfig = config;
            _currentIdMessage = config.Messages[0].Id;
            
            _dialogueView.Show();
            _dialogueView.UpdateView(config.Messages[0]);
            _dialogueIsActive = true;
            _dialogueView.ClickedAreaButton.interactable = true;
            
            _cameraController.SetActiveStateCamera(false);
        }

        private void EndDialogue()
        {
            if (!_dialogueIsActive) return;

            try
            {
                OnDialogueEnd?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in dialogue end event: {e.Message}");
            }

            if (_currentConfig != null && 
                _currentConfig.NeedSetTaskAfterDialogue && 
                _currentConfig.TaskConfig != null &&
                _taskSystemController != null)
            {
                try
                {
                    _taskSystemController.AddNewQuest(_currentConfig.TaskConfig.TaskDate);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to add quest: {e.Message}");
                }
            }

            try
            {
                if (_dialogueView != null)
                    _dialogueView.Hide();
                else
                    Debug.LogWarning("DialogueView reference is null!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error hiding dialogue: {e.Message}");
            }

            _currentConfig = null;
            _currentIdMessage = "";
            _dialogueIsActive = false;
            
            _cameraController.SetActiveStateCamera(true);
        }

        public void NextMessage()
        {
            if (_currentConfig == null || _currentConfig.Messages == null)
            {
                EndDialogue();
                return;
            }

            if (_currentConfig.Messages.Length == 0)
            {
                EndDialogue();
                return;
            }

            int lastIndex = _currentConfig.Messages.Length - 1;
            if (_currentIdMessage == _currentConfig.Messages[lastIndex].Id)
            {
                EndDialogue();
                return;
            }
    
            for (var i = 0; i < _currentConfig.Messages.Length; i++)
            {
                var message = _currentConfig.Messages[i];
                if (message.Id == _currentIdMessage && i + 1 < _currentConfig.Messages.Length)
                {
                    _dialogueView.UpdateView(_currentConfig.Messages[i + 1]);
                    _currentIdMessage = _currentConfig.Messages[i + 1].Id;
                    return;
                }
            }

            EndDialogue();
        }

        public void OnClickArea()
        {
            if (_dialogueView == null) return;
    
            if (_dialogueView.IsTyping)
            {
                if (_dialogueView.TypingRoutine != null)
                    _dialogueView.StopTypingCoroutine();
        
                _dialogueView.MessageTextComponent.text = _dialogueView.FullMessage;
                _dialogueView.Reset();
            }
            else
                NextMessage();
        }
    }
}
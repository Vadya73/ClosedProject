using System.Collections;
using System.Linq;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{
    public class DialogueView : BaseView<DialogueMessage>
    {
        [SerializeField] private DialogueCharsConfig _dialogueCharsConfig;
        [SerializeField] private Image _avatarImageComponent;
        [SerializeField] private TMP_Text _nameTextComponent;
        [SerializeField] private TMP_Text _messageTextComponent;
        [SerializeField] private Button _clickedAreaButton;
        [SerializeField] private float _textSpeed = 0.05f;

        private Coroutine _typingRoutine;
        private string _fullMessage;
        private bool _isTyping;
        public bool IsTyping => _isTyping;
        public Coroutine TypingRoutine => _typingRoutine;

        public Button ClickedAreaButton => _clickedAreaButton;
        public TMP_Text MessageTextComponent => _messageTextComponent;
        public string FullMessage => _fullMessage;

        public override void UpdateView(DialogueMessage taskData)
        {
            _avatarImageComponent.sprite = taskData.HasUniqueSprite 
                ? taskData.UniqueSprite 
                : _dialogueCharsConfig.Char.FirstOrDefault(c => c._character == taskData.DialogueChar)._sprite;

            _nameTextComponent.text = _dialogueCharsConfig.Char
                .FirstOrDefault(c => c._character == taskData.DialogueChar)
                ._fullName;

            _fullMessage = taskData.Message;
            StartTypingAnimation();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void StartTypingAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);
            
            _typingRoutine = StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            _isTyping = true;
            _messageTextComponent.text = "";
            
            foreach (char letter in _fullMessage.ToCharArray())
            {
                _messageTextComponent.text += letter;
                
                yield return new WaitForSeconds(_textSpeed);
                
                if (!_isTyping) break;
            }
            
            _messageTextComponent.text = _fullMessage;
            _isTyping = false;
            _typingRoutine = null;
        }

        public void Reset()
        {
            _isTyping = false;
            _typingRoutine = null;
        }

        public void StopTypingCoroutine()
        {
            if (_typingRoutine != null)
            {
                StopCoroutine(_typingRoutine);
                _typingRoutine = null;
            }
            _isTyping = false;
        }
    }
}
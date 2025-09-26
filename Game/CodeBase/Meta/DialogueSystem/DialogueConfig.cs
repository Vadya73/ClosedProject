using System;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Meta
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Config", fileName = "DialogueConfig", order = 0)]
    public class DialogueConfig : ScriptableObject
    {
        [SerializeField] private DialogueMessage[] _messages;
        [SerializeField] private bool _dialogueIsComplete;
        [SerializeField] private bool _needSetTaskAfterDialogue;
        [SerializeField, ShowIf("_needSetTaskAfterDialogue")]
        private TaskConfig _taskConfig;

        public DialogueMessage[] Messages => _messages;
        public bool NeedSetTaskAfterDialogue => _needSetTaskAfterDialogue;
        public TaskConfig TaskConfig => _taskConfig;
    }

    [Serializable]
    public class DialogueMessage : IData
    {
        public string Id = Guid.NewGuid().ToString();
        public DialogueCharsEnum DialogueChar;
        public bool HasUniqueSprite;
        public Sprite UniqueSprite;
        [TextArea] public string Message;
    }
    
    public enum DialogueCharsEnum
    {
        Player = 0,
        Mom = 1,
        Dealer = 2,
    }
}
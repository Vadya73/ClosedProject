using System;
using UnityEngine;

namespace Meta
{
    [CreateAssetMenu(menuName = "Dialogue/Chars Config", fileName = "DialogueCharsConfig", order = 1)]
    public class DialogueCharsConfig : ScriptableObject
    {
        [SerializeField] private DialogueCharSettings[] _char;
        
        public DialogueCharSettings[] Char => _char;
    }
    
    [Serializable]
    public struct DialogueCharSettings
    {
        public DialogueCharsEnum _character;
        public string _fullName;
        public string _description;
        public Sprite _sprite;
    }
}
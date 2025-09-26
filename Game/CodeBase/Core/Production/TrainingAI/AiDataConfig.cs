using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    [CreateAssetMenu(menuName = "AI/Ai Names Config", fileName = "AiNamesConfig", order = 0)]
    public class AiDataConfig : ScriptableObject
    {
        [SerializeField] private AIData _aiData;
        [SerializeField] private string[] _names;
        [SerializeField] private string[] _lastNames;
        [SerializeField] private Sprite[] _sprites;
        
        public string[] Names => _names;
        public string[] LastNames => _lastNames;
        public Sprite[] Sprites => _sprites;
        public AIData AiData => _aiData;


        public string GetRandomName()
        {
            var indexName = Random.Range(0, _names.Length);
            return _names[indexName];
        }

        public string GetRandomLastName()
        {
            var indexLastName = Random.Range(0, _lastNames.Length);
            return _names[indexLastName];
        }

        public Sprite GetRandomSprite()
        {
            var indexSprite = Random.Range(0, _sprites.Length);
            return _sprites[indexSprite];
        }
    }
    
    [Serializable]
    public class AIData
    {
        [SerializeField] private List<AI> _currentAis = new();
        public List<AI> CurrentAis => _currentAis;

        public void AddAI(AI ai)
        {
            _currentAis.Add(ai);
        }
    }
}
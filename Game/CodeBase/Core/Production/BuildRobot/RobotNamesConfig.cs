using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "RobotNamesConfig", fileName = "RobotNamesConfig", order = 0)]
    public class RobotNamesConfig : ScriptableObject
    {
        [SerializeField] private string[] _names;
        [SerializeField] private string[] _lastNames;
        
        public string[] Names => _names;
        public string[] LastNames => _lastNames;

        public string GetRandomName()
        {
            var indexName = UnityEngine.Random.Range(0, _names.Length);
            return _names[indexName];
        }

        public string GetRandomLastName()
        {
            var indexLastName = UnityEngine.Random.Range(0, _lastNames.Length);
            return _names[indexLastName];
        }

        public Sprite GetRandomAvatar()
        {
            return null;
        }
    }
}
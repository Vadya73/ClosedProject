using UnityEngine;

namespace Meta
{
    [CreateAssetMenu(menuName = "Task/All Tasks Config", fileName = "AllTaskConfigs", order = 0)]
    public class AllTaskConfigs : ScriptableObject
    {
        [SerializeField] private TaskConfig _dogTaskData;
        
        public TaskConfig DogTaskData => _dogTaskData;
    }
}
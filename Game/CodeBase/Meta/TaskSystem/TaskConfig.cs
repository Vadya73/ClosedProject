using System;
using UI;
using UnityEngine;

namespace Meta
{
    [CreateAssetMenu(menuName = "Quest/Quest Config", fileName = "Quest Config", order = 0)]
    public class TaskConfig : ScriptableObject
    {
        [SerializeField] private TaskData _taskData; 
        
        public TaskData TaskDate => _taskData; 
    }
    
    [Serializable]
    public class TaskData : IData
    {
        [SerializeField] private string _id = Guid.NewGuid().ToString();
        [SerializeField] private string _questName;
        [SerializeField] private bool _isCompleted;
            
        public string Id => _id;
        public string QuestName => _questName;
        public bool IsCompleted => _isCompleted;

        public void SetCompleted(bool b) => _isCompleted = b;
    }
}
using UnityEngine;

namespace Core
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Transform _bubbleSpawnPoint;
        [SerializeField] private WorkerConfig _workerConfig;
        
        public Transform BubbleSpawnPoint => _bubbleSpawnPoint;
        public WorkerConfig WorkerConfig => _workerConfig;
    }
}
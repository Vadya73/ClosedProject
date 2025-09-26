using UnityEngine;

namespace Core
{
    public class WorkerPlace : MonoBehaviour
    {
        [SerializeField] private Worker _worker;
        [SerializeField] private bool _isFree;
        
        public Worker Worker => _worker;
        public bool IsFree => _isFree;

        private void Awake()
        {
            if (_worker == null)
                _isFree = true;
        }

        public void SetWorker(Worker workerPrefab)
        {
            var workerInst = Instantiate(workerPrefab, transform);
            _worker = workerInst.GetComponent<Worker>();
            workerInst.transform.position =transform.position;
            _isFree = false;
            _worker = workerInst;
        }

        public void SetIsFree(bool isFree) => _isFree = isFree;

        public void ClearWorker() => _worker = null;
    }
}
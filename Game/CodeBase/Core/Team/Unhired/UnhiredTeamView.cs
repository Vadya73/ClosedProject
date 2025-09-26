using UI;
using UnityEngine;

namespace Core
{
    public class UnhiredTeamView : BaseView<IData>
    {
        [SerializeField] private Transform _workersContainer;
        
        public Transform WorkersContainer => _workersContainer;
        public RectTransform RectTransform => _workersContainer.GetComponent<RectTransform>();
    }
}

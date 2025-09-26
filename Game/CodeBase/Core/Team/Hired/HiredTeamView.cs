using UI;
using UnityEngine;

namespace Core
{
    public class HiredTeamView : BaseView<IData>
    {
        [SerializeField] private Transform _workersContainer;
        
        public Transform WorkersContainer => _workersContainer;
        public RectTransform ContainerRectTransform => _workersContainer.GetComponent<RectTransform>();
    }
}

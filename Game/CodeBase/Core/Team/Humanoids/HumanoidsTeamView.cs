using UI;
using UnityEngine;

namespace Core
{
    public class HumanoidsTeamView : BaseView<IData>
    {
        [SerializeField] private Transform _workersContainer;
        
        public Transform WorkersContainer => _workersContainer;
    }
}

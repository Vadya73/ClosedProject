using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class TeamSystemView : BaseView<IData>
    {
        [SerializeField] private WorkerPlace[] _workerPlaces;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _unhiredTeamButton;
        [SerializeField] private Button _hiredTeamButton;
        [SerializeField] private Button _humanoidTeamButton;
        
        public WorkerPlace[] WorkerPlaces => _workerPlaces;
        public Button ExitButton => _exitButton;
        public Button UnhiredTeamButton => _unhiredTeamButton;
        public Button HiredTeamButton => _hiredTeamButton;
        public Button HumanoidTeamButton => _humanoidTeamButton;
    }
}
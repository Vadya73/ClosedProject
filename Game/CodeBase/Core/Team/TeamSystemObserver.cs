using System;
using VContainer.Unity;

namespace Core
{
    public class TeamSystemObserver : IInitializable, IDisposable
    {
        private readonly TeamSystemController _teamSystemController;
        private readonly TeamSystemView _teamSystemView;
        private readonly UnhiredTeamController _unhiredTeamController;
        private readonly HiredTeamController _hiredTeamController;
        private readonly HumanoidsTeamController _humanoidsTeamController;

        public TeamSystemObserver(TeamSystemController teamSystemController, TeamSystemView teamSystemView, 
            UnhiredTeamController unhiredTeamController, HiredTeamController hiredTeamController, HumanoidsTeamController humanoidsTeamController)
        {
            _teamSystemController = teamSystemController;
            _teamSystemView = teamSystemView;
            _unhiredTeamController = unhiredTeamController;
            _hiredTeamController = hiredTeamController;
            _humanoidsTeamController = humanoidsTeamController;
        }
        public void Initialize()
        {
            _teamSystemView.ExitButton.onClick.AddListener(HideTeamView);
            _teamSystemView.HiredTeamButton.onClick.AddListener(ShowHiredTeam);
            _teamSystemView.UnhiredTeamButton.onClick.AddListener(ShowUnhiredTeam);
            _teamSystemView.HumanoidTeamButton.onClick.AddListener(ShowHumanoidTeam);
        }

        public void Dispose()
        {
            _teamSystemView.ExitButton.onClick.RemoveListener(HideTeamView); 
            _teamSystemView.HiredTeamButton.onClick.RemoveListener(ShowHiredTeam);
            _teamSystemView.UnhiredTeamButton.onClick.RemoveListener(ShowUnhiredTeam);
            _teamSystemView.HumanoidTeamButton.onClick.RemoveListener(ShowHumanoidTeam);
        }

        private void HideTeamView()
        {
            _teamSystemController.HideView();
            _teamSystemController.HideDescriptionView();
        }

        private void ShowHiredTeam()
        {
            _hiredTeamController.ShowView();

            _unhiredTeamController.HideView();
            _humanoidsTeamController.HideView();
            _teamSystemController.HideDescriptionView();
        }

        private void ShowUnhiredTeam()
        {
            _unhiredTeamController.ShowView();
            
            _hiredTeamController.HideView();
            _humanoidsTeamController.HideView();
            _teamSystemController.HideDescriptionView();
        }

        private void ShowHumanoidTeam()
        {
            _humanoidsTeamController.ShowView();

            _unhiredTeamController.HideView();
            _hiredTeamController.HideView();
            _teamSystemController.HideDescriptionView();
        }
    }
}
using VContainer;

namespace Core
{
    public class HumanoidsTeamController
    {
        private readonly HumanoidsTeamView _humanoidsTeamView;

        [Inject]
        public HumanoidsTeamController(HumanoidsTeamView humanoidsTeamView)
        {
            _humanoidsTeamView = humanoidsTeamView;
        }
        
        public void ShowView()
        {
            _humanoidsTeamView.ForcedShow();
        }

        public void HideView()
        {
            _humanoidsTeamView.ForcedHide();
        }
    }
}
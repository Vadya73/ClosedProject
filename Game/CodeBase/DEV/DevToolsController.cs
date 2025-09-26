using Core;
using Infrastructure;
using Services;
using VContainer;

namespace DEV
{
    public class DevToolsController
    {
        private readonly DevToolsView _view;
        private readonly LoadScreen _loadScreen;
        private readonly Wallet _wallet;
        private readonly TimeSystem _timeSystem;

        [Inject]
        public DevToolsController(DevToolsView view, LoadScreen loadScreen, Wallet wallet, TimeSystem timeSystem)
        {
            _view = view;
            _loadScreen = loadScreen;
            _wallet = wallet;
            _timeSystem = timeSystem;
        }
        
        public void CloseView() => _view.Hide();
        public void ShowView()
        {
            if (_view.IsActive)
            {
                _view.Hide();
                return;
            }
            _view.Show();
        }

        public void ShowLevel(int index)
        {
            _loadScreen.LoadScene(index);
            _view.ForcedHide();
        }

        public void AddCash(long count) => _wallet.AddWalletCount(WalletType.Cash, count);

        public void MultiplyTime(int multiply) => _timeSystem.SetTimeMultiplying(multiply);
    }
}
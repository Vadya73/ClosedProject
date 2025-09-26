using System;
using VContainer.Unity;

namespace DEV
{
    public class DevToolsObserver : IInitializable, IDisposable
    {
        private readonly DevToolsView _devToolsView;
        private readonly DevToolsController _devToolsController;

        public DevToolsObserver(DevToolsView devToolsView, DevToolsController devToolsController)
        {
            _devToolsView = devToolsView;
            _devToolsController = devToolsController;
        }
        
        public void Initialize()
        {
            _devToolsView.ForcedHide();
            
            _devToolsView.DevButton.onClick.AddListener(ShowView);
            _devToolsView.ExitButton.onClick.AddListener(CloseView);
            _devToolsView.OneLevelButton.onClick.AddListener(() => ShowLevel(1));
            _devToolsView.TwoLevelButton.onClick.AddListener(() => ShowLevel(2));
            _devToolsView.ThreeLevelButton.onClick.AddListener(() => ShowLevel(3));
            _devToolsView.FourLevelButton.onClick.AddListener(() => ShowLevel(4));
            _devToolsView.Add100CashButton.onClick.AddListener(() => AddCash(100));
            _devToolsView.Add1000CashButton.onClick.AddListener(() => AddCash(1000));
            _devToolsView.Add10000CashButton.onClick.AddListener(() => AddCash(10000));

        }

        public void Dispose()
        {
            _devToolsView.DevButton.onClick.RemoveListener(ShowView);
            _devToolsView.ExitButton.onClick.RemoveListener(CloseView);
            _devToolsView.OneLevelButton.onClick.RemoveListener(() => ShowLevel(1));
            _devToolsView.TwoLevelButton.onClick.RemoveListener(() => ShowLevel(2));
            _devToolsView.ThreeLevelButton.onClick.RemoveListener(() => ShowLevel(3));
            _devToolsView.Add100CashButton.onClick.RemoveListener(() => AddCash(100));
            _devToolsView.Add1000CashButton.onClick.RemoveListener(() => AddCash(1000));
            _devToolsView.Add10000CashButton.onClick.RemoveListener(() => AddCash(10000));
            _devToolsView.Timex1Button.onClick.RemoveListener(() => MultiplyTime(1));
            _devToolsView.Timex2Button.onClick.RemoveListener(() => MultiplyTime(2));
            _devToolsView.Timex3Button.onClick.RemoveListener(() => MultiplyTime(3));
        }

        private void MultiplyTime(int multiply)
        {
            _devToolsController.MultiplyTime(multiply);
        }

        private void AddCash(int count)
        {
            _devToolsController.AddCash(count);
        }

        private void ShowLevel(int index)
        {
            _devToolsController.ShowLevel(index);
        }

        private void ShowView()
        {
            _devToolsController.ShowView();
        }

        private void CloseView()
        {
            _devToolsController.CloseView();
        }
    }
}
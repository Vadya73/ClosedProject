using System;
using VContainer.Unity;

namespace Meta.RouletteWheel
{
    public class WheelOfFortuneObserver : IInitializable, IDisposable
    {
        private readonly WheelOfFortuneView _wheelOfFortuneView;
        private readonly WheelOfFortuneController _wheelOfFortuneController;
        private readonly WheelOfFortune _wheelOfFortune;

        public WheelOfFortuneObserver(WheelOfFortuneView wheelOfFortuneView, WheelOfFortuneController wheelOfFortuneController, 
            WheelOfFortune wheelOfFortune)
        {
            _wheelOfFortuneView = wheelOfFortuneView;
            _wheelOfFortuneController = wheelOfFortuneController;
            _wheelOfFortune = wheelOfFortune;
        }
        public void Initialize()
        {
            _wheelOfFortuneView.ExitButton.onClick.AddListener(HideView);
            _wheelOfFortuneView.SpinButton.onClick.AddListener(Spin);
        }

        public void Dispose()
        {
            _wheelOfFortuneView.ExitButton.onClick.RemoveListener(HideView);
            _wheelOfFortuneView.SpinButton.onClick.RemoveListener(Spin);
        }

        private void Spin()
        {
            _wheelOfFortuneController.OnSpinClicked();
        }

        private void HideView()
        {
            _wheelOfFortuneController.HideView();
        }
    }
}
using DG.Tweening;
using Infrastructure;
using VContainer;
using VContainer.Unity;
using Color = UnityEngine.Color;

namespace UI
{
    public class NotificationController : IInitializable
    {
        private readonly NotificationView _notificationView;
        private readonly AudioController _audioController;

        [Inject]
        public NotificationController(NotificationView notificationView, AudioController audioController)
        {
            _notificationView = notificationView;
            _audioController = audioController;
        }
        
        public void ShowErrorMessage(string message)
        {
            _notificationView.SetMessageColor(Color.red);
            ShowMessage(message);
        }

        private void ShowMessage(string message)
        {
            _audioController.PlayEffects(_audioController.SoundConfigs.NotificationSound);
            _notificationView.SetMessage(message);
            DOTween.Sequence()
                .AppendCallback(() => _notificationView.Show())
                .AppendInterval(3f)
                .AppendCallback(() => _notificationView.Hide());
        }

        public void Initialize()
        {
            _notificationView.ForcedHide();
        }
    }
}
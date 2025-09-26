using TMPro;
using UnityEngine;

namespace UI
{
    public class NotificationView : BaseView<IData>
    {
        [SerializeField] private TMP_Text _notificationText;

        public void SetMessage(string message)
        {
            _notificationText.text = message;
        }

        public void SetMessageColor(Color color)
        {
            _notificationText.color = color;
        }
    }
}
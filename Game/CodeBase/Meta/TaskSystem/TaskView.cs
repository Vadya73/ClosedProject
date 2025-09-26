using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;

namespace Meta
{
    public class TaskView : BaseView<TaskData>
    {
        [SerializeField] private TMP_Text _questTitle;
        
        private TaskData _taskData;
        public TaskData TaskData => _taskData;

        public override void UpdateView(TaskData taskData)
        {
            _questTitle.text = "- " + taskData.QuestName;
            _taskData = taskData;
        }

        public void DeleteView()
        {
            DOTween.Sequence()
                .AppendCallback(() => _questTitle.fontStyle = FontStyles.Strikethrough)
                .AppendInterval(1.5f)
                .OnComplete(() => Destroy(this.gameObject));
        }
    }
}
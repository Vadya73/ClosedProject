using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.Utilities;
using VContainer;
using VContainer.Unity;

namespace Meta
{
    public class TaskSystemController : IInitializable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly TaskSystemView _taskSystemView;
        private readonly PlayerConfig _playerConfig;
        private readonly TaskView _taskViewPrefab;
        private readonly AllTaskConfigs _allTaskConfigs;
        
        private List<TaskView> _taskViews;
        private List<TaskData> _currentQuests;

        private bool _hasTask;
        [Inject]
        public TaskSystemController(IObjectResolver objectResolver,TaskSystemView taskSystemView, PlayerConfig playerConfig, 
            AllTaskConfigs allTaskConfigs)
        {
            _objectResolver = objectResolver;
            _taskSystemView = taskSystemView;
            _playerConfig = playerConfig;
            _allTaskConfigs = allTaskConfigs;
            _taskViewPrefab = _playerConfig.TaskViewPrefab;
        }
        
        public void Initialize()
        {
            _currentQuests = _playerConfig.CurrentQuests.ToList();
            _currentQuests ??= new List<TaskData>();
            _taskViews ??= new List<TaskView>();
            _hasTask = !_currentQuests.IsNullOrEmpty();
            
            if (!_hasTask)
                _taskSystemView.ForcedHide();
        }

        public void AddNewQuest(TaskData taskData)
        {
            if (!_taskSystemView.IsActive)
                _taskSystemView.Show();
            
            _currentQuests.Add(taskData);
            TaskView taskView = _objectResolver.Instantiate(_taskViewPrefab, _taskSystemView.ParentToQuestPrefabs);
            _taskViews.Add(taskView);
            
            taskView.UpdateView(taskData);
        }

        public void CompleteQuest(TaskData taskData)
        {
            TaskView removeTaskView = null;
            
            foreach (var taskView in _taskViews)
            {
                if (taskView.TaskData == taskData)
                {
                    removeTaskView = taskView;
                    taskData.SetCompleted(true);
                    break;
                }
            }

            if (removeTaskView == null)
                return;
            
            removeTaskView.DeleteView();
            _taskViews.Remove(removeTaskView);
            _currentQuests.Remove(taskData);

            if (_currentQuests.IsNullOrEmpty())
                DOVirtual.DelayedCall(1f, () => _taskSystemView.Hide());
        }

        public void CompleteDogQuest()
        {
            CompleteQuest(_allTaskConfigs.DogTaskData.TaskDate);
        }
    }
}
using System;
using Core;
using VContainer.Unity;

namespace Meta
{
    public class TaskSystemObserver : IInitializable, IDisposable
    {
        private readonly BuildRobotController _buildRobotController;
        private readonly TaskSystemController _taskSystemController;

        public TaskSystemObserver(BuildRobotController buildRobotController, TaskSystemController taskSystemController)
        {
            _buildRobotController = buildRobotController;
            _taskSystemController = taskSystemController;
        }
        
        public void Initialize()
        {
            _buildRobotController.OnRobotCreateFinished += OnCreateBuildRobotDog;
        }

        public void Dispose()
        {
            
        }

        private void OnCreateBuildRobotDog(RobotSubtypes obj)
        {
            if (obj != RobotSubtypes.Dog)
                return;
            
            _buildRobotController.OnRobotCreateFinished -= OnCreateBuildRobotDog;
            _taskSystemController.CompleteDogQuest();
        }
    }
}
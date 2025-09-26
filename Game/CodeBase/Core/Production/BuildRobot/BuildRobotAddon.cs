using Infrastructure;
using UnityEngine.EventSystems;
using VContainer;

namespace Core
{
    public class BuildRobotAddon : ProductionAddon<RobotAddonConfig>, IPointerClickHandler
    {
        private AudioController _audioController;
        private SoundConfigs _soundConfigs;

        [Inject]
        private void Construct(AudioController audioController, SoundConfigs soundConfigs)
        {
            _audioController = audioController;
            _soundConfigs = soundConfigs;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _audioController.PlayEffects(_soundConfigs.DefaultClickSound);
        }
    }
}

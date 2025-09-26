using UnityEngine;

namespace Infrastructure
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private SoundConfigs _soundConfigs;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;

        public SoundConfigs SoundConfigs => _soundConfigs;
        public AudioSource MusicSource => _musicSource;
        public AudioSource EffectsSource => _effectsSource;
        
        public void PlayMusic()
        {
            _musicSource.Play();
        }

        public void PlayEffects(AudioClip clip)
        {
            _effectsSource.PlayOneShot(clip);
        }

        public void ChangeSoundState()
        {
            _effectsSource.mute = !_effectsSource.mute;
        }

        public void ChangeMusicState()
        {
            _musicSource.mute = !_musicSource.mute;
        }
    }
}

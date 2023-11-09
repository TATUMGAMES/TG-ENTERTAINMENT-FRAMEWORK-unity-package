using UnityEngine;

namespace EntertainmentFramework.AudioHandler
{
    [RequireComponent(typeof(AudioSource))]
    public class BackGroundMusic : MonoBehaviour
    {
        private AudioSource backgroundMusic;

        [Range(0.0f, 1.0f)]
        public float volume = 0.5f;

        public AudioData.EAudio eAudio;

        private void Start()
        {
            AudioManager.Instance.bgMusic = this;
            backgroundMusic = GetComponent<AudioSource>();
            backgroundMusic = AudioManager.Instance.Play(eAudio, volume, true, AudioManager.AudioType.Music);
            StopBgMusic();
        }

        /// <summary>
        /// Stops background music
        /// </summary>
        public void StopBgMusic()
        {
            int musicValue = PlayerPrefs.GetInt(Constants.MUSIC_ON_OFF);
            if (musicValue == 0)
            {
                if (backgroundMusic != null)
                    backgroundMusic.Stop();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Plays the background music
        /// </summary>
        public void StartBgMusic()
        {
            if (PlayerPrefs.GetInt(Constants.MUSIC_ON_OFF) == 1)
            {
                if (backgroundMusic != null)
                {
                    backgroundMusic.Play();
                    backgroundMusic.volume = volume * PlayerPrefs.GetFloat(Constants.SOUND_VOLUME);
                    return;
                }
            }
        }

        /// <summary>
        /// Changes the background music
        /// </summary>
        public void ChangeBGMusic(AudioData.EAudio Audio, float a_volume)
        {
            if (backgroundMusic != null)
            {
                Destroy(backgroundMusic.gameObject);
            }
            backgroundMusic = AudioManager.Instance.Play(Audio, a_volume, true, AudioManager.AudioType.Music);
        }

        private void OnDestroy()
        {
            Destroy(backgroundMusic);
        }

        /// <summary>
        /// Plays the single shot sound
        /// </summary>
        public void PlayOneShotSound()
        {
            AudioManager.Instance.Play(AudioData.EAudio.Tap1, 0.5f, AudioManager.AudioType.Sound);
        }
    }
}
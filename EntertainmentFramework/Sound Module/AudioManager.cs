using System.Linq;
using UnityEngine;

namespace EntertainmentFramework.AudioHandler
{
    public class AudioManager : MonoBehaviour
    {
        public BackGroundMusic bgMusic;

        public enum AudioType
        {
            None = 0,
            Sound,
            Music
        }

        public static AudioManager Instance;
        [SerializeField] private SoundData audioDataSO;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (!PlayerPrefs.HasKey(Constants.MUSIC_ON_OFF))
            {
                PlayerPrefs.SetInt(Constants.MUSIC_ON_OFF, 1);
            }
            if (!PlayerPrefs.HasKey(Constants.SOUND_ON_OFF))
            {
                PlayerPrefs.SetInt(Constants.SOUND_ON_OFF, 1);
                PlayerPrefs.SetFloat(Constants.SOUND_VOLUME, 1);
                PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME, 1);
            }
        }

        /// <summary>
        /// Play sound sfx
        /// </summary>
        /// <param name="id"></param>
        /// <param name="volume"></param>
        /// <param name="audioType"></param>
        /// <returns></returns>
        public AudioSource Play(AudioData.EAudio id, float volume, AudioType audioType)
        {
            if (audioType == AudioType.Music)
            {
                if (PlayerPrefs.GetInt(Constants.MUSIC_ON_OFF) == 0)
                {
                    return null;
                }
            }
            if (audioType == AudioType.Sound)
            {
                if (PlayerPrefs.GetInt(Constants.SOUND_ON_OFF) == 0)
                {
                    return null;
                }
            }

            AudioData audioData = audioDataSO.AudioDatas.Where(x => x.AudioID == id).FirstOrDefault();
            if (audioData != null)
            {
                return AudioPlayer.Play(new AudioPlayerData
                {
                    audioClip = audioData.AudioClip,
                    oneShot = true,
                    volume = volume * PlayerPrefs.GetFloat(Constants.SOUND_VOLUME),
                }).GetAudioSource();
            }
            else
                return null;
        }

        /// <summary>
        /// Play sound sfx
        /// </summary>
        /// <param name="id"></param>
        /// <param name="volume"></param>
        /// <param name="IsLoop"></param>
        /// <param name="audioType"></param>
        /// <returns></returns>
        public AudioSource Play(AudioData.EAudio id, float volume, bool IsLoop, AudioType audioType)
        {
            if (audioType == AudioType.Music)
            {
                if (PlayerPrefs.GetInt(Constants.MUSIC_ON_OFF) == 0)
                {
                    return null;
                }
            }
            if (audioType == AudioType.Sound)
            {
                if (PlayerPrefs.GetInt(Constants.SOUND_ON_OFF) == 0)
                {
                    return null;
                }
            }

            AudioData audioData = audioDataSO.AudioDatas.Where(x => x.AudioID == id).FirstOrDefault();
            if (audioData != null)
            {
                return AudioPlayer.Play(new AudioPlayerData
                {
                    audioClip = audioData.AudioClip,
                    oneShot = false,
                    loop = IsLoop,
                    volume = volume * PlayerPrefs.GetFloat(Constants.SOUND_VOLUME),
                }).GetAudioSource();
            }
            else
                return null;
        }
    }
}
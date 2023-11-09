using System;
using System.Collections;
using UnityEngine;

namespace EntertainmentFramework.AudioHandler
{
    public class AudioPlayer : MonoBehaviour
    {
        private Action onComplete;
        private AudioPlayerData audioPlayerData;
        private AudioSource audioSource;

        public static AudioPlayer Play(AudioPlayerData audioPlayerData, Action onComplete = null)
        {
            GameObject go = new GameObject("AudioPlayer");
            AudioPlayer audioPlayer = go.AddComponent<AudioPlayer>();

            audioPlayer.audioPlayerData = audioPlayerData;
            audioPlayer.onComplete = onComplete;

            if (audioPlayerData.oneShot)
            {
                audioPlayer.PlayOneShot();
            }
            else
            {
                audioPlayer.PlayAudio();
            }
            return audioPlayer;
        }

        public AudioSource GetAudioSource()
        {
            return audioSource;
        }

        public void PlayOneShot()
        {
            if (audioPlayerData.audioClip == null)
            {
                return;
            }
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = audioPlayerData.volume;
            audioSource.PlayOneShot(audioPlayerData.audioClip);
            StartCoroutine(OnComplete(audioPlayerData.audioClip.length));
        }

        public void PlayAudio()
        {
            if (audioPlayerData.audioClip == null)
            {
                return;
            }
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = audioPlayerData.audioClip;
            audioSource.volume = audioPlayerData.volume;
            audioSource.loop = audioPlayerData.loop;
            audioSource.Play();
            if (!audioPlayerData.loop)
            {
                StartCoroutine(OnComplete(audioPlayerData.audioClip.length));
            }
        }

        private IEnumerator OnComplete(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);

            Destroy(clipLength);
        }

        public void Destroy(float clipLength)
        {
            onComplete?.Invoke();
            Destroy(gameObject, clipLength);
        }
    }

    [Serializable]
    public class AudioPlayerData
    {
        public bool oneShot = true;
        public bool loop = false;
        public AudioClip audioClip = null;
        public float volume = 1.0f;
    }
}

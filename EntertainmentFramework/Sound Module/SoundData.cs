using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntertainmentFramework.AudioHandler
{

    [CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObject/SoundData", order = 1)]
    public class SoundData : ScriptableObject
    {
        public List<AudioData> AudioDatas = new List<AudioData>();
    }

    [Serializable]
    public class AudioData
    {
        public enum EAudio
        {
            Tap1,
            SplashBackground,
            MainMenuBackground,
            GameWorldBackground,
            Walking,
            PortalChange,
            PartsCollection,
            MeeleeAttack,
            LaserAttack,
            DeathSound,
            EnemyLaugh,
            EnemyWalk
        }

        public EAudio AudioID;
        public AudioClip AudioClip;
    }
}
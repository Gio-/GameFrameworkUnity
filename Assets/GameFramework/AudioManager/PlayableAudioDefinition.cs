using UnityEngine;
namespace GameFramework
{
    [System.Serializable]
    public class PlayableAudioDefinition
    {
        public AudioClip audioClip;
        [Header("Customizations")]
        public float volume = 1f;
        [Tooltip("If another instance of this audio can overlap old one")]
        public bool onceAtTime = false;
        public bool loop = false;
        public bool hasRandomPitch = false;
        [ShowIf("hasRandomPitch", true)]
        public float minRandomPitch = .9f;
        [ShowIf("hasRandomPitch", true)]
        public float maxRandomPitch = 1.1f;
        [Space]
        public bool is3Daudio = false;
        [ShowIf("is3Daudio", true)]
        public AudioRolloffMode rollofMode = AudioRolloffMode.Linear;
        [ShowIf("is3Daudio", true)]
        public float spatialBlend = 1;
        [ShowIf("is3Daudio", true)]
        public float minDistance = 1;
        [ShowIf("is3Daudio", true)]
        public float maxDistance = 500;

        //USED FROM USER TO STORE REFERENCE OF THIS AUDIO ACTION AND REMOVE IT ON OBJECT DISABLE/DESTROY
        public EventManager.Callback handler;
    }
}

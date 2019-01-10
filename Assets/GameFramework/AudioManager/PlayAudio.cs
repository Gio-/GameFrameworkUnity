/**
 * @author Fabrizio Coppolecchia
 *
 * Component used to play audio. Usage:
 * 1 - Attach to a object, select when you want to play audio from playEvent dropdown
 * 2 - make sure to attach right collider and eventually attach rigidbody to detect collision if you use trigger/trigger2D/collision/collision2d
 * 3 - define audio parameters
 * 
 * This class also provide a simple pool system to improve speed
 * 
 * @date - 2019/01/03
 */

using System;
using UnityEngine;

namespace GameFramework
{
    public class PlayAudio : MonoBehaviour
    {
        #region VARIABLES
        [SerializeField]
        private PlayableAudioDefinition audioDefinition = null;
        private AudioSource currentPlayngAudio = null;
        #endregion

        #region MONOBEHAVIOUR METHODS
        void Start()
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.START))
                PlayClip(audioDefinition);
        }
        private void OnEnable()
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.ENABLE))
                PlayClip(audioDefinition);
        }
        private void OnDisable()
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.DISABLE))
                PlayClip(audioDefinition);
        }
        private void OnDestroy()
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.DESTROY))
                PlayClip(audioDefinition);
        }

        #region COLLISION/TRIGGER DETECTIONS
        private void OnCollisionEnter(Collision collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.COLLISIONENTER) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))

                PlayClip(audioDefinition);
        }
        private void OnCollisionStay(Collision collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.COLLISIONSTAY) 
                && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))

                PlayClip(audioDefinition);
        }
        private void OnCollisionExit(Collision collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.COLLISIONEXIT) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))

                PlayClip(audioDefinition);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.COLLISIONENTER2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))
                PlayClip(audioDefinition);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.COLLISIONSTAY2D) 
                && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))
                PlayClip(audioDefinition);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.COLLISIONEXIT2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))
                PlayClip(audioDefinition);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.TRIGGERENTER) && Utilities.LayerIsInLayerMask(other.gameObject.layer, audioDefinition.allowedLayer))
                PlayClip(audioDefinition);
        }
        private void OnTriggerStay(Collider other)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.TRIGGERSTAY) 
                && Utilities.LayerIsInLayerMask(other.gameObject.layer,audioDefinition.allowedLayer)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))
                PlayClip(audioDefinition);
        }
        private void OnTriggerExit(Collider other)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.TRIGGEREXIT) && Utilities.LayerIsInLayerMask(other.gameObject.layer, audioDefinition.allowedLayer))
                PlayClip(audioDefinition);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.TRIGGERENTER2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))
                PlayClip(audioDefinition);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.TRIGGERSTAY2D) 
                && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))
                PlayClip(audioDefinition);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (audioDefinition.playEvent.Equals(ExecuteWhen.TRIGGEREXIT2D) && Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))
                PlayClip(audioDefinition);
        }
        #endregion
        #endregion

        #region GENERIC METHODS
        public void PlayClip(PlayableAudioDefinition audioDefinition)
        {
            AudioClipSettings clipSettings;
            AudioSettings3D audioSettings3D = null;
            //If 3d audio the settings class is created and populated with defined info in audioDefinition
            if (audioDefinition.is3Daudio)
            {
                audioSettings3D = new AudioSettings3D
                {
                    position = transform.position,
                    rollofMode = audioDefinition.rollofMode,
                    spatialBlend = audioDefinition.spatialBlend,
                    minDistance = audioDefinition.minDistance,
                    maxDistance = audioDefinition.maxDistance
                    
                };
            }
            //Calculate pitch (random or not)
            float pitch = audioDefinition.hasRandomPitch ? UnityEngine.Random.Range(audioDefinition.minRandomPitch, audioDefinition.maxRandomPitch) : 1;
            //Create clipSettings
            clipSettings = new AudioClipSettings(audioDefinition.audioClip, pitch, audioDefinition.loop, audioDefinition.volume, 0);
            string audioId = audioDefinition.audioClip.name;
            //IF THIS INSTANCE CAN PLAY ONLY ONCE AUDIO X TIME I WILL DISABLE CURRENT PLAYNG AUDIO IF EXIST
            if (audioDefinition.onceAtTime && currentPlayngAudio != null && currentPlayngAudio.isPlaying) { 
                currentPlayngAudio.Stop();
                currentPlayngAudio.gameObject.SetActive(false);
            }
            //Play Audio
            currentPlayngAudio = AudioManager.PlayAudio(audioDefinition.audioClip.name, clipSettings, audioSettings3D);
        }
        #endregion

    }

    [Serializable]
    public class PlayableAudioDefinition
    {
        public AudioClip audioClip;
        public ExecuteWhen playEvent;
        [Space]
        [ShowIf("playEvent", 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17)]
        public LayerMask allowedLayer;
        [ShowIf("playEvent", 1)]
        public EventsID eventToListen;
        [Space]
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
        public  EventManager.Callback handler;

    }
}
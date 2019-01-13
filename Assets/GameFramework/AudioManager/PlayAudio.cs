/**
 * @author Fabrizio Coppolecchia
 *
 * Component used to play audio. Usage:
 * 1 - Attach to a object, select when you want to execute an action using executeWhen dropdown
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
    public class PlayAudio : ExecuteAction
    {
        #region VARIABLES
        [SerializeField]
        private PlayableAudioDefinition audioDefinition = null;
        private AudioSource currentPlayngAudio = null;
        #endregion

        #region MONOBEHAVIOUR METHODS OVERRIDES
        protected override void Awake()
        {
            actionToExecute.AddListener(PlayClip);
            base.Awake();
        }

        #region COLLISION/TRIGGER DETECTIONS OVERRIDES
        protected override void OnCollisionStay(Collision collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONSTAY) 
                && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))

                Execute();
        }
        protected override void OnCollisionStay2D(Collision2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.COLLISIONSTAY2D) 
                && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))
                Execute();
        }
        protected override void OnTriggerStay(Collider other)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGERSTAY) 
                && Utilities.LayerIsInLayerMask(other.gameObject.layer,allowedLayers)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))
                Execute();
        }
        protected override void OnTriggerStay2D(Collider2D collision)
        {
            if (executeWhen.Equals(ExecuteWhen.TRIGGERSTAY2D) 
                && Utilities.LayerIsInLayerMask(collision.gameObject.layer, allowedLayers)
                && (currentPlayngAudio == null || !currentPlayngAudio.isPlaying))
                Execute();
        }
        #endregion
        #endregion

        #region GENERIC METHODS AND OVERRIDES
        /// <summary>
        /// Play a clip using PlayableAudioDefinition param
        /// </summary>
        /// <param name="audioDefinition">Define how the clip must be played</param>
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
        /// <summary>
        /// Play the clip using internal PlayableAudioDefinition
        /// </summary>
        private void PlayClip()
        {
            PlayClip(audioDefinition);
        }
        #endregion

    }

    
}
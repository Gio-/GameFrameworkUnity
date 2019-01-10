using System;
using UnityEngine;

namespace GameFramework
{
    public class PlayAudioMultiple : MonoBehaviour
    {
        #region VARIABLES
        //########### NEXT VAR WARNING! #################
        //If set true, Dispose will be called on disable instead destroy 
        //Note that if you destroy object with this flag set to true, the sound can't be played on destroy becouse actions are cleaned
        [SerializeField]
        [Header("WARNING! FLAG THIS IF OBJECT IS PART OF A POOL")]
        private bool isPoolObject = false;
        [SerializeField]
        private PlayableAudioDefinition[] audioList = null;
        #endregion

        #region ACTIONS
        public Action<PlayableAudioDefinition> StartAction { get; private set; }
        public Action EnableAction { get; private set; }
        public Action DisableAction { get; private set; }
        public Action DestroyAction { get; private set; }

        public Action<Collision> CollisionEnterAction { get; private set; }
        public Action<Collision> CollisionStayAction { get; private set; }
        public Action<Collision> CollisionExitAction { get; private set; }
        public Action<Collider> TriggerEnterAction { get; private set; }
        public Action<Collider> TriggerStayAction { get; private set; }
        public Action<Collider> TriggerExitAction { get; private set; }

        public Action<Collider2D> TriggerEnter2DAction { get; private set; }
        public Action<Collider2D> TriggerStay2DAction { get; private set; }
        public Action<Collider2D> TriggerExit2DAction { get; private set; }
        public Action<Collision2D> CollisionEnter2DAction { get; private set; }
        public Action<Collision2D> CollisionStay2DAction { get; private set; }
        public Action<Collision2D> CollisionExit2DAction { get; private set; }
        #endregion

        #region MONOBEHAVIOUR METHODS
        public void Awake()
        {
            Initializate();
        }
        void Start()
        {
            if (StartAction != null)
                StartAction.Invoke(null);
        }
        private void OnEnable()
        {
            if (isPoolObject)
                Initializate();
            if (EnableAction != null)
                EnableAction.Invoke();
        }
        private void OnDisable()
        {
            if (DisableAction != null)
                DisableAction.Invoke();
            if (isPoolObject)
                Dispose();
        }
        private void OnDestroy()
        {
            if (DestroyAction != null)
                DestroyAction.Invoke();
            if (!isPoolObject)
                Dispose();
        }

        #region COLLISION/TRIGGER DETECTIONS
        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionEnterAction != null)
                CollisionEnterAction.Invoke(collision);

        }
        private void OnCollisionStay(Collision collision)
        {
            if (CollisionStayAction != null)
                CollisionStayAction.Invoke(collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            if (CollisionExitAction != null)
                CollisionExitAction.Invoke(collision);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CollisionEnter2DAction != null)
                CollisionEnter2DAction.Invoke(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (CollisionStay2DAction != null)
                CollisionStay2DAction.Invoke(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (CollisionExit2DAction != null)
                CollisionExit2DAction.Invoke(collision);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (TriggerEnterAction != null)
                TriggerEnterAction.Invoke(other);
        }
        private void OnTriggerStay(Collider other)
        {
            if (TriggerStayAction != null)
                TriggerStayAction.Invoke(other);

        }
        private void OnTriggerExit(Collider other)
        {
            if (TriggerExitAction != null)
                TriggerExitAction.Invoke(other);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (TriggerEnter2DAction != null)
                TriggerEnter2DAction.Invoke(collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (TriggerStay2DAction != null)
                TriggerStay2DAction.Invoke(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (TriggerExit2DAction != null)
                TriggerExit2DAction.Invoke(collision);
        }
        #endregion
        #endregion

        #region INIT / DISPOSE
        private void Initializate()
        {
            #region BIND EACH AUDIO DEFINITION TO RIGHT EVENT (using playEvent var)
            foreach (PlayableAudioDefinition currentAudio in audioList)
            {
                //bind audio to right event
                switch (currentAudio.playEvent)
                {
                    case ExecuteWhen.NONE:
                        break;
                    case ExecuteWhen.EVENT:
                        currentAudio.handler = () => { PlayClip(currentAudio); }; 
                        EventManager.StartListening(currentAudio.eventToListen, currentAudio.handler);
                        break;
                    case ExecuteWhen.START:
                        StartAction += (e) => { PlayClip(currentAudio); };
                        break;
                    case ExecuteWhen.ENABLE:
                        EnableAction += () => { PlayClip(currentAudio); };
                        break;
                    case ExecuteWhen.DISABLE:
                        DisableAction += () => { PlayClip(currentAudio); };
                        break;
                    case ExecuteWhen.DESTROY:
                        DestroyAction += () => { PlayClip(currentAudio); };
                        break;
                    case ExecuteWhen.COLLISIONENTER:
                        CollisionEnterAction += (collision) => { OnCollisionAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.COLLISIONSTAY:
                        CollisionStayAction += (collision) => { OnCollisionAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.COLLISIONEXIT:
                        CollisionExitAction += (collision) => { OnCollisionAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.TRIGGERENTER:
                        TriggerEnterAction += (collision) => { OnTriggerAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.TRIGGERSTAY:
                        TriggerStayAction += (collision) => { OnTriggerAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.TRIGGEREXIT:
                        TriggerExitAction += (collision) => { OnTriggerAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.COLLISIONENTER2D:
                        CollisionEnter2DAction += (collision) => { OnCollision2DAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.COLLISIONSTAY2D:
                        CollisionStay2DAction += (collision) => { OnCollision2DAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.COLLISIONEXIT2D:
                        CollisionExit2DAction += (collision) => { OnCollision2DAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.TRIGGERENTER2D:
                        TriggerEnter2DAction += (collision) => { OnTrigger2DAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.TRIGGERSTAY2D:
                        TriggerStay2DAction += (collision) => { OnTrigger2DAction(currentAudio, collision); };
                        break;
                    case ExecuteWhen.TRIGGEREXIT2D:
                        TriggerExit2DAction += (collision) => { OnTrigger2DAction(currentAudio, collision); };
                        break;

                }
            }
            #endregion
        }

        private void PlayTest()
        {

        }

        private void Dispose()
        {
            #region UNSUBSCRIBE IN CASE OF TRIGGER BY EVENT
            foreach (PlayableAudioDefinition currentAudio in audioList)
            {
                if (currentAudio.playEvent.Equals(ExecuteWhen.EVENT))
                {
                    EventManager.StopListening(currentAudio.eventToListen, currentAudio.handler);
                    currentAudio.handler = null;
                }
            }
            #endregion
            #region UNSUBSCRIBE COLLISION/TRIGGER HANDLERS
            StartAction = null;
            EnableAction = null;
            DisableAction = null;
            DestroyAction = null;
            CollisionEnterAction = null;
            CollisionExitAction = null;
            CollisionStayAction = null;
            TriggerEnterAction = null;
            TriggerStayAction = null;
            TriggerExitAction = null;
            CollisionEnter2DAction = null;
            CollisionExit2DAction = null;
            CollisionStay2DAction = null;
            TriggerEnter2DAction = null;
            TriggerStay2DAction = null;
            TriggerExit2DAction = null;
            #endregion
        }
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
            if (!audioDefinition.onceAtTime)
                audioId += UnityEngine.Random.Range(0, 10000);
            //Play Audio
            AudioManager.PlayAudio(audioDefinition.audioClip.name, clipSettings, audioSettings3D);
        }
        #endregion

        #region BINDABLEACTIONS
        private void OnCollisionAction(PlayableAudioDefinition audioDefinition, Collision collision)
        {
            if (Utilities.LayerIsInLayerMask(collision.gameObject.layer, audioDefinition.allowedLayer))
            {
                PlayClip(audioDefinition);
            }
        }

        private void OnTriggerAction(PlayableAudioDefinition audioDefinition, Collider _collider)
        {
            if (Utilities.LayerIsInLayerMask(_collider.gameObject.layer, audioDefinition.allowedLayer))
            {
                PlayClip(audioDefinition);
            }
        }

        private void OnCollision2DAction(PlayableAudioDefinition audioDefinition, Collision2D collision2D)
        {
            if (Utilities.LayerIsInLayerMask(collision2D.gameObject.layer, audioDefinition.allowedLayer))
            {
                PlayClip(audioDefinition);
            }
        }

        private void OnTrigger2DAction(PlayableAudioDefinition audioDefinition, Collider2D _collider2D)
        {
            if (Utilities.LayerIsInLayerMask(_collider2D.gameObject.layer, audioDefinition.allowedLayer))
            {
                PlayClip(audioDefinition);
            }
        }
        #endregion
    }

}
/**
 * @author Fabrizio Coppolecchia
 *
 * Audio Manager utility, use this to play your audio!
 * This class also provide a simple pool system to improve speed
 * 
 * WARNING!!!!: WHEN YOU PLAY AUDIO USE clip.name AS AUDIO ID , EXPECIALLI IF YOU AVE ADDED AUDIO TO INITIAL POOL.
 * 
 * @date - 2019/01/03
 */

using System.Collections.Generic;
using UnityEngine;


namespace GameFramework { 
    public class AudioManager : MonoBehaviour
    {
        #region VARIABLES
        private static AudioManager s_Instance = null;   // SINGLETON

        [Range(0, 1)]
        public float globalVolume = 1f; // GAME VOLUME
        [SerializeField]
        private AudioSourcePoolInfo[] initialPoolInfo;

        private Dictionary<string, List<AudioSource>> audioSourcePool = new Dictionary<string, List<AudioSource>>(); //LIST OF POOLED AUDIO
        private Dictionary<string, List<AudioSource>> pausedAudio = new Dictionary<string, List<AudioSource>>(); //LIST OF PAUSED AUDIO (REMOVED FROM PREVIOUS LIST)
        #endregion

        #region PROPERTIES
        public static AudioManager Instance // PUBLIC SINGLETON PROPERTY
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
                    /*if (s_Instance == null) {
                        GameObject go = Instantiate(new GameObject());
                        go.name = "AudioManager";
                        s_Instance = go.AddComponent<AudioManager>();
                    }*/
                }

                return s_Instance;
            }
        }
        #endregion

        #region MONOBEHAVIOURS METHODS
        void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
            }
            else if (s_Instance != this)
            {
                Destroy(s_Instance);
            }

        }
        private void OnEnable()
        {
            Initialize();
        }
        private void OnDisable()
        {
            StopAllAudio();
            audioSourcePool.Clear();
            pausedAudio.Clear();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Initialize pool and pausedAudio dictionaries and populate pool based on initialPoolInfo var
        /// </summary>
        public void Initialize()
        {
            audioSourcePool = new Dictionary<string, List<AudioSource>>();
            pausedAudio = new Dictionary<string, List<AudioSource>>();
            if (initialPoolInfo == null)
                initialPoolInfo = new AudioSourcePoolInfo[] { };

            foreach (AudioSourcePoolInfo poolInfo in initialPoolInfo)
            {
                for(int i = 0; i < poolInfo.quantity; i++)
                {
                    AddAudioToPool(poolInfo.audio.name);
                }
            }
        }

        /// <summary>
        /// Add audio to pool and return audio source
        /// </summary>
        /// <param name="audioSourceId"></param>
        /// <returns> the audiosource component added to pool</returns>
        public static AudioSource AddAudioToPool(string audioSourceId)
        {
            GameObject audioObj;
            AudioSource aSource;
            audioObj = new GameObject("AudioPooledObject"); // create the temp object
            aSource = audioObj.AddComponent<AudioSource>(); // add an audio source
            audioObj.AddComponent<DisableAudioOnComplete>(); // Add checker to disable object when audio is not playng
            //If key not exist create it
            if (!AudioManager.Instance.audioSourcePool.ContainsKey(audioSourceId))
                AudioManager.Instance.audioSourcePool.Add(audioSourceId, new List<AudioSource>());
            //Add audio to pool
            AudioManager.Instance.audioSourcePool[audioSourceId].Add(aSource);

            return aSource;
        }

        //NB: useAcrive non è necessario perchè se ti serve sovrascrivere degli audio basta che ti salvi l' audio ricevuto dal playAudio e usi quello.
        //Monitora e nel caso rimuovilo
        /// <summary>
        /// Get Audio from pool
        /// </summary>
        /// <param name="audioSourceId"></param>
        /// <param name="useActive">If enabled audio will picked from an active one (must be refined)</param>
        /// <returns></returns>
        public AudioSource getPooledAudio(string audioSourceId, bool useActive = false)
        {
            foreach(AudioSource aSource in audioSourcePool[audioSourceId])
            {
                if(aSource != null) { 
                    GameObject audioObj = aSource.gameObject;
                    //TODO ATTENZIONE!!!!! al momento l' useActive non si comporta come dovrebbe 
                    //non sovrascrive un dato audio ma sovrascrive solo se trova  un audio con la stessa key abilitato nella pool
                    if ((useActive && audioObj.activeSelf) || (!useActive && !audioObj.activeSelf))
                        return aSource;
                }
            }
            //If audio is not available i add one to pool and return it
            return AddAudioToPool(audioSourceId);
        }

        /// <summary>
        /// Get audio from pool or add it to pool and then play 
        /// </summary>
        /// <param name="audioSourceId">Best pratice is to use clip.name</param>
        /// <param name="settings">Audio settings</param>
        /// <param name="settings3d"></param>
        /// <returns></returns>
        public static AudioSource PlayAudio(string audioSourceId, AudioClipSettings settings, AudioSettings3D settings3d = null)
        {
            if (AudioManager.Instance == null || settings.clip == null)
                return null;

            if (string.IsNullOrEmpty(audioSourceId)) //IF id is empty i will use clip.name
                audioSourceId = settings.clip.name;

            AudioSource aSource;

            //If audio is not pooled it will be pooled else will be use pooled one
            if (!AudioManager.Instance.audioSourcePool.ContainsKey(audioSourceId))
                aSource = AddAudioToPool(audioSourceId);
            else
                aSource = AudioManager.Instance.getPooledAudio(audioSourceId);
            // stop audio as first thing to avoid strange problems
            aSource.Stop();

            //Set AudioClipSettings in audiosource component
            float realVolume = (settings.volume * AudioManager.Instance.globalVolume); //Calculate volume
            aSource.clip = settings.clip; // Define the clips
            aSource.playOnAwake = false; // Disable play on awake to customize play time (the play function is called at the end of procedure)
            aSource.volume = realVolume; // Apply volume
            aSource.pitch = settings.pitch; //Aplly pitch
            aSource.loop = settings.loop; //Set loop
            aSource.mute = settings.mute; // Set Mute

            aSource.bypassEffects = settings.bypassEffects;
            aSource.bypassListenerEffects = settings.bypassListenerEffects;
            aSource.bypassReverbZones = settings.bypassReverbZones;
            aSource.priority = settings.priority;
            aSource.panStereo = settings.stereoPan;
            aSource.spatialBlend = settings.spatialBled;
            aSource.reverbZoneMix = settings.reverbZoneMix;
            aSource.spatialBlend = 0; //This will be overrided if settings3d is not null

            //Enable audio object
            aSource.gameObject.SetActive(true);

            //If settings3d is set they will be applyed over there
            if (settings3d != null)
            {
                aSource.rolloffMode = settings3d.rollofMode;
                aSource.spatialBlend = settings3d.spatialBlend;
                aSource.dopplerLevel = settings3d.dopplerLevel;
                aSource.spread = settings3d.spread;
                aSource.minDistance = settings3d.minDistance;
                aSource.maxDistance = settings3d.maxDistance;
                aSource.gameObject.transform.position = settings3d.position;
                aSource.Play();
            }
            else if (settings.delay > 0)
                aSource.PlayDelayed(settings.delay); //Play audio after delay
            else
                aSource.Play(); //Play audio without delay

            return aSource;
            
        }
        /// <summary>
        /// Get audio from pool or add it to pool and then play 
        /// </summary>
        /// <param name="audioSourceId"></param>
        /// <param name="clip"></param>
        /// <param name="pitch"></param>
        /// <param name="loop"></param>
        /// <param name="volume"></param>
        /// <param name="delay"></param>
        /// <param name="overflow"></param>
        /// <returns></returns>
        public static AudioSource PlayAudio(string audioSourceId, AudioClip clip, float pitch = 1, bool loop = false, float volume = 1, float delay = 0, bool overflow = true)
        {
            AudioClipSettings audioSettings = new AudioClipSettings(clip, delay, loop, volume, pitch, overflow);
            return PlayAudio(audioSourceId, audioSettings);
        }


        public static void StopAudio(string audioSourceId)
        {
            if (AudioManager.Instance.audioSourcePool.ContainsKey(audioSourceId))
            {
                foreach(AudioSource source in AudioManager.Instance.audioSourcePool[audioSourceId])
                {
                    if (source != null)
                        source.Stop();
                }
            }
        }
        public static void StopAllAudio()
        {
            foreach(KeyValuePair<string,List<AudioSource>> audioList in AudioManager.Instance.audioSourcePool)
            {
                StopAudio(audioList.Key);
            }
        }

        public static void PauseAudio(string audioSourceId)
        {

            if (AudioManager.Instance.audioSourcePool.ContainsKey(audioSourceId))
            {
                foreach (AudioSource audio in AudioManager.Instance.audioSourcePool[audioSourceId])
                {
                    if (audio.gameObject.activeSelf && audio.isPlaying) { 
                        audio.Pause();
                        if (!AudioManager.Instance.pausedAudio.ContainsKey(audioSourceId))
                            AudioManager.Instance.pausedAudio.Add(audioSourceId, new List<AudioSource>());

                        AudioManager.Instance.pausedAudio[audioSourceId].Add(audio);
                    }
                }
            }
        }
        public static void PauseAllAudio()
        {
            ResumeAllAudio();
            AudioManager.Instance.pausedAudio = new Dictionary<string, List<AudioSource>>();

            foreach (KeyValuePair<string, List<AudioSource>> audioList in AudioManager.Instance.audioSourcePool)
            {
                PauseAudio(audioList.Key);
            }
        }

        public static void ResumeAudio(string audioSourceId)
        {
            if (AudioManager.Instance.pausedAudio.ContainsKey(audioSourceId))
            {
                foreach (AudioSource audio in AudioManager.Instance.pausedAudio[audioSourceId])
                {
                    audio.UnPause();
                    AudioManager.Instance.pausedAudio[audioSourceId].Remove(audio);
                    AudioManager.Instance.audioSourcePool[audioSourceId].Add(audio);
                }
            }
        }
        public static void ResumeAllAudio()
        {
            foreach (KeyValuePair<string, List<AudioSource>> audioList in AudioManager.Instance.audioSourcePool)
            {
                ResumeAudio(audioList.Key);
            }
            AudioManager.Instance.pausedAudio = new Dictionary<string, List<AudioSource>>();
        }
        #endregion


    }
    public class AudioSourcePoolInfo
    {
        public AudioClip audio;
        public int quantity = 1;
    }
    public class AudioClipSettings
    {
        public AudioClip clip = null;
        public bool overflow = true;
        public float delay = 0;
        public float volume = 1;
        public bool loop = false;
        public float pitch = 1;
        public bool mute = false;
        public bool bypassEffects = false;
        public bool bypassListenerEffects = false;
        public bool bypassReverbZones = false;
        public int priority = 128;
        public float stereoPan = 0;
        public float spatialBled = 0;
        public float reverbZoneMix = 1;

        public AudioClipSettings(AudioClip clip, float pitch , bool loop, float volume, float delay, bool overflow = true)
        {
            this.clip = clip;
            this.delay = delay;
            this.volume = volume;
            this.pitch = pitch;
            this.loop = loop;
            this.overflow = overflow;
        }
    }
    public class AudioSettings3D
    {
        public Vector3 position = Vector3.zero;
        public AudioRolloffMode rollofMode = AudioRolloffMode.Linear;
        public float spatialBlend = 1f; //THIS MAKE AUDIO 3D
        public float dopplerLevel = 1f;
        public float spread = 0;
        public float minDistance = 1;
        public float maxDistance = 500;
    }
}
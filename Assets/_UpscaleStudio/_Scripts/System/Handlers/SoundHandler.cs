using System.Collections.Generic;
using _UpscaleStudio._Scripts.Data.SoundData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _UpscaleStudio._Scripts.System.Handlers {
    public class SoundHandler : MonoBehaviour {
        public static SoundHandler Instance;

        [SerializeField] private SoundData mainMusicData; 
        [SerializeField] private SoundData firstLevelMusicData; 
        private SoundData currentMusicData; 
        private AudioSource musicSource; 
        private Dictionary<GameObject, AudioSource> activeSounds = new Dictionary<GameObject, AudioSource>(); 

        private void Awake() {
            // Singleton pattern to ensure only one instance exists
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
                InitializeMusicSource();
            } else {
                Destroy(gameObject); 
            }

            // Subscribe to scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy() {
            // Unsubscribe from the scene loaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            // Play appropriate music based on the loaded scene
            if (scene.name == "MainMenu") {
                PlayMusic(mainMusicData);
            } else if (scene.name == "FirstLevel") {
                ChangeMusic(firstLevelMusicData);
            }
        }

        private void InitializeMusicSource() {
            // Initialize the AudioSource for music playback
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true; 
            musicSource.playOnAwake = false; 
        }

        public void PlaySound(SoundData soundData, Vector3 position = default, GameObject attachedObject = null) {
            // Play a sound effect at a specified position
            AudioSource source = new GameObject("Audio").AddComponent<AudioSource>();
            source.clip = soundData.audioClip;
            source.volume = soundData.volume;
            source.loop = soundData.loop;
            source.spatialBlend = soundData.is3D ? 1f : 0f; // Set to 3D or 2D sound

            if (attachedObject != null) {
                // Attach sound source to a game object
                source.transform.parent = attachedObject.transform; 
                activeSounds[attachedObject] = source; // Track the sound source
            }

            source.Play();

            // Destroy sound source after playback if not looping
            if (!soundData.loop) {
                Destroy(source.gameObject, soundData.audioClip.length);
            }
        }
        public void PauseAllSounds() {
            // Pause all currently active sounds
            foreach (var sound in activeSounds.Values) {
                sound.Pause();
            }
            if (musicSource.isPlaying) {
                musicSource.Pause(); // Pause the music as well
            }
        }

        public void ResumeAllSounds() {
            // Resume all currently paused sounds
            foreach (var sound in activeSounds.Values) {
                sound.UnPause();
            }
            if (musicSource.clip != null) {
                musicSource.UnPause(); // Resume the music
            }
        }

        public void StopAllSounds() {
            // Stop all active sounds
            foreach (var sound in activeSounds.Values) {
                if (sound != null) { 
                    sound.Stop();
                }
            }
            activeSounds.Clear(); 
        }
        public void StopSound(SoundData soundData, GameObject attachedObject) {
            // Stop a sound effect associated with a game object
            if (attachedObject != null && activeSounds.ContainsKey(attachedObject)) {
                AudioSource source = activeSounds[attachedObject];
                if (source.clip == soundData.audioClip) {
                    source.Stop();
                    Destroy(source.gameObject);
                    activeSounds.Remove(attachedObject);
                }
            }
        }
        // Play background music
        public void PlayMusic(SoundData soundData) {
           
            if (currentMusicData == soundData && musicSource.isPlaying) {
                return; 
            }
           

            currentMusicData = soundData;
            musicSource.clip = soundData.audioClip;
            musicSource.volume = soundData.volume;
            musicSource.loop = soundData.loop;
            musicSource.Play();
        }

        public void StopMusic() {
            // Stop currently playing background music
            if (musicSource.isPlaying) {
                musicSource.Stop();
            }
        }

        public void ChangeMusic(SoundData newMusic) {
            // Change background music to a new track
            StopMusic();
            PlayMusic(newMusic);
        }

        public void SetSoundSpeed(GameObject sourceGameObject, float speed) {
            // Set the playback speed (pitch) of a sound source
            AudioSource audioSource = sourceGameObject.GetComponentInChildren<AudioSource>();
            if (audioSource != null) {
                audioSource.pitch = speed;
            }
        }
        public void PlayFirstLevelMusic() {
            ChangeMusic(firstLevelMusicData);
        }
        [SerializeField] private float masterVolume = 1f; 

        public void SetMasterVolume(float volume) {
            masterVolume = volume;
            musicSource.volume = volume; 

            foreach (var sound in activeSounds.Values) {
                sound.volume = volume; 
            }
        }
        public float GetMasterVolume()
        {
            return masterVolume; 
        }
    }
}

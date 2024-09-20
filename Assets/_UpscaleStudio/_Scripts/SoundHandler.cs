using System.Collections.Generic;
using _UpscaleStudio._Scripts.Data.SoundData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _UpscaleStudio._Scripts {
    public class SoundHandler : MonoBehaviour {
        public static SoundHandler Instance;

        [SerializeField] private SoundData mainMusicData;
        [SerializeField] private SoundData firstLevelMusicData;
        private SoundData currentMusicData;
        private AudioSource musicSource;

        // Словарь для хранения активных аудиосорсов по объекту
        private Dictionary<GameObject, AudioSource> activeSounds = new Dictionary<GameObject, AudioSource>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeMusicSource();
            }
            else {
                Destroy(gameObject);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (scene.name == "MainMenu") {
                PlayMusic(mainMusicData);
            }
            else if (scene.name == "FirstLevel") {
                ChangeMusic(firstLevelMusicData);
            }
        }

        private void InitializeMusicSource() {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        public void PlaySound(SoundData soundData, Vector3 position = default, GameObject attachedObject = null) {
            AudioSource source = new GameObject("Audio").AddComponent<AudioSource>();
            source.clip = soundData.audioClip;
            source.volume = soundData.volume;
            source.loop = soundData.loop;
            source.spatialBlend = soundData.is3D ? 1f : 0f; // 3D or 2D sound

            if (attachedObject != null) {
                source.transform.parent = attachedObject.transform; // Sound attached to the object
                activeSounds[attachedObject] = source; // Добавляем в словарь
            }

            source.Play();

            if (!soundData.loop) {
                Destroy(source.gameObject, soundData.audioClip.length);
            }
        }

        public void StopSound(SoundData soundData, GameObject attachedObject) {
            if (attachedObject != null && activeSounds.ContainsKey(attachedObject)) {
                AudioSource source = activeSounds[attachedObject];
                if (source.clip == soundData.audioClip) {
                    source.Stop();
                    Destroy(source.gameObject);
                    activeSounds.Remove(attachedObject);
                }
            }
        }

        public void PlayMusic(SoundData soundData) {
            if (currentMusicData == soundData) return; // Avoid restarting the same music

            currentMusicData = soundData;
            musicSource.clip = soundData.audioClip;
            musicSource.volume = soundData.volume;
            musicSource.loop = soundData.loop;
            musicSource.Play();
        }

        public void StopMusic() {
            if (musicSource.isPlaying) {
                musicSource.Stop();
            }
        }

        public void ChangeMusic(SoundData newMusic) {
            StopMusic();
            PlayMusic(newMusic);
        }

        public void SetSoundSpeed(SoundData soundData, GameObject sourceGameObject, float speed) {
            AudioSource audioSource = sourceGameObject.GetComponent<AudioSource>();
            if (audioSource != null) {
                audioSource.pitch = speed;
            }
        }
    }
}

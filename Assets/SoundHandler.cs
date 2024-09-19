
using _UpscaleStudio._Scripts.Data.SoundData;
using UnityEngine;

public class SoundHandler : MonoBehaviour {
    public static SoundHandler Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundData soundData, Vector3 position = default, GameObject attachedObject = null) {
        AudioSource source = new GameObject("Audio").AddComponent<AudioSource>();
        source.clip = soundData.audioClip;
        source.volume = soundData.volume;
        source.loop = soundData.loop;
        source.spatialBlend = soundData.is3D ? 1f : 0f; // 3D or 2D sound

        if (attachedObject != null) {
            source.transform.parent = attachedObject.transform; // Sound attached to the object
        }

        source.Play();

        if (!soundData.loop) {
            Destroy(source.gameObject, soundData.audioClip.length);
        }
    }
}
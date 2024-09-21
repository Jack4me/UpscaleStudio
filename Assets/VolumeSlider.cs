using System;
using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
   
        [SerializeField] private Slider volumeSlider; // Ссылка на слайдер

        private void Start()
        {
            // Установим начальное значение слайдера
            if (SoundHandler.Instance != null)
            {
                volumeSlider.value = SoundHandler.Instance.GetMasterVolume(); // Получаем текущую громкость
            }

            // Добавляем слушатель для изменения громкости
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDestroy()
        {
            // Удаляем слушатель при уничтожении
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }

        private void SetVolume(float volume)
        {
            // Устанавливаем громкость в SoundHandler
            if (SoundHandler.Instance != null)
            {
                SoundHandler.Instance.SetMasterVolume(volume);
            }
        }
    }

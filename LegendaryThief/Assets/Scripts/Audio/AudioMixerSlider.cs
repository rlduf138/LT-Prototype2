using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerSlider : MonoBehaviour
{
      public AudioMixer masterMixer;
      public Slider bgmSlider;
      public Slider sfxSlider;
      public Slider masterSlider;

      private GameObject[] checker;

      protected void Awake()
      {
            checker = GameObject.FindGameObjectsWithTag("Menu");
            if(checker.Length >= 2)
            {
                  Debug.Log("Destroy MenuCanvas");
                  Destroy(this.gameObject);
            }
            DontDestroyOnLoad(transform.gameObject);
      }
      protected void Start()
      {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
      }

      public void MasterControl()
      {
            float sound = masterSlider.value;

            if (sound == -40f) masterMixer.SetFloat("Master", -80);
            else masterMixer.SetFloat("Master", sound);

      }

      public void BGMControl()
      {
            float sound = bgmSlider.value;

            if (sound == -40f) masterMixer.SetFloat("BGM", -80);
            else masterMixer.SetFloat("BGM", sound);

      }

      public void SfxControl()
      {
            float sound = sfxSlider.value;

            if (sound == -40f) masterMixer.SetFloat("SFX", -80);
            else masterMixer.SetFloat("SFX", sound);

      }


      public void ToggleAudioVolume()
      {
            AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
      }

      public void SaveOption()
      {
            PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
            PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
      }
}

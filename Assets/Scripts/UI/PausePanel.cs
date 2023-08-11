using UnityEngine;
using UnityEngine.Audio;

public class PausePanel : MonoBehaviour
{
   [SerializeField] private AudioMixerGroup _mixer;

   private void OnEnable()
   {
      Time.timeScale = 0f;
   }

   private void OnDisable()
   {
      Time.timeScale = 1f;
   }

   public void ToggleMusic(bool enable)
   {
      if (enable)
      {
         _mixer.audioMixer.SetFloat(Constants.AudioMixer.MusicVolume, Constants.AudioMixer.MaxVolumeValue);
      }
      else
      {
         _mixer.audioMixer.SetFloat(Constants.AudioMixer.MusicVolume, Constants.AudioMixer.MinVolumeValue);
      }
      
      PlayerPrefs.SetInt(Constants.AudioMixer.MusicVolume, enable ? 1 : 0);
   }

   public void ChangeVolume(float volume)
   {
      _mixer.audioMixer.SetFloat(Constants.AudioMixer.MasterVolume, Mathf.Lerp(Constants.AudioMixer.MinVolumeValue, Constants.AudioMixer.MaxVolumeValue, volume));
      PlayerPrefs.SetFloat(Constants.AudioMixer.MasterVolume, volume);
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField] private SoundData _musicSound, _sfxSound;
    [SerializeField] private AudioSource _musicSource, _sfxSource;

    // private bool _isSoundMute;
    private bool _isVibrationMute;
    
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        // _isSoundMute = PlayerData.GetSound();
        _isVibrationMute = PlayerData.IsVibrationMute();
        
        _musicSource.mute = PlayerData.IsSoundMuted();
        _sfxSource.mute = PlayerData.IsSoundMuted();
        
    }

    /// <summary>
    /// Plays background music based on on the provided ESound enum
    /// </summary>
    /// <param name="sound"></param>
    public void PlayMusic(ESound sound)
    {
        bool soundFound = false;
        foreach (var musicSound in _musicSound.soundList)
        {
            if (sound == musicSound.sound)
            {
                soundFound = true;
                _musicSource.clip = musicSound.clip;
                _musicSource.Play();
                break;
            }
        }
        
        if(!soundFound)
            Debug.LogError("Sound " + sound + " not found!");
    }

    /// <summary>
    /// Stops the currently playing background music
    /// </summary>
    public void StopMusic()
    {
        _musicSource.Stop();
    }
    
    /// <summary>
    /// Plays a sound effect based on the provided ESound enum
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySFX(ESound sound)
    {
        bool soundFound = false;
        foreach (var sfxSound in _sfxSound.soundList)
        {
            if (sound == sfxSound.sound)
            {
                soundFound = true;
                _sfxSource.PlayOneShot(sfxSound.clip);
                break;
            }
        }
        
        if(!soundFound)
            Debug.LogError("Sound " + sound + " not found!");
    }

    
    /// <summary>
    /// Initiates device vibration if vibration is not muted
    /// </summary>
    public void PlayVibration()
    {
        if(!_isVibrationMute) Handheld.Vibrate();
    }

    /// <summary>
    /// Toggles the vibration setting and saves the preference to the player's data
    /// </summary>
    public void ToggleVibration()
    {
        _isVibrationMute = !_isVibrationMute;
        PlayerData.SetVibration(_isVibrationMute);
    }

    /// <summary>
    /// Toggles the sound setting for both music and sound effects and saves the preference to the player's data
    /// </summary>
    public void ToggleSound()
    {
        var mute = !_musicSource.mute;
        
        _musicSource.mute = mute;
        _sfxSource.mute = mute;

        PlayerData.SetSound(mute);
    }
    
    
    
}

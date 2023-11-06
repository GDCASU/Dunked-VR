using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Music
{
    Ambient,
    Game
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip _ambientMusic, _gameMusic;

    private void Awake()        // Handle Singleton
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayMusic(Music.Ambient);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            _sfxSource.PlayOneShot(clip);
    }

    public void ChangeSFXVolume(float value)
    {
        _sfxSource.volume = value;
    }

    public void PlayMusic(Music track)
    {

        _musicSource.pitch = 1;
        if (_musicSource != null)
        {
            switch (track)
            {
                case Music.Ambient:
                    _musicSource.clip = _ambientMusic;
                    _musicSource.Play();
                    break;
                case Music.Game:
                    _musicSource.clip = _gameMusic;
                    _musicSource.Play();
                    break;
            }
        }
    }

    public float GetMusicVolume()
    {
        return _musicSource.volume;
    }

    public float GetSoundVolume()
    {
        return _sfxSource.volume;
    }

    public void ChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }

    public void StopMusicTrack()
    {
        _musicSource.Stop();
    }
}



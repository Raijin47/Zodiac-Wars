using System;
using UnityEngine;

[Serializable]
public class AudioSpriteSwap : AudioSettings
{
    [Space(10)]
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;

    [Space(10)]
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;

    public Sprite SoundOn => _soundOnSprite;
    public Sprite SoundOff => _soundOffSprite;
    public Sprite MusicOn => _musicOnSprite;
    public Sprite MusicOff => _musicOffSprite;

    private bool _soundValue;
    private bool _musicValue;

    public override void Init()
    {
        SoundValue = PlayerPrefs.GetInt(SoundSave, 1) == 1;
        MusicValue = PlayerPrefs.GetInt(MusicSave, 1) == 1;
    }

    public bool SoundValue
    {
        get => _soundValue;
        set
        {
            _soundValue = value;
            _mixer.audioMixer.SetFloat(Sound, value ? On : Off);
            PlayerPrefs.SetInt(SoundSave, value ? 1 : 0);
        }
    }

    public bool MusicValue
    {
        get => _musicValue;
        set
        {
            _musicValue = value;
            _mixer.audioMixer.SetFloat(Music, value ? On : Off);
            PlayerPrefs.SetInt(MusicSave, value ? 1 : 0);
        }
    }
}
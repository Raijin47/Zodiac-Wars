using System;
using UnityEngine;

[Serializable]
public class GameAudio
{
    [SerializeField] private AudioSource _audioSource;

    [Space(10)]
    [SerializeField] private AudioClip _onClick;

    public void OnClick() => _audioSource.PlayOneShot(_onClick);
}
using System;
using UnityEngine;

[Serializable]
public class GameAudio
{
    [SerializeField] private AudioSource _audioSource;

    [Space(10)]
    [SerializeField] private AudioClip _onClick;
    [SerializeField] private AudioClip[] _clips;


    public void OnClick() => _audioSource.PlayOneShot(_onClick);

    public void PlayClip(int id) => _audioSource.PlayOneShot(_clips[id]);
}
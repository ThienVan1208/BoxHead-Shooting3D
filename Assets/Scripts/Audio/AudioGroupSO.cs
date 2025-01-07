using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroupSO", menuName = "Audio/AudioGroupSO")]
public class AudioGroupSO : ScriptableObject
{
    [SerializeField] private AudioClip[] _audioClips;
    [Range(0f, 1f)] public float Volume;

    public AudioClip GetRandomClip()
    {
        return _audioClips[Random.Range(0, _audioClips.Length)];
    }
}

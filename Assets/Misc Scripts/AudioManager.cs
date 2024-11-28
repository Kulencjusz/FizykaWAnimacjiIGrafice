using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> clipList;
    private int currentTrackIndex = 0;

    private void Start()
    {
        if(clipList.Count > 0)
        {
            PlayTrack(currentTrackIndex);
        }
    }

    private void Update()
    {
        if (!source.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayTrack(int trackIndex)
    {
        source.clip = clipList[trackIndex];
        source.Play();
    }

    private void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % clipList.Count; 
        PlayTrack(currentTrackIndex);
    }
}

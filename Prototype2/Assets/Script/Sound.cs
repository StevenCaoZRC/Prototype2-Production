using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    public bool loop;
    // 0  is 2D and 1 is 3D
    [Range(0f, 1f)]
    public float SpatialBlend;

    //Populated in the AudioManager
    [HideInInspector]
    public AudioSource source;
}

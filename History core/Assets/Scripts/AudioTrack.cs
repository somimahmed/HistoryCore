// you manually assign the needed audio track through this class
using UnityEngine;

[System.Serializable]
public class AudioTrack {
    [Tooltip("Unique ID to reference this track in code.")]
    public string id;
    
    [Tooltip("The audio clip to play")]
    public AudioClip clip;
    
    [Tooltip("Should this track loop?")]
    public bool loop = false;
    
    [Range(0f, 1f), Tooltip("Default volume of this track.")]
    public float volume = 1f;
    
    [HideInInspector]
    public AudioSource source;  // We'll assign this at runtime
}
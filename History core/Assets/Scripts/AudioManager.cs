// assign this script to an empty manager object in the scene
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }
    
    [Header("Configure your tracks here")]
    public List<AudioTrack> tracks = new List<AudioTrack>();

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitAudioSources();
    }

    void InitAudioSources() {
        foreach (var t in tracks) {
            GameObject go = new GameObject("Track_" + t.id);
            go.transform.parent = transform;
            AudioSource src = go.AddComponent<AudioSource>();
            src.clip = t.clip;
            src.loop = t.loop;
            src.volume = t.volume;
            t.source = src;
        }
    }

    // play immediately
    public void Play(string id) {
        var t = GetTrack(id);
        if (t != null) t.source.Play();
    }

    // play after couple seconds
    public void PlayDelayed(string id, float delay) {
        var t = GetTrack(id);
        if (t != null) t.source.PlayDelayed(delay);
    }

    // schedule play at DSP time
    public void PlayScheduled(string id, double dspTime) {
        var t = GetTrack(id);
        if (t != null) t.source.PlayScheduled(dspTime);
    }

    public void Stop(string id) {
        var t = GetTrack(id);
        if (t != null) t.source.Stop();
    }

    public void Pause(string id) {
        var t = GetTrack(id);
        if (t != null) t.source.Pause();
    }

    public void UnPause(string id) {
        var t = GetTrack(id);
        if (t != null) t.source.UnPause();
    }

    public void SetVolume(string id, float vol) {
        var t = GetTrack(id);
        if (t != null) t.source.volume = Mathf.Clamp01(vol);
    }

    public void FadeIn(string id, float duration) {
        StartCoroutine(FadeRoutine(id, duration, targetVol: GetTrack(id).volume));
    }

    public void FadeOut(string id, float duration) {
        StartCoroutine(FadeRoutine(id, duration, targetVol: 0f));
    }

    IEnumerator FadeRoutine(string id, float duration, float targetVol) {
        var t = GetTrack(id);
        if (t == null) yield break;
        AudioSource src = t.source;
        float startVol = src.volume;
        float elapsed = 0f;
        if (!src.isPlaying) src.Play();
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            src.volume = Mathf.Lerp(startVol, targetVol, elapsed / duration);
            yield return null;
        }
        src.volume = targetVol;
        if (Mathf.Approximately(targetVol, 0f)) src.Stop();
    }

    AudioTrack GetTrack(string id) {
        var t = tracks.Find(x => x.id == id);
        if (t == null) Debug.LogWarning($"no track with this id '{id}' found"); 
        return t;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] tracks;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);

        foreach (Sound s in tracks) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.loop = true;
            s.source.clip = s.clip;
        }

        tracks[0].source.Play();
    }

    public void Play(int i) {
        tracks[i].source.Play();
    }

    
}

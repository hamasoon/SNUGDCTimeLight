using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundEffects effectSounds = new SoundEffects(); // 효과음 오디오 클립들 -> soundSources와 같은 name을 가질것!
    [SerializeField] SoundSources soundSources = new SoundSources(); // 효과음 오디오 재생기들 -> effectSounds와 같은 name을 가질것!
    [SerializeField] SoundEffects bgmSounds = new SoundEffects();  // BGM 오디오 클립들
    [SerializeField] AudioSource audioSourceBFM;  // BGM 재생기. BGM 재생은 한 군데서만 이루어지면 되므로 배열 X 

    void Start()
    {
        PlayBGM("WhiteNoise");
    }

    public void PlaySE(string _name) //사운드 재생 name은 효과음 사운드 이름
    {
        if(effectSounds.ContainsKey(_name) && soundSources.ContainsKey(_name))
        {
            if(!soundSources[_name].isPlaying)
            {
                soundSources[_name].clip = effectSounds[_name];
                soundSources[_name].Play();
                Debug.Log(_name);
                return;
            }
        }
    }
    
    public void PlaySE(string SourceName, string SoundName) //사운드 재생 name은 효과음 사운드 이름
    {
        if(effectSounds.ContainsKey(SoundName) && soundSources.ContainsKey(SourceName))
        {
            if(!soundSources[SourceName].isPlaying)
            {
                soundSources[SourceName].clip = effectSounds[SoundName];
                soundSources[SourceName].Play();
                Debug.Log(SoundName);
                return;
            }
        }
    }
    
    public void PlaySE (string SoundName, AudioSource Source) //사운드 재생 name은 효과음 사운드 이름
    {
        if(effectSounds.ContainsKey(SoundName))
        {
            if(!Source.isPlaying)
            {
                Source.clip = effectSounds[SoundName];
                Source.Play();
                Debug.Log(SoundName);
                return;
            }
        }
    }

    public void PlayBGM(string _name)
    {
        if(bgmSounds.ContainsKey(_name))
        {
            audioSourceBFM.clip = bgmSounds[_name];
            audioSourceBFM.Play();
        }
    }

    public void StopAllSE()
    {
        foreach(string name in soundSources.Keys)
        {
            soundSources[name].Stop();
        }
    }

    public void StopSE(string _name)
    {
        if(effectSounds.ContainsKey(_name) && soundSources.ContainsKey(_name))
        {
            soundSources[_name].Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    List<AudioData> AudioDatas = new List<AudioData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddAudio(string name, float duration)
    {
        AudioData data = new AudioData();
        data.name = name;
        data.duration = duration;
        AudioDatas.Add(data);
    }

    public void PlaySound(string audioName)
    {
        //AkSoundEngine.PostEvent(audioName, gameObject);
    }

    public float GetAudioLength(string audioName)
    {
        float length = 0;
        foreach(AudioData data in AudioDatas)
        {
            if(data.name == audioName)
            {
                length = data.duration;
            }
        }
        if(length == 0)
        {
            Debug.LogWarning("Audio not found");
        }
        return length;
    }
}

public struct AudioData
{
    public string name;
    public float duration;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    temp_bgm,
    COUNT
}

public enum SFX
{
    temp_sfx,
    COUNT
}

public class AudioManager : SingletonBehaviour<AudioManager>
{
    public Transform BGMTrs;
    public Transform SFXTrs;

    private Dictionary<BGM, AudioSource> m_BGMPlayer = new Dictionary<BGM, AudioSource>();
    private Dictionary<SFX, AudioSource> m_SFXPlayer = new Dictionary<SFX, AudioSource>();

    private AudioSource m_CurrBGMSource;

    private float m_TempBGMValue;
    //private float m_TempSFXValue;

    protected override void Init()
    {
        base.Init();

        LoadBGMPlayer();
        LoadSFXPlayer();
    }

    private void LoadBGMPlayer()
    {
        Logger.Log($"{GetType()}::LoadBGMPlayer");

        for (int i = 0; i < (int)BGM.COUNT; i++)
        {
            var audioName = ((BGM)i).ToString();
            var audioClip = Resources.Load($"Audio/{audioName}", typeof(AudioClip)) as AudioClip;
            if(audioClip == null)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            GameObject newObj = new GameObject(audioName);
            var newAudioSource = newObj.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = true;
            newAudioSource.playOnAwake = false;
            newObj.transform.parent = BGMTrs;

            m_BGMPlayer[(BGM)i] = newAudioSource;
        }
    }

    private void LoadSFXPlayer()
    {
        Logger.Log($"{GetType()}::LoadSFXPlayer");

        for (int i = 0; i < (int)SFX.COUNT; i++)
        {
            var audioName = ((SFX)i).ToString();
            var audioClip = Resources.Load($"Audio/{audioName}", typeof(AudioClip)) as AudioClip;
            if (audioClip == null)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            GameObject newObj = new GameObject(audioName);
            var newAudioSource = newObj.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = false;
            newAudioSource.playOnAwake = false;
            newObj.transform.parent = SFXTrs;

            m_SFXPlayer[(SFX)i] = newAudioSource;
        }
    }

    public void PlayBGM(BGM bgm)
    {
        Logger.Log($"{GetType()}::PlayBGM");

        if (m_CurrBGMSource != null)
        {
            m_CurrBGMSource.Stop();
            m_CurrBGMSource = null;
        }

        if(!m_BGMPlayer.ContainsKey(bgm))
        {
            Logger.LogError($"Invalid clip name. ({bgm})");
            return;
        }

        m_CurrBGMSource = m_BGMPlayer[bgm];
        m_CurrBGMSource.Play();
    }

    public void PlaySFX(SFX sfx)
    {
        if(!m_SFXPlayer.ContainsKey(sfx))
        {
            Logger.LogError($"Invalid clip name. ({sfx})");
            return;
        }

        m_SFXPlayer[sfx].Play();
    }

    public void MuteBGM()
    {
        m_TempBGMValue = m_CurrBGMSource.volume;
        m_CurrBGMSource.volume = 0f;
    }

    public void UnMute()
    {
        m_CurrBGMSource.volume = m_TempBGMValue;
    }
}

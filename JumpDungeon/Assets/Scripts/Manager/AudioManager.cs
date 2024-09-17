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

    private Dictionary<BGM, AudioSource> _bgmPlayer = new Dictionary<BGM, AudioSource>();
    private Dictionary<SFX, AudioSource> _sfxPlayer = new Dictionary<SFX, AudioSource>();

    private AudioSource _currBGMSource;
    private float _tempBGMValue;
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

            _bgmPlayer[(BGM)i] = newAudioSource;
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

            _sfxPlayer[(SFX)i] = newAudioSource;
        }
    }

    public void OnLoadUserData()
    {
        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if(userSettingsData != null)
        {
            SetBGMValue(userSettingsData.BGMValue);
            SetSFXValue(userSettingsData.SFXValue);
        }
    }

    public void PlayBGM(BGM bgm)
    {
        Logger.Log($"{GetType()}::PlayBGM");

        if (_currBGMSource != null)
        {
            _currBGMSource.Stop();
            _currBGMSource = null;
        }

        if(!_bgmPlayer.ContainsKey(bgm))
        {
            Logger.LogError($"Invalid clip name. ({bgm})");
            return;
        }

        _currBGMSource = _bgmPlayer[bgm];
        _currBGMSource.Play();
    }

    public void PlaySFX(SFX sfx)
    {
        if(!_sfxPlayer.ContainsKey(sfx))
        {
            Logger.LogError($"Invalid clip name. ({sfx})");
            return;
        }

        _sfxPlayer[sfx].Play();
    }

    public void MuteBGM()
    {
        _tempBGMValue = _currBGMSource.volume;
        _currBGMSource.volume = 0f;
    }

    public void UnMute()
    {
        _currBGMSource.volume = _tempBGMValue;
    }

    public void SetBGMValue(float value)
    {
        foreach (var item in _bgmPlayer)
        {
            item.Value.volume = value;
        }
    }

    public void SetSFXValue(float value)
    {
        foreach (var item in _sfxPlayer)
        {
            item.Value.volume = value;
        }
    }
}

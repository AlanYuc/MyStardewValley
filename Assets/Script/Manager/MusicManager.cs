using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效管理器
/// </summary>
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    /// <summary>
    /// 所有音效的字典
    /// </summary>
    public Dictionary<string,AudioClip> wavDict = new Dictionary<string,AudioClip>();

    private void Awake()
    {
        Instance = this;

        LoadMusicAsset();
        
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 加载音效资源
    /// </summary>
    private void LoadMusicAsset()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Music");
        foreach (AudioClip clip in audioClips)
        {
            wavDict.Add(clip.name, clip);
        }

        Debug.Log("加载音效资源 " + wavDict.Count + " 个");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 单次播放
    /// </summary>
    /// <param name="clipName"></param>
    public void PlayOneClip(string clipName)
    {
        if (!wavDict.ContainsKey(clipName))
        {
            Debug.Log($"没有{clipName}的音效");
            return;
        }

        AudioClip clip = wavDict[clipName];
        GameObject audioObj = new GameObject("TempAudio");
        AudioSource audioSource = audioObj.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        //播放完销毁对象
        Destroy(audioObj, clip.length);
    }

    /// <summary>
    /// 获取音频剪辑
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public AudioClip GetAudioClip(string clipName)
    {
        if (!wavDict.ContainsKey(clipName))
        {
            Debug.Log($"没有{clipName}的音效");
            return null;
        }

        return wavDict[clipName];
    }

}

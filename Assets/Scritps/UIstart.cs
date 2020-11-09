using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIstart : MonoBehaviour
{
    /// <summary>
    /// 开始按钮 
    /// </summary>
    private Button btnPlay;
    /// <summary>
    /// 声音按钮
    /// </summary>
    private Button btnSound;
    /// <summary>
    /// 背景音乐播放器
    /// </summary>
    private AudioSource audioSourceBG;

    private Image imgSound;

    /// <summary>
    /// 声音的图片
    /// </summary>
    public Sprite[] soundSprites;
	
	void Start ()// Use this for initialization
    {
        getComponents();
        btnPlay.onClick.AddListener(onPlayClick);
        btnSound.onClick.AddListener(onSoundClick);
    }

    void onDestroy()
    {
        btnPlay.onClick.RemoveListener(onPlayClick);
        btnSound.onClick.RemoveListener(onSoundClick);
    }


	/// <summary>
    /// 寻找组件
    /// </summary>
    private void getComponents()
    {
        btnPlay = transform.Find("btnPlay").GetComponent<Button>();
        btnSound = transform.Find("btnSound").GetComponent<Button>();
        audioSourceBG = transform.Find("btnSound").GetComponent<AudioSource>();
        imgSound = transform.Find("btnSound").GetComponent<Image>();
    }
    /// <summary>
    /// 开始按钮按下
    /// </summary>
    void onPlayClick()
    {
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }
    /// <summary>
    /// 当声音按钮按下
    /// </summary>
    void onSoundClick()
    {
        if(audioSourceBG.isPlaying)
        {
            //正在播放
            audioSourceBG.Pause();
            imgSound.sprite = soundSprites[1];
        }
        else
        {
            //停止播放
            audioSourceBG.Play();
            imgSound.sprite = soundSprites[0];
        }
    }


	// Update is called once per frame
	void Update ()
    {
		
	}
}

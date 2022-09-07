using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    //todo:
    //1.判断用户角色是否进入到结束区域，如果进入，出发游戏结束的相关操作
    //2.书写游戏结束的逻辑：
    // 2.1 调用结束UI ，展示胜利画面（通过更改UI透明度来实现）
    // 2.2 设置计时器，到指定时间，退出游戏
    //步骤一，应该写入触发器事件
    //步骤二，应该将游戏结束得操作，写入一个自定义的方法

    //在update中，使用一个开关来判断是否掉用2代表的方法
    //而这个变量开关，就是玩家是否已经进入结束区域， 在步骤一的触发器事件中赋值

    //声明一个开关，存储用户是否在触发器中
    bool m_IsPlayerAtExit;
    //声明一个公开对象，用来获取用户角色
    public GameObject player;

    //更改透明度的时间
    public float fadeDuration = 1.0f;
    //计时器
    float m_Timer;
    //正常显示结束UI的时间
    public float displayImageDuration = 1.0f;

    //声明一个CanvasGroup ， 用来获取UI中的实例，来更改UI中图像的透明度
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    bool m_IsPlayerCaught;

    //胜利的音乐
    public AudioSource exitAudio;
    //被抓住的音乐
    public AudioSource caughtAudio;
    //因为这两种声音只能播放一次，所以要设定一个布尔值来作为开关
    //没播放过是false ，播放过是true
    bool m_HasAudioPlayed;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);

        }
    }

    public void Caught()
    {
        m_IsPlayerCaught = true;
    }

    //结束当前关卡
    void EndLevel(CanvasGroup canvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        //计时器随着Update逐渐增大
        m_Timer += Time.deltaTime;
        canvasGroup.alpha = m_Timer / fadeDuration;
        //当计时器的时长大于我们设定的透明度变化时长通正常显示UI界面时长之和
        //
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                //退出当前应用程序，打包发布时才能生效
                Application.Quit();
                // UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }


}

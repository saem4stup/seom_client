using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoController : MonoBehaviour
{
    public GameObject myVideo;
    public VideoPlayer videoClip;

    public void OnPlayVideo()
    {
        myVideo.SetActive(true);
        videoClip.Play();
    }

    public void OnPauseVideo()
    {
        myVideo.SetActive(false);
        videoClip.Pause();
    }

    public void OnResetVideo()
    {
        videoClip.time = 0f;
        videoClip.playbackSpeed = 1f;
    }

    public void OnFastVideo(float speed)
    {
        videoClip.playbackSpeed = speed;
    }
}

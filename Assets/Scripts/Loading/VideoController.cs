using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // VideoPlayer 컴포넌트에서 비디오 재생이 끝났을 때 호출되는 이벤트 리스너를 추가합니다.
        videoPlayer.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        print  ("Video Is Over");
        ChangeScene.target();
    }   
}

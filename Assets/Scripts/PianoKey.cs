using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [Tooltip("이 건반의 계이름을 정확히 입력 (예: Do, Re, Mi)")]
    public string noteName; // Inspector에서 설정 (예: "Do", "Re")
    public AudioClip keySound; // 건반 소리 파일
    private AudioSource audioSource; // 소리 재생기

    private GameManager gameManager;

    void Start()
    {
        // 게임 세상에 있는 소리 재생기를 찾아서 가져옴
        // 스피커와 지휘자를 찾아옴
        audioSource = FindObjectOfType<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // 버튼을 누르면 이 함수가 실행됨
    public void PlaySound()
    {
        if (audioSource != null && keySound != null)
        {
            audioSource.PlayOneShot(keySound);
        }
        
        // 2. 지휘자에게 보고함
        if (gameManager != null)
        {
            gameManager.OnKeyPressed(noteName);
        }
    }
}

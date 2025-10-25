using UnityEngine;
using UnityEngine.UI; // Button 사용을 위해 추가 (선택 사항)

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;

    [Header("Info Popup")]
    public GameObject infoPopup;
    public GameObject[] pages;
    public GameObject nextButton; // Page 1, 2, 3에서 사용할 'Next' 버튼
    public GameObject prevButton; // Page 2, 3, 4에서 사용할 'Back' 버튼
    public GameObject startGameButton; // Page 4에서 사용할 'Let's Play!' 버튼
    public GameObject seeSongsButton; // Page 1의 'See Songs!' 버튼

    [Header("Success Popup")]
    public GameObject successPopup;

    private int currentPageIndex = 0;

    void Start()
    {
        if (successPopup != null) successPopup.SetActive(false);

        if (infoPopup != null)
        {
            infoPopup.SetActive(true);
            ShowPage(0); // 앱 시작 시 첫 페이지 활성화
        }
    }

    public void ToggleInfoPopup()
    {
        bool isActive = !infoPopup.activeSelf;
        infoPopup.SetActive(isActive);
        if (isActive)
        {
            ShowPage(0);
        }
    }

// 특정 페이지를 보여주는 함수 (버튼 로직 수정됨)
    void ShowPage(int index)
    {
        currentPageIndex = index;
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null) 
                pages[i].SetActive(i == index);
        }

        // 'See Songs!' 버튼: 첫 페이지(index 0)일 때만 보여준다.
        if (seeSongsButton != null) 
            seeSongsButton.SetActive(index == 0);

        // '이전' 버튼: 첫 페이지(index 0)가 아니면 보여준다.
        if (prevButton != null) 
            prevButton.SetActive(index > 0);

        // '다음' 버튼: 페이지 1 또는 2일 때만 보여준다 (index 1 or 2).
        if (nextButton != null) 
            nextButton.SetActive(index == 1 || index == 2);

        // '시작하기' 버튼: 마지막 페이지(index == pages.Length - 1)일 때만 보여준다.
        if (startGameButton != null) 
            startGameButton.SetActive(index == pages.Length - 1);
    }

    public void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            ShowPage(currentPageIndex + 1);
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            ShowPage(currentPageIndex - 1);
        }
    }

    // ▼▼▼ '시작하기' 버튼이 호출할 함수 ▼▼▼
    public void StartGame()
    {
        if (infoPopup != null)
        {
            infoPopup.SetActive(false); // InfoPopup 끄기
        }
        // 필요하다면 여기에 GameManager 초기화 함수 등을 호출
        // 예: if (gameManager != null) gameManager.InitializeForNewGame();
    }

    // --- Success Popup Functions ---
    public void OnClickRetry()
    {
        if (successPopup != null) successPopup.SetActive(false);
        if (gameManager != null) gameManager.ResetSong();
    }

    public void OnClickCloseSuccessPopup()
    {
        if (successPopup != null) successPopup.SetActive(false);
    }

    // --- General Functions ---
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }
}
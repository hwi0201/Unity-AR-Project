using UnityEngine;

public class UIManager : MonoBehaviour
{
    // --- 기존 팝업 켜고 끄는 부분 ---

    [Header("Managers")]
    public GameManager gameManager;

    [Header("Info Popup")]
    public GameObject infoPopup;
    public GameObject[] pages;
    public GameObject nextButton;
    public GameObject prevButton;

    [Header("Success Popup")]
    public GameObject successPopup;
    private int currentPageIndex = 0;

void Start()
{
    // 다른 팝업들은 시작할 때 모두 끈다.
    if (successPopup != null) successPopup.SetActive(false);
    
    // Info 팝업을 켜고, 항상 첫 페이지를 보여주도록 설정한다.
    if (infoPopup != null)
    {
        infoPopup.SetActive(true);
        ShowPage(0);
    }
}

    public void ToggleInfoPopup()
    {
        bool isActive = !infoPopup.activeSelf;
        infoPopup.SetActive(isActive);

        // 팝업이 켜질 때 항상 첫 페이지를 보여주도록 함
        if (isActive)
        {
            ShowPage(0);
        }
    }

    // 특정 페이지를 보여주는 함수
    void ShowPage(int index)
    {
        currentPageIndex = index;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        if (prevButton != null) prevButton.SetActive(index > 0);
        if (nextButton != null) nextButton.SetActive(index < pages.Length - 1);
    }

    // '다음' 버튼 누를 때 호출
    public void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            ShowPage(currentPageIndex + 1);
        }
    }

    // '이전' 버튼 누를 때 호출
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            ShowPage(currentPageIndex - 1);
        }
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
        // 앱을 종료합니다.
        Application.Quit();
        Debug.Log("Exit!");
    }

}

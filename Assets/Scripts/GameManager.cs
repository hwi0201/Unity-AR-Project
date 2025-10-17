using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public string[] notes;
}

public class GameManager : MonoBehaviour
{
    [Header("Song Data")]
    public Song[] allSongs;

    [Header("UI Feedback")]
    public Image successFeedback;
    public Image errorFeedback;
    public GameObject successPopup;

    [Header("Sheet Music UI")]
    public GameObject[] sheetMusicUIs; // HUD Canvas에 있는 SheetMusic_star, SheetMusic_airplane 등을 연결

    // --- 내부 변수 ---
    private Song currentSong;
    private int currentNoteIndex;
    private int currentLineIndex;
    private SheetMusicInfo currentSheetMusic; // 현재 활성화된 악보의 SheetMusicInfo 컴포넌트

    void Start()
    {
        if (successFeedback != null) successFeedback.gameObject.SetActive(false);
        if (errorFeedback != null) errorFeedback.gameObject.SetActive(false);
        if (successPopup != null) successPopup.gameObject.SetActive(false);
        // 게임 시작 시 모든 악보 UI를 끈다
        if (sheetMusicUIs != null)
        {
            foreach (var ui in sheetMusicUIs)
            {
                if (ui != null) ui.SetActive(false);
            }
        }
    }

    // ImageTracker가 호출할 함수 (인자 1개)
    public void StartNewSong(string songName)
    {
        // 이전에 켜져있던 악보가 있다면 끈다
        if (currentSheetMusic != null && currentSheetMusic.gameObject != null)
        {
            currentSheetMusic.gameObject.SetActive(false);
        }

        currentSong = null;
        foreach (Song song in allSongs)
        {
            if (song.name == songName) { currentSong = song; break; }
        }

        if (currentSong != null)
        {
            currentNoteIndex = 0;
            currentLineIndex = 0;

            // 이름이 같은 악보 UI를 찾아서 켜고, SheetMusicInfo 컴포넌트를 가져옴
            foreach (var ui in sheetMusicUIs)
            {
                // UI 오브젝트 이름이 "SheetMusic_star" 형식이어야 함
                if (ui.name == "SheetMusic_" + songName)
                {
                    ui.SetActive(true);
                    currentSheetMusic = ui.GetComponent<SheetMusicInfo>();
                    break;
                }
            }

            // 악보의 첫 페이지만 보여주도록 초기화
            if (currentSheetMusic != null)
            {
                for (int i = 0; i < currentSheetMusic.lines.Length; i++)
                {
                    if (currentSheetMusic.lines[i] != null)
                        currentSheetMusic.lines[i].SetActive(i == currentLineIndex);
                }
            }
            Debug.Log(currentSong.name + " 노래 시작!");
        }
    }

    public void OnKeyPressed(string noteName)
    {
        if (currentSong == null || currentSheetMusic == null) return;

        if (noteName == currentSong.notes[currentNoteIndex])
        {
            StartCoroutine(ShowFeedback(successFeedback));
            currentNoteIndex++;

            Debug.Log($"줄바꿈 체크: 현재 노트 인덱스 {currentNoteIndex} / 이 줄의 총 노트 수 {currentSheetMusic.notesPerLine[currentLineIndex]}");

            // 줄바꿈 체크 로직
            int notesInCurrentLine = currentSheetMusic.notesPerLine[currentLineIndex];
            if (notesInCurrentLine > 0 && currentNoteIndex == notesInCurrentLine)
            {
                currentLineIndex++;
                if (currentLineIndex < currentSheetMusic.lines.Length)
                {
                    currentSheetMusic.lines[currentLineIndex - 1].SetActive(false);
                    currentSheetMusic.lines[currentLineIndex].SetActive(true);
                }
            }

// 노래가 끝났다면, 바로 팝업을 띄우는 대신 딜레이 코루틴을 시작합니다.
            if (currentNoteIndex >= currentSong.notes.Length)
            {
                StartCoroutine(ShowSuccessPopupWithDelay(0.5f)); // 0.5초 딜레이
            }
        }
        else
        {
            StartCoroutine(ShowFeedback(errorFeedback));
        }
    }

    IEnumerator ShowSuccessPopupWithDelay(float delay)
    {
        // 지정된 시간(delay)만큼 기다립니다.
        yield return new WaitForSeconds(delay);

        // 기다린 후에 악보를 끄고 팝업을 켭니다.
        if (currentSheetMusic != null) currentSheetMusic.gameObject.SetActive(false);
        if (successPopup != null) successPopup.SetActive(true);
        currentSong = null;
    }

    public void ResetSong()
    {
        if (currentSong != null)
        {
            StartNewSong(currentSong.name);
        }
    }

    IEnumerator ShowFeedback(Image feedbackImage)
    {
        if (feedbackImage == null) yield break;
        feedbackImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        feedbackImage.gameObject.SetActive(false);
    }
}
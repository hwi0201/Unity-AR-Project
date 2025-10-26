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
    [Tooltip("피드백 표시 시간 (초)")]
    public float feedbackDuration = 0.15f;
    [Tooltip("성공 팝업 표시 전 딜레이 (초)")] // 팝업 딜레이 변수 추가
    public float successPopupDelay = 0.2f; // 기본 0.5초 딜레이

    [Header("Sheet Music UI")]
    public GameObject[] sheetMusicUIs;

    private Song currentSong;
    private int currentNoteIndex;
    private int currentLineIndex;
    private SheetMusicInfo currentSheetMusic;
    private Coroutine successFeedbackCoroutine;
    private Coroutine errorFeedbackCoroutine;

    void Start()
    {
        if (successFeedback != null) successFeedback.gameObject.SetActive(false);
        if (errorFeedback != null) errorFeedback.gameObject.SetActive(false);
        if (successPopup != null) successPopup.gameObject.SetActive(false);
        if (sheetMusicUIs != null)
        {
            foreach (var ui in sheetMusicUIs)
            {
                if (ui != null) ui.SetActive(false);
            }
        }
    }

    public void StartNewSong(string songName)
    {
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

            foreach (var ui in sheetMusicUIs)
            {
                if (ui.name == "SheetMusic_" + songName)
                {
                    ui.SetActive(true);
                    currentSheetMusic = ui.GetComponent<SheetMusicInfo>();
                    break;
                }
            }

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
            TriggerFeedback(successFeedback); // 개선된 피드백 호출
            currentNoteIndex++;

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

            // 노래가 끝났는지 확인
            if (currentNoteIndex >= currentSong.notes.Length)
            {
                // ▼▼▼ 딜레이 후 팝업 띄우는 코루틴 호출 ▼▼▼
                StartCoroutine(ShowSuccessPopupWithDelay(successPopupDelay));
            }
        }
        else
        {
            TriggerFeedback(errorFeedback); // 개선된 피드백 호출
        }
    }

    // ▼▼▼ 딜레이 후 팝업 띄우는 코루틴 ▼▼▼
    IEnumerator ShowSuccessPopupWithDelay(float delay)
    {
        // 마지막 피드백(✓)이 사라질 시간을 기다립니다.
        yield return new WaitForSeconds(delay);

        // 악보를 끄고 팝업을 켭니다.
        if (currentSheetMusic != null && currentSheetMusic.gameObject != null)
            currentSheetMusic.gameObject.SetActive(false);
        if (successPopup != null)
            successPopup.SetActive(true);

        currentSong = null; // 노래 상태 초기화
    }

    public void ResetSong()
    {
        // 팝업이 켜져있을 수 있으니 먼저 끈다.
        if (successPopup != null) successPopup.SetActive(false);

        if (currentSong != null) // Reset 시 currentSong이 null일 수 있으므로 null 체크 추가
        {
            // StartNewSong은 currentSong을 다시 찾으므로 여기서 null로 만들 필요 없음
            StartNewSong(currentSong.name);
        }
        else
        {
             Debug.LogWarning("리셋할 현재 노래 정보가 없습니다.");
             // 필요하다면 여기서 앱 초기 상태로 돌아가는 로직 추가 (예: InfoPopup 다시 켜기)
        }
    }

    // 피드백 코루틴 관리 함수
    void TriggerFeedback(Image feedbackImage)
    {
        if (feedbackImage == null) return;
        Coroutine runningCoroutine = (feedbackImage == successFeedback) ? successFeedbackCoroutine : errorFeedbackCoroutine;
        if (runningCoroutine != null) StopCoroutine(runningCoroutine);
        Coroutine newCoroutine = StartCoroutine(ShowFeedbackRoutine(feedbackImage));
        if (feedbackImage == successFeedback) successFeedbackCoroutine = newCoroutine;
        else errorFeedbackCoroutine = newCoroutine;
    }

    // 피드백 코루틴
    IEnumerator ShowFeedbackRoutine(Image feedbackImage)
    {
        feedbackImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(feedbackDuration);
        feedbackImage.gameObject.SetActive(false);
        if (feedbackImage == successFeedback) successFeedbackCoroutine = null;
        else errorFeedbackCoroutine = null;
    }
}
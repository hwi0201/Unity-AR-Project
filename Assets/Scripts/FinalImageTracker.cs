using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using TMPro; // 교수님 코드의 debugText를 위해 추가

public class FinalImageTracker : MonoBehaviour
{
    [Header("Managers & Main Components")]
    public GameManager gameManager;
    public ARTrackedImageManager trackedImageManager;

    [Header("Prefabs")]
    public GameObject[] PrefabsToSpawn;

    [Header("Debugging")]
    public TMP_Text debugText; // 교수님 코드의 디버그 텍스트 기능

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    // ▼▼▼ 교수님 코드 방식: 시작할 때 모든 프리팹을 미리 만들어 숨겨둠 ▼▼▼
    void Awake()
    {
        if (gameManager == null)
            Debug.LogError("치명적 에러: GameManager가 FinalImageTracker에 연결되지 않았습니다!");
        if (trackedImageManager == null)
            trackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach (GameObject prefab in PrefabsToSpawn)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.name = prefab.name; // 복제본의 이름을 원본 프리팹("star", "airplane" 등)과 똑같이 맞춰줌
            newObject.SetActive(false);
            spawnedObjects.Add(prefab.name, newObject);
        }
    }

    void OnEnable() { trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; }
    void OnDisable() { trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged; }

    // ▼▼▼ 교수님 코드의 안정적인 added, updated, removed 구조 사용 ▼▼▼
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // 새로 추가된 이미지 처리
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateSpawnedObject(trackedImage);
        }

        // 업데이트된 이미지 처리
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateSpawnedObject(trackedImage);
        }

        // 사라진 이미지 처리
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // 파괴하는 대신 비활성화해서 숨김
            if (spawnedObjects.TryGetValue(trackedImage.referenceImage.name, out GameObject spawnedObject))
            {
                spawnedObject.SetActive(false);
            }
        }
    }

    // ▼▼▼ 두 코드의 핵심 기능을 합친 헬퍼 함수 ▼▼▼
    void UpdateSpawnedObject(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject spawnedObject = spawnedObjects[imageName];

        // 이미지가 제대로 추적되고 있다면
        if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            // 이전에 꺼져있었다면 (즉, 처음 발견된 것이라면) 노래 시작
            if (!spawnedObject.activeSelf)
            {
                gameManager.StartNewSong(imageName);
            }

            // 우리 코드의 '띄우기' 기능 적용
            spawnedObject.transform.position = trackedImage.transform.position + trackedImage.transform.up * 0.02f;
            spawnedObject.transform.rotation = trackedImage.transform.rotation;
            spawnedObject.SetActive(true);
        }
        else // 추적을 놓쳤다면
        {
            spawnedObject.SetActive(false);
        }
    }

    // ▼▼▼ 교수님 코드의 디버그 텍스트 기능 ▼▼▼
    void Update()
    {
        if (debugText != null)
        {
            debugText.text = "Tracked Images: " + trackedImageManager.trackables.count.ToString();
        }
    }
}
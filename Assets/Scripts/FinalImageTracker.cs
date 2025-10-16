using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class FinalImageTracker : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;

    [Header("Prefabs")]
    // ▼▼▼ 수정 1: 3개의 프리팹을 하나로 통합 ▼▼▼
    public GameObject pianoPrefab; 

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    void Awake()
    {
        if (gameManager == null)
            Debug.LogError("치명적 에러: GameManager가 FinalImageTracker에 연결되지 않았습니다! Inspector 창을 확인해주세요!");

        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable() => trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    void OnDisable() => trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // “added” 단계에서는 name이 null일 수 있으므로 prefab 생성은 생략
        // 대신 “updated” 단계에서 확실히 처리
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.referenceImage == null) continue;

            string imageName = trackedImage.referenceImage.name;
            if (string.IsNullOrEmpty(imageName)) continue;

            // --- 새로 생성해야 할 경우 ---
            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking && !spawnedObjects.ContainsKey(imageName))
            {
                if (pianoPrefab != null)
                {
                    GameObject newObject = Instantiate(pianoPrefab, trackedImage.transform);
                    spawnedObjects.Add(imageName, newObject);

                    if (gameManager != null)
                        gameManager.StartNewSong(imageName);
                }
                else
                {
                    Debug.LogWarning($"⚠ {imageName}에 해당하는 prefab이 없습니다!");
                }
            }

            // --- 이미 존재할 경우 위치/활성 업데이트 ---
            else if (spawnedObjects.TryGetValue(imageName, out GameObject spawnedObject))
            {
                spawnedObject.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedObject.SetActive(trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);
            }
        }

        // removed는 동일하게 유지
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            if (trackedImage.referenceImage == null) continue;
            string imageName = trackedImage.referenceImage.name;
            if (string.IsNullOrEmpty(imageName)) continue;

            if (spawnedObjects.TryGetValue(imageName, out GameObject spawnedObject))
            {
                Destroy(spawnedObject);
                spawnedObjects.Remove(imageName);
            }
        }
    }
}

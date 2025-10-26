using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class MultipleImageTracker : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject[] PrefabsToSpawn;

    Dictionary<string, GameObject> spawnedObjects;
    public TMP_Text debugText;
    void Awake()
    {
        spawnedObjects = new Dictionary<string, GameObject>();

        foreach (GameObject obj in PrefabsToSpawn)
        {
            GameObject newObject = Instantiate(obj);
            newObject.name = obj.name;
            newObject.SetActive(false);

            spawnedObjects.Add(newObject.name, newObject);
        }
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("0");
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateSpawnObject(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateSpawnObject(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.referenceImage.name].SetActive(false);
        }
    }

    void UpdateSpawnObject(ARTrackedImage trackedImage)
    {
        Debug.Log(trackedImage.referenceImage.name);

        string referenceImageName = trackedImage.referenceImage.name;
        spawnedObjects[referenceImageName].transform.position = trackedImage.transform.position;
        spawnedObjects[referenceImageName].transform.rotation = trackedImage.transform.rotation;

        spawnedObjects[referenceImageName].SetActive(true);

    }
    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    [Obsolete]
    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    void Update()
    {
        //debugText.text = "trackedImageManger.trackables.count: " + trackedImageManager.trackables.count.ToString();
    }

}

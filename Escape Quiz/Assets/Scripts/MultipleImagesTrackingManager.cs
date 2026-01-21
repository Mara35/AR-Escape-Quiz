using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImagePrefabSpawner : MonoBehaviour
{
    // Prefabs to spawn (assign in Inspector)
    [SerializeField] private List<GameObject> prefabsToSpawn = new List<GameObject>();

    private ARTrackedImageManager _trackedImageManager;

    // Key: reference image name, Value: spawned prefab instance
    private readonly Dictionary<string, GameObject> _arObjects = new Dictionary<string, GameObject>();

    // ===== QUIZ RELATED (added, no existing logic removed) =====
    private ElectricityQuiz cachedQuiz;
    private bool socket2QuizTriggered = false;
    // ==========================================================

    private void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        if (_trackedImageManager != null)
            _trackedImageManager.trackablesChanged.AddListener(OnImagesTrackedChanged);
    }

    private void OnDisable()
    {
        if (_trackedImageManager != null)
            _trackedImageManager.trackablesChanged.RemoveListener(OnImagesTrackedChanged);
    }

    private void Start()
    {
        SetupSceneElements();

        cachedQuiz = FindObjectOfType<ElectricityQuiz>();

        if (cachedQuiz == null)
            Debug.LogError("ElectricityQuiz NOT found in scene!");
        else
            Debug.Log("ElectricityQuiz found and cached");
    }

    private void SetupSceneElements()
    {
        _arObjects.Clear();

        foreach (GameObject prefab in prefabsToSpawn)
        {
            if (prefab == null) continue;

            // Spawn hidden at origin
            GameObject arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            arObject.name = prefab.name;
            arObject.SetActive(false);

            // If duplicate names exist, prevent crash
            if (_arObjects.ContainsKey(arObject.name))
            {
                Destroy(arObject);
                continue;
            }

            _arObjects.Add(arObject.name, arObject);
        }
    }

    private void OnImagesTrackedChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        Debug.Log(
            $"[AR] Added: {eventArgs.added.Count}, " +
            $"Updated: {eventArgs.updated.Count}, " +
            $"Removed: {eventArgs.removed.Count}"
        );

        foreach (var trackedImage in eventArgs.added)
            UpdateTrackedImage(trackedImage);

        foreach (var trackedImage in eventArgs.updated)
            UpdateTrackedImage(trackedImage);

        foreach (var trackedImage in eventArgs.removed)
            HideTrackedImageObject(trackedImage.Value);
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if (trackedImage == null)
            return;

        Debug.Log(
            $"[AR] Image: {trackedImage.referenceImage.name}, " +
            $"State: {trackedImage.trackingState}"
        );

        string imageName = trackedImage.referenceImage.name;

        if (!_arObjects.TryGetValue(imageName, out GameObject obj) || obj == null)
        {
            Debug.LogWarning($"No prefab found for tracked image '{imageName}'.");
            return;
        }

        // Not reliably tracked => hide object
        if (trackedImage.trackingState == TrackingState.None ||
            trackedImage.trackingState == TrackingState.Limited)
        {
            obj.SetActive(false);
            return;
        }

        // Tracked => show & align
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(
            trackedImage.transform.position,
            trackedImage.transform.rotation
        );

        // ===== SOCKET2 QUIZ TRIGGER =====
        if (trackedImage.referenceImage.name == "Socket2"
            && (trackedImage.trackingState == TrackingState.Tracking
                || trackedImage.trackingState == TrackingState.Limited)
            && !socket2QuizTriggered
            && cachedQuiz != null)
        {
            Debug.Log("Triggering Socket2 Quiz (Limited tracking)");
            socket2QuizTriggered = true;
            cachedQuiz.ShowQuiz();
        }
        // ===============================
    }

    private void HideTrackedImageObject(ARTrackedImage trackedImage)
    {
        if (trackedImage == null)
            return;

        string imageName = trackedImage.referenceImage.name;

        if (_arObjects.TryGetValue(imageName, out GameObject obj) && obj != null)
            obj.SetActive(false);
    }
}

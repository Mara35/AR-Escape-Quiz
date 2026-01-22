using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedImageListener : MonoBehaviour
{
    private ARTrackedImageManager imageManager;

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // React only when a NEW image is detected
        foreach (var trackedImage in args.added)
        {
            HandleTrackedImage(trackedImage);
        }
    }

    private void HandleTrackedImage(ARTrackedImage trackedImage)
    {
        ScanTarget scanTarget = trackedImage.GetComponent<ScanTarget>();
        if (scanTarget == null)
            return;

        string imageName = trackedImage.referenceImage.name;

        // YOUR image order
        if (imageName == "HouseRules")
            scanTarget.quizID = 0;
        else if (imageName == "Socket2")
            scanTarget.quizID = 1;
        else if (imageName == "Valve_Pipes")
            scanTarget.quizID = 2;
        else if (imageName == "Start")
            scanTarget.quizID = 3;
        else
            return;

        scanTarget.OnImageScanned();
    }
}

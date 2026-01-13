using UnityEngine;

public class PuzzleProgress : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public int numberOfCorrectObjects = 2;

    private int collectedObjects = 0;
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader not found in the scene!");
        }
    }

    public void RegisterCorrectObject()
    {
        collectedObjects++;
        Debug.Log($"Correct objects collected: {collectedObjects}/{numberOfCorrectObjects}");

        if (collectedObjects >= numberOfCorrectObjects)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Call ANY existing SceneLoader method you want
        sceneLoader.LoadFourthHint();
        // Example alternatives:
        // sceneLoader.LoadFirstHint();
        // sceneLoader.LoadInstructions();
    }
}

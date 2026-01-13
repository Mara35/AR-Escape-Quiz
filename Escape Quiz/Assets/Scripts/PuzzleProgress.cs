using UnityEngine;

public class PuzzleProgress : MonoBehaviour
{
    public int numberOfCorrectObjects = 2;

    private int collectedObjects = 0;
    private SceneLoader sceneLoader;

    private void Start()
    {
        Debug.Log("[PuzzleProgress] START");

        sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader == null)
        {
            Debug.LogError("[PuzzleProgress] SceneLoader NOT FOUND");
        }
        else
        {
            Debug.Log("[PuzzleProgress] SceneLoader FOUND");
        }
    }

    public void RegisterCorrectObject()
    {
        collectedObjects++;
        Debug.Log($"[PuzzleProgress] REGISTERED → {collectedObjects}/{numberOfCorrectObjects}");

        if (collectedObjects >= numberOfCorrectObjects)
        {
            Debug.Log("[PuzzleProgress] PUZZLE COMPLETE → loading next scene");
            LoadNextScene();
        }
        else
        {
            Debug.Log("[PuzzleProgress] Puzzle NOT complete yet");
        }
    }

    private void LoadNextScene()
    {
        if (sceneLoader == null)
        {
            Debug.LogError("[PuzzleProgress] Cannot load scene – SceneLoader is NULL");
            return;
        }

        Debug.Log("[PuzzleProgress] Calling SceneLoader.LoadFourthHint()");
        sceneLoader.LoadFourthHint();
    }
}

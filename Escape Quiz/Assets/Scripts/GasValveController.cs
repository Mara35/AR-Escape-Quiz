using UnityEngine;

public class GasValveController : MonoBehaviour
{
    public ParticleSystem gasEffect;

    public float requiredRotation = 1080f;
    public float rotationSpeed = 0.4f;

    private float accumulatedRotation = 0f;
    private float lastAngle;
    private bool gasOff = false;

    private SceneLoader sceneLoader;

    void Start()
    {
        lastAngle = transform.eulerAngles.y;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        if (gasOff) return;

        if (Input.GetMouseButton(0))
        {
            float delta = Input.GetAxis("Mouse X") * rotationSpeed * 100f;
            transform.Rotate(Vector3.up, delta);

            float currentAngle = transform.eulerAngles.y;
            float angleDelta = Mathf.DeltaAngle(lastAngle, currentAngle);

            accumulatedRotation += Mathf.Abs(angleDelta);
            lastAngle = currentAngle;

            if (accumulatedRotation >= requiredRotation)
            {
                ShutOffGas();
            }
        }
    }

    void ShutOffGas()
    {
        gasOff = true;
        gasEffect.Stop();

        if (sceneLoader != null)
            sceneLoader.LoadFourthHint();
        else
            Debug.LogError("SceneLoader nicht gefunden!");
    }
}

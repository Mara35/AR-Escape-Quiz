using UnityEngine;

public class GasValveController : MonoBehaviour
{
    public ParticleSystem gasEffect;

    public float requiredRotation = 1080f;
    public float rotationSpeed = 0.4f;

    private float accumulatedRotation = 0f;
    private float lastAngle;
    private bool gasOff = false;

    private bool isTouchingValve = false;
    private SceneLoader sceneLoader;

    void Start()
    {
        lastAngle = transform.eulerAngles.y;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void Update()
    {
        if (gasOff) return;
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            isTouchingValve = IsTouchOnValve(touch.position);
        }

        if (touch.phase == TouchPhase.Moved && isTouchingValve)
        {
            float delta = touch.deltaPosition.x * rotationSpeed;
            RotateValve(delta);
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            isTouchingValve = false;
        }
    }

    bool IsTouchOnValve(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }

        return false;
    }

    void RotateValve(float delta)
    {
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

    void ShutOffGas()
    {
        gasOff = true;
        gasEffect.Stop();

        if (sceneLoader != null)
            sceneLoader.LoadFourthHint();
    }
}

using UnityEngine;

public class PlaneDrag : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private Plane dragPlane;
    private float fixedY;

    void Start()
    {
        cam = Camera.main;
        fixedY = transform.position.y;
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        Ray ray = cam.ScreenPointToRay(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                }
            }
        }

        if (touch.phase == TouchPhase.Moved && isDragging)
        {
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                transform.position = hitPoint;
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            isDragging = false;
        }
    }
}

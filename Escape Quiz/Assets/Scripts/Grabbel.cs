using UnityEngine;

public class MobileDrag : MonoBehaviour
{
    private Camera cam;
    private GameObject draggedObject;
    private float dragY;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        Ray ray = cam.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (touch.phase == TouchPhase.Began)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Drag"))
                {
                    draggedObject = hit.collider.gameObject;
                    dragY = draggedObject.transform.position.y;
                }
            }
        }

        if (touch.phase == TouchPhase.Moved && draggedObject != null)
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0, dragY, 0));
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 point = ray.GetPoint(distance);
                draggedObject.transform.position = point;
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            draggedObject = null;
        }
    }
}

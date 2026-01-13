using UnityEngine;

public class Grabbel : MonoBehaviour
{
    private GameObject selectedObject;

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit = CastRay(touch.position);

            if (hit.collider != null && hit.collider.CompareTag("Drag"))
            {
                selectedObject = hit.collider.gameObject;
            }
        }

        if (touch.phase == TouchPhase.Moved && selectedObject != null)
        {
            Vector3 position = new Vector3(
                touch.position.x,
                touch.position.y,
                Camera.main.WorldToScreenPoint(selectedObject.transform.position).z
            );

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
        }

        if (touch.phase == TouchPhase.Ended)
        {
            selectedObject = null;
        }
    }

    private RaycastHit CastRay(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }
}

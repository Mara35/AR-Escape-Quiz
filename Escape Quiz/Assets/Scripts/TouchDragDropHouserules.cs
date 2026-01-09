// DragManager.cs
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    [Header("Drag plane")]
    public Transform dragPlane; // z.B. ein leeres GameObject auf Posterhöhe (Child von QuizBoardRig)
    public float followSpeed = 20f;

    Camera cam;
    DraggableARItem current;
    Vector3 grabOffset;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        // Optional: UI berühren -> ignorieren
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(t.fingerId))
            return;

        if (t.phase == TouchPhase.Began)
        {
            Ray ray = cam.ScreenPointToRay(t.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var item = hit.collider.GetComponentInParent<DraggableARItem>();
                if (item != null)
                {
                    current = item;
                    current.BeginDrag();

                    // Offset, damit das Objekt nicht "springt"
                    grabOffset = current.transform.position - hit.point;
                }
            }
        }
        else if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
        {
            if (current == null) return;

            // Projektionspunkt auf Drag-Ebene
            Plane p = new Plane(dragPlane.forward, dragPlane.position);
            Ray ray = cam.ScreenPointToRay(t.position);
            if (p.Raycast(ray, out float enter))
            {
                Vector3 target = ray.GetPoint(enter) + grabOffset;
                current.FollowTo(target, followSpeed);
            }
        }
        else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
        {
            if (current == null) return;
            current.EndDrag();
            current = null;
        }
    }
}
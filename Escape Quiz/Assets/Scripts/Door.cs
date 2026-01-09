using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        public bool open;
        public float smooth = 1.0f;

        float DoorOpenAngle = -90.0f;
        float DoorCloseAngle = 0.0f;

        public AudioSource asource;
        public AudioClip openDoor, closeDoor;

        private bool victoryTriggered = false; // 🔒 nur einmal

        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // Touch-Eingabe (UNVERÄNDERT)
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        OpenDoor();
                    }
                }
            }

            // Türbewegung (UNVERÄNDERT)
            if (open)
            {
                Quaternion target = Quaternion.Euler(0, DoorOpenAngle, 0);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);

                // 🏁 NEU: Prüfen, ob Tür wirklich offen ist
                CheckForVictory();
            }
            else
            {
                Quaternion target = Quaternion.Euler(0, DoorCloseAngle, 0);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
            }
        }

        public void OpenDoor()
        {
            open = !open;
            asource.clip = open ? openDoor : closeDoor;
            asource.Play();
        }

        // 🔎 Victory erst bei kompletter Öffnung
        private void CheckForVictory()
        {
            if (victoryTriggered)
                return;

            float currentY = transform.localEulerAngles.y;

            // Unity-Winkel normalisieren (−180° bis +180°)
            if (currentY > 180f)
                currentY -= 360f;

            // Toleranz von 1 Grad
            if (Mathf.Abs(currentY - DoorOpenAngle) < 1.0f)
            {
                victoryTriggered = true;
                TriggerVictory();
            }
        }

        private void TriggerVictory()
        {
            Debug.Log("🚪 Tür vollständig geöffnet – Victory!");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.StopTimer();
            }

            SceneManager.LoadScene("VictoryScene");
        }
    }
}

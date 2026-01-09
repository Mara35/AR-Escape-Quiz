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

        private bool victoryTriggered = false;
        private bool openedByPlayer = false; // 🔑 NEU

        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // Touch-Eingabe (unverändert)
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

            // Türbewegung (unverändert)
            if (open)
            {
                Quaternion target = Quaternion.Euler(0, DoorOpenAngle, 0);
                transform.localRotation =
                    Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);

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
            openedByPlayer = open; // 🔑 nur beim Öffnen setzen

            asource.clip = open ? openDoor : closeDoor;
            asource.Play();
        }

        private void CheckForVictory()
        {
            // 🔒 Nur wenn Spieler die Tür geöffnet hat
            if (!openedByPlayer || victoryTriggered)
                return;

            float currentY = transform.localEulerAngles.y;
            if (currentY > 180f)
                currentY -= 360f;

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

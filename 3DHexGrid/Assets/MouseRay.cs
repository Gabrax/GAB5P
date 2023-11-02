using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{

    private Camera mainCamera;
    private bool isTeleporting;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTeleporting)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object clicked on has a collider
                if (hit.collider != null)
                {
                    isTeleporting = true;
                    Vector3 targetPosition = hit.point;
                    targetPosition.y = mainCamera.transform.position.y; // Keep the same Y position as the camera

                    // Move the camera to the target position
                    StartCoroutine(TeleportCamera(targetPosition));
                }
            }
        }
    }

    private IEnumerator TeleportCamera(Vector3 targetPosition)
    {
        float duration = 1.0f; // Adjust the teleport duration as needed
        float elapsedTime = 0;

        Vector3 initialPosition = mainCamera.transform.position;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        isTeleporting = false;
    }
}

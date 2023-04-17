using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // The player character's GameObject
    public float mouseSensitivity = 100f; // The sensitivity of the mouse movement
    public float distance = 3f; // The distance between the camera and the player character
    public float minHeight = 1f; // The minimum height of the camera above the player character
    public float maxHeight = 5f; // The maximum height of the camera above the player character
    public float maxDistance = 30f;

    private float xRotation = 0f; // The current rotation around the X axis
    private float currentHeight = 2f; // The current height of the camera above the player character

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
    }

    void LateUpdate()
    {
        // Calculate the new distance of the camera from the player character
        distance -= Input.GetAxis("Mouse ScrollWheel") * mouseSensitivity * Time.deltaTime;
        distance = Mathf.Clamp(distance, 1f, maxDistance);

        // Only rotate the camera if the right mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            // Get the mouse movement
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            // Calculate the new rotation around the Y axis
            float targetRotationY = transform.eulerAngles.y + mouseX;

            // Calculate the new rotation around the X axis
            xRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Calculate the new position of the camera
            Vector3 offset = new Vector3(0, currentHeight, -distance);
            Quaternion rotation = Quaternion.Euler(xRotation, targetRotationY, 0);
            Vector3 newPosition = player.transform.position + rotation * offset;

            // Update the position and rotation of the camera
            transform.position = newPosition;
            transform.rotation = rotation;

            player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, targetRotationY, 0); ;
        }
        else // Follow the player's position if not rotating the camera
        {
            // Calculate the new position of the camera
            Vector3 offset = new Vector3(0, currentHeight, -distance);
            Quaternion rotation = Quaternion.Euler(xRotation, player.transform.eulerAngles.y, 0);
            Vector3 newPosition = player.transform.position + rotation * offset;

            // Update the position and rotation of the camera
            transform.position = newPosition;
            transform.rotation = rotation;
        }
    }
}

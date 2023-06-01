using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject[] sprites;
    public float parallaxRange; 
    public float parallaxSpeed; 
    public Camera cam;
    public float spritesPosOffsetZ;
    public float spritesPosOffsetY;
    public float cameraMovementSpeed; 
    public float cameraMovementRange; 
    public float speedOffset;

    private Vector3 startPos;

    void Start()
    {
        cam = Camera.main;
        startPos = cam.transform.position;
    }

    void Update()
    {
        cam.transform.position = new Vector3(Mathf.PingPong(Time.time * cameraMovementSpeed, cameraMovementRange) + startPos.x, cam.transform.position.y, cam.transform.position.z);
        /*
        for (int i = 0; i < sprites.Length; i++)
        {
             float distance = Vector3.Distance(sprites[i].transform.position, cam.transform.position);
            // calculate the speed of the sprite based on its distance from the camera
            float speed = parallaxSpeed / (distance + 1);
            // update the position of the sprite
            float x = Mathf.Repeat((cam.transform.position.x * speed) - startPos.x, parallaxRange);
            sprites[i].transform.position = startPos + new Vector3(sprites[i].transform.position.x + x, spritesPosOffsetY, spritesPosOffsetZ);
        }
        */
    }
}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CursorHandler : MonoBehaviour
{
    public TileManager tileManager;
    public Transform CameraPivot;

    public Camera mainCamera;
    public float zoomSpeed = 10f; 

    public float stepDistance = 5.0f;
    public float speed = 10;
    public float YOffset = 1f;
    public float rotationSpeed = 5f;

    public Grid tilemap;

    private Tile previousTile;
    private Tile currentTile;

    private void Update()
    {
        MoveObject(transform);
        RotateObject(CameraPivot);
        ZoomCamera();
    }
    private void MoveObject(Transform objectToMove)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();

        if (movement != Vector3.zero)
        {
            previousTile = currentTile;

            currentTile = GetCurrentTile();
    
            Vector3 targetPosition = currentTile.gameObject.transform.position + movement;

            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition + Vector3.up * YOffset, speed * Time.deltaTime);
        }
    }

    void RotateObject(Transform objectToRotate)
    {
        float rotationAmount = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            rotationAmount = -1f; // Rotate left when Q key is pressed
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotationAmount = 1f; // Rotate right when E key is pressed
        }

        // Apply rotation to the object
        objectToRotate.Rotate(Vector3.up, rotationAmount * rotationSpeed * Time.deltaTime, Space.World);
    }

    private Tile GetCurrentTile()
    {
        Tile tile = null;
        float closestDistance = Mathf.Infinity;
        foreach (Floor floor in tileManager.Floors)
        {
            if (floor.gameObject.activeSelf)
            {
                foreach (Tile t in floor.GetTiles())
                {
                    if (t.gameObject.activeSelf)
                    {
                        Tile tileToCheck = GetHighestTile(t);

                        Vector3 objectPosition = tileToCheck.transform.position;
                        float distanceX = Mathf.Abs(objectPosition.x - transform.position.x);
                        float distanceZ = Mathf.Abs(objectPosition.z - transform.position.z);
                        float distanceToTile = distanceX + distanceZ;
                
                        if (distanceToTile < closestDistance)
                        {
                            tile = tileToCheck;
                            closestDistance = distanceToTile;
                        }
                    }
                }
            }
        }
        return tile;
    }

    private Tile GetHighestTile(Tile tile)
    {
        Tile highest = HasATileAbove(tile.gameObject);
        Tile current = tile;
        while (highest != null)
        {
            current = highest;
            highest = HasATileAbove(current.gameObject);
        }

        return current;
    }


    private Tile HasATileAbove(GameObject obj1)
    {
        RaycastHit hit;
        Physics.Raycast(obj1.transform.position, Vector3.up, out hit, obj1.transform.localScale.y);
        if (hit.collider != null)
        {
            Tile tile = hit.collider.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                return tile;
            }
        }
        return null;
    }

    void ZoomCamera()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new focal length based on the scroll input
        float newFocalLength = mainCamera.fieldOfView - (scrollInput * zoomSpeed);

        // Clamp the new focal length within a desired range (e.g., 10 to 60)
        float clampedFocalLength = Mathf.Clamp(newFocalLength, 10f, 60f);

        // Update the camera's focal length
        mainCamera.fieldOfView = clampedFocalLength;
    }
}

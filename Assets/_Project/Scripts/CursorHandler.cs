using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorHandler : MonoBehaviour
{
    public float stepDistance = 5.0f;
    public float yOffset = 0.5f; // Offset from the center of the tile on the Y axis

    public Grid tilemap; // Reference to the Tilemap component

    private Tile currentTile;

    private void Update()
    {
        MoveObject(transform);

        RaycastForTiles();
      
    }
    void MoveObject(Transform objectToMove)
    {
        float moveSpeed = 5f; // Adjust the move speed as desired

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize(); // Normalize the movement vector to avoid diagonal movement being faster

        if (movement != Vector3.zero)
        {
            // Get the closest tile position to the object's current position
            Vector3Int targetTilePosition = tilemap.WorldToCell(objectToMove.position);
            // Calculate the target position by adding the movement to the current tile position
            Vector3 targetPosition = tilemap.GetCellCenterWorld(targetTilePosition) + movement;
                // Move the object towards the target position

            float YOffset = currentTile ? currentTile.transform.localScale.y + yOffset : 0f;

            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition + Vector3.up * YOffset, moveSpeed * Time.deltaTime);           
        }
    }

    void RaycastForTiles()
    {
        Vector3 origin = transform.position; // Starting position of the raycast (e.g., the object's position)
        float maxDistance = 10f; // Maximum distance to check for hits

        // Define the directions to cast the raycasts
        Vector3[] directions = new Vector3[]
        {
        Vector3.down
        };

        foreach (Vector3 direction in directions)
        {
            Ray ray = new Ray(origin, direction);
            RaycastHit hit;

            // Perform the raycast and check for hits
            if (Physics.Raycast(ray, out hit, stepDistance))
            {
                // Check if the object hit has a component called "Tile"
                Tile tileComponent = hit.collider.GetComponent<Tile>();
                if (tileComponent != null)
                {
                    currentTile = tileComponent;
                    tileComponent.selected = true;
                    // Object hit has a "Tile" component
                    Vector3 hitPosition = hit.point; // World position of the hit
                    Debug.Log("Tile found at position: " + hitPosition);
                    // Perform additional actions if needed
                }
            }
        }
    }
}

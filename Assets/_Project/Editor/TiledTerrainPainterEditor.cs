using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TiledTerrainPainterEditor : EditorWindow
{

    private GameObject selectedPrefab;
    private float brushSize;
    private float brushStrength;
    private bool painting;

    private void OnEnable()
    {
        // Register the Repaint event
        EditorApplication.update += Repaint;
    }

    private void OnDisable()
    {
        // Unregister the Repaint event
        EditorApplication.update -= Repaint;
    }

    [MenuItem("Window/Tile Painter Tool")]
    public static void ShowWindow()
    {
        // Open the custom tool window
        TiledTerrainPainterEditor window = EditorWindow.GetWindow<TiledTerrainPainterEditor>("Tile Painter");
        window.Show();
    }

    private void OnGUI()
    {
        // Use EditorGUILayout to create UI elements for selecting prefab, setting height, brush size, brush strength, etc.
        selectedPrefab = EditorGUILayout.ObjectField("Prefab", selectedPrefab, typeof(GameObject), true) as GameObject;
        brushSize = EditorGUILayout.FloatField("Brush Size", brushSize);
        brushStrength = EditorGUILayout.FloatField("Brush Strength", brushStrength);

        // Add more UI elements for other settings as needed

        // Implement buttons for painting, saving, loading, etc.
        if (GUILayout.Button("Paint"))
        {
            // Handle painting logic
            // Use Raycast to get terrain height at cursor position, instantiate prefab at that height, etc.
        }

        // Add more buttons for other actions as needed
    }

    // Implement your painting logic in OnSceneGUI()
    private void OnSceneGUI(SceneView sceneView)
    {
        // Check if the "Paint" button is pressed and the mouse is over the viewport
        if (painting && Event.current.type == EventType.MouseMove && EditorGUIUtility.hotControl == 0)
        {
            // Hide the cursor
            Cursor.visible = false;

            // Get the mouse position in world space
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Move the prefab instance to the mouse position
                if (selectedPrefab != null)
                {
                    GameObject prefabInstance = PrefabUtility.InstantiatePrefab(selectedPrefab) as GameObject;
                    prefabInstance.transform.position = hit.point;
                }
            }

            // Repaint the Scene view
            Repaint();
        }

        // Check if the mouse button is released
        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            // Show the cursor
            Cursor.visible = true;

            // Stop painting
            painting = false;
        }
    }

    // Add any other methods or events as needed
}

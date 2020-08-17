using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    // Grid dimensions
    public int Width = 3;
    public int Height = 3;

    // Grid spacing
    public float Padding = 0.5f;

    // Grid placing
    public GameObject GridParent;
    public GameObject TilePrefab;

    void Start()
    {
        // Check if everything is correctly assigned
        bool canStart = true;

        if (TilePrefab == null)
        {
            canStart = false;
            Debug.LogWarning("Warning! 'tilePrefab' is not assigned!", this);
        }
        if (GridParent == null)
        {
            canStart = false;
            Debug.LogWarning("Warning! 'gridParent' is not assigned!", this);
        }

        // If everything is correctly assigned, start generating the grid
        if (canStart)
        {
            GenerateGrid();
        }
        else
        {
            Debug.LogError("'GenerateGrid' not started due to missing references!", this);
        }
    }

    private void GenerateGrid()
    {
        for (int widthIndex = 0; widthIndex < Width; widthIndex++)
        {
            for (int heightIndex = 0; heightIndex < Height; heightIndex++)
            {
                // Create tile position
                Vector3 position = new Vector3(widthIndex, 0, heightIndex);

                // Add tile padding
                position.x += (Padding * widthIndex);
                position.z += (Padding * heightIndex);

                // Spawn the tile prefab
                GameObject _tile = Instantiate(TilePrefab, position, GridParent.transform.rotation, GridParent.transform);

                // Give it a name (with position)
                _tile.name = "Tile [" + widthIndex + "," + heightIndex + "]";

            }
        }
    }
}

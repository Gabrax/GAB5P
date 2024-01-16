using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int boardSize = 100;
    public Color waterColor = Color.blue;
    public Color forestColor = Color.green;
    public Color mountainColor = Color.red;
    public Color clearingColor = Color.yellow;

    // Linear mixed generator parameters
    private const long m = 1 << 20; // Divisor greater than or equal to 2^20
    private const long a = 48271;    // Multiplier
    private float c = ((3 - (Mathf.Sqrt(3)) / 6)) * m; // Step

    public float seed = 123; // Initial seed

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        Color[,] terrainColors = new Color[boardSize, boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                seed = (a * seed + c) % m; // Linear mixed generator formula

                float randomValue = (float)seed / m; // Normalize to [0, 1]

                // Map the normalized value to specific ranges
                if (randomValue < 0.25f)
                    terrainColors[x, y] = waterColor;
                else if (randomValue < 0.5f)
                    terrainColors[x, y] = forestColor;
                else if (randomValue < 0.75f)
                    terrainColors[x, y] = mountainColor;
                else
                    terrainColors[x, y] = clearingColor;

                // Create a visual representation of the terrain
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                tile.transform.position = new Vector3(x, y, 0);
                tile.GetComponent<Renderer>().material.color = terrainColors[x, y];
            }
        }
    }
}

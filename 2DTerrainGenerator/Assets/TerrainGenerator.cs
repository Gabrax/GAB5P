using TMPro;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int boardSize = 100;
    public Color waterColor = Color.blue;
    public Color forestColor = Color.green;
    public Color mountainColor = Color.red;
    public Color clearingColor = Color.yellow;

    
    const long m = 4194304; 
    const long a = 20481; 
    float c = Mathf.Round(((3 - (Mathf.Sqrt(3))) / 6) * m); //886361
    float seed = 42;

    public TMP_Text displayText;
    public TMP_InputField inputField;

    void Start()
    {
        GenerateTerrain();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            UpdateVariable();
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        Color[,] terrainColors = new Color[boardSize, boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                seed = (a * seed + c) % m;

                float randomValue = (float)seed / m;

                if (randomValue < 0.25f)
                    terrainColors[x, y] = waterColor;
                else if (randomValue < 0.5f)
                    terrainColors[x, y] = forestColor;
                else if (randomValue < 0.75f)
                    terrainColors[x, y] = mountainColor;
                else
                    terrainColors[x, y] = clearingColor;

                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                tile.transform.position = new Vector3(x, y, 0);
                tile.GetComponent<Renderer>().material.color = terrainColors[x, y];
            }
        }
    }

    public void UpdateVariable()
    {
        if (int.TryParse(inputField.text, out int newValue))
        {
            seed = (uint)newValue;
            UpdateDisplay();
        }
        else
        {
            Debug.LogError("Invalid input. Please enter a valid integer.");
        }
    }

    void UpdateDisplay()
    {
        displayText.text = "myVariable: " + seed.ToString();
    }
}

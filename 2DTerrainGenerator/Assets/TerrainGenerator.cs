using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerrainGenerator : MonoBehaviour
{
    public int boardSize = 100;
    public Color waterColor = Color.blue;
    public Color forestColor = Color.green;
    public Color mountainColor = Color.red;
    public Color clearingColor = Color.yellow;

    // Linear mixed generator parameters
    private const long m = 1 << 22; 
    private const long a = 48271;    
    private float c = Mathf.Round(((3 - (Mathf.Sqrt(3))) / 6) * m);

    public TMP_Text displayText;
    public TMP_InputField inputField;
    public float seed = 123; 

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
                else if (randomValue > 0.25f && randomValue < 0.5f)
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
            seed = newValue;
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

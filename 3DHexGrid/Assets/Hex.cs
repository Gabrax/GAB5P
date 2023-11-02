using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] // allows picking parent game object without child
public class Hex : MonoBehaviour
{
    [SerializeField]
    private GlowHighlight highlight;
    private HexCoord hexCoordinates;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoord>();      
        highlight = GetComponent<GlowHighlight>();
    }
    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }

}

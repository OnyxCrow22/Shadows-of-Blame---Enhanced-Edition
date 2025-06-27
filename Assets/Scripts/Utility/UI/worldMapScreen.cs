using UnityEngine;

public class worldMapScreen : MonoBehaviour
{
    public GameObject worldMap, mainUI;
    public float zoomLevel = 0;
    public GameObject playerMarker;

    // It needs to open the map, and hide the MainUi;
    void OpenMap()
    {

    }

    // Adjust the zoom level of the map to a maximum of three, and allow the user to scroll their mouse wheel in and out to adjust the zoom. Additionally, controllers needs either the
    // right stick, or a dedicated UI button to adjust the zoom level.
    void adjustZoom()
    {

    }

    // When the user wants to place a marker, use either Raycasting, or get the mouse position on the map, and output the marker to the map. Then, draw a line from the player to the marker,
    // following roads, and finding the shortest legal route to the destination.
    void placeMarker()
    {

    }
}

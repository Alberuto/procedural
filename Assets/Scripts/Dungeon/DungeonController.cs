using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonController : MonoBehaviour {
    public TMP_InputField sizeXField;
    public TMP_InputField sizeYField;
    public TMP_InputField initPositionField;
    public TMP_InputField nRoomsField;

    public MapGenerator mapGenerator;
    public GameObject panelPrincipal; // o CanvasGroup para transparencia

    public void GenerateDungeon()
    {
        if (int.TryParse(sizeXField.text, out int sizeX) && int.TryParse(sizeYField.text, out int sizeY) &&
            int.TryParse(initPositionField.text, out int initPosition) && int.TryParse(nRoomsField.text, out int nRooms)) {

            if (panelPrincipal != null) {
                panelPrincipal.SetActive(false); // Si es un GameObject
            }
            mapGenerator.size = new Vector2Int(sizeX, sizeY);
            mapGenerator.roomSize = new Vector2(10, 10);
            mapGenerator.initPosition = initPosition;
            mapGenerator.nRooms = nRooms;
            mapGenerator.MazeGenerator();
        }
        else {
            Debug.LogError("Por favor, introduce valores válidos.");
        }
    }
}
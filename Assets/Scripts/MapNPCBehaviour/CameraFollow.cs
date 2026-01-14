using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, 0);  // Ajusta altura/distancia

    void LateUpdate(){  //LateUpdate para cámaras
        if (player != null) {
            transform.position = player.position + offset;
            transform.LookAt(player);  //Mira al jugador
        }
    }
}
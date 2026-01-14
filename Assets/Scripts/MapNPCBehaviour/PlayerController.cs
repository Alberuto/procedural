using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    void Update() {

        if (Input.GetButtonDown("Fire1")) { 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray,out hit, 1000)) {

                GetComponent<NavMeshAgent>().SetDestination(hit.point);
            }
        }
    }
    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Key")) {

            GameManager.Instance.KeyCollected();
            Destroy(other.gameObject);
        }
    }
}
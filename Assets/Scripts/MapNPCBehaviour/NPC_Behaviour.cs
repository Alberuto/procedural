using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour {

    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 min, max;
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    private GameManager gameManager;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.Instance;
        player = gameManager.player;  //Usa referencia del GM
        StartCoroutine("Follow");
    }

    // Update is called once per frame
    void Update() {
        
    }
    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
            GameManager.Instance.score -= 5000;
            GameManager.Instance.UpdateUI();
            GameManager.Instance.GameOver();
            GameManager.Instance.NPCSpeed *= 0.7f;
        }
    }

    #region Always Detect
    IEnumerator Follow() {
        while (true) {
            if (gameManager.IsGameActive) {

                agent.SetDestination(player.transform.position);
                agent.speed = gameManager.NPCSpeed;
            }
            yield return new WaitForSeconds(gameManager.updateInterval);
        }
    }
    #endregion
}
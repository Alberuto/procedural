using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    [Header("Estado Juego")]
    public bool IsGameActive = true;
    public int nivel = 0;
    public int score = 0;
    public float gameTime = 0f;

    [Header("Dificultad por Nivel")]
    public float maxNPCSpeed = 5f;         // velocidad maxima
    public float NPCSpeed = 0.1f;
    public float minUpdateInterval = 0.5f; //Reacción maxima
    public float updateInterval = 3f;      // intervalo 3s reaccionar

    [Header("UI")]
    public TextMeshProUGUI scoreText, timeText, keyText;
    public GameObject gameWinPanel,gameOverPanel, victoryFinalPanel;
    public GameObject player;
    public GameObject UI;
    public GameObject camara;

    [Header("Debug")]
    public int keysThisLevel = 1;
    private float speedupTimer = 0f;
    private int keysCollected = 0;

    void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(UI);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(camara);

            SceneManager.sceneLoaded += OnSceneLoaded;
            if (SceneManager.GetActiveScene().buildIndex == 0) {
                RestartGame();
            }
            StartCoroutine(ScoreLoop());
        }
        else {
            Destroy(gameObject);
        }
    }
    IEnumerator ScoreLoop() {

        while (IsGameActive) {
            yield return new WaitForSeconds(1f);  // Score fijo cada segundo
            score += 100;
            UpdateUI();
        }
    }
    void Update() {

        if (IsGameActive) {

            gameTime += Time.deltaTime;
            speedupTimer += Time.deltaTime;
            //Cada s incrementa velocidad/reacción
            if (speedupTimer >= 1f) {

                NPCSpeed = Mathf.Min(maxNPCSpeed, NPCSpeed + 0.1f);
                updateInterval = Mathf.Max(minUpdateInterval, updateInterval - 0.1f);
                speedupTimer = 0f;
            }
            UpdateUI();
        }
    }
    public void UpdateUI() {

        if (scoreText) scoreText.text = "Puntuacion: " + score;
        if (timeText) timeText.text = "Tiempo: " + Mathf.Floor(gameTime) + "s";
        if (keyText) keyText.text = "Llaves: " + keysCollected + " de " + keysThisLevel;
    }
    public void KeyCollected() {
        keysCollected++;
        UpdateUI();
        if (keysCollected >= keysThisLevel) {
            GameWin();
        }
    }
    public void GameOver() {

        IsGameActive = false;
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }
    public void GameWin() {

        IsGameActive = false;
        // ¡VICTORIA FINAL EN NIVEL 5!
        if (nivel >= 4) {
            // Juego completado - mostrar panel victoria final
            AudioManager.Instance.PlaySFX(AudioManager.Instance.victorySound);
            victoryFinalPanel.SetActive(true);
            Debug.Log("¡JUEGO COMPLETADO! Nivel máximo alcanzado.");
        }
        else {
            // Nivel normal - preparar siguiente
            AudioManager.Instance.PlaySFX(AudioManager.Instance.levelUpSound);
            Time.timeScale = 0f;
            if (gameWinPanel) gameWinPanel.SetActive(true);
        }
    }
    public void RestartGame() {

        Time.timeScale = 1f;
        IsGameActive = true;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel() {

        IsGameActive = true;
        Time.timeScale = 1f;
        nivel++;                          // Solo sube el número de nivel
        SceneManager.LoadScene(nivel.ToString());
    }
    public void MainMenu() {

        Time.timeScale = 1f;
        nivel = 0;
        score = 0;
        SceneManager.LoadScene("Menu"); //Cambia por tu escena menú que aun no existe. // consultar gfithub desktopo no funciona bien 
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        ResetLevelData(); //RESET keys
        gameWinPanel.SetActive(false);
        IsGameActive = true;
        ScoreLoop(); // SE QUEDA PILLADO EN EL CAMBIO DE ESCENA.
    }
    void ResetLevelData() {

        keysCollected = 0;
        keysThisLevel = nivel + 1;  // Nivel 0=1key, 1=2keys, etc.
        UpdateUI();
    }
}
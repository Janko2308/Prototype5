using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private float deathSpawnRate = 0.01f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public RawImage FirstHeart;
    public RawImage SecondHeart;
    public RawImage ThirdHeart;
    public int health = 3;
    public GameObject pauseScreen;
    public bool gameStarted = false;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //isGameActive = true;
        gameStarted = false;
        GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isPaused = !isPaused;
                titleScreen.gameObject.SetActive(!gameStarted);
                pauseScreen.gameObject.SetActive(isPaused);
                Time.timeScale = isPaused ? 0 : 1;
                AudioListener.pause = isPaused;
            }
        }
        if(Input.GetMouseButton(0))
        {
            GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = false;
        }
        
    }

    public void GameStart(int difficulty)
    {
        //GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;
        isGameActive = true;
        gameStarted = true;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            UpdateScore(5);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        //GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = false;
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        gameStarted = false;
        
        FirstHeart.gameObject.SetActive(false);
        SecondHeart.gameObject.SetActive(false);
        ThirdHeart.gameObject.SetActive(false);
        
        StopCoroutine(SpawnTarget());
        
        StartCoroutine(SpawnTargetsDeathScreen());
        restartButton.gameObject.SetActive(true);
    }

    IEnumerator SpawnTargetsDeathScreen()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(deathSpawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateHealth(int healthChange)
    {
        health += healthChange;
        if (health == 2)
        {
            ThirdHeart.gameObject.SetActive(false);
        }
        else if (health == 1)
        {
            SecondHeart.gameObject.SetActive(false);
        }
        else if (health == 0)
        {
            FirstHeart.gameObject.SetActive(false);
            GameOver();
        }
    }

}

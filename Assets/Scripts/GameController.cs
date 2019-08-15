using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject barrelPrefab;
    public AudioClip gameOverSound;

    public AudioSource audioSource;

    public Text txtScore;

    private GameObject player;

    public List<GameObject> enemiesList;

    private int playerScore = 0;
    private int enemiesAmout = 10;
    private float enemiesDistance = 0.99f;

    public bool isStarted = false;
    public bool isOver = false;

    public Image timeBar;
    private float barScale;
        

    void Start()
    {
        enemiesList = new List<GameObject>();
        txtScore.text = "TOQUE PARA INICIAR!!";
        txtScore.alignment = TextAnchor.MiddleCenter;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        barScale = timeBar.GetComponent<RectTransform>().localScale.x;
        CreateEnemies();
    }

    void Update()
    {
        DecreaseBar();
    }

    void IncreaseBar()
    {
        barScale += 0.2f;
        timeBar.GetComponent<RectTransform>().localScale = new Vector2(barScale, 1.0f);
        if (barScale >= 1)
        {
            barScale = 1;
        }
    }

    void DecreaseBar()
    {
        if (isStarted)
        {
            if (barScale > 0.01f)
            {
                barScale -= 0.15f * Time.deltaTime;
                timeBar.GetComponent<RectTransform>().localScale = new Vector2(barScale, 1.0f);
            }
            else
            {
                GameOver();
            }       
        }
    }

    void CreateEnemies()
    {
        for (int i = 0; i < enemiesAmout; i++)
        {
            if (enemiesList.Count < 2)
            {
                SpawnBarrel(new Vector2(0,-3.6f + (i * enemiesDistance)));
            }
            else
            {
                SpawnEnemy(new Vector2(0, -3.6f + (i * enemiesDistance)));
            }
        }
    }

    public void RemoveEnemyFromList(int direction)
    {
        GameObject enemy = enemiesList[0];
        enemy.GetComponent<EnemyController>().TakeDamage(direction);
        enemiesList.RemoveAt(0);
        EnemyReposition();
    }

    void SpawnEnemy(Vector2 position)
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = position;
        enemiesList.Add(newEnemy);
    }

    void SpawnBarrel(Vector2 position)
    {
        GameObject newBarrel = Instantiate(barrelPrefab);
        newBarrel.transform.position = position;
        enemiesList.Add(newBarrel);
    }

    void EnemyReposition()
    {
        SpawnEnemy(new Vector2(0,-3.6f + (enemiesAmout * enemiesDistance)));
        foreach (var enemy in enemiesList)
        {
            enemy.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y - enemiesDistance);
        }
    }

    public void CheckPlay()
    {
        if (enemiesList[0].gameObject.tag == "Enemy")
        {
            if (player.transform.localScale.x < 0 && enemiesList[0].transform.localScale.x > 0)
            {
                GameOver();
            }
            else if (player.transform.localScale.x > 0 && enemiesList[0].transform.localScale.x < 0)
            {
                GameOver();
            }
            else
            {
                AddScore();
            }
        }
        else
        {
            AddScore();
        }
        txtScore.text = "SCORE: " + playerScore;
    }

    void AddScore()
    {
        playerScore++;
        IncreaseBar();
    }

    public void GameOver()
    {
        isOver = true;
        isStarted = false;
        player.GetComponent<PlayerController>().Death();
        txtScore.text = "GAME OVER";
        Play(gameOverSound);
        Invoke("RestartScene", 2.0f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
    }

    public void Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}

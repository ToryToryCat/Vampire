using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Object")]
    [SerializeField] public Player player;
    [SerializeField] public PoolManager pool;
    [SerializeField] public LevelUp uiLevelUP;
    [SerializeField] public GameResult uiResult;
    [SerializeField] public GameObject EnemyCleaner;

    [Header("# Game Control")]
    public bool isLive = false;
    public float gameTime;
    public float maxGameTime = 2 * 10;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10,30,60,100,150,210,280,360,450,600 };

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GameStart(int playerId)
    {
        this.playerId = playerId;
        player.gameObject.SetActive(true);
        health = maxHealth;
        uiLevelUP.Select(this.playerId);
        isLive = true;
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoution());
    }

    IEnumerator GameOverRoution()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoution());
    }

    IEnumerator GameVictoryRoution()
    {
        isLive = false;
        EnemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GetExp()
    {
        if (!isLive) return;
        
        exp++;
        
        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) 
        {
            level++;
            exp = 0;

            uiLevelUP.Show();
        }
    }

    public void Stop()
    { 
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

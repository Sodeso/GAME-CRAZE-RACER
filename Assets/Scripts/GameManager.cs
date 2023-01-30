using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnerBots;
    [SerializeField] GameObject background;
    [SerializeField] GameObject panelLoseGame;
    [SerializeField] GameObject spawnerGold;
    [SerializeField] GameObject panelPause;


    [Space]

    [Header("Player")]
    [SerializeField] float playerSpeed;
    [SerializeField] float sideSpeed;

    [Space]

    [Header("Spawner bots/gold")]
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float timeBetweenSpawn;

    [Space]

    [Header("Background")]
    [SerializeField] float loopSpeed;

    [Space]

    public List<GameObject> listBots;
    public List<Sprite> playerCars;
    public GameObject goldObject;

    [Space]
    [Header("Stats")]
    public int currentScore;
    public int maxScore;
    public int lastScore;
    public int gold;

    TextMeshProUGUI textCurrentScore;
    TextMeshProUGUI textMaxScore;
    TextMeshProUGUI textLastScore;
    TextMeshProUGUI textGold;
    float _timer = 0;

    private void Awake()
    {
        textCurrentScore = GameObject.Find("CurrentScore").gameObject.GetComponent<TextMeshProUGUI>();
        textMaxScore = GameObject.Find("BestScore").gameObject.GetComponent<TextMeshProUGUI>();
        textLastScore = GameObject.Find("LastScore").gameObject.GetComponent<TextMeshProUGUI>();
        textGold = GameObject.Find("TextGold").gameObject.GetComponent<TextMeshProUGUI>();
        currentScore = 0;
        if (PlayerPrefs.HasKey($"{PlayerName.name}[MaxScore]"))
        {
            maxScore = PlayerPrefs.GetInt($"{PlayerName.name}[MaxScore]");
            lastScore = PlayerPrefs.GetInt($"{PlayerName.name}[LastScore]");
            gold = PlayerPrefs.GetInt($"{PlayerName.name}[Gold]");
        }
        UpdateText();
    }
    void Start()
    {
        StartCoroutine(WaitStartGame());
        player.gameObject.GetComponent<Player>().playerSpeed = playerSpeed;
        player.gameObject.GetComponent<Player>().sideSpeed = sideSpeed;
        spawnerBots.gameObject.GetComponent<BotSpawner>().maxX = maxX;
        spawnerBots.gameObject.GetComponent<BotSpawner>().minX = minX;
        spawnerBots.gameObject.GetComponent<BotSpawner>().maxY = maxY;
        spawnerBots.gameObject.GetComponent<BotSpawner>().minY = minY;
        spawnerGold.gameObject.GetComponent<GoldSpawner>().maxX = maxX;
        spawnerGold.gameObject.GetComponent<GoldSpawner>().minX = minX;
        spawnerGold.gameObject.GetComponent<GoldSpawner>().maxY = maxY;
        spawnerGold.gameObject.GetComponent<GoldSpawner>().minY = minY;
        spawnerBots.gameObject.GetComponent<BotSpawner>().timeBetweenSpawn = timeBetweenSpawn;
        spawnerGold.gameObject.GetComponent<GoldSpawner>().timeBetweenSpawn = timeBetweenSpawn;
        background.gameObject.GetComponent<BackgroundLooping>().loopSpeed = loopSpeed;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        UpdateText();
        if(_timer >= 6)
        {
            if (timeBetweenSpawn > 0.5f)
            {
                timeBetweenSpawn -= 0.1f;
                spawnerBots.gameObject.GetComponent<BotSpawner>().timeBetweenSpawn = timeBetweenSpawn;
            }
            loopSpeed += 0.3f;
            background.gameObject.GetComponent<BackgroundLooping>().loopSpeed = loopSpeed;
            _timer = 0;
        }
    }
    void UpdateText()
    {
        textCurrentScore.text = currentScore.ToString();
        textMaxScore.text = "Best:"+maxScore.ToString();
        textLastScore.text = "Last:"+lastScore.ToString();
        textGold.text = "Gold:"+gold.ToString();
    }
    public void ClickPause()
    {
        if (Time.timeScale == 0)
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
            
    }
    public void LoseGame()
    {
        panelLoseGame.SetActive(true);
        panelLoseGame.transform.Find("TextScore").gameObject.GetComponent<TextMeshProUGUI>().text = $"¬аш счЄт: {lastScore}";
        PlayerPrefs.SetInt($"{PlayerName.name}[MaxScore]", maxScore);
        PlayerPrefs.SetInt($"{PlayerName.name}[LastScore]", lastScore);
        PlayerPrefs.SetInt($"{PlayerName.name}[Gold]", gold);
        PlayerPrefs.Save();
        ScoreInMenu.SetLeader(PlayerName.name, lastScore);
    }
    public void ClickMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void ClickRestart()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void ClickBuyCar(int index)
    {
        if(index == 0)
        {
            if(gold >= 10)
            {
                gold -= 10;
                GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = playerCars[0];
            }

        }
        if(index == 1)
        {
            if (gold >= 20)
            {
                gold -= 20;
                GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = playerCars[1];
            }
        }
    }
    IEnumerator WaitStartGame()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public GameObject inGame, leaderBoard;
    private Button nextLevel;
    public TextMeshProUGUI countText;
    int count = 3;

    public TextMeshProUGUI currentLevelText, nextLevelText;
    public Image fill;

    public Sprite orange, gray;

    private void Awake()
    {
        instance= this;
    }
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if (GameManager.instance.failed)
        {
            if (leaderBoard.activeInHierarchy)
            {
                GameManager.instance.failed = false;
                Restart();
            }
        }
    }
    IEnumerator StartGame()
    {
        countText.text = count.ToString();
        yield return new WaitForSeconds(1);

        countText.text = (count-1).ToString();
        countText.color = Color.magenta;
        yield return new WaitForSeconds(1);

        countText.color = Color.blue;
        countText.text = (count-2).ToString();
        yield return new WaitForSeconds(1);

        countText.color = Color.green;
        countText.text = "GO !";
        yield return new WaitForSeconds(.5f);
        countText.gameObject.SetActive(false);
        GameManager.instance.start = true;
    }
    public void OpenLeaderBoard()
    {
        inGame.SetActive(false);
        leaderBoard.SetActive(true);
        if (GameManager.instance.failed)
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            nextLevelText.text = (PlayerPrefs.GetInt("Level", 1) + 1).ToString();
            fill.sprite = gray;

        }
        else
        {
            currentLevelText.text = (PlayerPrefs.GetInt("Level", 1) - 1).ToString();
            nextLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            fill.sprite = orange;
        }

        
    }
    private void Restart()
    {
        nextLevel = GameObject.Find("/GameUI/LeaderboradPanel/NextLevel").GetComponent<Button>();
        nextLevel.onClick.RemoveAllListeners();
        nextLevel.onClick.AddListener(() => Reload());
        nextLevel.transform.GetChild(0).GetComponent<Text>().text = "Again";
       
    }
    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}

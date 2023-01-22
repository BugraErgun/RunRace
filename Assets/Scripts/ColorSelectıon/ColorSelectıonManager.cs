using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ColorSelectıonManager : MonoBehaviour
{
    private Camera mainCamera;
    private int currentPlayer=0;

    public float speed = .5f;
    public float selectionPos = 13;

    public GameObject charParents;

    public Button playBtn, buyBtn,nextBtn,prevBtn;

    public int points = 60;
    public int[] colorPrice;

    void Awake()
    {
        mainCamera = Camera.main;
        CamPos();
        CheckIfBought();

    }

    public void Buy()
    {
        switch (currentPlayer)
        {
            case 1:
                if (points >= colorPrice[0]&&PlayerPrefs.GetInt("BlueBuy",0)==0)
                {
                    PlayerPrefs.SetInt("BlueBuy", 1);
                    points -= colorPrice[0];
                }
                break;
            case 2:
                if (points >= colorPrice[1] && PlayerPrefs.GetInt("YellowBuy",0) == 0)
                {
                    PlayerPrefs.SetInt("YellowBuy", 1);
                    points -= colorPrice[1];
                }
                break;
            case 3:
                if (points >= colorPrice[2] && PlayerPrefs.GetInt("GreenBuy", 0) == 0)
                {
                    PlayerPrefs.SetInt("GreenBuy", 1);
                    points -= colorPrice[2];
                }
                break;
        }
        CheckIfBought();

    }
    void CheckIfBought()
    {
        buyBtn.interactable = true;
        playBtn.interactable = true;
        buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Buy";
        buyBtn.image.color = Color.blue;

        switch (currentPlayer)
        {
           

            case 0:
                buyBtn.interactable = false;
                buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bought";
                break;
            case 1:
                if (PlayerPrefs.GetInt("BlueBuy")==1)
                {
                    buyBtn.interactable = false;
                    buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bought";
                }
                else
                {
                    playBtn.interactable = false;
                    if (points >= colorPrice[0])
                    {
                        buyBtn.image.color = Color.green;
                    }
                    else
                    {
                        buyBtn.image.color = Color.red;
                    }
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("YellowBuy") == 1)
                {
                    buyBtn.interactable = false;
                    buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bought";
                }
                else
                {
                    playBtn.interactable = false;
                    if (points >= colorPrice[1])
                    {
                        buyBtn.image.color = Color.green;
                    }
                    else
                    {
                        buyBtn.image.color = Color.red;
                    }
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("GreenBuy") == 1)
                {
                    buyBtn.interactable = false;
                    buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bought";
                }
                else
                {
                    playBtn.interactable = false;
                    if (points >= colorPrice[2])
                    {
                        buyBtn.image.color = Color.green;
                    }
                    else
                    {
                        buyBtn.image.color = Color.red;
                    }
                }
                break;
        }
    }
    void CamPos()
    {
        currentPlayer = PlayerPrefs.GetInt("PlayerColor");
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + (currentPlayer * 13),
            mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
    public void Play()
    {
        SceneManager.LoadScene("1");
        PlayerPrefs.SetInt("PlayerColor", currentPlayer);
        
    }
    public void Next()
    {
        if (currentPlayer<charParents.transform.childCount-1)
        {
            currentPlayer++;
            StartCoroutine(MoveToNext());
            CheckIfBought();
        }
    }
    public void Prev()
    {
        if (currentPlayer > 0)
        {
            currentPlayer--;
            StartCoroutine(MoveToPrev());
            CheckIfBought();
        }
    }
    IEnumerator MoveToNext()
    {
        Vector3 tempPos = new Vector3(mainCamera.transform.position.x + selectionPos, mainCamera.transform.position.y,
            mainCamera.transform.position.z);

        nextBtn.interactable = false;
        prevBtn.interactable = false;

        while (mainCamera.transform.position.x<tempPos.x)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, tempPos, speed*.5f);
            yield return new WaitForSeconds(Time.deltaTime*speed);
        }
        nextBtn.interactable = true;
        prevBtn.interactable = true;
        yield return null;    
    }
    IEnumerator MoveToPrev()
    {
        Vector3 tempPos = new Vector3(mainCamera.transform.position.x - selectionPos, mainCamera.transform.position.y,
            mainCamera.transform.position.z);
        nextBtn.interactable = false;
        prevBtn.interactable = false;
        while (mainCamera.transform.position.x > tempPos.x)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, tempPos, speed * .5f);
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }
        nextBtn.interactable = true;
        prevBtn.interactable = true;
        yield return null;
    }
}

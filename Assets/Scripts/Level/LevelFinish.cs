using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    public TextMeshProUGUI[] rankText;
    void Update()
    {
        rankText[0].text = GameManager.instance.firstPlace;
        rankText[1].text = GameManager.instance.secondPlace;
        rankText[2].text = GameManager.instance.thirdPlace;
    }
}

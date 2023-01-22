using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public TextMeshProUGUI[] nameTexts;
    public string a, b, c;
    public Image thirdPlaceImg,secondPlaceImg,firstPlaceImg;


    void Update()
    {
        nameTexts[0].text= a;
        nameTexts[1].text= b;
        nameTexts[2].text= c;

    }
}

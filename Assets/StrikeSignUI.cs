using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrikeSignUI : MonoBehaviour
{
    public TMP_Text strikeText;

    private void Awake()
    {
        LivesManager.onLifeUpdate += SetStrikeText;
        strikeText.text = string.Empty;
    }

    public void SetStrikeText(int strike)
    {
        if (strike <= 0) 
        {
            strikeText.text = "xxx";
        }
        else if (strike <= 1)
        {
            strikeText.text = "xx";
        }
        else if (strike <= 2)
        {
            strikeText.text = "x";
        }
        else
        {
            strikeText.text = string.Empty;
        }
    }
}

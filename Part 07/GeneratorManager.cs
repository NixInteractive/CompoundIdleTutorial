using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;
using System;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    public string[] buyAmounts;
    public string currentBuyAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentBuyAmount = buyAmounts[0];
        buttonText.text = currentBuyAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAmount()
    {
        int index = Array.IndexOf(buyAmounts, currentBuyAmount);
        index++;

        if(index > buyAmounts.Length-1)
        {
            index = 0;
        }

        currentBuyAmount = buyAmounts[index];
        buttonText.text = currentBuyAmount;
    }
}

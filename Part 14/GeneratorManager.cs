using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;
using System;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Transform generatorHolder;
    [SerializeField] private GameObject generatorPrefab;

    public string[] buyAmounts;
    public string currentBuyAmount;

    public GeneratorData[] availableGenerators;
    public List<Generator> activeGenerators;

    private void Awake()
    {
        SpawnGenerators();
    }

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

    private void SpawnGenerators()
    {
        for(int g = activeGenerators.Count; g < availableGenerators.Length; g++)
        {
            if(g >= availableGenerators.Length)
            {
                return;
            }

            GameObject newGen = Instantiate(generatorPrefab, generatorHolder);
            Generator newGenComponent = newGen.GetComponent<Generator>();
            newGenComponent.generatorManager = this;
            newGenComponent.Initialize(availableGenerators[g]);
            activeGenerators.Add(newGenComponent);

            if (g > 0)
            {
                newGenComponent.hasProduct = true;
                newGenComponent.product = activeGenerators[g - 1];
            }

            GetComponent<ManagerManager>().GenerateManagers(newGenComponent);
        }
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

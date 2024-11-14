using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

[System.Serializable]
public class GeneratorData 
{
    public BigDouble quantity, income, productCost, moneyCost, limiterCost, maxTime, currentTime, defaultMaxTime, defaultIncome;
    public bool isActivated;

    public GeneratorData(GeneratorData data)
    {
        quantity = data.quantity;
        income = data.income;
        productCost = data.productCost;
        moneyCost = data.moneyCost;
        limiterCost = data.limiterCost;
        maxTime = data.maxTime;
        currentTime = data.currentTime;
        defaultMaxTime = data.defaultMaxTime;
        defaultIncome = data.defaultIncome;
        isActivated = data.isActivated;
    }
}

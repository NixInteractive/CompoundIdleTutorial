using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;

[System.Serializable]
public class GeneratorData 
{
    public string name;
    public int generatorIndex;
    public BigDouble quantity, income, productCost, moneyCost, limiterCost, maxTime, currentTime, defaultMaxTime, defaultIncome;
    public bool isActivated;
    public Color barColor;
    public Sprite icon1;
    public Sprite icon2;

    public GeneratorData(GeneratorData data)
    {
        name = data.name;

        generatorIndex = data.generatorIndex;

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

        barColor = data.barColor;

        icon1 = data.icon1;
        icon2 = data.icon2;
    }

    public GeneratorData(string name, int generatorIndex, BigDouble quantity, BigDouble income, BigDouble productCost, BigDouble moneyCost, BigDouble limiterCost, BigDouble maxTime, BigDouble currentTime, BigDouble defaultMaxTime, BigDouble defaultIncome, bool isActivated, Color barColor, Sprite icon1, Sprite icon2)
    {
        this.name = name;
        this.generatorIndex = generatorIndex;
        this.quantity = quantity;
        this.income = income;
        this.productCost = productCost;
        this.moneyCost = moneyCost;
        this.limiterCost = limiterCost;
        this.maxTime = maxTime;
        this.currentTime = currentTime;
        this.defaultMaxTime = defaultMaxTime;
        this.defaultIncome = defaultIncome;
        this.isActivated = isActivated;
        this.barColor = barColor;
        this.icon1 = icon1;
        this.icon2 = icon2;
    }
}

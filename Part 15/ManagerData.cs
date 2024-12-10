using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ManagerData
{
    public string managerName;

    [TextArea]public string managerDescription;

    public int rarity;

    public BigDouble quantity;
    public BigDouble quantityToLevel;
    public BigDouble price;
    public BigDouble level;

    public Sprite icon;

    public enum ManagerTypes
    {
        Speed,
        Production,
        BonusChance,
        BonusAmount,
        CostReduction
    };

    public ManagerTypes managerType;

    public Generator target;

    public ManagerData(string managerName, string managerDescription, int rarity, BigDouble quantity, BigDouble quantityToLevel, BigDouble price, BigDouble level, Sprite icon, ManagerTypes managerType, Generator target)
    {
        this.managerName = managerName;
        this.managerDescription = managerDescription;
        this.rarity = rarity;
        this.quantity = quantity;
        this.quantityToLevel = quantityToLevel;
        this.price = price;
        this.level = level;
        this.icon = icon;
        this.managerType = managerType;
        this.target = target;
    }

    public ManagerData(ManagerData data)
    {
        managerName = data.managerName;
        managerDescription = data.managerDescription;
        rarity = data.rarity;
        quantity = data.quantity;
        quantityToLevel = data.quantityToLevel;
        price = data.price;
        level = data.level;
        icon = data.icon;
        managerType = data.managerType;
        target = data.target;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDisp;
    [SerializeField] private TextMeshProUGUI descDisp;
    [SerializeField] private TextMeshProUGUI quantityDisp;
    [SerializeField] private Image managerBG;
    [SerializeField] private Image managerIcon;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI levelDisp;
    [SerializeField] private TextMeshProUGUI priceDisp;

    private CurrencyManager currencyManager;

    public string managerName;
    public string managerDescription;

    public BigDouble quantity;
    public BigDouble quantityToLevel;
    public BigDouble price;
    public BigDouble level;

    public int rarity;

    public ManagerData data;

    public ManagerData.ManagerTypes managerType;

    public Generator target;

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        UpdateModifiers();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        UpdateButton();
    }

    private void UpdateButton()
    {
        if(quantity >= quantityToLevel && currencyManager.upgradeCurrency >= price)
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    private void UpdateDisplay()
    {
        nameDisp.text = managerName;
        descDisp.text = managerDescription;
        quantityDisp.text = $"{currencyManager.SciNotToUSName(quantity)}/{currencyManager.SciNotToUSName(quantityToLevel)}";
        progressBar.fillAmount = (float)(quantity/quantityToLevel).ToDouble();
        priceDisp.text = $"Buy: {price}";
        levelDisp.text = currencyManager.SciNotToUSName(level);
    }

    public void SetBGColor(Color color)
    {
        managerBG.color = color;
    }

    public void SetIcon(Sprite icon)
    {
        managerIcon.sprite = icon;
    }

    public void Upgrade()
    {
        currencyManager.upgradeCurrency -= price;
        level++;
        quantity -= quantityToLevel;

        price = Mathf.Round(100 * Mathf.Pow(1.25f, (float)level.ToDouble()));
        quantityToLevel = Mathf.Round(10 * Mathf.Pow(1.5f, (float)level.ToDouble()));
        UpdateModifiers();
    }

    public void Initialize(ManagerData newData)
    {
        data = newData;

        managerName = newData.managerName;
        managerDescription = newData.managerDescription;
        rarity = newData.rarity;
        quantity = newData.quantity;
        price = newData.price;
        level = newData.level;
        quantityToLevel = newData.quantityToLevel;
        managerType = newData.managerType;
        target = newData.target;
    }

    private void UpdateModifiers()
    {
        float modLevel = (float)(level - 1).ToDouble();

        switch (managerType)
        {
            case ManagerData.ManagerTypes.Speed:
                target.speedModifier = Mathf.Pow(2, modLevel);

                if(level > 1 || quantity > 0)
                {
                    target.alwaysActive = true;
                }

                break;
            case ManagerData.ManagerTypes.Production:
                target.productionModifier = Mathf.Pow(2, modLevel);
                break;
            case ManagerData.ManagerTypes.BonusChance:
                target.bonusChance = modLevel * 0.25f;
                break;
            case ManagerData.ManagerTypes.BonusAmount:
                target.bonusAmount = 1 + (0.5f * modLevel);
                break;
            case ManagerData.ManagerTypes.CostReduction:
                target.costModifier = 1 - (modLevel * 0.01f);
                target.costModifier = Mathf.Clamp(target.costModifier, 0.01f, 1);
                break;
        }
    }
}

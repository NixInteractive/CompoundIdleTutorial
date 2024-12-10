using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;
using System;

public class Generator : MonoBehaviour
{
    public string generatorName;

    #region UI_VALUES
    [SerializeField] private Image progressBar;
    [SerializeField] private Image icon;
    [SerializeField] private Image productCostIcon;

    [SerializeField] private TextMeshProUGUI nameDisp;
    [SerializeField] private TextMeshProUGUI quantityDisp;
    [SerializeField] private TextMeshProUGUI perSecondDisp;
    [SerializeField] private TextMeshProUGUI productCostDisp;
    [SerializeField] private TextMeshProUGUI limiterCostDisp;
    [SerializeField] private TextMeshProUGUI moneyCostDisp;
    [SerializeField] private TextMeshProUGUI buyButtonText;

    [SerializeField] private Button activateButton;
    [SerializeField] private Button buyButton;
    #endregion

    #region PARAMETERS
    public BigDouble income;
    public BigDouble quantity;
    public BigDouble productCost;
    public BigDouble moneyCost;
    public BigDouble limiterCost;
    public BigDouble baseMaxTime;
    public BigDouble modifiedMaxTime;
    public BigDouble currentTime;

    public float costModifier;
    public float speedModifier;
    public float productionModifier;
    public float bonusChance;
    public float bonusAmount;
    #endregion

    private float animationSpeed = 1;
    private float animationTimer;

    public bool hasProduct;
    public bool activated { get; set; }
    public bool alwaysActive = false;

    public Sprite icon1;
    public Sprite icon2;

    #region REFERENCES
    public CurrencyManager currencyManager;
    public Generator product;
    public GeneratorData data;
    public GeneratorManager generatorManager;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        UpdateGeneratorValues();
        UpdateDisplay();
        HandleActivation();
        UpdateProgressBar();
        UpdateBuyButton();
    }

    public void Initialize(GeneratorData newData)
    {
        data = newData;

        name = data.name;
        generatorName = data.name;

        quantity = data.quantity;
        income = data.income;
        productCost = data.productCost;
        moneyCost = data.moneyCost;
        limiterCost = data.limiterCost;
        modifiedMaxTime = data.modifiedMaxTime;
        currentTime = data.currentTime;
        baseMaxTime = data.baseMaxTime;

        activated = data.activated;

        progressBar.color = data.barColor;

        icon1 = data.icon1;
        icon2 = data.icon2;
    }

    private void UpdateBuyButton()
    {
        if (GetBuyAmount() > 0)
        {
            buyButton.interactable = true;
            buyButtonText.text = $"Buy x{GetBuyAmount()}";
        }
        else
        {
            buyButton.interactable = false;
            buyButtonText.text = "Can't Afford!";
        }
    }


    private void UpdateProgressBar()
    {
        progressBar.color = data.barColor;

        if(modifiedMaxTime < 0.25f)
        {
            progressBar.fillAmount = 1;
        }
        else
        {
            progressBar.fillAmount = (float)(currentTime / modifiedMaxTime).ToDouble();
        }
    }

    private void HandleActivation()
    {
        if (activated || alwaysActive)
        {
            activateButton.interactable = false;
            currentTime += Time.deltaTime;

            if(currentTime > modifiedMaxTime)
            {
                ProcessIncome();
                currentTime = 0;
                activated = false;
                activateButton.interactable = true;
            }
        }
    }

    private void ProcessIncome()
    {
        BigDouble incomeAmount = income * quantity * Mathf.Pow(2, productionModifier);

        int bonusRoll = UnityEngine.Random.Range(0, 100);

        if(bonusRoll < bonusChance)
        {
            incomeAmount *= bonusAmount;
        }

        if (hasProduct)
        {
            product.quantity += incomeAmount;
        }
        else
        {
            currencyManager.money += incomeAmount;
        }
    }

    private void UpdateDisplay()
    {
        quantityDisp.text = currencyManager.SciNotToUSName(quantity);
        nameDisp.text = generatorName;

        BigDouble amount = GetBuyAmount();

        if(amount < 1)
        {
            amount = 1;
        }

        moneyCostDisp.text = currencyManager.SciNotToUSName(moneyCost * costModifier * amount);
        limiterCostDisp.text = currencyManager.SciNotToUSName(limiterCost * costModifier * amount);

        if (!hasProduct)
        {
            productCostDisp.gameObject.SetActive(false);
            productCostIcon.gameObject.SetActive(false);
        }
        else
        {
            productCostDisp.text = currencyManager.SciNotToUSName(productCost * costModifier * amount);
            productCostIcon.sprite = product.icon1;
        }

        perSecondDisp.text = $"{currencyManager.SciNotToUSName(income * quantity * Mathf.Pow(2, productionModifier))} in {currencyManager.SciNotToUSName(Mathf.Round((float)(modifiedMaxTime - currentTime).ToDouble()))} seconds";
    }

    private void UpdateGeneratorValues()
    {
        modifiedMaxTime = baseMaxTime / Mathf.Pow(2, speedModifier);

        if(product == null)
        {
            hasProduct = false;
        }
    }

    private void HandleAnimation()
    {
        animationTimer += Time.deltaTime;

        if(animationTimer >= animationSpeed)
        {
            if(icon.sprite == icon1)
            {
                icon.sprite = icon2;
            }
            else
            {
                icon.sprite = icon1;
            }

            animationTimer = 0;
        }
    }

    private BigDouble GetBuyAmount()
    {
        BigDouble quantityToBuy = currencyManager.money / (moneyCost * costModifier);
        int buyAmountIndex = Array.IndexOf(generatorManager.buyAmounts, generatorManager.currentBuyAmount);

        if(quantityToBuy > currencyManager.limiter / (limiterCost * costModifier))
        {
            quantityToBuy = currencyManager.limiter / (limiterCost * costModifier);
        }

        if(hasProduct && quantityToBuy > product.quantity / (productCost * costModifier))
        {
            quantityToBuy = product.quantity / (productCost * costModifier);
        }

        switch (buyAmountIndex)
        {
            case 1:
                quantityToBuy *= 0.1f;
                break;
            case 2:
                quantityToBuy *= 0.25f;
                break;
            case 3:
                quantityToBuy *= 0.5f;
                break;
        }

        quantityToBuy = BigDouble.Floor(quantityToBuy);

        if(quantityToBuy >= 1)
        {
            if (buyAmountIndex == 0)
            {
                return 1;
            }
            else
            {
                return quantityToBuy;
            }
        }

        return 0;
    }

    public void Buy()
    {
        BigDouble buyAmount = GetBuyAmount();

        quantity += buyAmount;
        currencyManager.money -= buyAmount * moneyCost * costModifier;
        currencyManager.limiter -= buyAmount * limiterCost * costModifier;

        if (hasProduct)
        {
            product.quantity -= buyAmount * productCost * costModifier;
        }
    }
}

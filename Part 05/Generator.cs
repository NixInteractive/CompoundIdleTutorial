using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BreakInfinity;

public class Generator : MonoBehaviour
{
    public string generatorName;

    [SerializeField] private Image progressBar;
    [SerializeField] private Image icon;
    [SerializeField] private Image productCostIcon;

    [SerializeField] private TextMeshProUGUI nameDisp;
    [SerializeField] private TextMeshProUGUI quantityDisp;
    [SerializeField] private TextMeshProUGUI perSecondDisp;
    [SerializeField] private TextMeshProUGUI productCostDisp;
    [SerializeField] private TextMeshProUGUI limiterCostDisp;
    [SerializeField] private TextMeshProUGUI moneyCostDisp;

    [SerializeField] private Button activateButton;

    public BigDouble income;
    public BigDouble quantity;
    public BigDouble productCost;
    public BigDouble moneyCost;
    public BigDouble limiterCost;
    public BigDouble baseMaxTime;
    public BigDouble modifiedMaxTime;
    public BigDouble currentTime;

    public float speedModifier;
    public float productionModifier;
    public float bonusChance;
    public float bonusAmount;

    private float animationSpeed = 1;
    private float animationTimer;

    public bool hasProduct;
    public bool activated { get; set; }

    public Sprite icon1;
    public Sprite icon2;

    public CurrencyManager currencyManager;
    public Generator product;
    public GeneratorData data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();
        UpdateGeneratorValues();
        UpdateDisplay();
        HandleActivation();
        UpdateProgressBar();
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
        if (activated)
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

        int bonusRoll = Random.Range(0, 100);

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

        moneyCostDisp.text = currencyManager.SciNotToUSName(moneyCost);
        limiterCostDisp.text = currencyManager.SciNotToUSName(limiterCost);

        if (!hasProduct)
        {
            productCostDisp.gameObject.SetActive(false);
            productCostIcon.gameObject.SetActive(false);
        }
        else
        {
            productCostDisp.text = currencyManager.SciNotToUSName(productCost);
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
}
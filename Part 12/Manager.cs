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

    private CurrencyManager currencyManager;

    public string managerName;
    public string managerDescription;

    public BigDouble quantity;

    public int rarity;

    // Start is called before the first frame update
    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        nameDisp.text = managerName;
        descDisp.text = managerDescription;
        quantityDisp.text = currencyManager.SciNotToUSName(quantity);
    }

    public void SetBGColor(Color color)
    {
        managerBG.color = color;
    }
}

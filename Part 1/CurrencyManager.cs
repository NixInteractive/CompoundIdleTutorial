using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyDisp;

    public double money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money++;
        money *= 1.0001;

        moneyDisp.text = money.ToString();
    }
}

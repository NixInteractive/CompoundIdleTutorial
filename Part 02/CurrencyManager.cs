using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyDisp;
    [SerializeField] private TextMeshProUGUI limiterDisp;
    [SerializeField] private TextMeshProUGUI limiterPerSecDisp;

    public BigDouble money;
    public BigDouble limiter;
    public BigDouble limiterPerSec;

    [SerializeField] private Image limiterBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money++;
        money *= 1.00001f;

        ProcessLimiter();

        moneyDisp.text = $"Money: {SciNotToUSName(money)}";
        limiterDisp.text = $"Limiter: {SciNotToUSName(limiter)}";
        limiterPerSecDisp.text = $"{SciNotToUSName(limiterPerSec)}/s";
    }

    private void ProcessLimiter()
    {
        limiterBar.fillAmount += Time.deltaTime;

        if(limiterBar.fillAmount >= 1)
        {
            limiterBar.fillAmount = 0;
            limiter += limiterPerSec;
        }
    }

    public string SciNotToUSName(BigDouble num)
    {
        string displayNumber = $"{(num.Mantissa * BigDouble.Pow(10, num.Exponent % 3)):G3}";
        int prefixIndex = (int)BigDouble.Floor(BigDouble.Abs(num.Exponent)).ToDouble();
        string name = string.Empty;
        int prefixOffset = 0;

        if(num.Exponent < 33)
        {
            prefixIndex /= 3;
            name += $"{Prefixes.suffixes[prefixIndex]}";
        }
        else
        {
            prefixIndex = (prefixIndex - 3) / 3;
            int tempPrefixIndex = prefixIndex;
            List<int> indices  = new List<int>();

            for(int i = 0; i < prefixIndex.ToString().Length; i++)
            {
                int lastNum = tempPrefixIndex % 10;
                indices.Add(lastNum);
                tempPrefixIndex /= 10;
                name += Prefixes.prefixes[indices[i] + prefixOffset];
                prefixOffset += 10;
            }
        }

        return $"{displayNumber} {name}";
    }
}

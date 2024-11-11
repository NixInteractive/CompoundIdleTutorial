using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public BigDouble money;

    public TextMeshProUGUI moneyDisp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyDisp.text = SciNotToUSName(money);
    }

    public string SciNotToUSName(BigDouble num)
    {
        string displayNumber = $"{(num.Mantissa * BigDouble.Pow(10, num.Exponent % 3)):G3} ";
        int prefixIndex = (int)BigDouble.Floor(BigDouble.Abs(num.Exponent)).ToDouble();
        string name = string.Empty;
        int prefixOffset = 0;

        if(num.Exponent <= 2)
        {
            displayNumber = $"{(Mathf.Round((float)(num.Mantissa * BigDouble.Pow(10, num.Exponent % 3)).ToDouble())):G3} ";
        }

        if(num.Exponent < 33)
        {
            prefixIndex /= 3;
            name += $"{Prefixes.prefixes[prefixIndex]}";
        }
        else
        {
            prefixIndex = (prefixIndex - 3) / 3;
            int tempPrefixIndex = prefixIndex;
            List<int> prefixList = new List<int>();

            for(int i = 0; i < prefixIndex.ToString().Length; i++)
            {
                int lastNum = tempPrefixIndex % 10;
                prefixList.Add(lastNum);
                tempPrefixIndex /= 10;
                name += Prefixes.list[prefixList[i] + prefixOffset];
                prefixOffset += 10;
            }
        }

        return displayNumber + name;
    }
}

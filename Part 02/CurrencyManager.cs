using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BreakInfinity;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyDisp;

    public BigDouble money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money++;
        money *= 1.00001f;

        moneyDisp.text = SciNotToUSName(money);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDisp;
    [SerializeField] private TextMeshProUGUI descDisp;

    public string managerName;
    public string managerDescription;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}

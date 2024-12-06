using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform[] menus;
    
    private int activeMenuIndex = 0;

    [Range(0f, 1f)]
    public float animSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeScrollbars();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(i == activeMenuIndex)
            {
                ChangeMenuSize(animSpeed, menus[i]);
            }
            else
            {
                ChangeMenuSize(-animSpeed, menus[i]);
            }
        }

        if (menus[activeMenuIndex].localScale.y < 1)
        {
            InitializeScrollbars();
        }
    }

    public void MenuToggle(int index)
    {
        activeMenuIndex = index;
    }

    private void ChangeMenuSize(float speed, RectTransform rectToChange)
    {
        float newSize = rectToChange.localScale.y;
        newSize += speed;
        newSize = Mathf.Clamp(newSize, 0f, 1f);
        rectToChange.localScale = new Vector3(1, newSize, 1);
    }

    private void InitializeScrollbars()
    {
        Scrollbar[] scrollbars = FindObjectsOfType<Scrollbar>();

        foreach(Scrollbar scroller in scrollbars)
        {
            scroller.value = 1f;
        }
    }
}

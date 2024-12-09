using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour
{
    [SerializeField] private Transform managerHolder;
    [SerializeField] private GameObject managerPrefab;

    public ManagerData[] availableManagers;
    public List<Manager> activeManagers;

    public Color[] rarityColors;

    private void Awake()
    {
        SpawnManagers();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnManagers()
    {
        for(int m = activeManagers.Count; m < availableManagers.Length; m++)
        {
            if(m >= availableManagers.Length)
            {
                return;
            }

            GameObject newMan = Instantiate(managerPrefab, managerHolder);
            Manager newManComponent = newMan.GetComponent<Manager>();
            newManComponent.managerName = availableManagers[m].managerName;
            newManComponent.managerDescription = availableManagers[m].managerDescription;
            newManComponent.rarity = availableManagers[m].rarity;
            newManComponent.SetBGColor(rarityColors[availableManagers[m].rarity]);
            newMan.name = availableManagers[m].managerName;
            activeManagers.Add(newManComponent);

            if(newManComponent.quantity == 0)
            {
                newManComponent.gameObject.SetActive(false);
            }
        }
    }

    private void RefreshManagers()
    {
        foreach(Manager manager in activeManagers)
        {
            if(manager.quantity > 0)
            {
                manager.gameObject.SetActive(true);
            }
        }
    }

    public void AcquireManager()
    {
        List<Manager> common = new List<Manager>();
        List<Manager> uncommon = new List<Manager>();
        List<Manager> rare = new List<Manager>();

        foreach(Manager manager in activeManagers)
        {
            switch (manager.rarity)
            {
                case 0:
                    common.Add(manager);
                    break;
                case 1:
                    uncommon.Add(manager);
                    break;
                case 2:
                    rare.Add(manager);
                    break;
            }
        }

        int bucketRoll = Random.Range(0, 100);

        if(bucketRoll >= 90 && rare.Count > 0)
        {
            rare[Random.Range(0, rare.Count)].quantity++;
        }
        else if(bucketRoll >= 60 && uncommon.Count > 0)
        {
            uncommon[Random.Range(0, uncommon.Count)].quantity++;
        }
        else
        {
            common[Random.Range(0, common.Count)].quantity++;
        }

        RefreshManagers();
    }
}

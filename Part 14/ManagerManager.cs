using BreakInfinity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ManagerManager : MonoBehaviour
{
    [SerializeField] private Transform managerHolder;
    [SerializeField] private GameObject managerPrefab;

    public List<ManagerData> availableManagers;
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
        for(int m = activeManagers.Count; m < availableManagers.Count; m++)
        {
            if(m >= availableManagers.Count)
            {
                return;
            }

            GameObject newMan = Instantiate(managerPrefab, managerHolder);
            Manager newManComponent = newMan.GetComponent<Manager>();
            newManComponent.Initialize(availableManagers[m]);
            newManComponent.SetBGColor(rarityColors[availableManagers[m].rarity]);
            newManComponent.SetIcon(availableManagers[m].icon);
            newMan.name = availableManagers[m].managerName;
            activeManagers.Add(newManComponent);

            if(newManComponent.quantity == 0 && newManComponent.level == 1)
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

    public void GenerateManagers(Generator generatorObject)
    {
        GeneratorData generator = generatorObject.data;
        string[] types = Enum.GetNames(typeof(ManagerData.ManagerTypes));
        float rarityIndex = 0;

        foreach (string type in types)
        {
            string newName = $"{generator.name} {type}";
            string newDesc = $"Increases the {type} of {generator.name}";
            int newRarity = (int)Mathf.Floor(rarityIndex % 3);
            BigDouble newQuantity = 0;
            BigDouble newQuantityToLevel = 10;
            BigDouble newPrice = 100;
            Sprite newIcon = generator.icon1;
            ManagerData.ManagerTypes newType = (ManagerData.ManagerTypes)Enum.Parse(typeof(ManagerData.ManagerTypes), type);

            ManagerData newManager = new ManagerData(newName, newDesc, newRarity, newQuantity, newQuantityToLevel, newPrice, 1, newIcon, newType, generatorObject);
            rarityIndex += 0.5f;
            availableManagers.Add(newManager);
        }
    }
}

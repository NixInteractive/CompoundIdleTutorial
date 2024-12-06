using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour
{
    [SerializeField] private Transform managerHolder;
    [SerializeField] private GameObject managerPrefab;

    public ManagerData[] availableManagers;
    public List<Manager> activeManagers;

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
        int rand = Random.Range(0, activeManagers.Count);

        activeManagers[rand].quantity++;
        RefreshManagers();
    }
}

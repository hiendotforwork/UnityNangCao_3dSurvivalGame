using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public List<string> inventoryItemList = new List<string>();
    
    //Category buttons
    Button toolsBTN;
    
    //Craft buttons
    Button craftAxeBTN;
    
    //Requirement Text
    TMP_Text axeReq1, axeReq2;

    public bool isOpen;
    
    //All bluesprint
    private Blueprint AxeBP=new Blueprint("Axe","Stone","Stick", 3, 3, 2);
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        
        //Axe
        axeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TMP_Text>();
        axeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TMP_Text>();
        
        craftAxeBTN=toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate{CaftAnyItem(AxeBP);});
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isOpen = !isOpen;
            craftingScreenUI.SetActive(isOpen);
            toolsScreenUI.SetActive(false);

            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }


    void CaftAnyItem(Blueprint blueprintToCraft)
    {
        //add item to inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.ItemName);
        //remove resources from inventory
        if (blueprintToCraft.numOfReq == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1Amount);
        }
        else if(blueprintToCraft.numOfReq == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1Amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2Amount);
        }
        
        //refesh screen
        StartCoroutine(calculate());
        
        RefeshNeededItems();
    }

    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);
        InventorySystem.Instance.ReCalculateList();
    }
    public void RefeshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;
        inventoryItemList = InventorySystem.Instance.itemList;
        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1; 
                    break;
            }
        }
        // --- AXE ---
        axeReq1.text = "3 Stone [" + stone_count + "]";
        axeReq2.text = "3 Stick [" + stick_count + "]";
    
        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }
    }
}

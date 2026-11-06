using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
 
    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquid;
    public bool isOpen;
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;
    // Biến này dùng để theo dõi tiến trình ẩn Pop-up
    private Coroutine hidePopupCoroutine;
    //public bool isFull;
    private void Awake()
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
    
    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
 
            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            isOpen = false;
            if (!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void ReCalculateList()
    {
        itemList.Clear();
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string result = name.Replace("(Clone)", "");
                itemList.Add(result);
            }
        }
    }

    public void AddToInventory(string itemName)
    {
        whatSlotToEquid = FindNextEmtySlot();
        itemToAdd=Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquid.transform.position, whatSlotToEquid.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquid.transform);
        //itemList.Add(itemName);
        ReCalculateList();
        CraftingSystem.Instance.RefeshNeededItems();
        TriggerPickupPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }

        if (counter == slotList.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject FindNextEmtySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter=amountToRemove;
        for (var i =slotList.Count -1; i == 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }
        ReCalculateList();
        CraftingSystem.Instance.RefeshNeededItems();
    }
    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        // Cập nhật thông tin UI
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;

        // Bước quan trọng: Nếu đang có một tiến trình đếm ngược ẩn Pop-up chạy dở, hãy dừng nó lại
        if (hidePopupCoroutine != null)
        {
            StopCoroutine(hidePopupCoroutine);
        }

        // Bắt đầu một tiến trình đếm ngược 2 giây mới và gán nó vào biến quản lý
        hidePopupCoroutine = StartCoroutine(HidePopupAfterSeconds(2f));
    }
    private IEnumerator HidePopupAfterSeconds(float seconds)
    {
        // Đợi số giây truyền vào (ở đây là 2 giây)
        yield return new WaitForSeconds(seconds);

        // Sau khi đợi xong, ẩn Pop-up đi
        pickupAlert.SetActive(false);
    }
}

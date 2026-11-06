using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    // Dữ liệu Máu
    public float currentHealth;
    public float maxHealth;

    // Dữ liệu Năng lượng
    public float currentCalories;
    public float maxCalories;

    // Dữ liệu Nước
    public float currentHydrationPercent;
    public float maxHydrationPercent;

    public GameObject playerBody;
    public bool isHydrationActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;
        StartCoroutine(DecreaseHydration()); // Bắt đầu giảm Hydration
    }

    void Update()
    {
        // Testing the health bar
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentCalories -= 10;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentHydrationPercent -= 10;
        }
    }

    public IEnumerator DecreaseHydration()
    {
        while (isHydrationActive)
        {
            currentHydrationPercent -= 1;
            yield return new WaitForSeconds(2f); // Đợi 2 giây rồi mới trừ tiếp
        }
    }
}
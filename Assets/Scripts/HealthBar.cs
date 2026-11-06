using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI healthCounter;
    public GameObject playerState; // Kéo PlayerStateSystem vào đây

    private float currentHealth, maxHealth;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        // Lấy dữ liệu từ PlayerState
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        // Cập nhật thanh Slider (Giá trị từ 0 đến 1)
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;

        // Cập nhật chữ
        healthCounter.text = currentHealth + "/" + maxHealth;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CaloriesBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI caloriesCounter;
    public GameObject playerState; // Kéo PlayerStateSystem vào đây

    private float currentCalories, maxCalories;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        // Lấy dữ liệu từ PlayerState
        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;

        // Cập nhật thanh Slider (Giá trị từ 0 đến 1)
        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;

        // Cập nhật chữ
        caloriesCounter.text = currentCalories + "/" + maxCalories;
    }
}

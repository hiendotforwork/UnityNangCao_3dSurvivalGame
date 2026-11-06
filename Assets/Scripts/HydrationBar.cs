using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI hydrationCounter;
    public GameObject playerState; // Kéo PlayerStateSystem vào đây

    private float currentHydrationPercent, maxHydrationPercent;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        // Lấy dữ liệu từ PlayerState
        currentHydrationPercent = playerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydrationPercent = playerState.GetComponent<PlayerState>().maxHydrationPercent;
        // Cập nhật thanh Slider (Giá trị từ 0 đến 1)
        slider.value = currentHydrationPercent / maxHydrationPercent;

        // Cập nhật chữ
        hydrationCounter.text = currentHydrationPercent + "%";
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
public class TestDrag : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("CLICKED: " + gameObject.name);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class MotsSelection : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public GameObject highlightImage;

    private bool isSelected = false;

    void Start()
    {
        highlightImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
            highlightImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
            highlightImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = true;
        highlightImage.SetActive(true);

       MotsSelectionsManager.instance.Select(this);
    }

    public void Deselect()
    {
        isSelected = false;
        highlightImage.SetActive(false);
    }
}

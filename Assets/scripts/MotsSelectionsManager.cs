using UnityEngine;

public class MotsSelectionsManager : MonoBehaviour
{
     public static MotsSelectionsManager instance;

    private MotsSelection currentSelected;

    void Awake()
    {
        instance = this;
    }

    public void Select(MotsSelection word)
    {
        if (currentSelected != null && currentSelected != word)
        {
            currentSelected.Deselect();
        }

        currentSelected = word;
    }
}

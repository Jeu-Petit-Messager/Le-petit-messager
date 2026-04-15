using UnityEngine;
using UnityEngine.UI;

public class XavierZoneCanetteProg : MonoBehaviour
{
    public static float canetteCollectees;

    public GameObject canette1;
    public GameObject canette2;
    public GameObject canette3;
    public GameObject canette4;
    public GameObject canette5;
    public GameObject canette6;

    public GameObject flecheBlanche;

    public void Update()
    {
        if(!canette1.activeSelf)
        {
            if (canetteCollectees == 1)
            {
                canette1.SetActive(true);
            }
        }
        if (!canette2.activeSelf)
        {
            if (canetteCollectees == 2)
            {
                canette2.SetActive(true);
            }
        }
        if (!canette3.activeSelf)
        {
            if (canetteCollectees == 3)
            {
                canette3.SetActive(true);
            }
        }
        if (!canette4.activeSelf)
        {
            if (canetteCollectees == 4)
            {
                canette4.SetActive(true);
            }
        }
        if (!canette5.activeSelf)
        {
            if (canetteCollectees == 5)
            {
                canette5.SetActive(true);
            }
        }
        if (!canette6.activeSelf)
        {
            if (canetteCollectees == 6)
            {
                canette6.SetActive(true);
                flecheBlanche.SetActive(false);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Pour des elements de UI 3D qui doivent toujours faire face la camera */
public class PourUILookAt : MonoBehaviour
{
    public Transform cible;

    // Update is called once per frame
    void Update()
    {
        /* Regarder la cible lorsque active */
        if (cible.gameObject.activeSelf)
        {
            transform.LookAt(cible);
        }
    }
}

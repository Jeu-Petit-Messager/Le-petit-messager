using UnityEngine;
using UnityEngine.Rendering;

public class TransitionNuit : MonoBehaviour
{
    public Volume globalVolume;

    // toutes les lumières des lampadaires
    public Light[] lampadaires;

    public float tempsAvantNuit = 120f; // 2 minutes
    public float dureeTransition = 120f; // 2 minutes transition

    private float temps = 0f;
    private bool transitionCommence = false;

    void Start()
    {
        globalVolume.weight = 0f;

        // début lampadaires éteints
        foreach (Light lampe in lampadaires)
        {
            lampe.intensity = 0f;
        }
    }

    void Update()
    {
        temps += Time.deltaTime;

        // commencer après 2 min
        if (temps >= tempsAvantNuit)
        {
            transitionCommence = true;
        }

        // transition progressive
        if (transitionCommence)
        {
            // effets nuit
            globalVolume.weight += Time.deltaTime / dureeTransition;

            globalVolume.weight = Mathf.Clamp01(globalVolume.weight);

            // quand la transition est FINIE
            if (globalVolume.weight >= 1f)
            {
                // allumer lampadaires
                foreach (Light lampe in lampadaires)
                {
                    lampe.intensity = Mathf.Lerp(
                        lampe.intensity,
                        100f, // Intensité finale
                        Time.deltaTime * 0.5f
                    );
                }
            }
        }
    }
}
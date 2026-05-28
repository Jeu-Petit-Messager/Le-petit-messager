using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionNuit : MonoBehaviour
{
    public Volume globalVolume;
    // toutes les lumières des lampadaires
    public Light[] lampadaires;

    public float tempsAvantNuit = 480f; // 8 minutes
    public float dureeTransition = 200f; // 3.2 minutes transition

    public Animator fadeAnimator;

    private float temps = 0f;
    private bool transitionCommence = false;


    void Start()
    {
        globalVolume.weight = 0f;

        if(lampadaires!=null)
        {
            // début lampadaires éteints
            foreach (Light lampe in lampadaires)
            {
                lampe.intensity = 0f;
            }
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
        if (transitionCommence && globalVolume != null)
        {
            // effets nuit
            globalVolume.weight += Time.deltaTime / dureeTransition;

            globalVolume.weight = Mathf.Clamp01(globalVolume.weight);

            // quand la transition est FINIE
            if (globalVolume.weight >= 1f)
            {
                if(lampadaires != null)
                {
                    // allumer lampadaires
                    foreach (Light lampe in lampadaires)
                    {
                        lampe.intensity = Mathf.Lerp(
                            lampe.intensity,
                            30f, // Intensité finale
                            Time.deltaTime * 0.5f
                        );
                    }
                }

            }
        }
    }
}
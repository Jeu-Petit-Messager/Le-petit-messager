using UnityEngine;
using UnityEngine.SceneManagement;

public class DesactiveCameraIntro : MonoBehaviour
{
    public static DesactiveCameraIntro instance;

    public Camera introCamera;
    public Camera jeuCamera;

    void Awake()
    {
        instance = this;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton de phase de test regroupant les informations de la planète et de la camera du joueur.

public class GameManager : MonoBehaviour {

    public static GameManager GameInstance { get; private set; }

    public PlaneteBehaviour pi;
    public CameraManager ci;
    public Inputs inputs;

    [Space]
    [Header("Loads Mouips")]
    public MouipBehaviour mouipBasic;
    public MouipBehaviour mouipSworder;
    public MouipBehaviour mouipGunner;
    public MouipBehaviour mouipTank;

    private void Awake()
    {
        if(GameInstance == null)
        {
            GameInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Security360OrNegativeCAPRotation()
    {
        if(ci.AngleDeVue > 360.0f)
        {
            ci.AngleDeVue = 0.0f;
        }
        if (ci.AngleDeVue < 0.0f)
        {
            ci.AngleDeVue = 360.0f;
        }
    }
}

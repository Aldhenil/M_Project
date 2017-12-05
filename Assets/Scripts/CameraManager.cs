using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{

    public float AngleDeVue = 0f;
    [Range(3.0f, 5.0f)]
    public float zoomSize = 0f;
    public float zoomSpeed = .5f;
    public GameObject target;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.GameInstance;
    }

    private void Update()
    {
        if(target !=null)
        {
            if (target.GetComponents<IMouip>() != null) // Si c'est un mouip.
            {
                // Se fixe sur l'angle de l'unité.
                AngleDeVue = target.GetComponent<MouipBehaviour>().GetEntityAngle;
            }
        }

        // Set Camera Position
        SetCameraPosition();

        // Empèche les valeurs au dessus de 360° et en dessous de 0°.
        GameManager.GameInstance.Security360OrNegativeCAPRotation();

    }


    private void SetCameraPosition()
    {
        transform.position = new Vector3(
            transform.position.x, 
            gm.pi.radiusLimitHeigth - (gm.pi.radiusBattleLand/2.0f), 
            transform.position.z);
        //GetComponent<Camera>().orthographicSize = (gm.pi.radiusBattleLand*0.5f)+zoomSize;
    }


}

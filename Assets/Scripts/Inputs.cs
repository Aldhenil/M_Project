using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

    private GameManager gm;
    private Camera playerCamera;
    public float sensibility = 0.1f;

    // Paramètre TOUCH
    private Vector2 startPosition;
    private Vector2 endPosition;

    private void Start()
    {
        gm = GameManager.GameInstance;
        playerCamera = gm.ci.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_STANDALONE_WIN
        WindowsInputs();
#endif

#if UNITY_ANDROID
            AndroidInputs();
#endif
            
    }

    private void WindowsInputs()
    {
        // PC INPUT
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gm.ci.target == null) gm.ci.AngleDeVue -= Time.deltaTime * sensibility;
            else gm.ci.target = null;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gm.ci.target == null) gm.ci.AngleDeVue += Time.deltaTime * sensibility;
            else gm.ci.target = null;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            gm.pi.AddMouip(MOUIP_TYPE.BASIC);
        }

    }

    private void AndroidInputs()
    {
        // TOUCH INPUT
        // Si le joueur pose 1 doigt
        // Tourner autour de la planète
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            gm.ci.AngleDeVue += (-touchDeltaPosition.x * (sensibility / 100));
        }

        //Spawn Mouip quand on appuie sur l'écran
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            gm.pi.AddMouip(0);
        }

        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Pinch Zoom Camera
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDelatMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDelatMag - touchDeltaMag;

            playerCamera.orthographicSize += deltaMagnitudeDiff * gm.ci.zoomSpeed;
            playerCamera.orthographicSize = Mathf.Max(playerCamera.orthographicSize, .1f);
        }

    }

}

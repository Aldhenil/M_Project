using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{

}

public class BuildingBehaviour : EntitiyBehaviour {

    // [Header("Building Base")]


    // Use this for initialization
    new protected void Start () {
        base.Start();

        EntityHeight = gm.pi.radiusLimitHeigth;
        UpdateCircularPosition();


        // Aller chercher les données du bâtiments dans la BDD. 

    }

    // Update is called once per frame
    new protected void Update () {
        base.Update();

    }
}

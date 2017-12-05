using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class PlaneteBehaviour : MonoBehaviour {

    private GameManager gm;
    public Sprite displaySprite;

    [Range(10.0f, 100.0f)]
    public float radiusLimitHeigth = 0f;
    [Range(1.0f, 5.0f)]
    public float radiusBattleLand = 1.5f;
    public float radiusLimitDown = 0f;

    // La liste des Mouips présent sur la planète.
    public List<EntitiyBehaviour> entitiesList = new List<EntitiyBehaviour>();

    private void Start()
    {
        gm = GameManager.GameInstance;

        // Dessine la planète avec le sprite en référence.
        GetComponent<SpriteRenderer>().sprite = displaySprite;
        radiusLimitDown = radiusLimitHeigth - radiusBattleLand;

        // Récupèration des Mouips déjà présent en jeu
        //  On check quand même si il n'éxiste pas de Mouip présent sur la planète au lancement.
        //  Si c'est le cas on les ajoutes à la liste.
        MouipBehaviour[] mouipsAlreadyExisted = FindObjectsOfType(typeof(MouipBehaviour)) as MouipBehaviour[];
        foreach (MouipBehaviour mouipReader in mouipsAlreadyExisted)
        {
            gm.pi.entitiesList.Add(mouipReader);
        }
    }

    private void Update()
    {
        // Rotation de la planète en fonction de l'angle de vue de la camera (Cette dernière ne bouge pas).
        transform.localEulerAngles = new Vector3(0, 0, gm.ci.AngleDeVue);
    }

    #region PushMouip Method
    // Adding Mouip to the planet by spawner building
    public void AddMouip(SpawnerBehaviour _spawnerData)
    {
        switch (_spawnerData.MouipToSpawn)
        {
            case (MOUIP_TYPE.BASIC):
                MouipBehaviour newMouip = Instantiate(gm.mouipBasic, transform);
                newMouip.EntityHeight = _spawnerData.EntityHeight;
                newMouip.SetEntityAngle = _spawnerData.GetEntityAngle;
                // newMouip.speed = 1.0f;
                newMouip.GetComponent<SpriteRenderer>().flipX = _spawnerData.GetComponent<SpriteRenderer>().flipX;
                entitiesList.Add(newMouip);
                break;
        }
    }

    // Add specific Mouip to the planet on the segment target by the camera.
    public void AddMouip(MOUIP_TYPE _mouipType)
    {
        //GuiTextDebug.debug("Ajout d'un [" + entitiesList.Count + "] Mouip au jeu.");

        switch (_mouipType)
        {
            case (MOUIP_TYPE.BASIC):
                MouipBehaviour newMouip = Instantiate(gm.mouipBasic, transform);
                newMouip.EntityHeight = Random.Range(radiusLimitDown, radiusLimitHeigth);
                newMouip.SetEntityAngle = gm.ci.AngleDeVue;
                float tempRand = Random.Range(0, 2);
                newMouip.Sr.flipX = tempRand == 0 ? true : false;
                entitiesList.Add(newMouip);
                break;
        }
    }

    // Add Mouip of the type of you would to the game in the team selected.
    public void AddMouip(uint _team, uint _mouipType)
    {

    }
    #endregion

    #region PopMouip Method
    // Delete all Mouips in on the planet.
    public void PopMouip()
    {
        GuiTextDebug.debug("Suppression de tous les Mouips (" + entitiesList.Count.ToString() + ") de la planète.");

        /*
        while (entitiesList.Count > 0)
        {
            GameObject tempRef = entitiesList[0].gameObject;
            entitiesList.Remove(entitiesList[0]);
            Destroy(tempRef);
        }
        */
        
        // Nouvelle version pour ne cibler que les Mouips
        for(int i = 0; i < entitiesList.Count; i++)
        {
            EntitiyBehaviour mouipReader = entitiesList[i];
            if (mouipReader.GetComponent<IMouip>() != null)
            {
                entitiesList.Remove(mouipReader);
                Destroy(mouipReader);
            }

        }
    }
    #endregion

    // Déssine l'interface de débug de la planète.
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        radiusLimitDown = radiusLimitHeigth - radiusBattleLand;

        UnityEditor.Handles.color = new Color(0.0f, 0.3f, 0.0f, 0.2f); ;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.back, transform.right, 360.0f, radiusLimitHeigth);

        UnityEditor.Handles.color = new Color(0.3f, 0.0f, 0.0f, 0.35f); ;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.back, transform.right, 360.0f, radiusLimitDown);

        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, radiusLimitHeigth);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, radiusLimitDown);

        Debug.DrawLine( new Vector3(transform.position.x, transform.position.y + radiusLimitDown, transform.position.z), 
                        new Vector3(transform.position.x, transform.position.y + radiusLimitHeigth, transform.position.z));

        // Affichage de la grille de bâtiments / spells (On affiche la même grille pour les deux layers)

    }
    #endif
}

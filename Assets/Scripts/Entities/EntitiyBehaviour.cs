using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiyBehaviour : MonoBehaviour {

    [Header("Base")]
    protected GameManager gm;
    private SpriteRenderer sr;

    protected float entityAngle = 0f;
    public float EntityHeight = 0f;

    public SpriteRenderer Sr
    {
        get
        {
            return sr;
        }

        set
        {
            sr = value;
        }
    }

    public float GetEntityAngle
    {
        get
        {
            return Mathf.Abs(entityAngle) % 360.0f;
        }
    }

    public float SetEntityAngle
    {
        set
        {
            entityAngle = Mathf.Abs(value) % 360.0f;
        }
    }

    protected void Awake()
    {
        Sr = GetComponent<SpriteRenderer>();
        gm = GameManager.GameInstance;
    }

    // Use this for initialization
    protected void Start () {
        SetOrderInLayer();

    }

    public void UpdateCircularPosition()
    {
        // Position Trigonométrique circulaire des unités.
        transform.position = new Vector3(
            /*X*/ gm.pi.transform.position.x + (Mathf.Cos((-(gm.ci.AngleDeVue + 90.0f /*Correction de placement*/) / 180 * Mathf.PI) + (GetEntityAngle / 180 * Mathf.PI))) * (EntityHeight),
            /*Y*/ gm.pi.transform.position.y - (Mathf.Sin((-(gm.ci.AngleDeVue + 90.0f /*Correction de placement*/) / 180 * Mathf.PI) + (GetEntityAngle / 180 * Mathf.PI))) * (EntityHeight),
            /*Z*/ transform.position.z);

        // Correction de sens de rotation.
        transform.localEulerAngles = new Vector3(0, 0, -(GetEntityAngle));
    }

    // Trie des entités.
    // On parcours chaque entité pour connaitre leur hauteur par rapport au centre de la planète.
    // L'entité la plus basse débute sur le layer 1, puis, crechendo, les autres prennent leur place sur la 2, la 3 ...
    // Devrait être appelé lorsqu'un Mouip change de hauteur, et lorsqu'on add un Mouip à la planète. (Event sur écoute) Car bouffe beaucoup de process.
    protected void SetOrderInLayer()
    {
        // Sorting
        if (gm.pi.entitiesList.Count > 0)
        {
            gm.pi.entitiesList.Sort(delegate (EntitiyBehaviour a, EntitiyBehaviour b) {
                return (a.EntityHeight).CompareTo(b.EntityHeight);
            });
        }

        for (int i = 0; i < gm.pi.entitiesList.Count; i++)
        {
            gm.pi.entitiesList[i].sr.sortingOrder = gm.pi.entitiesList.Count - i;
        }
    }

}

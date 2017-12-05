using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : BuildingBehaviour {

    [Header("Spawner Data")]
    [SerializeField] MOUIP_TYPE mouipToSpawn;
    [SerializeField] float spawnSpeed;  // 1 = 1 Mouip / Seconde


    public MOUIP_TYPE MouipToSpawn
    {
        get
        {
            return mouipToSpawn;
        }

        set
        {
            mouipToSpawn = value;
        }
    }

    // Use this for initialization
    new void Start () {
        base.Start();

        InvokeRepeating("SpawningMouip", spawnSpeed, spawnSpeed);
    }

    // Update is called once per frame
    new void Update () {
        base.Update();


    }

    private void SpawningMouip()
    {
        gm.pi.AddMouip(this);
    }
}

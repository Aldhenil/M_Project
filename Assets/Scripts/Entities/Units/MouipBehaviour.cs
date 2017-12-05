using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Collections.Generic;

// Formule de position circulaire :
// Axe X de l'entité = Planète.Pos.X + cos(AngleDeVue + -(Entité.Angle) / 180 * PI) * Planète.Radius
// Axe Y de l'entité = Planète.Pos.Y + sin(AngleDeVue + -(Entité.Angle) / 180 * PI) * Planète.Radius - Entité.DécalageY [Simule la hauteur]
// Formule de rotation circulaire :
// sfSprite_setRotation(spriteTemp, (game->tabPlayers[i]->fAngleView + -(mouip_reader->fAngle))); // Le + 90 est plus tourner le Mouip face au centre de la planete.

public interface IMouip
{
    
}

public enum MOUIP_TYPE
{
    BASIC, SWORDER, GUNNER, TANK
}

public class MouipBehaviour : EntitiyBehaviour {

    [Header("Mouip Base")]
    // Singleton pour phase de test.
    public float speed = 1f;

    new protected void Start()
    {
        base.Start();
        //base.SetOrderInLayer();

        InvokeRepeating("Security360OrNegativeCAPRotationMouip", 60.0f, 60.0f);
        InvokeRepeating("SecurityBattleLandChecker", 1.0f, 1.0f);
    }

    new protected void Update () {
        base.Update();

        UpdateCircularPosition();

        // Déplacement de l'entité, en fonction de son orientation horizontal, et proportionnel à la superficie de la planète.
        EntityAngle += !Sr.flipX ? Time.deltaTime * ((speed/gm.pi.radiusLimitHeigth)/2.0f)*100.0f : Time.deltaTime * -(((speed / gm.pi.radiusLimitHeigth) / 2.0f) * 100.0f);

        // Check si l'entité ne dépasse pas la limite de Battle Land
        //SecurityBattleLandChecker(); // Opti : Ne lancer ce Check QUE lorsque l'entité change sa hauteur. Opti2: Ne lancer ce check QUE tout les X secondes (Coroutine).

        // Sécurité diverses
        //Security360OrNegativeCAPRotationMouip(); // Utiliser une coroutine pour ne plus checker ça à chaque image de jeu. 
        //SecurityFlipSprite(); // Obsolete (A revoir avant de delete)
        //SetOrderInLayer();
    }

    // Sécurité 0° - 360°
    /// <summary>
    /// Cette fonction de sécurité permet de SET la donnée d'angle de l'entité en un angle plus "propre", afin que cette donnée n'excède jamais une valeur monstrueuse.
    /// </summary>
    private void Security360OrNegativeCAPRotationMouip()
    {
        if (EntityAngle > 360.0f)
        {
            EntityAngle = EntityAngle % 360.0f;
        }
        if (EntityAngle < 0.0f)
        {
            EntityAngle = (-EntityAngle) % 360.0f;
        }
    }

    // Sécurité EntityHeight pour rester entre radiusBattleLand et radiusLimitHeigth de la planète
    private void SecurityBattleLandChecker()
    {
        if (EntityHeight > gm.pi.radiusLimitHeigth)
        {
            EntityHeight = gm.pi.radiusLimitHeigth;
        }
        if (EntityHeight < gm.pi.radiusLimitDown)
        {
            EntityHeight = gm.pi.radiusLimitDown;
        }
    }

    // Sécurité de Flip de Sprite suivant la vitesse de l'unité
    private void SecurityFlipSprite()
    {
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        SpriteRenderer[] allChildren = GetComponentsInChildren<SpriteRenderer>();
        if (speed < 0)
        {
            foreach (SpriteRenderer srChild in allChildren)
            {
                srChild.flipX = true;
            }
        }
        else
        {
            foreach (SpriteRenderer srChild in allChildren)
            {
                srChild.flipX = false;
            }
        }
    }

}

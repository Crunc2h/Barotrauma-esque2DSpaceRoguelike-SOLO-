using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderProjectileManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile" && !collision.gameObject.GetComponent<Projectile>().TeleportedToTarget)
        {
            collision.gameObject.GetComponent<Projectile>().TeleportedToTarget = true;
            StartCoroutine(TeleportProjectileToTargetShip(collision.gameObject));
        }
    }

    private IEnumerator TeleportProjectileToTargetShip(GameObject projectile)
    {
        var originalPosition = projectile.transform.position;
        projectile.transform.position = new Vector3(9999, 9999, projectile.transform.position.z);

        var targetMuzzleY = projectile.GetComponent<Projectile>().DefenderShip.GetComponent<CombatProperties>().GetMuzzlePosition().y;
        float targetX;
        
        if (projectile.GetComponent<Projectile>().DefenderShip.tag == "Enemy")
        {
            targetX = originalPosition.x + 30;
        }
        else
        {
            targetX = originalPosition.x - 30;
        }
       
        yield return new WaitForSeconds(1);
        
        projectile.transform.position = new Vector3(targetX, targetMuzzleY, projectile.transform.position.z);

    }
}

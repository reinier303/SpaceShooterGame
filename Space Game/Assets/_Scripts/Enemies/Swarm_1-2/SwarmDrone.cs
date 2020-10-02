using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmDrone : BaseEnemy
{
    public Stat FireRate;
    public Stat ShootDistance;
    public Stat BulletAmount;
    public Stat BulletSpread;

    private bool canFire;

    protected override void Awake()
    {
        base.Awake();
        canFire = true;
    }

    protected void Update()
    {
        Move();
        Rotate();
        Fire();
    }

    private void Fire()
    {
        if (canFire && gameManager.PlayerAlive && Vector2.Distance(Player.position, transform.position) <= ShootDistance.GetValue())
        {
            canFire = false;

            float angleIncrease;
            float spread = BulletSpread.GetValue();

            //Make sure there are no divisions by 0 in case of single projectile
            if ((int)BulletAmount.GetValue() == 1)
            {
                angleIncrease = 0;
                spread = 0;
            }
            else
            {
                angleIncrease = spread / ((int)BulletAmount.GetValue() - 1);
            }

            for (int i = 0; i < (int)BulletAmount.GetValue(); i++)
            {
                //Calculate new rotation
                float newRotation = (transform.eulerAngles.z - angleIncrease * i) + (spread / 2);

                //Spawn Projectile with extra rotation based on projectile count
                GameObject projectileObject = objectPooler.SpawnFromPool("SwarmBullet", transform.position + transform.up,
                Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newRotation));
            }
            StartCoroutine(FireCooldownTimer());
        }
    }

    private IEnumerator FireCooldownTimer()
    {
        yield return new WaitForSeconds(1 / FireRate.GetValue());
        canFire = true;
    }
}

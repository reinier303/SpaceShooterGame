using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(menuName = "WeaponSystem/Weapons/MineWeapon", order = 997)]
    public class MineWeapon : Weapon
    {
        public override void Fire(ObjectPooler objectPooler, Transform player)
        {
            GameObject Mine = objectPooler.SpawnFromPool("PlayerMine", player.position - (player.up * 0.75f), Quaternion.identity);
            Mine.GetComponent<Rigidbody2D>().AddForce(-player.up * 150);
        }
    }
}

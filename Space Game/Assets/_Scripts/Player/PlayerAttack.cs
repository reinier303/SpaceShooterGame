using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //TEMP: Gamemanager delegates 
    public ObjectPooler RObjectPooler;

    private bool canFire;
    public float fireCooldown;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        canFire = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //TEMP: Get input from inputManager
        if(Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if(canFire)
        {
            RObjectPooler.SpawnFromPool("Bullet", transform.position, transform.rotation);
            canFire = false;
            StartCoroutine(FireCooldownTimer());
        }
    }

    private IEnumerator FireCooldownTimer()
    {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }
}

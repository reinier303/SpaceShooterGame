using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmling : BaseEnemy
{
    protected void Update()
    {
        Move();
        Rotate();
    }
}

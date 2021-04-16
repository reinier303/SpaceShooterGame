using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceGame
{
    public class Swarmling : BaseEnemy
    {
        protected void Update()
        {
            Move();
            Rotate();
        }
    }
}
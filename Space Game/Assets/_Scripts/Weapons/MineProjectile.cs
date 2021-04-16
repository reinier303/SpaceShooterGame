using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class MineProjectile : PlayerProjectile
    {
        public override void Initialize()
        {
            float value = Modules["Size"].GetStatValue();
            transform.localScale = new Vector2(value, value);
        }

        protected override void Move()
        {
            //This method is meant to override.
        }
    }
}

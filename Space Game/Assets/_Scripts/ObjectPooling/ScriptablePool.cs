using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(menuName = "ObjectPool/Pool", order = 999)]
    public class ScriptablePool : ScriptableObject
    {
        public string Tag;
        public GameObject Prefab;
        public int Amount;
    }
}
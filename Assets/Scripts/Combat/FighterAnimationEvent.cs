using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class FighterAnimationEvent : MonoBehaviour
    {
        Fighter fighter;
        void Start()
        {
            fighter = transform.parent.GetComponent<Fighter>();
        }
        //Animation event for actually hitting an enemy
        void Hit()
        {
            fighter.Hit();
        }
    }

}
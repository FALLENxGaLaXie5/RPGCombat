using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;
        Health targetHealth;
        float timeSinceLastAttack = 0;
        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;


            if (target != null && !GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                //cancel movement action
                mover.Cancel();
                //set attack behavior to play
                AttackBehavior();
            }
        }

        void AttackBehavior()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event as well
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
            targetHealth = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;
        }

        //Animation event for actually hitting an enemy
        public void Hit()
        {
            targetHealth.TakeDamage(weaponDamage);
            print("I played the animation event on the attack animation! Kill 'em!");
        }                              
    }
}
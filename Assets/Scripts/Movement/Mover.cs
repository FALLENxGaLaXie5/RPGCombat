using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using RPG.Core;
using System;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        IAstarAI ai;
        Animator animator;
        ActionScheduler actionScheduler;

        Ray lastRay;

        // Start is called before the first frame update
        void Start()
        {
            ai = GetComponent<IAstarAI>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            ai.destination = destination;
            ai.isStopped = false;
        }

        public void Cancel()
        {
            ai.isStopped = true;
        }

        void UpdateAnimator()
        {
            Vector3 velocity = ai.desiredVelocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}

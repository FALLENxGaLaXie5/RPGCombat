using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Combat;
using UnityEngine;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover playerMovement;
        Fighter playerFighter;
        // Start is called before the first frame update
        void Start()
        {
            playerMovement = GetComponent<Mover>();
            playerFighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (ProcessCombat()) return;
            if (ProcessMovement()) return;
        }
        
        bool ProcessMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    playerMovement.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        bool ProcessCombat()
        {            
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (Input.GetMouseButtonDown(0))
                {
                    playerFighter.Attack(target);
                }
                return true;
            }                            
            return false;
        }

        static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
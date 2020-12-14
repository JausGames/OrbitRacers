using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;

namespace Inputs
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        //[SerializeField] PlayerCombat combat;
        [SerializeField] PlayerController controller = null;
        [SerializeField] private Vector2 move;
        [SerializeField] private Vector2 look;
        [SerializeField] private float index;
        
        private Controls controls;
        private Controls Controls
        {
        get
            {
                if (controls != null) { return controls; }
                return controls = new Controls();
            }
        }
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void FindPlayer()
        {
            index = playerInput.playerIndex;
            var controllers = FindObjectsOfType<PlayerController>();
            controller = controllers.FirstOrDefault(m => m.GetPlayerIndex() == index);

            /*var combats = FindObjectsOfType<PlayerCombat>();
            combat = combats.FirstOrDefault(m => m.GetPlayerIndex() == index);*/
        }
        public void OnMove(CallbackContext context)
        {
            if (controller == null) return;
            var move = context.ReadValue<Vector2>();
            Debug.Log("Move magnitude = " + move.magnitude);
            controller.SetMove(move);
        }
        public void OnLook(CallbackContext context)
        {
            /*if (controller == null) return;
            var look = context.ReadValue<Vector2>();*/
        }
        public void OnAttack(CallbackContext context)
        {
            /*if (combat == null) return;
            var canc = context.canceled;
            var perf = context.performed;
            combat.Attack(!canc, perf);*/
        }
        public void OnDash(CallbackContext context)
        {
            if (controller == null) return;
            var canc = context.canceled;
            var perf = context.performed;
            controller.Dash(perf, canc);
        }
        public void OnJump(CallbackContext context)
        {
            /*if (combat == null) return;
            var canc = context.canceled;
            var perf = context.performed;
            controller.Jump(!canc, perf);*/
        }
    }
}

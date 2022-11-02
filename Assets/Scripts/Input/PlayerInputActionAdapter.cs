using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CyberCripple.Input
{
    public class PlayerInputActionAdapter : MonoBehaviour
    {
        public void Run(InputAction.CallbackContext callbackContext)
        {
            Debug.Log($"Run: {callbackContext.ReadValue<Vector2>()}");
        }

        public void Jump(InputAction.CallbackContext callbackContext)
        {
            Debug.Log($"Jump: {callbackContext.performed}");
        }

        public void Lost(PlayerInput input)
        {
            Console.WriteLine(input.playerIndex);
        }
    }
}
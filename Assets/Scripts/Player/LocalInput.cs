using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class LocalInput : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _steerAction;

        [SerializeField]
        private InputActionReference _aimAction;

        public Vector2 SteerInput { get; private set; }
        public Vector2 AimInput { get; private set; }

        private Vector2 lastMousePosition;

        private void Awake()
        {
            _steerAction.action.actionMap.Enable();
        }

        private void Start()
        {
            lastMousePosition = Mouse.current.position.ReadValue();
        }

        private void Update()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var stickPosition = _aimAction.action.ReadValue<Vector2>();
            if (mousePosition != lastMousePosition)
            {
                // Kinda inaccurate, for better results use a camera to raycast from the mouse position, etc.
                AimInput = new Vector2(mousePosition.x - Screen.width / 2f, mousePosition.y - Screen.height / 2f);
                lastMousePosition = mousePosition;
            }
            else if (stickPosition != Vector2.zero)
            {
                AimInput = stickPosition;
            }

            SteerInput = _steerAction.action.ReadValue<Vector2>();

        }
    }
}

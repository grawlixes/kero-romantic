using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

namespace KinematicCharacterController.Examples
{
    public class ExamplePlayer : MonoBehaviour
    {
        public ExampleCharacterController Character;
        public ExampleCharacterCamera CharacterCamera;
        public bool canJump = true;
        public bool freeCamera = true;
 
        private bool canInput = false;
        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            CharacterCamera.SetFollowTransform(Character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            CharacterCamera.IgnoredColliders.Clear();
            CharacterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (canInput)
                HandleCharacterInput();
        }

        private void LateUpdate()
        {
            if (freeCamera)
            {
                // Handle rotating the camera along with physics movers
                if (CharacterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null)
                {
                    CharacterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
                    CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
                }

                HandleCameraInput();

            }
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
            float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            CharacterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            /*
            // Handle toggling zoom level
            if (Input.GetMouseButtonDown(1))
            {
                CharacterCamera.TargetDistance = (CharacterCamera.TargetDistance == 0f) ? CharacterCamera.DefaultDistance : 0f;
            }
            */
        }

        public void MoveForward()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();
            characterInputs.MoveAxisForward = 1;
            characterInputs.miniJump = true;

            Character.SetInputs(ref characterInputs);

            PlayerCharacterInputs none = new PlayerCharacterInputs();
            Character.SetInputs(ref none);
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
            if (canJump)
                characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            // The frog does a "mini-jump" whenever he's grounded and we're moving him forward.
            // Actual jump takes priority, though, so he'll always do a big stationary jump if you press space.
            characterInputs.miniJump = (!characterInputs.JumpDown) && 
                                       ((characterInputs.MoveAxisForward != 0) || 
                                        (characterInputs.MoveAxisRight != 0));
            characterInputs.sprinting = Input.GetKey(KeyCode.LeftShift);
            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }

        public void ToggleInputCapability(bool capable)
        {
            canInput = capable;
        }

        public void ToggleJumpCapability()
        {
            canJump = !canJump;
        }
    }
}
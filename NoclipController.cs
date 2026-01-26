using System.Reflection;
using UnityEngine;

namespace MineMogulModMenu {
    public class NoclipController : MonoBehaviour {
        public bool NoclipActive { get; private set; }
        public float Speed { get { return Config.Instance.Player.NoclipSpeed; }}
        private float oldWalkSpeed = 0f;
        private float oldSprintSpeed = 0f;
        private float oldDuckSpeed = 0f;
        private float oldGravity = 0f;
        private void ResetOldValues() {
            if (oldDuckSpeed != 0f && oldSprintSpeed != 0f && oldWalkSpeed != 0f) {
                GameUtilities.LocalPlayerController.WalkSpeed = oldWalkSpeed;
                oldWalkSpeed = 0f;
                GameUtilities.LocalPlayerController.DuckSpeed = oldDuckSpeed;
                oldDuckSpeed = 0f;
                GameUtilities.LocalPlayerController.SprintSpeed = oldSprintSpeed;
                oldSprintSpeed = 0f;
                GameUtilities.LocalPlayerController.Gravity = oldGravity;
                oldGravity = 0f;
            }
        }
        private void CaptureOldValues() {
            if (oldDuckSpeed == 0f && oldSprintSpeed == 0f && oldWalkSpeed == 0f && oldGravity == 0f) {
                oldWalkSpeed = GameUtilities.LocalPlayerController.WalkSpeed;
                oldSprintSpeed = GameUtilities.LocalPlayerController.SprintSpeed;
                oldDuckSpeed = GameUtilities.LocalPlayerController.DuckSpeed;
                oldGravity = GameUtilities.LocalPlayerController.Gravity;
            }
        }
        private void FreezePlayerMovement() {
            GameUtilities.LocalPlayerController.DuckSpeed = 0f;
            GameUtilities.LocalPlayerController.SprintSpeed = 0f;
            GameUtilities.LocalPlayerController.WalkSpeed = 0f;
            GameUtilities.LocalPlayerController.Gravity = 0f;
        }
        private void DoNoclip() {
            Transform characterTransform = GameUtilities.LocalPlayerController.transform;
            Transform cameraTransform = GameUtilities.LocalPlayerController.PlayerCamera.transform;
            if (!cameraTransform || !characterTransform) return;

            var playerVelocity = typeof(PlayerController).GetField("_velocity", BindingFlags.NonPublic | BindingFlags.Instance);
            playerVelocity?.SetValue(GameUtilities.LocalPlayerController, Vector3.zero);

            Vector3 noclipDirection = Vector3.zero;
            
            if (Input.GetKey(KeyCode.W)) {
                noclipDirection += cameraTransform.forward;
            }
            if (Input.GetKey(KeyCode.A)) {
                noclipDirection -= cameraTransform.right;
            }
            if (Input.GetKey(KeyCode.S)) {
                noclipDirection -= cameraTransform.forward;
            }
            if (Input.GetKey(KeyCode.D)) {
                noclipDirection += cameraTransform.right;
            }
            if (Input.GetKey(KeyCode.Space)) {
                noclipDirection += characterTransform.up;
            }
            if (Input.GetKey(KeyCode.C)) {
                noclipDirection -= characterTransform.up;
            }
            if (Input.GetKey(KeyCode.LeftShift)) {
                noclipDirection *= 2;
            }
            if (Input.GetKey(KeyCode.LeftControl)) {
                noclipDirection /= 2;
            }

            characterTransform.position += noclipDirection * Speed * Time.deltaTime;
        }
        private void ActivateNoclip() {
            NoclipActive = true;
            CaptureOldValues();
            FreezePlayerMovement();
        }
        private void DisableNoclip() {
            NoclipActive = false;
            ResetOldValues();
        }
        // run after CharacterController to prevent overwrites
        private void LateUpdate() {
            if (Input.GetKeyDown(KeyCode.V) && Config.Instance.Player.Noclip) {
                NoclipActive = !NoclipActive;
                if (!NoclipActive) 
                    DisableNoclip();
                else 
                    ActivateNoclip();
            }
            
            if (!Config.Instance.Player.Noclip) {
                DisableNoclip();
            }

            if (NoclipActive)
                DoNoclip();
        }
    }
}
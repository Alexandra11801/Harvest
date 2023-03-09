using System.Collections;
using Harvest.PlayerControl;
using Harvest.UI;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Harvest.InputManagement
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private JoystickRing joystick;
        [SerializeField] private float directionChangingThreshold;
        private PlayerControls controls;

        private void Awake()
        {
            controls = new PlayerControls();
            EnhancedTouchSupport.Enable();
            controls.Player.PointerContact.started += _ => StartTouchProcessing();
            controls.Player.PointerContact.canceled += _ => StopTouchProcessing();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void StartTouchProcessing()
        {
            StartCoroutine(nameof(ProcessTouch));
        }

        private void StopTouchProcessing()
        {
            StopCoroutine(nameof(ProcessTouch));
            joystick.HideJoystick();
        }

        private IEnumerator ProcessTouch()
        {
#if UNITY_EDITOR
            yield return null;
#elif UNITY_IOS
            yield return new WaitUntil(() => Touch.activeTouches.Count > 0);
#elif UNITY_ANDROID
            yield return new WaitUntil(() => Touch.activeTouches.Count > 0);
#endif
            var touchStartPosition = controls.Player.PointerPosition.ReadValue<Vector2>();
            joystick.ShowJoystick(touchStartPosition);
            while (true)
            {
                yield return null;
                var touchPosition = controls.Player.PointerPosition.ReadValue<Vector2>();
                joystick.UpdateJoystick(touchPosition);
                var direction = touchStartPosition - touchPosition;
                if (direction.magnitude > directionChangingThreshold)
                {
                    player.WalkingDirection = new Vector3(direction.x, 0, direction.y).normalized;
                }
            }
        }
    }
}
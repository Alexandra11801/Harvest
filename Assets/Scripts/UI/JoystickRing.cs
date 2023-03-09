using UnityEngine;
using UnityEngine.UI;

namespace Harvest.UI
{
    public class JoystickRing : MonoBehaviour
    {
        [SerializeField] private Image ring;
        [SerializeField] private Image circle;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private RectTransform rect;
        [SerializeField] private float maxDistance;

        public void ShowJoystick(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, position, null, out var localPosition);
            transform.localPosition = localPosition;
            ring.enabled = true;
            circle.enabled = true;
            circle.transform.localPosition = Vector3.zero;
        }

        public void HideJoystick()
        {
            ring.enabled = false;
            circle.enabled = false;
        }

        public void UpdateJoystick(Vector2 touchPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, touchPosition, null,
                out var localTouchPosition);
            circle.transform.localPosition = Vector2.ClampMagnitude(localTouchPosition, maxDistance);
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace HumanPinball
{
    public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // Change this to according the desired precision
        [SerializeField] private float maxDistance = 100f;

        public Vector2 Input { get; private set; }

        private Vector2 startPos;
        private Vector2 delta;
        private PointerEventData eventData;

        private void Update()
        {
            if (eventData == null) return;

            delta = eventData.position - startPos;
            delta.x = Mathf.Clamp(delta.x, -maxDistance, maxDistance);
            delta.y = Mathf.Clamp(delta.y, -maxDistance, maxDistance);
            Input = delta / maxDistance;
            startPos = eventData.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.eventData = eventData;
            startPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.eventData = null;
            delta = Vector2.zero;
            startPos = Vector2.zero;
            Input = Vector2.zero;
        }
    }
}
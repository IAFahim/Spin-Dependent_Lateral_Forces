using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
    public class TriggerAfterDelay : MonoBehaviour
    {
        public UnityEvent unityEvent;
        public float duration = 1;
        private float _timePassed;

        private void Update()
        {
            if (_timePassed < duration)
            {
                _timePassed += Time.deltaTime;
                return;
            }
            
            unityEvent.Invoke();
        }
    }
}
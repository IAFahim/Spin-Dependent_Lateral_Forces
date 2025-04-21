using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class SceneSwitch : MonoBehaviour
    {
        public InputActionReference inputActionReference;
        public string sceneName;
        
        private void OnEnable()
        {
            inputActionReference.action.Enable();
            inputActionReference.action.performed += MoveOnperformed;
        }

        private void MoveOnperformed(InputAction.CallbackContext obj)
        {
            SceneManager.LoadScene(sceneName);
        }

        private void OnDisable()
        {
            inputActionReference.action.Disable();
            inputActionReference.action.performed -= MoveOnperformed;
        }
    }
}
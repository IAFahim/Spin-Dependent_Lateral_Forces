using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Runtime
{
    public class ImageSwitcher : MonoBehaviour
    {
        public InputActionReference move;

        public Image image;
        public Sprite[] images;
        public int currentImageIndex;

        private void OnEnable()
        {
            move.action.Enable();
            move.action.performed += MoveOnperformed;
        }

        private void MoveOnperformed(InputAction.CallbackContext obj)
        {
            var vector2 = obj.ReadValue<Vector2>();
            if (vector2.x < 0)
            {
                ChangeImage(-1);
            }
            else if (vector2.x > 0)
            {
                ChangeImage(1);
            }
        }

        private void ChangeImage(int direction)
        {
            currentImageIndex += direction;
            if (currentImageIndex < 0) currentImageIndex = images.Length - 1;
            else if (currentImageIndex >= images.Length) currentImageIndex = 0;

            image.sprite = images[currentImageIndex];
        }

        private void OnDestroy()
        {
            move.action.Disable();
            move.action.performed -= MoveOnperformed;
        }
    }
}
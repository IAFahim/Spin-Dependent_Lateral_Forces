using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Runtime
{
    public class MatFloatAnimate : MonoBehaviour
    {
        public GameObject gameObject;
        public Renderer renderer;
        public float target = 1;
        public float duration = 1;

        private MaterialPropertyBlock _materialPropertyBlock;
        private float _initial;
        private MotionHandle _motionHandle;

        public string floatShaderId = "_Float";
        private static int _floatId;

        private void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            _floatId = Shader.PropertyToID(floatShaderId);
            _materialPropertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(_materialPropertyBlock);
            _initial = _materialPropertyBlock.GetFloat(_floatId);
        }

        private void OnEnable()
        {
            Play();
        }

        public void Play()
        {
            _motionHandle = LMotion.Create(_initial, target, duration)
                .BindToMaterialFloat(renderer.material, _floatId);
        }


        public void Stop()
        {
            if (_motionHandle.IsActive()) _motionHandle.Complete();
        }

        private void OnDestroy()
        {
            Stop();
        }
    }
}
using System;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime
{
    public class MaterialFade1 : MonoBehaviour
    {
        public GameObjectRendererColorChange[] gameObjectRendererColorChanges;
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");


        private void Awake()
        {
            foreach (var gameObjectRendererColorChange in gameObjectRendererColorChanges)
            {
                gameObjectRendererColorChange.Setup(BaseColorId);
            }
        }

        public void OnEnable()
        {
            foreach (var gameObjectRendererColorChange in gameObjectRendererColorChanges)
            {
                gameObjectRendererColorChange.Play();
            }
        }

        private void OnDisable()
        {
            foreach (var gameObjectRendererColorChange in gameObjectRendererColorChanges)
            {
                gameObjectRendererColorChange.Stop();
            }
        }
    }

    [Serializable]
    public class GameObjectRendererColorChange
    {
        public GameObject gameObject;
        public bool disableOnEnd;
        public Renderer renderer;
        public Color targetColor;
        public float duration = 1;

        private int _nameID;
        private MaterialPropertyBlock _materialPropertyBlock;
        private Color _initialBaseColor;
        private MotionHandle _motionHandle;


        public void Setup(int nameID)
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(_materialPropertyBlock);
            _nameID = nameID;
            _initialBaseColor = _materialPropertyBlock.GetColor(nameID);
        }

        public void Play()
        {
            _motionHandle = LMotion.Create(_initialBaseColor, targetColor, duration)
                .WithOnComplete(Callback)
                .BindToMaterialColor(renderer.material, _nameID);
        }

        private void Callback()
        {
            if (disableOnEnd) gameObject.SetActive(false);
        }

        public void Stop()
        {
            if (_motionHandle.IsActive()) _motionHandle.Complete();
        }
    }
}
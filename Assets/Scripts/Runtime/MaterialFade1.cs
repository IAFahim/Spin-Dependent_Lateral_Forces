using System;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Runtime
{
    public class MaterialFade1 : MonoBehaviour
    {
        public GameObjectRendererColorChange[] gameObjectRendererColorChanges;
        public string colorShaderId = "_BaseColor";
        private static int _baseColorId;


        private void Awake()
        {
            _baseColorId = Shader.PropertyToID(colorShaderId);
            foreach (var gameObjectRendererColorChange in gameObjectRendererColorChanges)
            {
                gameObjectRendererColorChange.Setup(_baseColorId);
            }
        }

        public void OnEnable()
        {
            foreach (var gameObjectRendererColorChange in gameObjectRendererColorChanges)
            {
                gameObjectRendererColorChange.Play();
            }
        }

        private void OnDestroy()
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
                .BindToMaterialColor(renderer.material, _nameID);
        }
        

        public void Stop()
        {
            if (_motionHandle.IsActive()) _motionHandle.Complete();
        }
    }
}
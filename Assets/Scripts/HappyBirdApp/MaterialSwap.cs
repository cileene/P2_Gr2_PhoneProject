using UnityEngine;

namespace HappyBirdApp
{
    public class MaterialSwap : MonoBehaviour
    {
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material alternativeMaterial;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.sharedMaterial = normalMaterial;
        }
        
        private void Start()
        {
            if (GameManager.Instance.birdFriction)
            {
                _meshRenderer.sharedMaterial = alternativeMaterial;
            }
        }

        public void SwapMaterial()
        {
            if (_meshRenderer.sharedMaterial == normalMaterial)
            {
                _meshRenderer.sharedMaterial = alternativeMaterial;
            }
        }
    }
}
using UnityEngine;

namespace HappyBirdApp
{
    public class MaterialSwap : MonoBehaviour
    {
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material alternativeMaterial;

        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = normalMaterial;
        }

        public void SwapMaterial()
        {
            if (meshRenderer.sharedMaterial == normalMaterial)
            {
                meshRenderer.sharedMaterial = alternativeMaterial;
            }
            else
            {
                meshRenderer.sharedMaterial = normalMaterial;
            }
        }
    }
}
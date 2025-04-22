using UnityEngine;

namespace HappyBirdApp
{
    public class MaterialSwap : MonoBehaviour
    {
        [SerializeField] private Material normalMaterial;
        [SerializeField] private Material alternativeMaterial;

        public void SwapMaterial()
        {
            // Get the MeshRenderer component of the GameObject
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // Check if the current material is the normal material
            if (meshRenderer.material == normalMaterial)
            {
                // Swap to the alternative material
                meshRenderer.material = alternativeMaterial;
            }
            else
            {
                // Swap back to the normal material
                meshRenderer.material = normalMaterial;
            }
        }
    }
}
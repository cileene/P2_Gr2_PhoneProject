using UnityEngine;

namespace HappyBirdApp
{
    public class Parallax : MonoBehaviour
    {
        private MeshRenderer meshRenderer;

        public float animationSpeed = 0.06f;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            meshRenderer.sharedMaterial.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
        }
    }
}

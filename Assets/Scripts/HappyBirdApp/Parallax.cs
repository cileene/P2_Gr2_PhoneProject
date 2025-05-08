using UnityEngine;

namespace HappyBirdApp
{
    public class Parallax : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        public float animationSpeed = 0.06f;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            _meshRenderer.sharedMaterial.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
        }
    }
}

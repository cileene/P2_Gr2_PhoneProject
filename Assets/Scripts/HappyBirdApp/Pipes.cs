using UnityEngine;

namespace HappyBirdApp
{
    public class Pipes : MonoBehaviour
    {
        public Transform top;
        public Transform bottom;
        public float speed = 5f;
        public float gap = 2f;

        private float _leftEdge;

        private void Start()
        {
            if (Camera.main) _leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
            top.position += Vector3.up * gap / 2;
            bottom.position += Vector3.down * gap / 2;
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x < _leftEdge) {
                Destroy(gameObject);
            }
        }

    }
}

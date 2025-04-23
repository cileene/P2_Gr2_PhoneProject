using UnityEngine;

namespace HappyBirdApp
{
    public class Player : MonoBehaviour
    {
        public Sprite[] sprites;
        public float strength = 2f;
        public float gravity = -4.81f;  //Tyngdekraften - kan bruges til at variere sværhedsgrad
        public float tilt = 3f;

        private SpriteRenderer spriteRenderer;
        private Vector3 direction;
        private int spriteIndex;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f); // Calling and repeating a function
        }

        private void OnEnable()
        {
            Vector3 position = transform.position;
            position.y = 0f;
            transform.position = position;
            direction = Vector3.zero;
        }

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            //     direction = Vector3.up * strength;
            //  }

            //Apply gravity and update the position
            //direction.y += gravity * Time.deltaTime;
            //transform.position += direction * Time.deltaTime;

            //Tilt the bird based on the direction
            //Vector3 rotation = transform.eulerAngles;
            // rotation.z = direction.y * tilt;
            //transform.eulerAngles = rotation;

            if (Input.GetKeyDown(KeyCode.Space)) {
                //Debug.Log("Space pressed - Initial jump force applied");
                direction = Vector3.up * strength;
            }

            if (Input.GetMouseButtonDown(0)) {
                //Debug.Log("Mouse clicked - Initial jump force applied");
                direction = Vector3.up * strength;
            }

            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            Vector3 rotation = transform.eulerAngles;
            rotation.z = direction.y * tilt;
            transform.eulerAngles = rotation;
        }

        private void AnimateSprite()
        {
            spriteIndex++;

            if(spriteIndex >= sprites.Length ) {
                spriteIndex = 0;
            }

            spriteRenderer.sprite = sprites[spriteIndex];

        }

   

  
        private void OnTriggerEnter2D(Collider2D other)
        {
            FlappyBirdManager gameManager;

            if (other.gameObject.tag == "Obstacle") {
                gameManager = Object.FindFirstObjectByType<FlappyBirdManager>();
                if (gameManager != null) {
                    gameManager.GameOver();
                }
            } else if (other.gameObject.tag == "Scoring") {
                gameManager = Object.FindFirstObjectByType<FlappyBirdManager>();
                if (gameManager != null) {
                    gameManager.IncreaseScore();
                }
            }
        }

    }
}

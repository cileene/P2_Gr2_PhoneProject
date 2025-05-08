using UnityEngine;

namespace HappyBirdApp
{
    public class ShakePhone : MonoBehaviour
    {
        [Header("Shake Settings")] 
        public float shakeDetectionThreshold = 1.0f;
        public float minShakeInterval = 0.5f;
        private float sqrShakeThreshold;
        private float lastShakeTime;
        [SerializeField] private GameObject background;
        private float _animationSpeed;
        private GameManager _gm;

        private void Start()
        {
            _gm = GameManager.Instance;
            
            _animationSpeed = background.GetComponent<Parallax>().animationSpeed;
            sqrShakeThreshold = shakeDetectionThreshold * shakeDetectionThreshold;
            lastShakeTime = Time.unscaledTime;
            if (_gm.birdFriction) ToggleBirdHardMode(); // hardmode is now a one-way street
        }

        private void Update()
        {
            Vector3 acceleration = Input.acceleration;
            if (acceleration.sqrMagnitude >= sqrShakeThreshold
                && Time.unscaledTime - lastShakeTime >= minShakeInterval && !_gm.wasShaken)
            {
                Debug.Log("Phone was shaken!");
                _gm.wasShaken = true;
                lastShakeTime = Time.time;
                ToggleBirdHardMode();
            }
        }

        private void ToggleBirdHardMode()
        {
            background.GetComponent<MaterialSwap>().SwapMaterial();
            _animationSpeed = 10f;
            background.transform.rotation = Quaternion.Euler(0, 0, 180);
            _gm.birdFriction = true;
        }
    }
}
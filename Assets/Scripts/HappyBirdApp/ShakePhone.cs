using UnityEngine;

namespace HappyBirdApp
{
    public class ShakePhone : MonoBehaviour
    {
        [Header("Shake Settings")] 
        public float shakeDetectionThreshold = 2.5f;
        public float minShakeInterval = 0.5f;
        private float _sqrShakeThreshold;
        private float _lastShakeTime;
        [SerializeField] private GameObject background;
        private float _animationSpeed;
        private GameManager _gm;

        private void Start()
        {
            _gm = GameManager.Instance;
            
            _animationSpeed = background.GetComponent<Parallax>().animationSpeed;
            _sqrShakeThreshold = shakeDetectionThreshold * shakeDetectionThreshold;
            _lastShakeTime = Time.unscaledTime;
            if (_gm.birdFriction) ToggleBirdHardMode(); // hardmode is now a one-way street
        }

        private void Update()
        {
            //if (!_gm.parisPopUpSeen || !_gm.wasShaken) return;
            
            Vector3 acceleration = Input.acceleration;
            if (acceleration.sqrMagnitude >= _sqrShakeThreshold
                && Time.unscaledTime - _lastShakeTime >= minShakeInterval)
            {
                Debug.Log("Phone was shaken!");
                _gm.wasShaken = true;
                _lastShakeTime = Time.time;
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
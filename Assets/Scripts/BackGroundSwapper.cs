using UnityEngine;

// This class is responsible for swapping the background images in the game.
public class BackGroundSwapper : MonoBehaviour
{
    [SerializeField] private Sprite bg1;
    [SerializeField] private Sprite bg2;
    [SerializeField] private Sprite bg3;
    [SerializeField] private Sprite bg4;
    [SerializeField] private Sprite bg5;
    private GameManager _gm;

    private void Start()
    {
        _gm = GameManager.Instance;
        HandleBackgroundImage();
    }

    private void HandleBackgroundImage()
    {
        var bgImage = GetComponent<SpriteRenderer>();

        if (_gm.gyroCodeSeen)
        {
            bgImage.sprite = bg5;
        }
        else if (_gm.currentLevel == 1)
        {
            bgImage.sprite = bg4;
        }
        else if (_gm.deathGamePlayed)
        {
            bgImage.sprite = bg3;
        }
        else if (_gm.iDMojiCreated)
        {
            bgImage.sprite = bg2;
        }
        else
        {
            bgImage.sprite = bg1;
        }
    }
}
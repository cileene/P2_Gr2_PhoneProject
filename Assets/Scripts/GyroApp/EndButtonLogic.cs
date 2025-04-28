using UnityEngine;

namespace GyroApp
{
    public class EndButtonLogic : MonoBehaviour
    {
       public void CloseButton()
       {
           GameManager.Instance.rotationChance = 1f;
           GameManager.Instance.rotationFriction = true;
           GameManager.Instance.textFriction = true;
           GameManager.Instance.shuffleHomeScreen = true;
           GameManager.Instance.playerIsTrapped = false;
           SceneHandler.LoadScene("Home");
       }
    }
}

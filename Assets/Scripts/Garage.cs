using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    string playerTag = "Player";
    [SerializeField] bool isLeaveFromGarage;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == playerTag && isLeaveFromGarage)
        {
            SceneManager.LoadScene(0);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            isLeaveFromGarage = true;
        }
    }
}

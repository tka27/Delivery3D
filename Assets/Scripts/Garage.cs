using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    string playerTag = "Player";
    bool isLeaveFromGarage;
    [SerializeField] StaticData staticData;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == playerTag && isLeaveFromGarage)
        {
            staticData.totalMoney += staticData.currentMoney;
            staticData.currentMoney = 0;
            SaveSystem.Save(new SaveData());
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

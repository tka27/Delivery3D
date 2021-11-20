using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Garage : MonoBehaviour
{
    string playerTag = "Player";
    bool isLeaveFromGarage;
    [SerializeField] StaticData staticData;
    [SerializeField] GameObject toGarageCanvas;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == playerTag && isLeaveFromGarage)
        {
            staticData.totalMoney += staticData.currentMoney;
            GarageEnterProcess();
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == playerTag)
        {
            isLeaveFromGarage = true;
        }
    }
    public void ActivateGarageCanvas()
    {
        toGarageCanvas.SetActive(true);
    }

    public void ToGarageConfirm()
    {
        staticData.totalMoney += staticData.currentMoney / 2;
        GarageEnterProcess();
    }
    void GarageEnterProcess()
    {
        Time.timeScale = 1;
        staticData.currentMoney = 0;
        SaveSystem.Save();
        SceneManager.LoadScene(0);
    }
}

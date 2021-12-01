using UnityEngine;

public class UpgradeBtns : MonoBehaviour
{
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject carInfoCanvas;
    [SerializeField] Transform moneyUpgradePos;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    public void UpgradeBtnClick()
    {
        SoundData.PlayBtn();
        if (!upgradeCanvas.activeSelf)
        {
            upgradeCanvas.SetActive(true);
            carInfoCanvas.SetActive(false);
            moneyGO.position = moneyUpgradePos.position;
        }
        else
        {
            CloseBtnClick();
        }
    }
    public void CloseBtnClick()
    {
        SoundData.PlayBtn();
        upgradeCanvas.SetActive(false);
        carInfoCanvas.SetActive(true);
        moneyGO.position = moneyNormalPos.position;
    }
}

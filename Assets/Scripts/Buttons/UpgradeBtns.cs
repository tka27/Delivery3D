using UnityEngine;

public class UpgradeBtns : MonoBehaviour
{
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] GameObject prevCanvas;
    [SerializeField] Transform moneyUpgradePos;
    [SerializeField] Transform moneyNormalPos;
    [SerializeField] Transform moneyGO;
    public void UpgradeBtnClick()
    {
        SoundData.PlayBtn();

        upgradeCanvas.SetActive(true);
        prevCanvas.SetActive(false);
        moneyGO.position = moneyUpgradePos.position;

    }
    public void CloseBtnClick()
    {
        SoundData.PlayBtn();
        upgradeCanvas.SetActive(false);
        prevCanvas.SetActive(true);
        moneyGO.position = moneyNormalPos.position;
    }
}

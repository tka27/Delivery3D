using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    public void OnClick()
    {
        SaveSystem.Save(new SaveData());
        staticData.totalMoney -= staticData.moneyForGame;
        SceneManager.LoadScene(1);
    }
}

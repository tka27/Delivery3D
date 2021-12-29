using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] StaticData staticData;
    public void OnClick()
    {
        SoundData.PlayBtn();
        SaveSystem.Save();
        SceneManager.LoadScene(1);
    }
}

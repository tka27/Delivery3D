using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetBtn : MonoBehaviour
{
    public void ResetData()
    {
        SaveSystem.ClearSaveData();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}

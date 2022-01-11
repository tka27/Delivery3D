using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBtn : MonoBehaviour
{
    [SerializeField] GameSettings settings;
    [SerializeField] StaticData staticData;
    public void Next()
    {
        settings.tutorialLvl++;
        gameObject.SetActive(false);
    }

    public void StartTutorial()
    {
        SoundData.PlayBtn();
        SaveSystem.Save();
        staticData.selectedCarID = 2;
        staticData.trailerIsSelected = true;
        SceneManager.LoadScene(1);
    }
    public void SkipTutorial()
    {
        settings.tutorialLvl = -1;
        settings.SavePrefs();
    }
}

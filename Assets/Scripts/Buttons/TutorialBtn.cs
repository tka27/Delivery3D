using UnityEngine;

public class TutorialBtn : MonoBehaviour
{
    [SerializeField] GameSettings settings;
    [SerializeField] TutorialData tutorialData;
    public void Next()
    {
        switch (settings.tutorialLvl)
        {
            case 1:
                break;


            default: return;
        }
        settings.tutorialLvl++;
    }
}

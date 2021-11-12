using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBtns : MonoBehaviour
{
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    [SerializeField] List<Transform> perkBtns;
    Vector3 normalScale;

    void Start()
    {
        normalScale = transform.localScale;
    }

    public void Perk0()
    {
        foreach (var btn in perkBtns)
        {
            btn.localScale = normalScale;
        }
        transform.localScale = normalScale * 1.15f;
        mainMenuSceneData.selectedPerkID = 0;
    }
    public void Perk1()
    {
        foreach (var btn in perkBtns)
        {
            btn.localScale = normalScale;
        }
        transform.localScale = normalScale * 1.15f;
        mainMenuSceneData.selectedPerkID = 1;
    }
    public void Perk2()
    {
        foreach (var btn in perkBtns)
        {
            btn.localScale = normalScale;
        }
        transform.localScale = normalScale * 1.15f;
        mainMenuSceneData.selectedPerkID = 2;
    }
    public void Perk3()
    {
        foreach (var btn in perkBtns)
        {
            btn.localScale = normalScale;
        }
        transform.localScale = normalScale * 1.15f;
        mainMenuSceneData.selectedPerkID = 3;
    }
    public void Perk4()
    {
        foreach (var btn in perkBtns)
        {
            btn.localScale = normalScale;
        }
        transform.localScale = normalScale * 1.15f;
        mainMenuSceneData.selectedPerkID = 4;
    }
}

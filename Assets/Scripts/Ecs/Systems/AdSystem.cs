using Leopotam.Ecs;


sealed class AdSystem : IEcsInitSystem, IEcsDestroySystem
{
    StaticData staticData;

    public void Init()
    {
        CarReturnBtns.returnEvent += AdCheck;
    }
    public void Destroy()
    {
        CarReturnBtns.returnEvent -= AdCheck;
    }

    void AdCheck()
    {
        if (staticData.adProgress >= 100)
        {
            staticData.adProgress = staticData.adProgress % 100;
            InterstitialAD.singleton.ShowAD();
        }
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInfo : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> productIcons;
    [SerializeField] StaticData staticData;
    [SerializeField] ProductData productData;
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject buyBtn;
    [SerializeField] GameObject upgradeBtn;
    [SerializeField] GameObject price;
    [SerializeField] Text priceText;
    [SerializeField] GameObject padlock;
    [SerializeField] Image trailerBackground;

    // Update is called once per frame

    public void InfoUpdate()
    {
        int carID = staticData.selectedCarID;

        staticData.availableProducts = new List<Product>(); // produtcs update
        switch (carID)
        {
            case 0:
                staticData.availableProducts.Add(new Product(ProductType.Wheat, 0, productData.wheat, 0));
                staticData.availableProducts.Add(new Product(ProductType.Bread, 0, productData.bread, 0));

                break;

            case 1:
                staticData.availableProducts.Add(new Product(ProductType.Wheat, 0, productData.wheat, 0));
                staticData.availableProducts.Add(new Product(ProductType.Meat, 0, productData.meat, 0));
                break;

            default: return;
        }

        foreach (var icon in productIcons)
        {
            icon.gameObject.SetActive(false);
        }
        if (staticData.carsUnlockStatus[carID])
        {
            for (int i = 0; i < staticData.availableProducts.Count; i++)
            {
                productIcons[i].gameObject.SetActive(true);
                productIcons[i].sprite = staticData.availableProducts[i].icon;
            }
        }
        else
        {
            for (int i = 0; i < staticData.availableProducts.Count; i++)
            {
                productIcons[i].gameObject.SetActive(true);
                productIcons[i].sprite = productData.question;
            }
        }




        //car status check
        if (staticData.carsUnlockStatus[carID] && staticData.carsBuyStatus[carID])
        {
            upgradeBtn.SetActive(true);
            buyBtn.SetActive(false);
            playBtn.SetActive(true);
            padlock.SetActive(false);
            price.SetActive(false);
        }
        else if (staticData.carsUnlockStatus[carID] && !staticData.carsBuyStatus[carID])
        {
            upgradeBtn.SetActive(false);
            playBtn.SetActive(false);
            buyBtn.SetActive(true);
            padlock.SetActive(true);
            price.SetActive(true);
            priceText.text = staticData.allCars[carID].price.ToString();
        }
        else if (!staticData.carsUnlockStatus[carID])
        {
            upgradeBtn.SetActive(false);
            playBtn.SetActive(false);
            buyBtn.SetActive(false);
            padlock.SetActive(true);
            price.SetActive(false);
        }

        if (staticData.trailerIsSelected)
        {
            trailerBackground.color = new Color(0, 1, 0, 0.2352941f);
            mainMenuSceneData.trailers[carID].SetActive(true);
            if (!staticData.trailersBuyStatus[carID])
            {
                playBtn.SetActive(false);
                if (staticData.carsBuyStatus[carID])
                {
                    buyBtn.SetActive(true);
                    priceText.text = (staticData.allCars[carID].price / 2).ToString();
                }
            }
        }
        else
        {
            trailerBackground.color = new Color(1, 0, 0, 0.2352941f);
            mainMenuSceneData.trailers[carID].SetActive(false);
        }


    }
}
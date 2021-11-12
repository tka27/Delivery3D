using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfoPanel : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> productIcons;
    [SerializeField] MainMenuSceneData mainMenuSceneData;
    [SerializeField] StaticData staticData;
    [SerializeField] ProductData productData;

    // Update is called once per frame

    void Update()
    {
        if (mainMenuSceneData.carInfoUpdateRequest)
        {
            mainMenuSceneData.carInfoUpdateRequest = false;

            staticData.availableProducts = new List<Product>();
            switch (staticData.selectedCarID)
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

            for (int i = 0; i < staticData.availableProducts.Count; i++)
            {
                productIcons[i].gameObject.SetActive(true);
                productIcons[i].sprite = staticData.availableProducts[i].icon;
            }




        }
    }
}
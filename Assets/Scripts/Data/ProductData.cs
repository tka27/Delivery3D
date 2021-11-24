using UnityEngine;

[CreateAssetMenu]
public class ProductData : ScriptableObject
{
    public Sprite question;
    public Sprite wheat;
    public Sprite water;
    public Sprite bread;
    public Sprite fuel;
    public Sprite autoParts;
    public Sprite meat;
    public Sprite milk;
    public Sprite pizza;
    public Sprite cheese;
    public Sprite eggs;

    public Sprite FindProductIcon(ProductType type)
    {
        switch (type)
        {
            case ProductType.Wheat:
                return wheat;

            case ProductType.Bread:
                return bread;

            case ProductType.Cheese:
                return cheese;

            case ProductType.Eggs:
                return eggs;

            case ProductType.Meat:
                return meat;

            case ProductType.Milk:
                return milk;

            case ProductType.Pizza:
                return pizza;

            case ProductType.Water:
                return water;

            default:
                Debug.LogError("Sprite not found");
                return null;
        }
    }
}
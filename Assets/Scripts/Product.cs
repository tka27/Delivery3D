using UnityEngine;
public class Product
{
    public ProductType type { get; }
    public float mass { get; set; }
    public Sprite icon { get; }
    public float defaultPrice { get; }
    public Product(ProductType productType, float productMass, Sprite sprite, float price)
    {
        type = productType;
        mass = productMass;
        icon = sprite;
        defaultPrice = price;
    }
    public Product(ProductType productType, Sprite sprite, float price)
    {
        type = productType;
        mass = 0;
        icon = sprite;
        defaultPrice = price;
    }
}

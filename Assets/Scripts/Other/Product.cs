using UnityEngine;

public class Product
{
    public ProductType type { get; }
    public float mass { get; set; }
    public Sprite icon { get; }
    public float defaultPrice { get; }
    public float currentPrice { get; set; }
    public Product(ProductType productType, float productMass, Sprite icon, float price)
    {
        type = productType;
        mass = productMass;
        this.icon = icon;
        defaultPrice = price;
        currentPrice = defaultPrice;
    }
    public Product(ProductType productType, Sprite icon, float price)
    {
        type = productType;
        mass = 0;
        this.icon = icon;
        defaultPrice = price;
        currentPrice = defaultPrice;
    }
}

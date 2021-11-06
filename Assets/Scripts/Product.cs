using UnityEngine;
public class Product
{
    public ProductType type { get; }
    public float mass { get; set; }
    public Sprite icon { get; }
    public Product(ProductType productType, float productMass, Sprite sprite)
    {
        type = productType;
        mass = productMass;
        icon = sprite;
    }
    public Product(ProductType productType, Sprite sprite)
    {
        type = productType;
        mass = 0;
        icon = sprite;
    }
}

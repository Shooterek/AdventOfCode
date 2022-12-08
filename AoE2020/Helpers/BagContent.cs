public class BagContent{
    public BagContent(string bagColor, int requiredQuantity)
    {
        BagColor = bagColor;
        RequiredQuantity = requiredQuantity;
    }
    public string BagColor { get; set; }
    public int RequiredQuantity { get; set; }
}
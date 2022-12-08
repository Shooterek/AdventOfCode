public class PropertyValueRange
{
    public PropertyValueRange(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}
using System.Collections.Generic;

public class LuggageRule{
    public LuggageRule(string bagColor)
    {
        BagColor = bagColor;
        RequiredContent = new List<BagContent>();
    }
    public string BagColor { get; set; }
    public List<BagContent> RequiredContent { get; set; }
}
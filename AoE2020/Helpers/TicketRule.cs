using System.Collections.Generic;

public class TicketRule{
    public TicketRule(string propertyName)
    {
        PropertyName = propertyName;
        PropertyValidRanges = new List<PropertyValueRange>();
    }
    public string PropertyName { get; set; }
    public List<PropertyValueRange> PropertyValidRanges { get; set; }

    public bool CheckIfValueIsValid(int value){
        foreach(var range in PropertyValidRanges){
            if(value >= range.MinValue && value <= range.MaxValue){
                return true;
            }
        }
        return false;
    }
}
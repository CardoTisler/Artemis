using System.Runtime.Serialization;

namespace ArtemisApi.Enums;

public enum SystemShelf
{
    [EnumMember(Value = "SHELF.WANT_TO_READ")]
    WANT_TO_READ,
    
    [EnumMember(Value = "SHELF.READING")]
    READING,
    
    [EnumMember(Value = "SHELF.READ")]
    READ
    
}
using System.Runtime.Serialization;

namespace AppApi.Helpers
{
    public enum BillType
    {
        [EnumMember(Value = "Ρεύμα")]
        Electricity,

        [EnumMember(Value = "Φυσικό αέριο")]
        NaturalGas,

        [EnumMember(Value = "Ρεύμα, Φυσικό αέριο")]
        Both
    }
}
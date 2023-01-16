using System.Runtime.Serialization;

namespace AppApi.Helpers
{
    public enum Status
    {
        [EnumMember(Value = "Σε Αναμονή")]
        Pending,

        [EnumMember(Value = "Εγκριθείσα")]
        Approved,

        [EnumMember(Value = "Απορριφθείσα")]
        Rejected
    }
}
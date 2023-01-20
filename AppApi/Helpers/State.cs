using System.Runtime.Serialization;

namespace AppApi.Helpers
{
    public enum State
    {
        [EnumMember(Value = "Αποθήκευση")]
        Saved,

        [EnumMember(Value = "Υποβολή")]
        Submitted
    }
}
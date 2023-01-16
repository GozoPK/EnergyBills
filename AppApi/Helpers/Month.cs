using System.Runtime.Serialization;

namespace AppApi.Helpers
{
    public enum Month
    {
        [EnumMember(Value = "Ιανουάριος")]
        January = 1,

        [EnumMember(Value = "Φεβρουάριος")]
        February = 2,

        [EnumMember(Value = "Μάρτιος")]
        March = 3,

        [EnumMember(Value = "Απρίλιος")]
        April = 4,

        [EnumMember(Value = "Μάιος")]
        May = 5,

        [EnumMember(Value = "Ιούνιος")]
        June = 6,

        [EnumMember(Value = "Ιούλιος")]
        July = 7,

        [EnumMember(Value = "Αύγουστος")]
        August = 8,

        [EnumMember(Value = "Σεπτέμβριος")]
        September = 9,

        [EnumMember(Value = "Οκτώβριος")]
        October = 10,

        [EnumMember(Value = "Νοέμβριος")]
        November = 11,

        [EnumMember(Value = "Δεκέμβριος")]
        December = 12
    }
}
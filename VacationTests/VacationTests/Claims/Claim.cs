using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VacationTests.Claims
{
    public class Claim
    {
        // метод инициализации
        public Claim(string id, ClaimType type, ClaimStatus status, Director director, DateTime startDate,
            DateTime endDate, int? childAgeInMonths, string userId, bool paidNow)
        {
            Id = id;
            Type = type;
            Status = status;
            Director = director;
            StartDate = startDate;
            EndDate = endDate;
            ChildAgeInMonths = childAgeInMonths;
            UserId = userId;
            PaidNow = paidNow;
        }
        // свойства класса
        public string Id { get; }
        [property: JsonConverter(typeof(StringEnumConverter))]
        public ClaimType Type { get; }
        public ClaimStatus Status { get; }
        public Director Director { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int? ChildAgeInMonths { get; }
        public string UserId { get; }
        public bool PaidNow { get; }
    }

    public class Director
    {
        public Director(int id, string name, string position)
        {
            Id = id;
            Name = name;
            Position = position;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
    
    // Об enum https://ulearn.me/course/basicprogramming/Konstanty_i_enum_y_f1740706-b8e2-4bd4-ab87-3cc710a52449
    public enum ClaimType
    {
        [System.ComponentModel.Description("Дополнительный")] [EnumMember(Value = "Дополнительный")]
        Additional,
        [System.ComponentModel.Description("По уходу за ребенком")] [EnumMember(Value = "По уходу за ребенком")]
        Child,
        [System.ComponentModel.Description("Не оплачиваемый")] [EnumMember(Value = "Не оплачиваемый")]
        NotPaid,
        [System.ComponentModel.Description("Основной")] [EnumMember(Value = "Основной")]
        Paid,
        [System.ComponentModel.Description("На учебу")] [EnumMember(Value = "На учебу")]
        Study
    }
    public enum ClaimStatus
    {
        [System.ComponentModel.Description("Согласовано")]
        Accepted = 0,
        [System.ComponentModel.Description("Отклонено")]
        Rejected = 1,
        [System.ComponentModel.Description("На согласовании")]
        NonHandled = 2
    }
}
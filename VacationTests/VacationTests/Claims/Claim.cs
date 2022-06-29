using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VacationTests.Claims
{
    public record Claim(
        // перечисляем какие свойства будут у класса Claim
        string Id,
        [property: JsonConverter(typeof(StringEnumConverter))]
        ClaimType Type,
        ClaimStatus Status,
        Director Director,
        DateTime StartDate,
        DateTime EndDate,
        int? ChildAgeInMonths,
        string UserId,
        bool PaidNow)
    {
        // добавляем статический метод для создания экземпляра класса со значениями по умолчанию
        public static Claim CreateDefault()
        {
            var random = new Random();
            var randomClaimId = random.Next(1, 101).ToString();

            return new Claim(
                randomClaimId,
                ClaimType.Paid,
                ClaimStatus.NonHandled,
                Director.CreateDefault(),
                // DirectorBuilder.ADefaultDirector(),
                // new Director(1, "Name", "Position"),
                DateTime.Now.AddDays(7).Date,
                DateTime.Now.AddDays(12).Date,
                null,
                "1",
                false);
        }

        // можно также добавить второй метод для создания типичного заявления по уходу за ребёнком
        public static Claim CreateChildType()
        {
            var random = new Random();
            var childAgeInMonths = random.Next(1, 101);
            return CreateDefault() with
            {
                Type = ClaimType.Child,
                ChildAgeInMonths = childAgeInMonths
            };
        }
    }

    public record Director(
        int Id,
        string Name,
        string Position)
    {
        public static Director CreateDefault()
        {
            return new Director(
                14,
                "Бублик Владимир Кузьмич",
                "Директор департамента");
        }

        public static Director CreateSuperDirector()
        {
            return CreateDefault() with
            {
                Id = 24320,
                Name = "Кирпичников Алексей Николаевич",
                Position = "Руководитель управления"
            };
        }
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
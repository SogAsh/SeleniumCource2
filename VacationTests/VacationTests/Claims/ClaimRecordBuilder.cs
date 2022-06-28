using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VacationTests.Claims
{
    public record ClaimRecordBuilder (
        // перечисляем какие свойства будут у класса Claim
        string Id,
        [property: JsonConverter(typeof(StringEnumConverter))]
        ClaimType Type,
        ClaimStatus Status,
        int? ChildAgeInMonths,
        string UserId)
    {
        // добавляем статический метод для создания экземпляра класса со значениями по умолчанию
        public static ClaimRecordBuilder CreateDefault()
        {
            var random = new Random();
            var randomClaimId = random.Next(1, 101).ToString();

            return new ClaimRecordBuilder(
                randomClaimId,
                ClaimType.Paid,
                ClaimStatus.NonHandled,
                null,
                "1"
            );
        }

        // можно также добавить второй метод для создания типичного заявления по уходу за ребёнком
        public static ClaimRecordBuilder CreateChildType()
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
}
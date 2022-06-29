// using System;
// using System.ComponentModel;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;
//
// namespace VacationTests.Claims
// {
//     public record ClaimRecord (
//         // перечисляем какие свойства будут у класса Claim
//         string Id,
//         [property: JsonConverter(typeof(StringEnumConverter))]
//         ClaimType Type,
//         ClaimStatus Status,
//         int? ChildAgeInMonths,
//         string UserId,
//         Director directorBuilder,
//         DateTime startDay,
//         DateTime endDate,
//         bool paidNow)
//     {
//         // добавляем статический метод для создания экземпляра класса со значениями по умолчанию
//         public static ClaimRecord CreateDefault()
//         {
//             var random = new Random();
//             var randomClaimId = random.Next(1, 101).ToString();
//
//             return new ClaimRecord(
//                 randomClaimId,
//                 ClaimType.Paid,
//                 ClaimStatus.NonHandled,
//                 null,
//                 "1",
//                 DirectorBuilder.ADefaultDirector(),
//                 startDay = DateTime.Now.Date.AddDays(7),
//                 endDate  = DateTime.Now.Date.AddDays(12),
//                 paidNow = false
//             );
//         }
//
//         // можно также добавить второй метод для создания типичного заявления по уходу за ребёнком
//         public static ClaimRecord CreateChildType()
//         {
//             var random = new Random();
//             var childAgeInMonths = random.Next(1, 101);
//             return CreateDefault() with
//             {
//                 Type = ClaimType.Child,
//                 ChildAgeInMonths = childAgeInMonths
//             };
//         }
//     }
//     public enum ClaimStatus
//     {
//         [Description("Согласовано")] Accepted = 0,
//         [Description("Отклонено")] Rejected = 1,
//         [Description("На согласовании")] NonHandled = 2
//     }
//
//     public enum ClaimType
//     {
//         [Description("Дополнительный")] Additional,
//         [Description("По уходу за ребенком")] Child,
//         [Description("Не оплачиваемый")] NotPaid,
//         [Description("Основной")] Paid,
//         [Description("На учебу")] Study
//     }
// }
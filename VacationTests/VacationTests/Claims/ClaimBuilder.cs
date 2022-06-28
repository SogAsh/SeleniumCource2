using System;

namespace VacationTests.Claims
{
    // Builder – это тоже класс
    // название должно быть обязательно <имя класса, для которого создаем Builder>Builder
    public class ClaimBuilder
    {
        // создаем переменные со значениями по умолчанию для каждого свойства класса отпуск
        private const string DefaultUserId = "1";
        private string id = new Random().Next(101).ToString();
        private ClaimType type = ClaimType.Paid;
        private ClaimStatus status = ClaimStatus.NonHandled;
        private string userId = DefaultUserId;
        private int? childAgeInMonths;
        
        private int directorId = 14; 
        private string directorName = "Бублик Владимир Кузьмич";
        private string directorPosition = "Директор департамента";
        
        private DateTime startDate = DateTime.Now.Date.AddDays(7);
        private DateTime endDate = DateTime.Now.Date.AddDays(12);
        private bool paidNow = false;

        public ClaimBuilder()
        {
        }

        // для каждого свойства создаем метод With<название свойства>, возвращающий ClaimBuilder
        // метод принимает новое значение свойства, записывает это значение в переменную, созданную выше
        // с помощью таких методов можно будет задать необходимые поля
        public ClaimBuilder WithId(string newId)
        {
            id = newId;
            return this;
        }

        public ClaimBuilder WithType(ClaimType newClaimType)
        {
            type = newClaimType;
            return this;
        }

        public ClaimBuilder WithStatus(ClaimStatus newStatus)
        {
            status = newStatus;
            return this;
        }

        public ClaimBuilder WithUserId(string newUserId)
        {
            userId = newUserId;
            return this;
        }

        // можно создать перегрузку метода для удобных входных данных
        public ClaimBuilder WithUserId(int newUserId)
        {
            userId = newUserId.ToString();
            return this;
        }

        public ClaimBuilder WithChildAgeInMonths(int newChildAgeInMonths)
        {
            childAgeInMonths = newChildAgeInMonths;
            return this;
        }
        public ClaimBuilder WithDirector(int directorId, string directorName, string directorPosition)
        {
            this.directorId = directorId;
            this.directorName = directorName;
            this.directorPosition = directorPosition;
            return this;
        }
        public ClaimBuilder WithStartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }
        public ClaimBuilder WithEndDate(DateTime endDate)
        {
            this.endDate = endDate;
            return this;
        }
        public ClaimBuilder WithEWithPeriodndDate(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            return this;
        }

        // основной метод, который возвращает экземпляр класса Claim
        // todo заменить код ниже на public Claim Build() => new(id, type, status, director, startDate, endDate, childAgeInMonths, userId, paidNow);
        public Claim Build() => new Claim (id, type, status,
            new Director(directorId, directorName, directorPosition), startDate,
            endDate, childAgeInMonths, userId, paidNow);

        // статический метод, который возвращает экземпляр класса ClaimBuilder вместе со значениями по умолчанию
        public static ClaimBuilder AClaim()
        {
            // создаем переменные со значениями по умолчанию для каждого свойства класса отпуск
            // private const string DefaultUserId = "1";
            // private string id = new Random().Next(101).ToString();
            // private ClaimType type = ClaimType.Paid;
            // private ClaimStatus status = ClaimStatus.NonHandled;
            // private string userId = DefaultUserId;
            // private int? childAgeInMonths;
            
            return new ClaimBuilder();
        }

        // статический метод, который возвращает экземпляр класса Claim вместе со значениями по умолчанию
        // для случая, если нам никакие данные не нужны
        public static Claim ADefaultClaim() => AClaim().Build();

        // статический метод, который возвращает экземпляр класса Claim
        // для случая, если нам важен отпуск по уходу за ребёнком
        public static Claim AChildClaim() => AClaim()
            .WithType(ClaimType.Child)
            .WithChildAgeInMonths(new Random().Next(1, 101))
            .Build();
    }

    public class DirectorBuilder
    {
        private int id = 14;
        private string name = "Бублик Владимир Кузьмич";
        private string position = "Директор департамента";
        public DirectorBuilder()
        {
        }
        public DirectorBuilder WithId(int newId)
        {
            id = newId;
            return this;
        }
        public DirectorBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }
        public DirectorBuilder WithPosition(string position)
        {
            this.position = position;
            return this;
        }
        public Director Build() => new Director (id, name, position);
        
        public static DirectorBuilder ADirector()
        {
            return new DirectorBuilder();
        }
        public static Director ADefaultDirector() => ADirector().Build();
        public static Director TheSuperDirector() => ADirector().WithId(24320).WithName("Кирпичников Алексей Николаевич").WithPosition("Руководитель управления").Build();

    }
}
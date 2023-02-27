namespace Chat.Domain.Models.Authentication.ValueObjects
{
    public record UserId
    {
        public string Value { get; init; }
        private UserId(string value) => Value = value;
        public static UserId From(string value)
        {
            Validate(value);
            return new(value);
        }

        private static void Validate(string value)
        {
        }
    }
}
namespace Chat.Domain.Models.Authentication.ValueObjects
{
    public record RefreshToken
    {
        public string Value { get; init; }
        private RefreshToken(string value) => Value = value;
        public static RefreshToken From(string value)
        {
            Validate(value);
            return new(value);
        }

        private static void Validate(string value)
        {
        }
    }
}
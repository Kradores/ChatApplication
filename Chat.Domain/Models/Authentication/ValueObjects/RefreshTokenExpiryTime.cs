namespace Chat.Domain.Models.Authentication.ValueObjects
{
    public record RefreshTokenExpiryTime
    {
        public DateTime Value { get; init; }
        private RefreshTokenExpiryTime(DateTime value) => Value = value;
        public static RefreshTokenExpiryTime From(DateTime value)
        {
            Validate(value);
            return new(value);
        }

        private static void Validate(DateTime value)
        {
        }
    }
}
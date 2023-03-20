using Chat.Infrastructure.Entities;

namespace Chat.Tests.Integration.Seeds;

public static class UserSeeds
{
    public static readonly List<User> Users = new()
    {
        new User()
        {
            Id = "0a43b9a2-e2cc-4bea-8f60-f22cf1b82060",
            RefreshToken = "vG07mpe8ctSJ5mY1WRjOQf/08CNkdafJMWW78jjymdI=",
            RefreshTokenExpiryTime = DateTime.Now.AddDays(7),
            UserName = "Marcel",
            NormalizedUserName = "MARCEL",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEAQAzRKZdw3L2RmQeePQCYxRxI+rmdeF4l4Q/rr+jOq59cvoAqM2CR98oEeSMcsIaA==",
            SecurityStamp = "NLF6BES3ZOKVGWANZR5PNXC3DLDIZEAL",
            ConcurrencyStamp = "ebdde094-47db-4493-9c7b-d441d1541176",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
        },
        new User()
        {
            Id = "46d4631f-0c7f-4f5d-b1a6-2ff39c3d422b",
            RefreshToken = "CYbVRRlDWQ3k0dXRCrb+xi5GFYF6a2FKvWNyNrrs3xw=",
            RefreshTokenExpiryTime = DateTime.Now.AddDays(7),
            UserName = "Viorel",
            NormalizedUserName = "VIOREL",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEAG4kBqpH+w0LH4bHRdwBRREsEYoJFsEY4v2VLm/PpIU/COZNlkhGzDVazpM1Cd5SQ==",
            SecurityStamp = "KZRGS7APJNTJ4ULV3HU7K6K2HBWL4XXL",
            ConcurrencyStamp = "4011806f-dc1f-487d-a7a9-286799dbff2b",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
        },
        new User()
        {
            Id = "91768415-284e-460a-b4bd-ddf33e1bc512",
            RefreshToken = "W545pU8IYpGnPvbCwT1qCzazcF+9VDGtnHRJgaPo89s=",
            RefreshTokenExpiryTime = DateTime.Now.AddDays(7),
            UserName = "Viorica",
            NormalizedUserName = "VIORICA",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEGxn91h3BtreAeeKqbfJk1TxvJdR+poq0pqNjTk96mWI0x7ANh0PbvBCYTP1+kRW/A==",
            SecurityStamp = "D3V6UIA6KQBQOKMAU4SEISR3R3F7KNFA",
            ConcurrencyStamp = "68fa26f6-0699-41a7-a805-dcc330ee8247",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
        }
    };
}

namespace LeaseFilms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'56899a19-a89c-4dec-b3a8-18bdccd45924', N'guest@leasefilms.com', 0, N'ACa86HemomM67/mwVNGL8RnBzF8417no6EFe9Jj4WBkX5LNr13A7sOo2SABHDHyPUA==', N'd7febd00-b84d-44b6-9c17-7721b345bf9d', NULL, 0, 0, NULL, 1, 0, N'guest@leasefilms.com')
              INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b893af48-6ac4-4a07-85c6-6735f1b1ec04', N'admin@leasefilms.com', 0, N'AGZIA+jejQrRo6lKar75NwTdYyRBSIot6rpzNknKRwAI9MwWxLHUxytqI/+kJojrWg==', N'18ce756c-6088-42e5-97be-ec1d9fb6e7ab', NULL, 0, 0, NULL, 1, 0, N'admin@leasefilms.com')
              INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'23a85b8c-f71d-4b19-b78a-4e5ab8a7ad23', N'CanManageMovies')
              INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b893af48-6ac4-4a07-85c6-6735f1b1ec04', N'23a85b8c-f71d-4b19-b78a-4e5ab8a7ad23')");
        }

        public override void Down()
        {
        }
    }
}

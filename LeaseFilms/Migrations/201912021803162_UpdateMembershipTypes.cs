namespace LeaseFilms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Pay as You Go' WHERE SignUpFee = 0");
            Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE SignUpFee > 0");
        }

        public override void Down()
        {
        }
    }
}

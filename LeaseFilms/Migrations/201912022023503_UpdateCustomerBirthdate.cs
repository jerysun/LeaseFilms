namespace LeaseFilms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateCustomerBirthdate : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET Birthdate = CAST('1980-1-1' AS DATETIME) WHERE Id=1 AND Name='John Smith'");
            Sql("UPDATE Customers SET Birthdate = null WHERE Id=2 AND Name='May Williams'");
        }

        public override void Down()
        {
        }
    }
}

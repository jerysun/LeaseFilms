namespace LeaseFilms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies(Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES('Hangover', 3, CAST('2011-05-01' AS DATETIME), CAST('2011-07-07' AS DATETIME), 5)");
            Sql("INSERT INTO Movies(Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES('Die Hard', 1, CAST('2012-12-23' AS DATETIME), CAST('2012-12-27' AS DATETIME), 2)");
            Sql("INSERT INTO Movies(Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES('The Terminator', 1, CAST('1994-03-08' AS DATETIME), CAST('1994-04-17' AS DATETIME), 7)");
            Sql("INSERT INTO Movies(Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES('The Story', 7, CAST('2009-06-01' AS DATETIME), CAST('2009-10-22' AS DATETIME), 3)");
            Sql("INSERT INTO Movies(Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES('Titanic', 8, CAST('2006-08-15' AS DATETIME), CAST('2006-09-17' AS DATETIME), 11)");
            Sql("INSERT INTO Movies(Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES('Wall Street: Money Never Sleeps', 5, CAST('2010-09-24' AS DATETIME), CAST('2010-11-29' AS DATETIME), 8)");
        }

        public override void Down()
        {
        }
    }
}

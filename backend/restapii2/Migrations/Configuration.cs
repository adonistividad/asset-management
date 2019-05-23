namespace restapii2.Migrations
{
    using restapii2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<restapii2.Context.RestApii2Context>
    {
        private readonly bool _pendingMigrations;
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //// Check if there are migrations pending to run, this can happen if database doesn't exists or if there was any
            ////  change in the schema
            //var migrator = new DbMigrator(this);
            //_pendingMigrations = migrator.GetPendingMigrations().Any();

            //// If there are pending migrations run migrator.Update() to create/update the database then run the Seed() method to populate
            ////  the data if necessary
            //if (_pendingMigrations)
            //{
            //    migrator.Update();
            //    Seed(new restapii2.Context.RestApii2Context());
            //}

            Seed(new restapii2.Context.RestApii2Context());
        }

        protected override void Seed(restapii2.Context.RestApii2Context context)
        {
            try
            {
                //  This method will be called after migrating to the latest version.
                var assets = new List<tblAsset> {
                    new tblAsset{ assetId=1, assetLabel="Lenovo Yoga", assetDescription="Lenovo Yoga | Laptop", assetType="Laptop", assetStatus="In-use", purchaseDate=Convert.ToDateTime("2018-04-10"), currentOwner="John Doe"},
                    new tblAsset{ assetId=2, assetLabel="Apple MacBook", assetDescription="Apple MacBook | Laptop", assetType="Laptop", assetStatus="Available", purchaseDate=Convert.ToDateTime("2018-02-12"), currentOwner=""},
                    new tblAsset{ assetId=3, assetLabel="Apple iPad Air", assetDescription="Apple iPad Air | Tablet", assetType="Tablet", assetStatus="In-use", purchaseDate=Convert.ToDateTime("2018-03-09"), currentOwner="Jane Doe"},
                    new tblAsset{ assetId=4, assetLabel="iPhone 7", assetDescription="iPhone 7 | Mobile", assetType="Mobile", assetStatus="In-repair", purchaseDate=Convert.ToDateTime("2018-01-10"), currentOwner=""},
                    new tblAsset{ assetId=5, assetLabel="Samsung Galaxy Tab S 10.5", assetDescription="Samsung Galaxy Tab S 10.5 | Tablet", assetType="Tablet", assetStatus="Available", purchaseDate=Convert.ToDateTime("2018-06-16"), currentOwner=""},
                };
                if (!context.Assets.Any()) { assets.ForEach(s => context.Assets.AddOrUpdate(s)); }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
        }
    }
}

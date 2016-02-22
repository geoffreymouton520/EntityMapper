using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Models;

namespace Data
{
    public class EntityMapperDbContext:DbContext
    {
        //static EntityMapperDbContext()
        //{
        //    Database.SetInitializer<EntityMapperDbContext>(null);
        //}
        public EntityMapperDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new CreateDatabaseIfNotExists<EntityMapperDbContext>());

            Configuration.LazyLoadingEnabled = false;
            //Configuration.ValidateOnSaveEnabled = false;
        }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Models.System> Systems { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<MappingOrigin> MappingOrigins { get; set; }
        public DbSet<EntityMapping> EntityMappings { get; set; }
        public DbSet<PropertyMapping> PropertyMappings { get; set; }
        public DbSet<TestData> TestDatas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        //    modelBuilder.Entity<Models.System>()
        //.HasRequired(f => f.Domain)
        //.WithRequiredDependent()
        //.WillCascadeOnDelete(false);

   


                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<EntityMapping>()
                   .HasRequired(m => m.Source)
                   .WithMany(t => t.SourceMappings)
                   .HasForeignKey(m => m.SourceId)
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<EntityMapping>()
                        .HasRequired(m => m.Destination)
                        .WithMany(t => t.DestinationMappings)
                        .HasForeignKey(m => m.DestinationId)
                        .WillCascadeOnDelete(false);


            modelBuilder.Entity<PropertyMapping>()
       .HasRequired(m => m.Source)
       .WithMany(t => t.SourceMappings)
       .HasForeignKey(m => m.SourceId)
       .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyMapping>()
                        .HasRequired(m => m.Destination)
                        .WithMany(t => t.DestinationMappings)
                        .HasForeignKey(m => m.DestinationId)
                        .WillCascadeOnDelete(false);
        }
    }
    //internal sealed class Configuration : DbMigrationsConfiguration<EntityMapperDbContext>
    //{
    //    public Configuration()
    //    {
    //        AutomaticMigrationsEnabled = false;
    //    }
    //}
}

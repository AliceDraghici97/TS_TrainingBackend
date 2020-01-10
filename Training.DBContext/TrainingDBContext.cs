using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Model;
using Training.Model.Entity;

namespace Training.DBContext
{
    public class TrainingDBContext : DbContext
    {

        public TrainingDBContext() : base("name=TrainingDB")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<StudentsXSubject> StudentsXSubjects { get; set; }

        public virtual DbSet<User> Users{ get; set; }

  
        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            IEnumerable<object> addedEntries = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added)
                .Select(e => e.Entity);
            foreach (var x in addedEntries)
            {
                GenericEntity genericEntity = (GenericEntity)x;
                genericEntity.CreatedOn = DateTime.Now;
                genericEntity.ModifiedOn = DateTime.Now;
            }

            IEnumerable<object> updatedEntries = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified)
                .Select(e => e.Entity);
            foreach (var x in updatedEntries)
            {
                GenericEntity genericEntity = (GenericEntity)x;
                genericEntity.ModifiedOn = DateTime.Now;
            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            //dbModelBuilder.Entity<Student>().ToTable("Students");
            //dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(dbModelBuilder);
        }
    }
}
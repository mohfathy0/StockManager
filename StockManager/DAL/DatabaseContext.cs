using System;
using MySql.Data.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DataModel;
using System.Data.Entity;

namespace StockManager.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("Entities")
        {

        }

        public virtual DbSet<ActionLog> ActionLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAccessProfile> UserAccessProfiles { get; set; }
        public virtual DbSet<UserAccessProfileDetail> UserAccessProfileDetails { get; set; }
        public virtual DbSet<UserSettingsProfile> UserSettingsProfiles { get; set; }
        public virtual DbSet<UserSettingsProfileProperty> UserSettingsProfileProperties { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSettings { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}

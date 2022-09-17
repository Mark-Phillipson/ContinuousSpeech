using ControlWSR.Models;

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ControlWSR
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=VoiceLaunchModel")
        {
        }

        //public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
         public  virtual DbSet<GrammarName> GrammarNames { get; set; }
         public  virtual DbSet<GrammarItem> GrammarItems { get; set; }
        public virtual DbSet<AdditionalCommand> AdditionalCommands { get; set; }
        public virtual DbSet<ApplicationsToKill> ApplicationsToKills { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<CurrentWindow> CurrentWindows { get; set; }
        public virtual DbSet<CustomIntelliSense> CustomIntelliSenses { get; set; }
        public virtual DbSet<CustomWindowsSpeechCommand> CustomWindowsSpeechCommands { get; set; }
        public virtual DbSet<Example> Examples { get; set; }
        public virtual DbSet<GeneralLookup> GeneralLookups { get; set; }
        public virtual DbSet<HtmlTag> HtmlTags { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Launcher> Launchers { get; set; }
        public virtual DbSet<LauncherMultipleLauncherBridge> LauncherMultipleLauncherBridges { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<MousePosition> MousePositions { get; set; }
        public virtual DbSet<MultipleLauncher> MultipleLaunchers { get; set; }
        public virtual DbSet<PropertyTabPosition> PropertyTabPositions { get; set; }
        public virtual DbSet<SavedMousePosition> SavedMousePositions { get; set; }
        public virtual DbSet<tblApplicationCaption> tblApplicationCaptions { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<ValuesToInsert> ValuesToInserts { get; set; }
        public virtual DbSet<VisualStudioCommand> VisualStudioCommands { get; set; }
        public virtual DbSet<WindowsSpeechVoiceCommand> WindowsSpeechVoiceCommands { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalCommand>()
                .Property(e => e.WaitBefore)
                .HasPrecision(1, 1);

            modelBuilder.Entity<ApplicationsToKill>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetRoleClaims)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserTokens)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.CustomIntelliSenses)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomIntelliSense>()
                .HasMany(e => e.AdditionalCommands)
                .WithRequired(e => e.CustomIntelliSense)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.CustomIntelliSenses)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Launcher>()
                .HasMany(e => e.LauncherMultipleLauncherBridges)
                .WithRequired(e => e.Launcher)
                .WillCascadeOnDelete(false);
        }
    }
}

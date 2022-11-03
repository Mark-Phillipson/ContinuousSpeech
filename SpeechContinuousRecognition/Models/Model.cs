
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using System;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Windows.Forms;

namespace SpeechContinuousRecognition.Models
{
    public partial class Model : DbContext
    {
        private readonly string _connectionString;

        public Model( string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }




        //public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<GrammarName> GrammarNames { get; set; }
         public  virtual DbSet<GrammarItem> GrammarItems { get; set; }
        public virtual DbSet<AdditionalCommand> AdditionalCommands { get; set; }
        public virtual DbSet<ApplicationsToKill> ApplicationsToKills { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
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
        public virtual DbSet<ValuesToInsert> ValuesToInserts { get; set; }
        public virtual DbSet<VisualStudioCommand> VisualStudioCommands { get; set; }
        public virtual DbSet<WindowsSpeechVoiceCommand> WindowsSpeechVoiceCommands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalCommand>()
                .Property(e => e.WaitBefore)
                .HasPrecision(1, 1);

            //modelBuilder.Entity<Category>()
            //    .HasMany(e => e.CustomIntelliSenses)
            //    .WithRequired(e => e.Category)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CustomIntelliSense>()
            //    .HasMany(e => e.AdditionalCommands)
            //    .WithRequired(e => e.CustomIntelliSense)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Language>()
            //    .HasMany(e => e.CustomIntelliSenses)
            //    .WithRequired(e => e.Language)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Launcher>()
            //    .HasMany(e => e.LauncherMultipleLauncherBridges)
            //    .WithRequired(e => e.Launcher)
            //    .WillCascadeOnDelete(false);
        }
    }
}

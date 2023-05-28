
using DataAccessLibrary.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using System;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Windows.Forms;

namespace SpeechContinuousRecognition.Models
{
	public partial class VoiceAdminDbContext : DbContext
	{
		private readonly string _connectionString;

		public VoiceAdminDbContext(string connectionString)
		{
			_connectionString = connectionString;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}
		public virtual DbSet<ApplicationDetail> ApplicationDetails { get; set; }
		public virtual DbSet<Prompt> Prompts { get; set; }
		public virtual DbSet<Microphone> Microphones { get; set; }
		public virtual DbSet<Idiosyncrasy> Idiosyncrasies { get; set; }
		public virtual DbSet<PhraseListGrammarStorage> PhraseListGrammars { get; set; }

		public virtual DbSet<GrammarName> GrammarNames { get; set; } = null!;
		public virtual DbSet<GrammarItem> GrammarItems { get; set; } = null!;
		public virtual DbSet<AdditionalCommand> AdditionalCommands { get; set; } = null!;
		public virtual DbSet<ApplicationsToKill> ApplicationsToKills { get; set; } = null!;
		public virtual DbSet<Appointment> Appointments { get; set; } = null!;
		public virtual DbSet<Category> Categories { get; set; } = null!;
		public virtual DbSet<Computer> Computers { get; set; } = null!;
		public virtual DbSet<CurrentWindow> CurrentWindows { get; set; } = null!;
		public virtual DbSet<CustomIntelliSense> CustomIntelliSenses { get; set; } = null!;
		public virtual DbSet<CustomWindowsSpeechCommand> CustomWindowsSpeechCommands { get; set; } = null!;
		public virtual DbSet<Example> Examples { get; set; } = null!;
		public virtual DbSet<GeneralLookup> GeneralLookups { get; set; } = null!;
		public virtual DbSet<HtmlTag> HtmlTags { get; set; } = null!;
		public virtual DbSet<Language> Languages { get; set; } = null!;
		public virtual DbSet<Launcher> Launchers { get; set; } = null!;
		public virtual DbSet<LauncherMultipleLauncherBridge> LauncherMultipleLauncherBridges { get; set; } = null!;
		public virtual DbSet<Login> Logins { get; set; } = null!;
		public virtual DbSet<MousePosition> MousePositions { get; set; } = null!;
		public virtual DbSet<MultipleLauncher> MultipleLaunchers { get; set; } = null!;
		public virtual DbSet<PropertyTabPosition> PropertyTabPositions { get; set; } = null!;
		public virtual DbSet<SavedMousePosition> SavedMousePositions { get; set; } = null!;
		public virtual DbSet<tblApplicationCaption> tblApplicationCaptions { get; set; } = null!;
		public virtual DbSet<ValuesToInsert> ValuesToInserts { get; set; } = null!;
		public virtual DbSet<VisualStudioCommand> VisualStudioCommands { get; set; } = null!;
		public virtual DbSet<WindowsSpeechVoiceCommand> WindowsSpeechVoiceCommands { get; set; } = null!;

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

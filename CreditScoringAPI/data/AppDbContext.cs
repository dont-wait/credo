using Microsoft.EntityFrameworkCore;
using CreditScoringAPI.Models.Entities;
using CreditScoringAPI.Models.Enums;

namespace CreditScoringAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ── DbSets ───────────────────────────────────────────────────────────────
    public DbSet<Customer>                 Customers                  { get; set; }
    public DbSet<LoanApplication>          LoanApplications           { get; set; }
    public DbSet<CreditBureauHistory>      CreditBureauHistories      { get; set; }
    public DbSet<PreviousLoanApplication>  PreviousLoanApplications   { get; set; }
    public DbSet<InstallmentPayment>       InstallmentPayments        { get; set; }
    public DbSet<ModelPrediction>          ModelPredictions           { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Đăng ký Enums với Npgsql
        modelBuilder.HasPostgresEnum<ContractType>(name: "contract_type");
        modelBuilder.HasPostgresEnum<GenderType>(name: "gender_type");
        modelBuilder.HasPostgresEnum<SuiteType>(name: "suite_type");
        modelBuilder.HasPostgresEnum<IncomeType>(name: "income_type");
        modelBuilder.HasPostgresEnum<EducationType>(name: "education_type");
        modelBuilder.HasPostgresEnum<FamilyStatus>(name: "family_status");
        modelBuilder.HasPostgresEnum<HousingType>(name: "housing_type");
        modelBuilder.HasPostgresEnum<OccupationType>(name: "occupation_type");
        modelBuilder.HasPostgresEnum<OrganizationType>(name: "organization_type");
        modelBuilder.HasPostgresEnum<WallsMaterial>(name: "wallsmaterial");
        modelBuilder.HasPostgresEnum<HouseTypeMode>(name: "housetype_mode");
        modelBuilder.HasPostgresEnum<FondKapremont>(name: "fondkapremont");
        modelBuilder.HasPostgresEnum<EmergencyState>(name: "emergency_state");
        modelBuilder.HasPostgresEnum<ContractStatus>(name: "contract_status");
        modelBuilder.HasPostgresEnum<PredictionResult>(name: "prediction_result");

        // ── Customer ─────────────────────────────────────────────────────────
        modelBuilder.Entity<Customer>(e =>
        {
            e.ToTable("customers");
            e.HasKey(x => x.CustomerId);
            e.Property(x => x.CustomerId).UseIdentityAlwaysColumn();

            // Unique constraint trên CCCD/CMND
            e.HasIndex(x => x.IdNumber).IsUnique();

            // Native Enums
            e.Property(x => x.Gender)
             .HasColumnType("gender_type");

            e.Property(x => x.FamilyStatus)
             .HasColumnType("family_status");

            e.Property(x => x.EducationType)
             .HasColumnType("education_type");

            e.Property(x => x.IncomeType)
             .HasColumnType("income_type");

            e.Property(x => x.OrganizationType)
             .HasColumnType("organization_type");

            e.Property(x => x.OccupationType)
             .HasColumnType("occupation_type");

            e.Property(x => x.HousingType)
             .HasColumnType("housing_type");

            e.Property(x => x.HousetypeMode)
             .HasColumnType("housetype_mode");

            e.Property(x => x.WallsmaterialMode)
             .HasColumnType("wallsmaterial");

            e.Property(x => x.Fondkapremont)
             .HasColumnType("fondkapremont");

            e.Property(x => x.EmergencyState)
             .HasColumnType("emergency_state");

            // CHECK constraints
            e.ToTable(t =>
            {
                t.HasCheckConstraint("CK_customers_cnt_children",       "cnt_children >= 0");
                t.HasCheckConstraint("CK_customers_cnt_fam_members",    "cnt_fam_members >= 1");
                t.HasCheckConstraint("CK_customers_amt_income_total",   "amt_income_total > 0");
                t.HasCheckConstraint("CK_customers_region_rating",      "region_rating_client BETWEEN 1 AND 3");
                t.HasCheckConstraint("CK_customers_region_rating_city", "region_rating_client_w_city BETWEEN 1 AND 3");
            });

            // Relations
            e.HasMany(x => x.LoanApplications)
             .WithOne(x => x.Customer)
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.CreditBureauHistories)
             .WithOne(x => x.Customer)
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.PreviousLoanApplications)
             .WithOne(x => x.Customer)
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.InstallmentPayments)
             .WithOne(x => x.Customer)
             .HasForeignKey(x => x.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ── LoanApplication ──────────────────────────────────────────────────
        modelBuilder.Entity<LoanApplication>(e =>
        {
            e.ToTable("loan_applications");
            e.HasKey(x => x.ApplicationId);
            e.Property(x => x.ApplicationId).UseIdentityAlwaysColumn();

            e.Property(x => x.NameContractType)
             .HasColumnType("contract_type");

            e.Property(x => x.NameTypeSuite)
             .HasColumnType("suite_type");

            e.Property(x => x.Status)
             .HasConversion(
                 v => v.ToString().ToLower(),
                 v => Enum.Parse<ApplicationStatus>(v, true))
             .HasDefaultValue(ApplicationStatus.Pending);

            e.ToTable(t =>
            {
                t.HasCheckConstraint("CK_loan_amt_credit",   "amt_credit > 0");
                t.HasCheckConstraint("CK_loan_amt_annuity",  "amt_annuity > 0");
                t.HasCheckConstraint("CK_loan_status",       "status IN ('pending','approved','rejected')");
            });

            e.HasMany(x => x.ModelPredictions)
             .WithOne(x => x.LoanApplication)
             .HasForeignKey(x => x.ApplicationId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ── CreditBureauHistory ───────────────────────────────────────────────
        modelBuilder.Entity<CreditBureauHistory>(e =>
        {
            e.ToTable("credit_bureau_history");
            e.HasKey(x => x.BureauId);
            e.Property(x => x.BureauId).UseIdentityAlwaysColumn();

            // Index cho aggregate nhanh
            e.HasIndex(x => x.CustomerId).HasDatabaseName("idx_bureau_customer");
        });

        // ── PreviousLoanApplication ───────────────────────────────────────────
        modelBuilder.Entity<PreviousLoanApplication>(e =>
        {
            e.ToTable("previous_loan_applications");
            e.HasKey(x => x.PrevAppId);
            e.Property(x => x.PrevAppId).UseIdentityAlwaysColumn();

            e.Property(x => x.NameContractStatus)
             .HasColumnType("contract_status");

            e.HasIndex(x => x.CustomerId).HasDatabaseName("idx_prev_app_customer");

            e.HasMany(x => x.InstallmentPayments)
             .WithOne(x => x.PreviousLoanApplication)
             .HasForeignKey(x => x.PrevAppId)
             .OnDelete(DeleteBehavior.SetNull);
        });

        // ── InstallmentPayment ────────────────────────────────────────────────
        modelBuilder.Entity<InstallmentPayment>(e =>
        {
            e.ToTable("installment_payments");
            e.HasKey(x => x.PaymentId);
            e.Property(x => x.PaymentId).UseIdentityAlwaysColumn();

            e.HasIndex(x => x.CustomerId).HasDatabaseName("idx_inst_customer");
        });

        // ── ModelPrediction ───────────────────────────────────────────────────
        modelBuilder.Entity<ModelPrediction>(e =>
        {
            e.ToTable("model_predictions");
            e.HasKey(x => x.PredictionId);
            e.Property(x => x.PredictionId).UseIdentityAlwaysColumn();

            e.Property(x => x.ModelType)
             .HasConversion<string>();

            e.Property(x => x.Result)
             .HasColumnType("prediction_result");

            e.ToTable(t =>
            {
                t.HasCheckConstraint("CK_pred_probability", "probability_default BETWEEN 0 AND 1");
                t.HasCheckConstraint("CK_pred_model_type",  "model_type IN ('ANN', 'LSTM')");
            });

            e.HasIndex(x => x.ApplicationId).HasDatabaseName("idx_pred_application");
            e.HasIndex(x => x.CustomerId).HasDatabaseName("idx_pred_customer");
        });
    }
}
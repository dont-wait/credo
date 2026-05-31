using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreditScoringAPI.Models.Enums;

namespace CreditScoringAPI.Models.Entities;

[Table("loan_applications")]
public class LoanApplication
{
    [Key]
    [Column("application_id")]
    public int ApplicationId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    // ── Thông tin khoản vay ──────────────────────────────────────────────────
    [Required]
    [Column("name_contract_type")]
    public ContractType NameContractType { get; set; }

    [Required]
    [Column("amt_credit", TypeName = "numeric(15,2)")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal AmtCredit { get; set; }

    [Required]
    [Column("amt_annuity", TypeName = "numeric(12,2)")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal AmtAnnuity { get; set; }

    [Column("amt_goods_price", TypeName = "numeric(15,2)")]
    public decimal? AmtGoodsPrice { get; set; }

    [Column("name_type_suite")]
    public SuiteType? NameTypeSuite { get; set; }

    // Server tự điền → dùng để tính WEEKDAY + HOUR features
    [Column("applied_at")]
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

    [Required, MaxLength(20)]
    [Column("status")]
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // ── Navigation ───────────────────────────────────────────────────────────
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public ICollection<ModelPrediction> ModelPredictions { get; set; } = new List<ModelPrediction>();
}
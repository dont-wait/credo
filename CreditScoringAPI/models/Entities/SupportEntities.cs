using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreditScoringAPI.Models.Enums;

namespace CreditScoringAPI.Models.Entities;

// =============================================================================
// BẢNG 3: credit_bureau_history  (BUREAU_*)
// =============================================================================
[Table("credit_bureau_history")]
public class CreditBureauHistory
{
    [Key]
    [Column("bureau_id")]
    public int BureauId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    // Ngày mở khoản tín dụng (âm = số ngày trước hiện tại)
    [Required]
    [Column("days_credit")]
    public int DaysCredit { get; set; }

    [Column("credit_day_overdue")]
    public int CreditDayOverdue { get; set; } = 0;

    [Column("days_credit_enddate")]
    public int? DaysCreditEnddate { get; set; }

    [Column("days_enddate_fact")]
    public int? DaysEnddateFact { get; set; }

    [Column("amt_credit_max_overdue", TypeName = "numeric(15,2)")]
    public decimal AmtCreditMaxOverdue { get; set; } = 0;

    [Column("cnt_credit_prolong")]
    public short CntCreditProlong { get; set; } = 0;

    [Column("amt_credit_sum", TypeName = "numeric(15,2)")]
    public decimal AmtCreditSum { get; set; } = 0;

    [Column("amt_credit_sum_debt", TypeName = "numeric(15,2)")]
    public decimal AmtCreditSumDebt { get; set; } = 0;

    [Column("amt_credit_sum_limit", TypeName = "numeric(15,2)")]
    public decimal? AmtCreditSumLimit { get; set; }

    [Column("amt_credit_sum_overdue", TypeName = "numeric(15,2)")]
    public decimal AmtCreditSumOverdue { get; set; } = 0;

    [Column("amt_annuity", TypeName = "numeric(12,2)")]
    public decimal? AmtAnnuity { get; set; }

    [MaxLength(50)]
    [Column("credit_type")]
    public string? CreditType { get; set; }

    [MaxLength(20)]
    [Column("credit_active")]
    public string? CreditActive { get; set; }

    [Column("reported_at")]
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

    // ── Navigation ───────────────────────────────────────────────────────────
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;
}


// =============================================================================
// BẢNG 4: previous_loan_applications  (PREV_*)
// =============================================================================
[Table("previous_loan_applications")]
public class PreviousLoanApplication
{
    [Key]
    [Column("prev_app_id")]
    public int PrevAppId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [Column("name_contract_status")]
    public ContractStatus NameContractStatus { get; set; }

    [Required]
    [Column("amt_application", TypeName = "numeric(15,2)")]
    public decimal AmtApplication { get; set; }

    [Column("amt_credit", TypeName = "numeric(15,2)")]
    public decimal? AmtCredit { get; set; }

    [Required]
    [Column("days_decision")]
    public int DaysDecision { get; set; }

    [Column("applied_at")]
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

    // ── Navigation ───────────────────────────────────────────────────────────
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public ICollection<InstallmentPayment> InstallmentPayments { get; set; } = new List<InstallmentPayment>();
}


// =============================================================================
// BẢNG 5: installment_payments  (INST_*)
// =============================================================================
[Table("installment_payments")]
public class InstallmentPayment
{
    [Key]
    [Column("payment_id")]
    public int PaymentId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("prev_app_id")]
    public int? PrevAppId { get; set; }

    [Column("num_instalment_version")]
    public short? NumInstalmentVersion { get; set; }

    [Column("num_instalment_number")]
    public short? NumInstalmentNumber { get; set; }

    [Required]
    [Column("days_instalment")]
    public int DaysInstalment { get; set; }

    [Column("days_entry_payment")]
    public int? DaysEntryPayment { get; set; }

    [Required]
    [Column("amt_instalment", TypeName = "numeric(12,2)")]
    public decimal AmtInstalment { get; set; }

    [Column("amt_payment", TypeName = "numeric(12,2)")]
    public decimal? AmtPayment { get; set; }

    [Column("recorded_at")]
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    // ── Navigation ───────────────────────────────────────────────────────────
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(PrevAppId))]
    public PreviousLoanApplication? PreviousLoanApplication { get; set; }
}
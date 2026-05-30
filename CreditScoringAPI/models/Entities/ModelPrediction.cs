using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreditScoringAPI.Models.Enums;

namespace CreditScoringAPI.Models.Entities;

// =============================================================================
// BẢNG 6: model_predictions
// =============================================================================
[Table("model_predictions")]
public class ModelPrediction
{
    [Key]
    [Column("prediction_id")]
    public int PredictionId { get; set; }

    [Column("application_id")]
    public int ApplicationId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [Column("model_type")]
    public ModelType ModelType { get; set; }                    // ANN | LSTM

    [Required]
    [Column("probability_default", TypeName = "numeric(8,6)")]
    public decimal ProbabilityDefault { get; set; }            // PD score ∈ [0,1]

    [Required]
    [Column("threshold_used", TypeName = "numeric(8,6)")]
    public decimal ThresholdUsed { get; set; }

    [Required]
    [Column("result")]
    public PredictionResult Result { get; set; }               // DEFAULT | NON_DEFAULT

    /// <summary>
    /// Snapshot 130 features đã scale — lưu để audit / retrain
    /// Lưu dạng JSON string, deserialize khi cần
    /// </summary>
    [Column("feature_vector", TypeName = "jsonb")]
    public string? FeatureVector { get; set; }

    [Column("predicted_at")]
    public DateTime PredictedAt { get; set; } = DateTime.UtcNow;

    // ── Navigation ───────────────────────────────────────────────────────────
    [ForeignKey(nameof(ApplicationId))]
    public LoanApplication LoanApplication { get; set; } = null!;

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;
}
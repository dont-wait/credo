using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreditScoringAPI.Models.Enums;

namespace CreditScoringAPI.Models.Entities;

[Table("customers")]
public class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    // ── Định danh ────────────────────────────────────────────────────────────
    [Required, MaxLength(100)]
    [Column("full_name")]
    public string FullName { get; set; } = null!;

    [Required, MaxLength(20)]
    [Column("id_number")]
    public string IdNumber { get; set; } = null!;         // CCCD/CMND — UNIQUE

    [Required]
    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }             // → DAYS_BIRTH

    [Required]
    [Column("gender")]
    public GenderType Gender { get; set; }

    [MaxLength(20)]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [MaxLength(100)]
    [Column("email")]
    public string? Email { get; set; }

    // ── Hôn nhân & gia đình ──────────────────────────────────────────────────
    [Column("family_status")]
    public FamilyStatus? FamilyStatus { get; set; }

    [Column("cnt_children")]
    [Range(0, short.MaxValue)]
    public short CntChildren { get; set; } = 0;

    [Column("cnt_fam_members")]
    [Range(1, short.MaxValue)]
    public short CntFamMembers { get; set; } = 1;

    // ── Học vấn & nghề nghiệp ────────────────────────────────────────────────
    [Column("education_type")]
    public EducationType? EducationType { get; set; }

    [Column("income_type")]
    public IncomeType? IncomeType { get; set; }

    [Column("occupation_type")]
    public OccupationType? OccupationType { get; set; }

    [Column("organization_type")]
    public OrganizationType? OrganizationType { get; set; }

    [Required]
    [Column("amt_income_total", TypeName = "numeric(15,2)")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal AmtIncomeTotal { get; set; }

    [Column("employment_since")]
    public DateOnly? EmploymentSince { get; set; }        // → DAYS_EMPLOYED

    // ── Liên lạc ─────────────────────────────────────────────────────────────
    [Column("flag_emp_phone")]
    public bool FlagEmpPhone { get; set; } = false;

    [Column("flag_work_phone")]
    public bool FlagWorkPhone { get; set; } = false;

    [Column("flag_phone")]
    public bool FlagPhone { get; set; } = false;

    [Column("flag_email")]
    public bool FlagEmail { get; set; } = false;

    // ── Tài sản ───────────────────────────────────────────────────────────────
    [Column("flag_own_car")]
    public bool FlagOwnCar { get; set; } = false;

    [Column("own_car_age")]
    public short? OwnCarAge { get; set; }

    [Column("flag_own_realty")]
    public bool FlagOwnRealty { get; set; } = false;

    // ── Nhà ở ────────────────────────────────────────────────────────────────
    [Column("housing_type")]
    public HousingType? HousingType { get; set; }

    [Column("housetype_mode")]
    public HouseTypeMode? HousetypeMode { get; set; }

    [Column("wallsmaterial_mode")]
    public WallsMaterial? WallsmaterialMode { get; set; }

    [Column("fondkapremont_mode")]
    public FondKapremont? Fondkapremont { get; set; }

    [Column("emergencystate_mode")]
    public EmergencyState? EmergencyState { get; set; }

    [Column("elevators_avg", TypeName = "numeric(6,4)")]
    public decimal? ElevatorsAvg { get; set; }

    [Column("floorsmax_avg", TypeName = "numeric(6,4)")]
    public decimal? FloorsMaxAvg { get; set; }

    [Column("floorsmax_mode", TypeName = "numeric(6,4)")]
    public decimal? FloorsMaxMode { get; set; }

    [Column("floorsmax_medi", TypeName = "numeric(6,4)")]
    public decimal? FloorsMaxMedi { get; set; }

    // ── Vùng địa lý ──────────────────────────────────────────────────────────
    [Column("region_rating_client")]
    [Range(1, 3)]
    public short? RegionRatingClient { get; set; }

    [Column("region_rating_client_w_city")]
    [Range(1, 3)]
    public short? RegionRatingClientWCity { get; set; }

    [Column("reg_region_not_work_region")]
    public bool RegRegionNotWorkRegion { get; set; } = false;

    [Column("live_region_not_work_region")]
    public bool LiveRegionNotWorkRegion { get; set; } = false;

    [Column("reg_city_not_live_city")]
    public bool RegCityNotLiveCity { get; set; } = false;

    [Column("reg_city_not_work_city")]
    public bool RegCityNotWorkCity { get; set; } = false;

    [Column("live_city_not_work_city")]
    public bool LiveCityNotWorkCity { get; set; } = false;

    // ── Vòng xã hội ──────────────────────────────────────────────────────────
    [Column("obs_30_cnt_social_circle")]
    public short Obs30CntSocialCircle { get; set; } = 0;

    [Column("def_30_cnt_social_circle")]
    public short Def30CntSocialCircle { get; set; } = 0;

    [Column("obs_60_cnt_social_circle")]
    public short Obs60CntSocialCircle { get; set; } = 0;

    [Column("def_60_cnt_social_circle")]
    public short Def60CntSocialCircle { get; set; } = 0;

    // ── Giấy tờ ──────────────────────────────────────────────────────────────
    [Column("flag_document_3")]
    public bool FlagDocument3 { get; set; } = false;

    [Column("flag_document_6")]
    public bool FlagDocument6 { get; set; } = false;

    [Column("flag_document_8")]
    public bool FlagDocument8 { get; set; } = false;

    // ── EXT_SOURCE (điểm tín dụng ngoài) ─────────────────────────────────────
    [Column("ext_source_1", TypeName = "numeric(8,6)")]
    [Range(typeof(decimal), "0", "1")]
    public decimal? ExtSource1 { get; set; }

    [Column("ext_source_2", TypeName = "numeric(8,6)")]
    [Range(typeof(decimal), "0", "1")]
    public decimal? ExtSource2 { get; set; }

    [Column("ext_source_3", TypeName = "numeric(8,6)")]
    [Range(typeof(decimal), "0", "1")]
    public decimal? ExtSource3 { get; set; }

    // ── Số lần tra CIC ────────────────────────────────────────────────────────
    [Column("amt_req_credit_bureau_day")]
    public short AmtReqCreditBureauDay { get; set; } = 0;

    [Column("amt_req_credit_bureau_mon")]
    public short AmtReqCreditBureauMon { get; set; } = 0;

    [Column("amt_req_credit_bureau_qrt")]
    public short AmtReqCreditBureauQrt { get; set; } = 0;

    [Column("amt_req_credit_bureau_year")]
    public short AmtReqCreditBureauYear { get; set; } = 0;

    // ── Ngày để tính DAYS_* ───────────────────────────────────────────────────
    [Column("registration_date")]
    public DateOnly? RegistrationDate { get; set; }

    [Column("id_publish_date")]
    public DateOnly? IdPublishDate { get; set; }

    [Column("last_phone_change_date")]
    public DateOnly? LastPhoneChangeDate { get; set; }

    // ── Metadata ─────────────────────────────────────────────────────────────
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // ── Navigation ───────────────────────────────────────────────────────────
    public ICollection<LoanApplication> LoanApplications { get; set; } = new List<LoanApplication>();
    public ICollection<CreditBureauHistory> CreditBureauHistories { get; set; } = new List<CreditBureauHistory>();
    public ICollection<PreviousLoanApplication> PreviousLoanApplications { get; set; } = new List<PreviousLoanApplication>();
    public ICollection<InstallmentPayment> InstallmentPayments { get; set; } = new List<InstallmentPayment>();
}
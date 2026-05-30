namespace CreditScoringAPI.Models.Enums;

public enum ContractType
{
    CashLoans,
    RevolvingLoans
}

public enum GenderType
{
    M,
    F
}

public enum SuiteType
{
    Unaccompanied,
    Family,
    SpousePartner,
    Children,
    OtherA,
    OtherB,
    GroupOfPeople
}

public enum IncomeType
{
    Working,
    StateServant,
    CommercialAssociate,
    Pensioner,
    Unemployed,
    Student,
    Businessman,
    MaternityLeave
}

public enum EducationType
{
    SecondarySpecial,
    HigherEducation,
    IncompleteHigher,
    LowerSecondary,
    AcademicDegree
}

public enum FamilyStatus
{
    SingleNotMarried,
    Married,
    CivilMarriage,
    Widow,
    Separated
}

public enum HousingType
{
    HouseApartment,
    RentedApartment,
    WithParents,
    MunicipalApartment,
    OfficeApartment,
    CoopApartment
}

public enum OccupationType
{
    Laborers,
    CoreStaff,
    Accountants,
    Managers,
    Drivers,
    SalesStaff,
    CleaningStaff,
    CookingStaff,
    PrivateServiceStaff,
    MedicineStaff,
    SecurityStaff,
    HighSkillTechStaff,
    WaitersBarmenStaff,
    LowSkillLaborers,
    RealtyAgents,
    Secretaries,
    ItStaff,
    HrStaff
}

public enum WallsMaterial
{
    StoneBrick,
    Block,
    Panel,
    Mixed,
    Wooden,
    Others,
    Monolith
}

public enum HouseTypeMode
{
    BlockOfFlats,
    TerracedHouse,
    SpecificHousing
}

public enum FondKapremont
{
    RegOperAccount,
    OrgSpecAccount,
    RegOperSpecAccount,
    NotSpecified
}

public enum EmergencyState
{
    No,
    Yes
}

public enum ContractStatus
{
    Approved,
    Refused,
    Canceled,
    UnusedOffer
}

public enum PredictionResult
{
    Default,
    NonDefault
}

public enum ApplicationStatus
{
    Pending,
    Approved,
    Rejected
}

public enum ModelType
{
    ANN,
    LSTM
}
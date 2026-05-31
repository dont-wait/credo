using NpgsqlTypes;

namespace CreditScoringAPI.Models.Enums;

public enum ContractType
{
    [PgName("Cash loans")] CashLoans,
    [PgName("Revolving loans")] RevolvingLoans
}

public enum GenderType
{
    [PgName("M")] M,
    [PgName("F")] F
}

public enum SuiteType
{
    [PgName("Unaccompanied")] Unaccompanied,
    [PgName("Family")] Family,
    [PgName("Spouse, partner")] SpousePartner,
    [PgName("Children")] Children,
    [PgName("Other_A")] OtherA,
    [PgName("Other_B")] OtherB,
    [PgName("Group of people")] GroupOfPeople
}

public enum IncomeType
{
    [PgName("Working")] Working,
    [PgName("State servant")] StateServant,
    [PgName("Commercial associate")] CommercialAssociate,
    [PgName("Pensioner")] Pensioner,
    [PgName("Unemployed")] Unemployed,
    [PgName("Student")] Student,
    [PgName("Businessman")] Businessman,
    [PgName("Maternity leave")] MaternityLeave
}

public enum EducationType
{
    [PgName("Secondary / secondary special")] SecondarySpecial,
    [PgName("Higher education")] HigherEducation,
    [PgName("Incomplete higher")] IncompleteHigher,
    [PgName("Lower secondary")] LowerSecondary,
    [PgName("Academic degree")] AcademicDegree
}

public enum FamilyStatus
{
    [PgName("Single / not married")] SingleNotMarried,
    [PgName("Married")] Married,
    [PgName("Civil marriage")] CivilMarriage,
    [PgName("Widow")] Widow,
    [PgName("Separated")] Separated
}

public enum HousingType
{
    [PgName("House / apartment")] HouseApartment,
    [PgName("Rented apartment")] RentedApartment,
    [PgName("With parents")] WithParents,
    [PgName("Municipal apartment")] MunicipalApartment,
    [PgName("Office apartment")] OfficeApartment,
    [PgName("Co-op apartment")] CoopApartment
}

public enum OccupationType
{
    [PgName("Laborers")] Laborers,
    [PgName("Core staff")] CoreStaff,
    [PgName("Accountants")] Accountants,
    [PgName("Managers")] Managers,
    [PgName("Drivers")] Drivers,
    [PgName("Sales staff")] SalesStaff,
    [PgName("Cleaning staff")] CleaningStaff,
    [PgName("Cooking staff")] CookingStaff,
    [PgName("Private service staff")] PrivateServiceStaff,
    [PgName("Medicine staff")] MedicineStaff,
    [PgName("Security staff")] SecurityStaff,
    [PgName("High skill tech staff")] HighSkillTechStaff,
    [PgName("Waiters/barmen staff")] WaitersBarmenStaff,
    [PgName("Low-skill Laborers")] LowSkillLaborers,
    [PgName("Realty agents")] RealtyAgents,
    [PgName("Secretaries")] Secretaries,
    [PgName("IT staff")] ItStaff,
    [PgName("HR staff")] HrStaff
}

public enum OrganizationType
{
    [PgName("Business Entity Type 1")] BusinessEntityType1,
    [PgName("Business Entity Type 2")] BusinessEntityType2,
    [PgName("Business Entity Type 3")] BusinessEntityType3,
    [PgName("School")] School,
    [PgName("Government")] Government,
    [PgName("Religion")] Religion,
    [PgName("Other")] Other,
    [PgName("XNA")] XNA,
    [PgName("Electricity")] Electricity,
    [PgName("Medicine")] Medicine,
    [PgName("Self-employed")] SelfEmployed,
    [PgName("Transport: type 1")] TransportType1,
    [PgName("Transport: type 2")] TransportType2,
    [PgName("Transport: type 3")] TransportType3,
    [PgName("Transport: type 4")] TransportType4,
    [PgName("Construction")] Construction,
    [PgName("Housing")] Housing,
    [PgName("Kindergarten")] Kindergarten,
    [PgName("Trade: type 1")] TradeType1,
    [PgName("Trade: type 2")] TradeType2,
    [PgName("Trade: type 3")] TradeType3,
    [PgName("Trade: type 4")] TradeType4,
    [PgName("Trade: type 5")] TradeType5,
    [PgName("Trade: type 6")] TradeType6,
    [PgName("Trade: type 7")] TradeType7,
    [PgName("Industry: type 1")] IndustryType1,
    [PgName("Industry: type 2")] IndustryType2,
    [PgName("Industry: type 3")] IndustryType3,
    [PgName("Industry: type 4")] IndustryType4,
    [PgName("Industry: type 5")] IndustryType5,
    [PgName("Industry: type 6")] IndustryType6,
    [PgName("Industry: type 7")] IndustryType7,
    [PgName("Industry: type 8")] IndustryType8,
    [PgName("Industry: type 9")] IndustryType9,
    [PgName("Industry: type 10")] IndustryType10,
    [PgName("Industry: type 11")] IndustryType11,
    [PgName("Industry: type 12")] IndustryType12,
    [PgName("Industry: type 13")] IndustryType13,
    [PgName("Military")] Military,
    [PgName("Services")] Services,
    [PgName("Security Ministries")] SecurityMinistries,
    [PgName("Police")] Police,
    [PgName("Postal")] Postal,
    [PgName("Agriculture")] Agriculture,
    [PgName("Restaurant")] Restaurant,
    [PgName("Culture")] Culture,
    [PgName("Hotel")] Hotel,
    [PgName("Bank")] Bank,
    [PgName("Insurance")] Insurance,
    [PgName("Mobile")] Mobile,
    [PgName("Legal Services")] LegalServices,
    [PgName("Advertising")] Advertising,
    [PgName("Cleaning")] Cleaning,
    [PgName("Telecom")] Telecom,
    [PgName("Realtor")] Realtor,
    [PgName("University")] University,
    [PgName("Emergency")] Emergency,
    [PgName("Security")] Security
}

public enum WallsMaterial
{
    [PgName("Stone, brick")] StoneBrick,
    [PgName("Block")] Block,
    [PgName("Panel")] Panel,
    [PgName("Mixed")] Mixed,
    [PgName("Wooden")] Wooden,
    [PgName("Others")] Others,
    [PgName("Monolith")] Monolith
}

public enum HouseTypeMode
{
    [PgName("block of flats")] BlockOfFlats,
    [PgName("terraced house")] TerracedHouse,
    [PgName("specific housing")] SpecificHousing
}

public enum FondKapremont
{
    [PgName("reg oper account")] RegOperAccount,
    [PgName("org spec account")] OrgSpecAccount,
    [PgName("reg oper spec account")] RegOperSpecAccount,
    [PgName("not specified")] NotSpecified
}

public enum EmergencyState
{
    [PgName("No")] No,
    [PgName("Yes")] Yes
}

public enum ContractStatus
{
    [PgName("Approved")] Approved,
    [PgName("Refused")] Refused,
    [PgName("Canceled")] Canceled,
    [PgName("Unused offer")] UnusedOffer
}

public enum PredictionResult
{
    [PgName("DEFAULT")] Default,
    [PgName("NON_DEFAULT")] NonDefault
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
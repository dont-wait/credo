using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditScoringAPI.Migrations
{
    /// <inheritdoc />
    public partial class SyncSchemaWithPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:contract_status.contract_status", "Approved,Refused,Canceled,Unused offer")
                .Annotation("Npgsql:Enum:contract_type.contract_type", "Cash loans,Revolving loans")
                .Annotation("Npgsql:Enum:education_type.education_type", "Secondary / secondary special,Higher education,Incomplete higher,Lower secondary,Academic degree")
                .Annotation("Npgsql:Enum:emergency_state.emergency_state", "No,Yes")
                .Annotation("Npgsql:Enum:family_status.family_status", "Single / not married,Married,Civil marriage,Widow,Separated")
                .Annotation("Npgsql:Enum:fondkapremont.fond_kapremont", "reg oper account,org spec account,reg oper spec account,not specified")
                .Annotation("Npgsql:Enum:gender_type.gender_type", "M,F")
                .Annotation("Npgsql:Enum:housetype_mode.house_type_mode", "block of flats,terraced house,specific housing")
                .Annotation("Npgsql:Enum:housing_type.housing_type", "House / apartment,Rented apartment,With parents,Municipal apartment,Office apartment,Co-op apartment")
                .Annotation("Npgsql:Enum:income_type.income_type", "Working,State servant,Commercial associate,Pensioner,Unemployed,Student,Businessman,Maternity leave")
                .Annotation("Npgsql:Enum:occupation_type.occupation_type", "Laborers,Core staff,Accountants,Managers,Drivers,Sales staff,Cleaning staff,Cooking staff,Private service staff,Medicine staff,Security staff,High skill tech staff,Waiters/barmen staff,Low-skill Laborers,Realty agents,Secretaries,IT staff,HR staff")
                .Annotation("Npgsql:Enum:organization_type.organization_type", "Business Entity Type 1,Business Entity Type 2,Business Entity Type 3,School,Government,Religion,Other,XNA,Electricity,Medicine,Self-employed,Transport: type 1,Transport: type 2,Transport: type 3,Transport: type 4,Construction,Housing,Kindergarten,Trade: type 1,Trade: type 2,Trade: type 3,Trade: type 4,Trade: type 5,Trade: type 6,Trade: type 7,Industry: type 1,Industry: type 2,Industry: type 3,Industry: type 4,Industry: type 5,Industry: type 6,Industry: type 7,Industry: type 8,Industry: type 9,Industry: type 10,Industry: type 11,Industry: type 12,Industry: type 13,Military,Services,Security Ministries,Police,Postal,Agriculture,Restaurant,Culture,Hotel,Bank,Insurance,Mobile,Legal Services,Advertising,Cleaning,Telecom,Realtor,University,Emergency,Security")
                .Annotation("Npgsql:Enum:prediction_result.prediction_result", "DEFAULT,NON_DEFAULT")
                .Annotation("Npgsql:Enum:suite_type.suite_type", "Unaccompanied,Family,Spouse, partner,Children,Other_A,Other_B,Group of people")
                .Annotation("Npgsql:Enum:wallsmaterial.walls_material", "Stone, brick,Block,Panel,Mixed,Wooden,Others,Monolith");

            migrationBuilder.AlterColumn<int>(
                name: "organization_type",
                table: "customers",
                type: "organization_type",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:contract_status.contract_status", "Approved,Refused,Canceled,Unused offer")
                .OldAnnotation("Npgsql:Enum:contract_type.contract_type", "Cash loans,Revolving loans")
                .OldAnnotation("Npgsql:Enum:education_type.education_type", "Secondary / secondary special,Higher education,Incomplete higher,Lower secondary,Academic degree")
                .OldAnnotation("Npgsql:Enum:emergency_state.emergency_state", "No,Yes")
                .OldAnnotation("Npgsql:Enum:family_status.family_status", "Single / not married,Married,Civil marriage,Widow,Separated")
                .OldAnnotation("Npgsql:Enum:fondkapremont.fond_kapremont", "reg oper account,org spec account,reg oper spec account,not specified")
                .OldAnnotation("Npgsql:Enum:gender_type.gender_type", "M,F")
                .OldAnnotation("Npgsql:Enum:housetype_mode.house_type_mode", "block of flats,terraced house,specific housing")
                .OldAnnotation("Npgsql:Enum:housing_type.housing_type", "House / apartment,Rented apartment,With parents,Municipal apartment,Office apartment,Co-op apartment")
                .OldAnnotation("Npgsql:Enum:income_type.income_type", "Working,State servant,Commercial associate,Pensioner,Unemployed,Student,Businessman,Maternity leave")
                .OldAnnotation("Npgsql:Enum:occupation_type.occupation_type", "Laborers,Core staff,Accountants,Managers,Drivers,Sales staff,Cleaning staff,Cooking staff,Private service staff,Medicine staff,Security staff,High skill tech staff,Waiters/barmen staff,Low-skill Laborers,Realty agents,Secretaries,IT staff,HR staff")
                .OldAnnotation("Npgsql:Enum:organization_type.organization_type", "Business Entity Type 1,Business Entity Type 2,Business Entity Type 3,School,Government,Religion,Other,XNA,Electricity,Medicine,Self-employed,Transport: type 1,Transport: type 2,Transport: type 3,Transport: type 4,Construction,Housing,Kindergarten,Trade: type 1,Trade: type 2,Trade: type 3,Trade: type 4,Trade: type 5,Trade: type 6,Trade: type 7,Industry: type 1,Industry: type 2,Industry: type 3,Industry: type 4,Industry: type 5,Industry: type 6,Industry: type 7,Industry: type 8,Industry: type 9,Industry: type 10,Industry: type 11,Industry: type 12,Industry: type 13,Military,Services,Security Ministries,Police,Postal,Agriculture,Restaurant,Culture,Hotel,Bank,Insurance,Mobile,Legal Services,Advertising,Cleaning,Telecom,Realtor,University,Emergency,Security")
                .OldAnnotation("Npgsql:Enum:prediction_result.prediction_result", "DEFAULT,NON_DEFAULT")
                .OldAnnotation("Npgsql:Enum:suite_type.suite_type", "Unaccompanied,Family,Spouse, partner,Children,Other_A,Other_B,Group of people")
                .OldAnnotation("Npgsql:Enum:wallsmaterial.walls_material", "Stone, brick,Block,Panel,Mixed,Wooden,Others,Monolith");

            migrationBuilder.AlterColumn<string>(
                name: "organization_type",
                table: "customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "organization_type",
                oldNullable: true);
        }
    }
}

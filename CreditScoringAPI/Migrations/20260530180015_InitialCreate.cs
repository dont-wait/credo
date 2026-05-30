using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CreditScoringAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    CREATE TYPE gender_type        AS ENUM ('M', 'F');
                    CREATE TYPE contract_type      AS ENUM ('Cash loans', 'Revolving loans');
                    CREATE TYPE suite_type         AS ENUM ('Unaccompanied', 'Family', 'Spouse, partner', 'Children', 'Other_A', 'Other_B', 'Group of people');
                    CREATE TYPE income_type        AS ENUM ('Working', 'State servant', 'Commercial associate', 'Pensioner', 'Unemployed', 'Student', 'Businessman', 'Maternity leave');
                    CREATE TYPE education_type     AS ENUM ('Secondary / secondary special', 'Higher education', 'Incomplete higher', 'Lower secondary', 'Academic degree');
                    CREATE TYPE family_status      AS ENUM ('Single / not married', 'Married', 'Civil marriage', 'Widow', 'Separated');
                    CREATE TYPE housing_type       AS ENUM ('House / apartment', 'Rented apartment', 'With parents', 'Municipal apartment', 'Office apartment', 'Co-op apartment');
                    CREATE TYPE occupation_type    AS ENUM ('Laborers', 'Core staff', 'Accountants', 'Managers', 'Drivers', 'Sales staff', 'Cleaning staff', 'Cooking staff', 'Private service staff', 'Medicine staff', 'Security staff', 'High skill tech staff', 'Waiters/barmen staff', 'Low-skill Laborers', 'Realty agents', 'Secretaries', 'IT staff', 'HR staff');
                    CREATE TYPE wallsmaterial      AS ENUM ('Stone, brick', 'Block', 'Panel', 'Mixed', 'Wooden', 'Others', 'Monolith');
                    CREATE TYPE housetype_mode     AS ENUM ('block of flats', 'terraced house', 'specific housing');
                    CREATE TYPE fondkapremont      AS ENUM ('reg oper account', 'org spec account', 'reg oper spec account', 'not specified');
                    CREATE TYPE emergency_state    AS ENUM ('No', 'Yes');
                    CREATE TYPE contract_status    AS ENUM ('Approved', 'Refused', 'Canceled', 'Unused offer');
                    CREATE TYPE prediction_result  AS ENUM ('DEFAULT', 'NON_DEFAULT');
                EXCEPTION WHEN duplicate_object THEN null;
                END $$;
            ");
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    id_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "gender_type", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    family_status = table.Column<string>(type: "family_status", nullable: true),
                    cnt_children = table.Column<short>(type: "smallint", nullable: false),
                    cnt_fam_members = table.Column<short>(type: "smallint", nullable: false),
                    education_type = table.Column<string>(type: "education_type", nullable: true),
                    income_type = table.Column<string>(type: "income_type", nullable: true),
                    occupation_type = table.Column<string>(type: "occupation_type", nullable: true),
                    organization_type = table.Column<string>(type: "text", nullable: true),
                    amt_income_total = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    employment_since = table.Column<DateOnly>(type: "date", nullable: true),
                    flag_emp_phone = table.Column<bool>(type: "boolean", nullable: false),
                    flag_work_phone = table.Column<bool>(type: "boolean", nullable: false),
                    flag_phone = table.Column<bool>(type: "boolean", nullable: false),
                    flag_email = table.Column<bool>(type: "boolean", nullable: false),
                    flag_own_car = table.Column<bool>(type: "boolean", nullable: false),
                    own_car_age = table.Column<short>(type: "smallint", nullable: true),
                    flag_own_realty = table.Column<bool>(type: "boolean", nullable: false),
                    housing_type = table.Column<string>(type: "housing_type", nullable: true),
                    housetype_mode = table.Column<string>(type: "housetype_mode", nullable: true),
                    wallsmaterial_mode = table.Column<string>(type: "wallsmaterial", nullable: true),
                    fondkapremont_mode = table.Column<string>(type: "fondkapremont", nullable: true),
                    emergencystate_mode = table.Column<string>(type: "emergency_state", nullable: true),
                    elevators_avg = table.Column<decimal>(type: "numeric(6,4)", nullable: true),
                    floorsmax_avg = table.Column<decimal>(type: "numeric(6,4)", nullable: true),
                    floorsmax_mode = table.Column<decimal>(type: "numeric(6,4)", nullable: true),
                    floorsmax_medi = table.Column<decimal>(type: "numeric(6,4)", nullable: true),
                    region_rating_client = table.Column<short>(type: "smallint", nullable: true),
                    region_rating_client_w_city = table.Column<short>(type: "smallint", nullable: true),
                    reg_region_not_work_region = table.Column<bool>(type: "boolean", nullable: false),
                    live_region_not_work_region = table.Column<bool>(type: "boolean", nullable: false),
                    reg_city_not_live_city = table.Column<bool>(type: "boolean", nullable: false),
                    reg_city_not_work_city = table.Column<bool>(type: "boolean", nullable: false),
                    live_city_not_work_city = table.Column<bool>(type: "boolean", nullable: false),
                    obs_30_cnt_social_circle = table.Column<short>(type: "smallint", nullable: false),
                    def_30_cnt_social_circle = table.Column<short>(type: "smallint", nullable: false),
                    obs_60_cnt_social_circle = table.Column<short>(type: "smallint", nullable: false),
                    def_60_cnt_social_circle = table.Column<short>(type: "smallint", nullable: false),
                    flag_document_3 = table.Column<bool>(type: "boolean", nullable: false),
                    flag_document_6 = table.Column<bool>(type: "boolean", nullable: false),
                    flag_document_8 = table.Column<bool>(type: "boolean", nullable: false),
                    ext_source_1 = table.Column<decimal>(type: "numeric(8,6)", nullable: true),
                    ext_source_2 = table.Column<decimal>(type: "numeric(8,6)", nullable: true),
                    ext_source_3 = table.Column<decimal>(type: "numeric(8,6)", nullable: true),
                    amt_req_credit_bureau_day = table.Column<short>(type: "smallint", nullable: false),
                    amt_req_credit_bureau_mon = table.Column<short>(type: "smallint", nullable: false),
                    amt_req_credit_bureau_qrt = table.Column<short>(type: "smallint", nullable: false),
                    amt_req_credit_bureau_year = table.Column<short>(type: "smallint", nullable: false),
                    registration_date = table.Column<DateOnly>(type: "date", nullable: true),
                    id_publish_date = table.Column<DateOnly>(type: "date", nullable: true),
                    last_phone_change_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                    table.CheckConstraint("CK_customers_amt_income_total", "amt_income_total > 0");
                    table.CheckConstraint("CK_customers_cnt_children", "cnt_children >= 0");
                    table.CheckConstraint("CK_customers_cnt_fam_members", "cnt_fam_members >= 1");
                    table.CheckConstraint("CK_customers_region_rating", "region_rating_client BETWEEN 1 AND 3");
                    table.CheckConstraint("CK_customers_region_rating_city", "region_rating_client_w_city BETWEEN 1 AND 3");
                });

            migrationBuilder.CreateTable(
                name: "credit_bureau_history",
                columns: table => new
                {
                    bureau_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    days_credit = table.Column<int>(type: "integer", nullable: false),
                    credit_day_overdue = table.Column<int>(type: "integer", nullable: false),
                    days_credit_enddate = table.Column<int>(type: "integer", nullable: true),
                    days_enddate_fact = table.Column<int>(type: "integer", nullable: true),
                    amt_credit_max_overdue = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    cnt_credit_prolong = table.Column<short>(type: "smallint", nullable: false),
                    amt_credit_sum = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    amt_credit_sum_debt = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    amt_credit_sum_limit = table.Column<decimal>(type: "numeric(15,2)", nullable: true),
                    amt_credit_sum_overdue = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    amt_annuity = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    credit_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    credit_active = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    reported_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credit_bureau_history", x => x.bureau_id);
                    table.ForeignKey(
                        name: "FK_credit_bureau_history_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loan_applications",
                columns: table => new
                {
                    application_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    name_contract_type = table.Column<string>(type: "contract_type", nullable: false),
                    amt_credit = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    amt_annuity = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    amt_goods_price = table.Column<decimal>(type: "numeric(15,2)", nullable: true),
                    name_type_suite = table.Column<string>(type: "suite_type", nullable: true),
                    applied_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "pending"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_applications", x => x.application_id);
                    table.CheckConstraint("CK_loan_amt_annuity", "amt_annuity > 0");
                    table.CheckConstraint("CK_loan_amt_credit", "amt_credit > 0");
                    table.CheckConstraint("CK_loan_status", "status IN ('pending','approved','rejected')");
                    table.ForeignKey(
                        name: "FK_loan_applications_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "previous_loan_applications",
                columns: table => new
                {
                    prev_app_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    name_contract_status = table.Column<string>(type: "contract_status", nullable: false),
                    amt_application = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    amt_credit = table.Column<decimal>(type: "numeric(15,2)", nullable: true),
                    days_decision = table.Column<int>(type: "integer", nullable: false),
                    applied_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_previous_loan_applications", x => x.prev_app_id);
                    table.ForeignKey(
                        name: "FK_previous_loan_applications_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "model_predictions",
                columns: table => new
                {
                    prediction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    application_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    model_type = table.Column<string>(type: "text", nullable: false),
                    probability_default = table.Column<decimal>(type: "numeric(8,6)", nullable: false),
                    threshold_used = table.Column<decimal>(type: "numeric(8,6)", nullable: false),
                    result = table.Column<string>(type: "prediction_result", nullable: false),
                    feature_vector = table.Column<string>(type: "jsonb", nullable: true),
                    predicted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model_predictions", x => x.prediction_id);
                    table.CheckConstraint("CK_pred_model_type", "model_type IN ('ANN', 'LSTM')");
                    table.CheckConstraint("CK_pred_probability", "probability_default BETWEEN 0 AND 1");
                    table.ForeignKey(
                        name: "FK_model_predictions_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_model_predictions_loan_applications_application_id",
                        column: x => x.application_id,
                        principalTable: "loan_applications",
                        principalColumn: "application_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "installment_payments",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    prev_app_id = table.Column<int>(type: "integer", nullable: true),
                    num_instalment_version = table.Column<short>(type: "smallint", nullable: true),
                    num_instalment_number = table.Column<short>(type: "smallint", nullable: true),
                    days_instalment = table.Column<int>(type: "integer", nullable: false),
                    days_entry_payment = table.Column<int>(type: "integer", nullable: true),
                    amt_instalment = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    amt_payment = table.Column<decimal>(type: "numeric(12,2)", nullable: true),
                    recorded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_installment_payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_installment_payments_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_installment_payments_previous_loan_applications_prev_app_id",
                        column: x => x.prev_app_id,
                        principalTable: "previous_loan_applications",
                        principalColumn: "prev_app_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "idx_bureau_customer",
                table: "credit_bureau_history",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_id_number",
                table: "customers",
                column: "id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_inst_customer",
                table: "installment_payments",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_installment_payments_prev_app_id",
                table: "installment_payments",
                column: "prev_app_id");

            migrationBuilder.CreateIndex(
                name: "IX_loan_applications_customer_id",
                table: "loan_applications",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_pred_application",
                table: "model_predictions",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "idx_pred_customer",
                table: "model_predictions",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "idx_prev_app_customer",
                table: "previous_loan_applications",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credit_bureau_history");

            migrationBuilder.DropTable(
                name: "installment_payments");

            migrationBuilder.DropTable(
                name: "model_predictions");

            migrationBuilder.DropTable(
                name: "previous_loan_applications");

            migrationBuilder.DropTable(
                name: "loan_applications");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}

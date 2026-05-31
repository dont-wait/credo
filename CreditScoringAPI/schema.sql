-- =============================================================================
-- CREDIT SCORING DEMO — PostgreSQL Schema
-- Dựa trên model ANN/LSTM (Home Credit Dataset)
-- 130 features → 5 nhóm nguồn dữ liệu
-- =============================================================================

-- ---------------------------------------------------------------------------
-- ENUMS
-- ---------------------------------------------------------------------------

CREATE TYPE contract_type     AS ENUM ('Cash loans', 'Revolving loans');
CREATE TYPE gender_type       AS ENUM ('M', 'F');
CREATE TYPE suite_type        AS ENUM ('Unaccompanied', 'Family', 'Spouse, partner', 'Children', 'Other_A', 'Other_B', 'Group of people');
CREATE TYPE income_type       AS ENUM ('Working', 'State servant', 'Commercial associate', 'Pensioner', 'Unemployed', 'Student', 'Businessman', 'Maternity leave');
CREATE TYPE education_type    AS ENUM ('Secondary / secondary special', 'Higher education', 'Incomplete higher', 'Lower secondary', 'Academic degree');
CREATE TYPE family_status     AS ENUM ('Single / not married', 'Married', 'Civil marriage', 'Widow', 'Separated');
CREATE TYPE housing_type      AS ENUM ('House / apartment', 'Rented apartment', 'With parents', 'Municipal apartment', 'Office apartment', 'Co-op apartment');
CREATE TYPE occupation_type   AS ENUM ('Laborers', 'Core staff', 'Accountants', 'Managers', 'Drivers', 'Sales staff', 'Cleaning staff', 'Cooking staff', 'Private service staff', 'Medicine staff', 'Security staff', 'High skill tech staff', 'Waiters/barmen staff', 'Low-skill Laborers', 'Realty agents', 'Secretaries', 'IT staff', 'HR staff');
CREATE TYPE organization_type AS ENUM ('Business Entity Type 1', 'Business Entity Type 2', 'Business Entity Type 3', 'School', 'Government', 'Religion', 'Other', 'XNA', 'Electricity', 'Medicine', 'Self-employed', 'Transport: type 1', 'Transport: type 2', 'Transport: type 3', 'Transport: type 4', 'Construction', 'Housing', 'Kindergarten', 'Trade: type 1', 'Trade: type 2', 'Trade: type 3', 'Trade: type 4', 'Trade: type 5', 'Trade: type 6', 'Trade: type 7', 'Industry: type 1', 'Industry: type 2', 'Industry: type 3', 'Industry: type 4', 'Industry: type 5', 'Industry: type 6', 'Industry: type 7', 'Industry: type 8', 'Industry: type 9', 'Industry: type 10', 'Industry: type 11', 'Industry: type 12', 'Industry: type 13', 'Military', 'Services', 'Security Ministries', 'Police', 'Postal', 'Agriculture', 'Restaurant', 'Culture', 'Hotel', 'Bank', 'Insurance', 'Mobile', 'Legal Services', 'Advertising', 'Cleaning', 'Telecom', 'Realtor', 'University', 'Emergency', 'Security');
CREATE TYPE wallsmaterial     AS ENUM ('Stone, brick', 'Block', 'Panel', 'Mixed', 'Wooden', 'Others', 'Monolith');
CREATE TYPE housetype_mode    AS ENUM ('block of flats', 'terraced house', 'specific housing');
CREATE TYPE fondkapremont     AS ENUM ('reg oper account', 'org spec account', 'reg oper spec account', 'not specified');
CREATE TYPE emergency_state   AS ENUM ('No', 'Yes');
CREATE TYPE contract_status   AS ENUM ('Approved', 'Refused', 'Canceled', 'Unused offer');
CREATE TYPE prediction_result AS ENUM ('DEFAULT', 'NON_DEFAULT');


-- =============================================================================
-- BẢNG 1: customers
-- Thông tin cá nhân — nhập từ form giao diện
-- =============================================================================
CREATE TABLE customers (
    customer_id         SERIAL          PRIMARY KEY,

    -- Định danh
    full_name           VARCHAR(100)    NOT NULL,
    id_number           VARCHAR(20)     NOT NULL UNIQUE,            -- CCCD/CMND
    date_of_birth       DATE            NOT NULL,                   -- → DAYS_BIRTH
    gender              gender_type     NOT NULL,                   -- → CODE_GENDER_*
    phone_number        VARCHAR(20),
    email               VARCHAR(100),

    -- Hôn nhân & gia đình
    family_status       family_status,                             -- → NAME_FAMILY_STATUS_*
    cnt_children        SMALLINT        NOT NULL DEFAULT 0          CHECK (cnt_children >= 0),
    cnt_fam_members     SMALLINT        NOT NULL DEFAULT 1          CHECK (cnt_fam_members >= 1),

    -- Học vấn & nghề nghiệp
    education_type      education_type,                            -- → NAME_EDUCATION_TYPE_*
    income_type         income_type,                               -- → NAME_INCOME_TYPE_*
    occupation_type     occupation_type,                           -- → OCCUPATION_TYPE_*
    organization_type   organization_type,                         -- → ORGANIZATION_TYPE_*
    amt_income_total    NUMERIC(15,2)   NOT NULL CHECK (amt_income_total > 0),
    employment_since    DATE,                                       -- → DAYS_EMPLOYED

    -- Liên lạc
    flag_emp_phone      BOOLEAN         NOT NULL DEFAULT FALSE,    -- → FLAG_EMP_PHONE
    flag_work_phone     BOOLEAN         NOT NULL DEFAULT FALSE,    -- → FLAG_WORK_PHONE
    flag_phone          BOOLEAN         NOT NULL DEFAULT FALSE,    -- → FLAG_PHONE
    flag_email          BOOLEAN         NOT NULL DEFAULT FALSE,    -- → FLAG_EMAIL

    -- Tài sản
    flag_own_car        BOOLEAN         NOT NULL DEFAULT FALSE,    -- → FLAG_OWN_CAR
    own_car_age         SMALLINT,                                  -- → OWN_CAR_AGE (năm)
    flag_own_realty     BOOLEAN         NOT NULL DEFAULT FALSE,    -- → FLAG_OWN_REALTY

    -- Nhà ở
    housing_type        housing_type,                              -- → NAME_HOUSING_TYPE_*
    housetype_mode      housetype_mode,                            -- → HOUSETYPE_MODE_*
    wallsmaterial_mode  wallsmaterial,                             -- → WALLSMATERIAL_MODE_*
    fondkapremont_mode  fondkapremont,                             -- → FONDKAPREMONT_MODE_*
    emergencystate_mode emergency_state,                           -- → EMERGENCYSTATE_MODE
    elevators_avg       NUMERIC(6,4),                              -- → ELEVATORS_AVG
    floorsmax_avg       NUMERIC(6,4),                              -- → FLOORSMAX_AVG
    floorsmax_mode      NUMERIC(6,4),                              -- → FLOORSMAX_MODE
    floorsmax_medi      NUMERIC(6,4),                              -- → FLOORSMAX_MEDI

    -- Vùng địa lý
    region_rating_client        SMALLINT CHECK (region_rating_client BETWEEN 1 AND 3),
    region_rating_client_w_city SMALLINT CHECK (region_rating_client_w_city BETWEEN 1 AND 3),
    reg_region_not_work_region  BOOLEAN  NOT NULL DEFAULT FALSE,
    live_region_not_work_region BOOLEAN  NOT NULL DEFAULT FALSE,
    reg_city_not_live_city      BOOLEAN  NOT NULL DEFAULT FALSE,
    reg_city_not_work_city      BOOLEAN  NOT NULL DEFAULT FALSE,
    live_city_not_work_city     BOOLEAN  NOT NULL DEFAULT FALSE,

    -- Vòng xã hội (30 / 60 ngày)
    obs_30_cnt_social_circle    SMALLINT DEFAULT 0,
    def_30_cnt_social_circle    SMALLINT DEFAULT 0,
    obs_60_cnt_social_circle    SMALLINT DEFAULT 0,
    def_60_cnt_social_circle    SMALLINT DEFAULT 0,

    -- Giấy tờ nộp kèm
    flag_document_3             BOOLEAN NOT NULL DEFAULT FALSE,
    flag_document_6             BOOLEAN NOT NULL DEFAULT FALSE,
    flag_document_8             BOOLEAN NOT NULL DEFAULT FALSE,

    -- Điểm tín dụng ngoài (EXT_SOURCE — từ CIC / đối tác)
    ext_source_1    NUMERIC(8,6),    -- [0, 1]
    ext_source_2    NUMERIC(8,6),    -- [0, 1]
    ext_source_3    NUMERIC(8,6),    -- [0, 1]

    -- Số lần tra CIC
    amt_req_credit_bureau_day   SMALLINT DEFAULT 0,
    amt_req_credit_bureau_mon   SMALLINT DEFAULT 0,
    amt_req_credit_bureau_qrt   SMALLINT DEFAULT 0,
    amt_req_credit_bureau_year  SMALLINT DEFAULT 0,

    -- Ngày đăng ký & đổi phone (để tính DAYS_REGISTRATION, DAYS_ID_PUBLISH, DAYS_LAST_PHONE_CHANGE)
    registration_date           DATE,
    id_publish_date             DATE,
    last_phone_change_date      DATE,

    -- Metadata
    created_at      TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

COMMENT ON TABLE customers IS 'Thông tin khách hàng — nhập từ form. Tương ứng application_train (cột chính).';
COMMENT ON COLUMN customers.employment_since   IS 'Ngày bắt đầu làm việc → DAYS_EMPLOYED = TODAY - employment_since';
COMMENT ON COLUMN customers.date_of_birth      IS 'Ngày sinh → DAYS_BIRTH = TODAY - date_of_birth';
COMMENT ON COLUMN customers.ext_source_1       IS 'Điểm tín dụng ngoài nguồn 1, do CIC/đối tác cấp';
COMMENT ON COLUMN customers.registration_date  IS 'Ngày đăng ký hộ khẩu/KT → DAYS_REGISTRATION';
COMMENT ON COLUMN customers.id_publish_date    IS 'Ngày cấp CMND/CCCD → DAYS_ID_PUBLISH';


-- =============================================================================
-- BẢNG 2: loan_applications
-- Thông tin đơn vay — nhập từ form giao diện
-- =============================================================================
CREATE TABLE loan_applications (
    application_id      SERIAL          PRIMARY KEY,
    customer_id         INT             NOT NULL REFERENCES customers(customer_id) ON DELETE CASCADE,

    -- Thông tin khoản vay
    name_contract_type  contract_type   NOT NULL,                  -- → NAME_CONTRACT_TYPE
    amt_credit          NUMERIC(15,2)   NOT NULL CHECK (amt_credit > 0),
    amt_annuity         NUMERIC(12,2)   NOT NULL CHECK (amt_annuity > 0),
    amt_goods_price     NUMERIC(15,2),                             -- → AMT_GOODS_PRICE
    name_type_suite     suite_type,                                -- → NAME_TYPE_SUITE_*

    -- Thời điểm nộp đơn (server tự lấy NOW())
    applied_at          TIMESTAMPTZ     NOT NULL DEFAULT NOW(),    -- → WEEKDAY_APPR_PROCESS_START_*, HOUR_APPR_PROCESS_START

    -- Trạng thái & kết quả
    status              VARCHAR(20)     NOT NULL DEFAULT 'pending' CHECK (status IN ('pending','approved','rejected')),
    created_at          TIMESTAMPTZ     NOT NULL DEFAULT NOW()
);

COMMENT ON TABLE loan_applications IS 'Đơn vay — 1 khách hàng có thể có nhiều đơn vay theo thời gian.';
COMMENT ON COLUMN loan_applications.applied_at IS 'Server tự điền NOW() → trích WEEKDAY và HOUR để OHE.';


-- =============================================================================
-- BẢNG 3: credit_bureau_history  (BUREAU_*)
-- Lịch sử tín dụng từ CIC — server query/aggregate để ra 11 features
-- =============================================================================
CREATE TABLE credit_bureau_history (
    bureau_id           SERIAL          PRIMARY KEY,
    customer_id         INT             NOT NULL REFERENCES customers(customer_id) ON DELETE CASCADE,

    -- Ngày mở khoản tín dụng (âm = số ngày trước hiện tại, giống raw data)
    days_credit         INT             NOT NULL,                  -- → BUREAU_DAYS_CREDIT_*
    credit_day_overdue  INT             NOT NULL DEFAULT 0,
    days_credit_enddate INT,
    days_enddate_fact   INT,

    -- Số dư & dư nợ
    amt_credit_max_overdue  NUMERIC(15,2) DEFAULT 0,              -- → BUREAU_AMT_CREDIT_MAX_OVERDUE_*
    cnt_credit_prolong      SMALLINT      NOT NULL DEFAULT 0,     -- → BUREAU_CNT_CREDIT_PROLONG_SUM
    amt_credit_sum          NUMERIC(15,2) DEFAULT 0,              -- → BUREAU_AMT_CREDIT_SUM_*
    amt_credit_sum_debt     NUMERIC(15,2) DEFAULT 0,              -- → BUREAU_AMT_CREDIT_SUM_DEBT_*
    amt_credit_sum_limit    NUMERIC(15,2),
    amt_credit_sum_overdue  NUMERIC(15,2) DEFAULT 0,
    amt_annuity             NUMERIC(12,2),

    credit_type             VARCHAR(50),
    credit_active           VARCHAR(20),

    reported_at     TIMESTAMPTZ     NOT NULL DEFAULT NOW()
);

COMMENT ON TABLE credit_bureau_history IS 'Lịch sử tín dụng từ CIC. Server aggregate theo customer_id → 11 BUREAU_* features.';

-- Index để aggregate nhanh
CREATE INDEX idx_bureau_customer ON credit_bureau_history(customer_id);


-- =============================================================================
-- BẢNG 4: previous_loan_applications  (PREV_*)
-- Lịch sử đơn vay trước tại cùng tổ chức — server aggregate → 6 features
-- =============================================================================
CREATE TABLE previous_loan_applications (
    prev_app_id         SERIAL          PRIMARY KEY,
    customer_id         INT             NOT NULL REFERENCES customers(customer_id) ON DELETE CASCADE,

    -- Thông tin đơn
    name_contract_status    contract_status NOT NULL,             -- → PREV_REFUSED_RATE
    amt_application         NUMERIC(15,2)   NOT NULL,             -- → PREV_AMT_APPLICATION_*
    amt_credit              NUMERIC(15,2),                        -- → PREV_AMT_CREDIT_MEAN

    -- Ngày quyết định (âm = số ngày trước hiện tại)
    days_decision           INT             NOT NULL,             -- → PREV_DAYS_DECISION_*

    -- Metadata
    applied_at              TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

COMMENT ON TABLE previous_loan_applications IS 'Lịch sử đơn vay cũ tại cùng tổ chức. Server aggregate → 6 PREV_* features.';

CREATE INDEX idx_prev_app_customer ON previous_loan_applications(customer_id);


-- =============================================================================
-- BẢNG 5: installment_payments  (INST_*)
-- Lịch sử trả góp — server aggregate → 5 features
-- =============================================================================
CREATE TABLE installment_payments (
    payment_id          SERIAL          PRIMARY KEY,
    customer_id         INT             NOT NULL REFERENCES customers(customer_id) ON DELETE CASCADE,
    prev_app_id         INT             REFERENCES previous_loan_applications(prev_app_id),

    -- Kỳ góp
    num_instalment_version  SMALLINT,
    num_instalment_number   SMALLINT,

    -- Ngày (âm = số ngày trước hiện tại)
    days_instalment         INT         NOT NULL,                 -- ngày đến hạn
    days_entry_payment      INT,                                  -- ngày thực tế trả

    -- Số tiền
    amt_instalment          NUMERIC(12,2) NOT NULL,              -- → INST_PAYMENT_DIFF_*
    amt_payment             NUMERIC(12,2),                       -- thực tế trả

    recorded_at             TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

COMMENT ON TABLE installment_payments IS 'Lịch sử trả góp. Server tính PAYMENT_DIFF, DAYS_PAST_DUE, IS_LATE rồi aggregate → 5 INST_* features.';

CREATE INDEX idx_inst_customer ON installment_payments(customer_id);


-- =============================================================================
-- BẢNG 6: model_predictions
-- Lưu lại kết quả dự đoán của model cho mỗi lần gọi
-- =============================================================================
CREATE TABLE model_predictions (
    prediction_id       SERIAL          PRIMARY KEY,
    application_id      INT             NOT NULL REFERENCES loan_applications(application_id) ON DELETE CASCADE,
    customer_id         INT             NOT NULL REFERENCES customers(customer_id),

    -- Output model
    model_type          VARCHAR(10)     NOT NULL CHECK (model_type IN ('ANN', 'LSTM')),
    probability_default NUMERIC(8,6)    NOT NULL CHECK (probability_default BETWEEN 0 AND 1),
    threshold_used      NUMERIC(8,6)    NOT NULL,
    result              prediction_result NOT NULL,               -- 'DEFAULT' | 'NON_DEFAULT'

    -- Snapshot 130 features đã scale (để audit / re-train)
    feature_vector      JSONB,

    predicted_at        TIMESTAMPTZ     NOT NULL DEFAULT NOW()
);

COMMENT ON TABLE model_predictions IS 'Log kết quả dự đoán. feature_vector lưu 130 features đã scale để audit.';

CREATE INDEX idx_pred_application ON model_predictions(application_id);
CREATE INDEX idx_pred_customer    ON model_predictions(customer_id);


-- =============================================================================
-- VIEW: v_model_input_raw
-- Server gọi view này → đủ data thô để tính 130 features → scale → model
-- =============================================================================
CREATE OR REPLACE VIEW v_model_input_raw AS
SELECT
    -- ── IDs ──────────────────────────────────────────────────────────────────
    la.application_id,
    c.customer_id,

    -- ── Nhóm 1A: Thông tin vay (từ loan_applications) ────────────────────────
    la.name_contract_type,
    la.amt_credit,
    la.amt_annuity,
    la.amt_goods_price,
    la.name_type_suite,
    EXTRACT(DOW FROM la.applied_at)::SMALLINT               AS weekday_appr_process_start,  -- 0=Sun..6=Sat
    EXTRACT(HOUR FROM la.applied_at)::SMALLINT              AS hour_appr_process_start,

    -- ── Nhóm 1B: Thông tin khách hàng (từ customers) ─────────────────────────
    c.gender,
    c.family_status,
    c.education_type,
    c.income_type,
    c.occupation_type,
    c.organization_type,
    c.housing_type,
    c.housetype_mode,
    c.wallsmaterial_mode,
    c.fondkapremont_mode,
    c.emergencystate_mode,

    c.amt_income_total,
    c.cnt_children,
    c.cnt_fam_members,

    -- DAYS_* → tính từ ngày hiện tại (âm theo convention Home Credit)
    -(CURRENT_DATE - c.date_of_birth)                       AS days_birth,
    CASE WHEN c.employment_since IS NULL THEN NULL
         ELSE -(CURRENT_DATE - c.employment_since)          END AS days_employed,
    CASE WHEN c.registration_date IS NULL THEN NULL
         ELSE -(CURRENT_DATE - c.registration_date)         END AS days_registration,
    CASE WHEN c.id_publish_date IS NULL THEN NULL
         ELSE -(CURRENT_DATE - c.id_publish_date)           END AS days_id_publish,
    CASE WHEN c.last_phone_change_date IS NULL THEN NULL
         ELSE -(CURRENT_DATE - c.last_phone_change_date)    END AS days_last_phone_change,

    c.flag_own_car,
    c.own_car_age,
    c.flag_own_realty,
    c.flag_emp_phone,
    c.flag_work_phone,
    c.flag_phone,
    c.flag_email,
    c.flag_document_3,
    c.flag_document_6,
    c.flag_document_8,

    c.region_rating_client,
    c.region_rating_client_w_city,
    c.reg_region_not_work_region,
    c.live_region_not_work_region,
    c.reg_city_not_live_city,
    c.reg_city_not_work_city,
    c.live_city_not_work_city,

    c.elevators_avg,
    c.floorsmax_avg,
    c.floorsmax_mode,
    c.floorsmax_medi,

    c.obs_30_cnt_social_circle,
    c.def_30_cnt_social_circle,
    c.obs_60_cnt_social_circle,
    c.def_60_cnt_social_circle,

    c.ext_source_1,
    c.ext_source_2,
    c.ext_source_3,

    c.amt_req_credit_bureau_day,
    c.amt_req_credit_bureau_mon,
    c.amt_req_credit_bureau_qrt,
    c.amt_req_credit_bureau_year,

    -- ── Nhóm 2: Derived (server tự tính) ─────────────────────────────────────
    la.amt_credit / NULLIF(c.amt_income_total, 0)           AS credit_income_percent,
    c.amt_income_total / NULLIF(c.cnt_fam_members, 0)       AS income_per_person,
    -- days_employed_percent tính sau khi có cả 2 days

    -- ── Nhóm 3: BUREAU aggregate (LEFT JOIN để khách hàng mới vẫn có NULL) ───
    bur.bureau_days_credit_min,
    bur.bureau_days_credit_max,
    bur.bureau_days_credit_mean,
    bur.bureau_amt_credit_sum_max,
    bur.bureau_amt_credit_sum_mean,
    bur.bureau_amt_credit_sum_sum,
    bur.bureau_amt_credit_sum_debt_mean,
    bur.bureau_amt_credit_sum_debt_sum,
    bur.bureau_amt_credit_max_overdue_max,
    bur.bureau_amt_credit_max_overdue_mean,
    bur.bureau_cnt_credit_prolong_sum,

    -- ── Nhóm 4: PREV aggregate ───────────────────────────────────────────────
    prv.prev_amt_application_mean,
    prv.prev_amt_application_max,
    prv.prev_amt_credit_mean,
    prv.prev_days_decision_mean,
    prv.prev_days_decision_min,
    prv.prev_refused_rate,

    -- ── Nhóm 5: INSTALLMENTS aggregate ───────────────────────────────────────
    ins.inst_payment_diff_mean,
    ins.inst_payment_diff_sum,
    ins.inst_days_past_due_mean,
    ins.inst_days_past_due_max,
    ins.inst_num_late

FROM loan_applications la
JOIN customers c ON c.customer_id = la.customer_id

-- BUREAU aggregate
LEFT JOIN (
    SELECT
        customer_id,
        MIN(days_credit)                    AS bureau_days_credit_min,
        MAX(days_credit)                    AS bureau_days_credit_max,
        AVG(days_credit)                    AS bureau_days_credit_mean,
        MAX(amt_credit_sum)                 AS bureau_amt_credit_sum_max,
        AVG(amt_credit_sum)                 AS bureau_amt_credit_sum_mean,
        SUM(amt_credit_sum)                 AS bureau_amt_credit_sum_sum,
        AVG(amt_credit_sum_debt)            AS bureau_amt_credit_sum_debt_mean,
        SUM(amt_credit_sum_debt)            AS bureau_amt_credit_sum_debt_sum,
        MAX(amt_credit_max_overdue)         AS bureau_amt_credit_max_overdue_max,
        AVG(amt_credit_max_overdue)         AS bureau_amt_credit_max_overdue_mean,
        SUM(cnt_credit_prolong)             AS bureau_cnt_credit_prolong_sum
    FROM credit_bureau_history
    GROUP BY customer_id
) bur ON bur.customer_id = c.customer_id

-- PREV APPLICATION aggregate
LEFT JOIN (
    SELECT
        customer_id,
        AVG(amt_application)                                        AS prev_amt_application_mean,
        MAX(amt_application)                                        AS prev_amt_application_max,
        AVG(amt_credit)                                             AS prev_amt_credit_mean,
        AVG(days_decision)                                          AS prev_days_decision_mean,
        MIN(days_decision)                                          AS prev_days_decision_min,
        AVG((name_contract_status = 'Refused')::INT)               AS prev_refused_rate
    FROM previous_loan_applications
    GROUP BY customer_id
) prv ON prv.customer_id = c.customer_id

-- INSTALLMENTS aggregate
LEFT JOIN (
    SELECT
        customer_id,
        AVG(amt_instalment - COALESCE(amt_payment, 0))                                              AS inst_payment_diff_mean,
        SUM(amt_instalment - COALESCE(amt_payment, 0))                                              AS inst_payment_diff_sum,
        AVG(GREATEST(COALESCE(days_entry_payment, days_instalment) - days_instalment, 0))           AS inst_days_past_due_mean,
        MAX(GREATEST(COALESCE(days_entry_payment, days_instalment) - days_instalment, 0))           AS inst_days_past_due_max,
        SUM((GREATEST(COALESCE(days_entry_payment, days_instalment) - days_instalment, 0) > 0)::INT) AS inst_num_late
    FROM installment_payments
    GROUP BY customer_id
) ins ON ins.customer_id = c.customer_id;

COMMENT ON VIEW v_model_input_raw IS 'Server SELECT * FROM v_model_input_raw WHERE application_id = ? → đủ data thô → Python encode + scale → model.';

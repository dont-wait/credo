namespace CreditScoringAPI.Models
{
    public class CreditRequest
    {
        public double AMT_INCOME_TOTAL { get; set; }

        public double AMT_CREDIT { get; set; }

        public double AMT_ANNUITY { get; set; }

        public int DAYS_BIRTH { get; set; }

        public int DAYS_EMPLOYED { get; set; }

        public double CNT_FAM_MEMBERS { get; set; }
    }
}
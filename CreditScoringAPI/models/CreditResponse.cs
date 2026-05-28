namespace CreditScoringAPI.Models
{
    public class CreditResponse
    {
        public double ProbabilityDefault { get; set; }

        public int CreditScore { get; set; }

        public string RiskGroup { get; set; }

        public string Decision { get; set; }
    }
}
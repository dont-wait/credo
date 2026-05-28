using CreditScoringAPI.Models;

namespace CreditScoringAPI.Services
{
    public class PredictionService
    {
        public CreditResponse Predict(CreditRequest req)
        {
            // fake predict demo
            double prob =
                (req.AMT_CREDIT / req.AMT_INCOME_TOTAL) * 0.1;

            if (prob > 1)
                prob = 1;

            int score =
                (int)(750 - prob * 600);

            string risk;
            string decision;

            if (score >= 670)
            {
                risk = "Xuất sắc";
                decision = "APPROVE";
            }
            else if (score >= 620)
            {
                risk = "Tốt";
                decision = "APPROVE";
            }
            else if (score >= 570)
            {
                risk = "Trung bình khá";
                decision = "APPROVE";
            }
            else if (score >= 300)
            {
                risk = "Trung bình";
                decision = "MANUAL REVIEW";
            }
            else
            {
                risk = "Kém";
                decision = "DECLINE";
            }

            return new CreditResponse
            {
                ProbabilityDefault = prob,
                CreditScore = score,
                RiskGroup = risk,
                Decision = decision
            };
        }
    }
}
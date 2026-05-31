using CreditScoringAPI.Models.Entities;

namespace CreditScoringAPI.Repository;

public interface IUnitOfWork : IDisposable
{
    IRepository<Customer>                Customers                { get; }
    IRepository<LoanApplication>         LoanApplications         { get; }
    IRepository<CreditBureauHistory>     CreditBureauHistories    { get; }
    IRepository<PreviousLoanApplication> PreviousLoanApplications { get; }
    IRepository<InstallmentPayment>      InstallmentPayments      { get; }
    IRepository<ModelPrediction>         ModelPredictions         { get; }

    Task<int> SaveChangesAsync();
}
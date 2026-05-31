using CreditScoringAPI.Models.Entities;

namespace CreditScoringAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<Customer>                Customers                { get; }
    public IRepository<LoanApplication>         LoanApplications         { get; }
    public IRepository<CreditBureauHistory>     CreditBureauHistories    { get; }
    public IRepository<PreviousLoanApplication> PreviousLoanApplications { get; }
    public IRepository<InstallmentPayment>      InstallmentPayments      { get; }
    public IRepository<ModelPrediction>         ModelPredictions         { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Customers                = new GenericRepository<Customer>(context);
        LoanApplications         = new GenericRepository<LoanApplication>(context);
        CreditBureauHistories    = new GenericRepository<CreditBureauHistory>(context);
        PreviousLoanApplications = new GenericRepository<PreviousLoanApplication>(context);
        InstallmentPayments      = new GenericRepository<InstallmentPayment>(context);
        ModelPredictions         = new GenericRepository<ModelPrediction>(context);
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
        => _context.Dispose();
}
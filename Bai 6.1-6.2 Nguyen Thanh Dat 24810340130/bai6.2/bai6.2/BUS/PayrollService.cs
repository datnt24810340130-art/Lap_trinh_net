using bai6._2.DAL;

namespace bai6._2.BUS;

public sealed class PayrollService
{
    private readonly PayrollRepository _repository = new();

    public int CloseMonth(string month) => _repository.CloseMonth(MonthValidator.Normalize(month));
}

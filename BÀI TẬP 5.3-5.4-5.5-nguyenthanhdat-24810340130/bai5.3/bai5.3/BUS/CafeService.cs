using System.Data;
using bai5._3.DAL;
using bai5._3.DTO;

namespace bai5._3.BUS;

public sealed class CafeService
{
    private readonly CafeRepository _repository = new();

    public List<DiningTableDto> GetTables() => _repository.GetTables();
    public List<MenuCategoryDto> GetCategories() => _repository.GetCategories();
    public DataTable GetMenuItems(int? categoryId = null) => _repository.GetMenuItems(categoryId);
    public DataTable GetOrderItems(int tableId) => _repository.GetOrderItems(tableId);

    public void AddOrderItem(int tableId, int menuItemId, int quantity)
    {
        if (tableId <= 0 || menuItemId <= 0)
        {
            throw new ArgumentException("Vui lòng chọn bàn và món ăn hợp lệ.");
        }
        if (quantity <= 0)
        {
            throw new ArgumentException("Số lượng phải lớn hơn 0.");
        }

        _repository.AddOrderItem(tableId, menuItemId, quantity);
    }

    public decimal PayTable(int tableId) => _repository.PayTable(tableId);

    public void MoveTable(int sourceTableId, int targetTableId)
    {
        if (sourceTableId <= 0 || targetTableId <= 0 || sourceTableId == targetTableId)
        {
            throw new ArgumentException("Vui lòng chọn hai bàn khác nhau và hợp lệ.");
        }
        _repository.MoveTable(sourceTableId, targetTableId);
    }

    public void AddCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tên danh mục không được để trống.");
        }
        _repository.AddCategory(name.Trim());
    }

    public void DeleteCategory(int categoryId) => _repository.DeleteCategory(categoryId);

    public void SaveMenuItem(int? menuItemId, string name, int categoryId, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tên món không được để trống.");
        }
        if (categoryId <= 0)
        {
            throw new ArgumentException("Vui lòng chọn danh mục.");
        }
        if (price < 0)
        {
            throw new ArgumentException("Đơn giá không được âm.");
        }

        _repository.SaveMenuItem(menuItemId, name.Trim(), categoryId, price);
    }

    public void DeleteMenuItem(int menuItemId) => _repository.DeleteMenuItem(menuItemId);
}
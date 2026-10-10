namespace bai5._2
{
    public partial class Form1 : Form
    {
        // Service model class
        private class Service
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            public Service(string name, decimal price)
            {
                Name = name;
                Price = price;
            }

            public override string ToString()
            {
                return $"{Name} - {Price:N0} VNĐ";
            }
        }

        // Service categories data
        private Dictionary<string, List<Service>> serviceData = new()
        {
            {
                "Khám bệnh",
                new List<Service>
                {
                    new Service("Khám tổng quát", 100000),
                    new Service("Khám ngoài giờ", 150000),
                    new Service("Khám chuyên khoa", 200000)
                }
            },
            {
                "Xét nghiệm",
                new List<Service>
                {
                    new Service("Xét nghiệm máu", 50000),
                    new Service("Xét nghiệm nước tiểu", 30000),
                    new Service("Xét nghiệm chức năng gan", 100000),
                    new Service("Xét nghiệm chức năng thận", 100000)
                }
            },
            {
                "Chụp X-Quang",
                new List<Service>
                {
                    new Service("Chụp ngực", 150000),
                    new Service("Chụp sống lưng", 150000),
                    new Service("Chụp tay chân", 100000),
                    new Service("Chụp cắt lớp", 250000)
                }
            },
            {
                "Vắc-xin",
                new List<Service>
                {
                    new Service("Vắc-xin phòng bệnh cumDD", 200000),
                    new Service("Vắc-xin thủy đậu", 150000),
                    new Service("Vắc-xin viêm gan B", 100000),
                    new Service("Vắc-xin bạch hầu", 80000)
                }
            }
        };

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Populate ComboBox with categories
            cboCategory.DataSource = serviceData.Keys.ToList();
            cboCategory.SelectedIndex = 0;

            // Load initial services for the first category
            LoadServicesByCategory();
        }

        private void LoadServicesByCategory()
        {
            string selectedCategory = cboCategory.SelectedItem?.ToString();
            if (selectedCategory != null && serviceData.TryGetValue(selectedCategory, out var services))
            {
                lstAvailableServices.DataSource = new List<Service>(services);
            }
        }

        private void CalculateTotalCost()
        {
            decimal total = 0;
            foreach (Service service in lstSelectedServices.Items)
            {
                total += service.Price;
            }

            txtTotalBeforeDiscount.Text = total.ToString("N0") + " VNĐ";

            // Calculate discounted price
            if (decimal.TryParse(txtDiscountPercent.Text, out decimal discountPercent) && discountPercent >= 0 && discountPercent <= 100)
            {
                decimal discount = total * (discountPercent / 100);
                decimal finalCost = total - discount;
                txtFinalCost.Text = finalCost.ToString("N0") + " VNĐ";
            }
            else
            {
                txtFinalCost.Text = total.ToString("N0") + " VNĐ";
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadServicesByCategory();
            lstAvailableServices.SelectedIndex = -1;
        }

        private void lstAvailableServices_DoubleClick(object sender, EventArgs e)
        {
            TransferToSelected();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            TransferToSelected();
        }

        private void TransferToSelected()
        {
            if (lstAvailableServices.SelectedItem != null)
            {
                Service selectedService = (Service)lstAvailableServices.SelectedItem;
                var currentItems = lstSelectedServices.Items.Cast<Service>().ToList();
                currentItems.Add(selectedService);
                lstSelectedServices.DataSource = new List<Service>(currentItems);
                CalculateTotalCost();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelectedServices.SelectedItem != null)
            {
                var currentItems = lstSelectedServices.Items.Cast<Service>().ToList();
                currentItems.RemoveAt(lstSelectedServices.SelectedIndex);
                lstSelectedServices.DataSource = new List<Service>(currentItems);
                CalculateTotalCost();
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            lstSelectedServices.DataSource = new List<Service>();
            CalculateTotalCost();
        }

        private void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }
    }
}

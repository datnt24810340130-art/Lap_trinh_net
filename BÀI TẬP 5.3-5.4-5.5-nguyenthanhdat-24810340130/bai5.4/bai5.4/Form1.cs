using bai5._4.BUS;
using bai5._4.DAL;
using bai5._4.GUI;

namespace bai5._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Controls.Add(new HotelDashboard(new HotelBus(new HotelDal())));
        }
    }
}

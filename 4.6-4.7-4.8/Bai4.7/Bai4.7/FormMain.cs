namespace Bai4._7;

public partial class FormMain : Form
{
    private readonly ToolStripStatusLabel statusMessageLabel = new("Sẵn sàng");
    private readonly ToolStripStatusLabel userLabel = new("Người dùng: Admin");
    private readonly ToolStripStatusLabel clockLabel = new();
    private readonly System.Windows.Forms.Timer clockTimer = new();

    public FormMain()
    {
        InitializeComponent();
        clockTimer.Interval = 1000;
        clockTimer.Tick += (_, _) => clockLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        clockTimer.Start();
        clockLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

    private void InitializeComponent()
    {
        var menuStrip = new MenuStrip();
        var systemMenu = new ToolStripMenuItem("Hệ thống");
        var logoutItem = new ToolStripMenuItem("Đăng xuất", null, (_, _) =>
        {
            statusMessageLabel.Text = "Đã đăng xuất";
            MessageBox.Show("Đã đăng xuất khỏi hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
        var exitItem = new ToolStripMenuItem("Thoát", null, (_, _) => Close());
        systemMenu.DropDownItems.AddRange([logoutItem, new ToolStripSeparator(), exitItem]);

        var catalogMenu = new ToolStripMenuItem("Danh mục");
        var studentItem = new ToolStripMenuItem("Quản lý Sinh viên", null, (_, _) => OpenChild<FormStudent>());
        var productItem = new ToolStripMenuItem("Quản lý Sản phẩm", null, (_, _) => OpenChild<FormProduct>());
        catalogMenu.DropDownItems.AddRange([studentItem, productItem]);

        var windowMenu = new ToolStripMenuItem("Cửa sổ");
        windowMenu.DropDownItems.AddRange([
            new ToolStripMenuItem("Cascade", null, (_, _) => LayoutMdi(MdiLayout.Cascade)),
            new ToolStripMenuItem("Tile Horizontal", null, (_, _) => LayoutMdi(MdiLayout.TileHorizontal)),
            new ToolStripMenuItem("Tile Vertical", null, (_, _) => LayoutMdi(MdiLayout.TileVertical))
        ]);
        menuStrip.Items.AddRange([systemMenu, catalogMenu, windowMenu]);

        var toolStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
        toolStrip.Items.Add(new ToolStripButton("Sinh viên", null, (_, _) => OpenChild<FormStudent>()) { DisplayStyle = ToolStripItemDisplayStyle.Text, ToolTipText = "Mở quản lý sinh viên" });
        toolStrip.Items.Add(new ToolStripButton("Sản phẩm", null, (_, _) => OpenChild<FormProduct>()) { DisplayStyle = ToolStripItemDisplayStyle.Text, ToolTipText = "Mở quản lý sản phẩm" });
        toolStrip.Items.Add(new ToolStripSeparator());
        toolStrip.Items.Add(new ToolStripButton("Cascade", null, (_, _) => LayoutMdi(MdiLayout.Cascade)) { DisplayStyle = ToolStripItemDisplayStyle.Text });
        toolStrip.Items.Add(new ToolStripButton("Tile H", null, (_, _) => LayoutMdi(MdiLayout.TileHorizontal)) { DisplayStyle = ToolStripItemDisplayStyle.Text });
        toolStrip.Items.Add(new ToolStripButton("Tile V", null, (_, _) => LayoutMdi(MdiLayout.TileVertical)) { DisplayStyle = ToolStripItemDisplayStyle.Text });

        var statusStrip = new StatusStrip();
        statusStrip.Items.Add(userLabel);
        statusStrip.Items.Add(new ToolStripStatusLabel { Spring = true });
        statusStrip.Items.Add(statusMessageLabel);
        statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
        statusStrip.Items.Add(clockLabel);

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Mở quản lý sinh viên", null, (_, _) => OpenChild<FormStudent>());
        contextMenu.Items.Add("Mở quản lý sản phẩm", null, (_, _) => OpenChild<FormProduct>());

        MainMenuStrip = menuStrip;
        IsMdiContainer = true;
        Text = "Hệ thống quản lý - MDI";
        WindowState = FormWindowState.Maximized;
        Controls.Add(statusStrip);
        Controls.Add(toolStrip);
        Controls.Add(menuStrip);
        ContextMenuStrip = contextMenu;
    }

    private void OpenChild<T>() where T : Form, new()
    {
        var existingChild = MdiChildren.OfType<T>().FirstOrDefault();
        if (existingChild is not null)
        {
            existingChild.Activate();
            return;
        }

        var child = new T { MdiParent = this };
        if (child is FormStudent student)
        {
            student.StatusChanged += ChildStatusChanged;
        }

        child.FormClosed += (_, _) =>
        {
            if (child is FormStudent closedStudent)
            {
                closedStudent.StatusChanged -= ChildStatusChanged;
            }
        };
        child.Show();
    }

    private void ChildStatusChanged(object? sender, StatusMessageEventArgs e)
    {
        statusMessageLabel.Text = e.Message;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            clockTimer.Stop();
            clockTimer.Dispose();
        }

        base.Dispose(disposing);
    }
}

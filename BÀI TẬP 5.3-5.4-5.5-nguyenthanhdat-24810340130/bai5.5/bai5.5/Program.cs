using bai5._5.BUS;
using bai5._5.DAL;
using bai5._5.GUI;

ApplicationConfiguration.Initialize();

var connectionFactory = new DbConnectionFactory();
var materialBus = new MaterialBus(new MaterialDal(connectionFactory));
var supplierBus = new SupplierBus(new SupplierDal(connectionFactory));
var inventoryBus = new InventoryBus(new InventoryDal(connectionFactory));

Application.Run(new MainForm(materialBus, supplierBus, inventoryBus));

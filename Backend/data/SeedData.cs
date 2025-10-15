using Microsoft.EntityFrameworkCore;
using Backend.Contexts;
using Backend.Models;

namespace Backend.Data;

public static class SeedData
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Đảm bảo database được tạo
        await context.Database.EnsureCreatedAsync();

        // Seed Users
        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new User { Username = "admin", Password = "123456", FullName = "Quản trị viên", Role = "admin" },
                new User { Username = "staff01", Password = "123456", FullName = "Nguyễn Văn A", Role = "staff" },
                new User { Username = "staff02", Password = "123456", FullName = "Lê Thị B", Role = "staff" }
            };
            await context.Users.AddRangeAsync(users);
        }

        // Seed Customers
        if (!await context.Customers.AnyAsync())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Khách hàng 1", Phone = "0909000001", Email = "kh1@mail.com", Address = "Địa chỉ 1" },
                new Customer { Name = "Khách hàng 2", Phone = "0909000002", Email = "kh2@mail.com", Address = "Địa chỉ 2" },
                new Customer { Name = "Khách hàng 3", Phone = "0909000003", Email = "kh3@mail.com", Address = "Địa chỉ 3" },
                new Customer { Name = "Khách hàng 4", Phone = "0909000004", Email = "kh4@mail.com", Address = "Địa chỉ 4" },
                new Customer { Name = "Khách hàng 5", Phone = "0909000005", Email = "kh5@mail.com", Address = "Địa chỉ 5" },
                new Customer { Name = "Khách hàng 6", Phone = "0909000006", Email = "kh6@mail.com", Address = "Địa chỉ 6" },
                new Customer { Name = "Khách hàng 7", Phone = "0909000007", Email = "kh7@mail.com", Address = "Địa chỉ 7" },
                new Customer { Name = "Khách hàng 8", Phone = "0909000008", Email = "kh8@mail.com", Address = "Địa chỉ 8" },
                new Customer { Name = "Khách hàng 9", Phone = "0909000009", Email = "kh9@mail.com", Address = "Địa chỉ 9" },
                new Customer { Name = "Khách hàng 10", Phone = "0909000010", Email = "kh10@mail.com", Address = "Địa chỉ 10" },
                new Customer { Name = "Khách hàng 11", Phone = "0909000011", Email = "kh11@mail.com", Address = "Địa chỉ 11" },
                new Customer { Name = "Khách hàng 12", Phone = "0909000012", Email = "kh12@mail.com", Address = "Địa chỉ 12" },
                new Customer { Name = "Khách hàng 13", Phone = "0909000013", Email = "kh13@mail.com", Address = "Địa chỉ 13" },
                new Customer { Name = "Khách hàng 14", Phone = "0909000014", Email = "kh14@mail.com", Address = "Địa chỉ 14" },
                new Customer { Name = "Khách hàng 15", Phone = "0909000015", Email = "kh15@mail.com", Address = "Địa chỉ 15" },
                new Customer { Name = "Khách hàng 16", Phone = "0909000016", Email = "kh16@mail.com", Address = "Địa chỉ 16" },
                new Customer { Name = "Khách hàng 17", Phone = "0909000017", Email = "kh17@mail.com", Address = "Địa chỉ 17" },
                new Customer { Name = "Khách hàng 18", Phone = "0909000018", Email = "kh18@mail.com", Address = "Địa chỉ 18" },
                new Customer { Name = "Khách hàng 19", Phone = "0909000019", Email = "kh19@mail.com", Address = "Địa chỉ 19" },
                new Customer { Name = "Khách hàng 20", Phone = "0909000020", Email = "kh20@mail.com", Address = "Địa chỉ 20" }
            };
            await context.Customers.AddRangeAsync(customers);
        }

        // Seed Categories
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new Category { CategoryName = "Đồ uống" },
                new Category { CategoryName = "Bánh kẹo" },
                new Category { CategoryName = "Gia vị" },
                new Category { CategoryName = "Đồ gia dụng" },
                new Category { CategoryName = "Mỹ phẩm" }
            };
            await context.Categories.AddRangeAsync(categories);
        }

        // Seed Suppliers
        if (!await context.Suppliers.AnyAsync())
        {
            var suppliers = new List<Supplier>
            {
                new Supplier { Name = "Công ty ABC", Phone = "0909123456", Email = "abc@gmail.com", Address = "Hà Nội" },
                new Supplier { Name = "Công ty XYZ", Phone = "0912123456", Email = "xyz@gmail.com", Address = "TP HCM" },
                new Supplier { Name = "Công ty 123", Phone = "0933123456", Email = "123@gmail.com", Address = "Đà Nẵng" }
            };
            await context.Suppliers.AddRangeAsync(suppliers);
        }

        await context.SaveChangesAsync();

        // Seed Products
        if (!await context.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new Product { CategoryId = 2, SupplierId = 1, ProductName = "Coca Cola lon", Barcode = "8900000000001", Price = 314838, Unit = "hộp" },
                new Product { CategoryId = 1, SupplierId = 3, ProductName = "Pepsi lon", Barcode = "8900000000002", Price = 114807, Unit = "cái" },
                new Product { CategoryId = 3, SupplierId = 3, ProductName = "Trà Xanh 0 độ", Barcode = "8900000000003", Price = 415725, Unit = "tuýp" },
                new Product { CategoryId = 2, SupplierId = 1, ProductName = "Sting dâu", Barcode = "8900000000004", Price = 351670, Unit = "cái" },
                new Product { CategoryId = 3, SupplierId = 2, ProductName = "Red Bull", Barcode = "8900000000005", Price = 402179, Unit = "lon" },
                new Product { CategoryId = 2, SupplierId = 2, ProductName = "Bánh Oreo", Barcode = "8900000000006", Price = 209283, Unit = "chai" },
                new Product { CategoryId = 5, SupplierId = 3, ProductName = "Bánh Chocopie", Barcode = "8900000000007", Price = 212528, Unit = "lon" },
                new Product { CategoryId = 1, SupplierId = 2, ProductName = "Kẹo Alpenliebe", Barcode = "8900000000008", Price = 34313, Unit = "lon" },
                new Product { CategoryId = 5, SupplierId = 1, ProductName = "Kẹo bạc hà", Barcode = "8900000000009", Price = 316289, Unit = "cái" },
                new Product { CategoryId = 1, SupplierId = 2, ProductName = "Socola KitKat", Barcode = "8900000000010", Price = 139959, Unit = "chai" },
                new Product { CategoryId = 5, SupplierId = 1, ProductName = "Nước mắm Nam Ngư", Barcode = "8900000000011", Price = 51792, Unit = "chai" },
                new Product { CategoryId = 2, SupplierId = 2, ProductName = "Nước tương Maggi", Barcode = "8900000000012", Price = 462539, Unit = "lon" },
                new Product { CategoryId = 5, SupplierId = 3, ProductName = "Muối i-ốt", Barcode = "8900000000013", Price = 173302, Unit = "cái" },
                new Product { CategoryId = 1, SupplierId = 1, ProductName = "Bột ngọt Ajinomoto", Barcode = "8900000000014", Price = 443069, Unit = "cái" },
                new Product { CategoryId = 2, SupplierId = 2, ProductName = "Dầu ăn Tường An", Barcode = "8900000000015", Price = 281354, Unit = "tuýp" },
                new Product { CategoryId = 2, SupplierId = 1, ProductName = "Nồi cơm điện", Barcode = "8900000000016", Price = 405347, Unit = "hộp" },
                new Product { CategoryId = 1, SupplierId = 3, ProductName = "Ấm siêu tốc", Barcode = "8900000000017", Price = 113087, Unit = "chai" },
                new Product { CategoryId = 3, SupplierId = 2, ProductName = "Quạt máy", Barcode = "8900000000018", Price = 69968, Unit = "hộp" },
                new Product { CategoryId = 4, SupplierId = 1, ProductName = "Bếp gas mini", Barcode = "8900000000019", Price = 416845, Unit = "lon" },
                new Product { CategoryId = 3, SupplierId = 3, ProductName = "Máy xay sinh tố", Barcode = "8900000000020", Price = 334564, Unit = "hộp" },
                new Product { CategoryId = 1, SupplierId = 1, ProductName = "Sữa rửa mặt Hazeline", Barcode = "8900000000021", Price = 188475, Unit = "lon" },
                new Product { CategoryId = 4, SupplierId = 1, ProductName = "Kem dưỡng da Pond's", Barcode = "8900000000022", Price = 413840, Unit = "hộp" },
                new Product { CategoryId = 3, SupplierId = 3, ProductName = "Dầu gội Sunsilk", Barcode = "8900000000023", Price = 158950, Unit = "tuýp" },
                new Product { CategoryId = 4, SupplierId = 2, ProductName = "Sữa tắm Dove", Barcode = "8900000000024", Price = 336928, Unit = "chai" },
                new Product { CategoryId = 1, SupplierId = 1, ProductName = "Nước hoa Romano", Barcode = "8900000000025", Price = 352508, Unit = "cái" },
                new Product { CategoryId = 1, SupplierId = 1, ProductName = "Cà phê G7", Barcode = "8900000000026", Price = 201228, Unit = "lon" },
                new Product { CategoryId = 2, SupplierId = 1, ProductName = "Trà Lipton", Barcode = "8900000000027", Price = 38039, Unit = "cái" },
                new Product { CategoryId = 2, SupplierId = 3, ProductName = "Sữa Vinamilk", Barcode = "8900000000028", Price = 252845, Unit = "chai" },
                new Product { CategoryId = 3, SupplierId = 1, ProductName = "Sữa TH True Milk", Barcode = "8900000000029", Price = 35278, Unit = "hộp" },
                new Product { CategoryId = 3, SupplierId = 2, ProductName = "Nước suối Lavie", Barcode = "8900000000030", Price = 331637, Unit = "lon" },
                new Product { CategoryId = 5, SupplierId = 3, ProductName = "Khăn giấy Tempo", Barcode = "8900000000031", Price = 102525, Unit = "chai" },
                new Product { CategoryId = 4, SupplierId = 3, ProductName = "Giấy vệ sinh Pulppy", Barcode = "8900000000032", Price = 495429, Unit = "chai" },
                new Product { CategoryId = 3, SupplierId = 2, ProductName = "Bình nước Lock&Lock", Barcode = "8900000000033", Price = 354771, Unit = "gói" },
                new Product { CategoryId = 2, SupplierId = 1, ProductName = "Hộp nhựa Tupperware", Barcode = "8900000000034", Price = 297415, Unit = "cái" },
                new Product { CategoryId = 1, SupplierId = 3, ProductName = "Dao Inox", Barcode = "8900000000035", Price = 47523, Unit = "hộp" },
                new Product { CategoryId = 3, SupplierId = 1, ProductName = "Bàn chải Colgate", Barcode = "8900000000036", Price = 136417, Unit = "chai" },
                new Product { CategoryId = 2, SupplierId = 2, ProductName = "Kem đánh răng P/S", Barcode = "8900000000037", Price = 93713, Unit = "hộp" },
                new Product { CategoryId = 2, SupplierId = 3, ProductName = "Nước súc miệng Listerine", Barcode = "8900000000038", Price = 223906, Unit = "gói" },
                new Product { CategoryId = 1, SupplierId = 2, ProductName = "Bông tẩy trang", Barcode = "8900000000039", Price = 317819, Unit = "tuýp" },
                new Product { CategoryId = 4, SupplierId = 1, ProductName = "Khẩu trang 3M", Barcode = "8900000000040", Price = 464252, Unit = "gói" },
                new Product { CategoryId = 3, SupplierId = 1, ProductName = "Bánh mì sandwich", Barcode = "8900000000041", Price = 279350, Unit = "cái" },
                new Product { CategoryId = 5, SupplierId = 2, ProductName = "Mì gói Hảo Hảo", Barcode = "8900000000042", Price = 9413, Unit = "hộp" },
                new Product { CategoryId = 1, SupplierId = 2, ProductName = "Mì Omachi", Barcode = "8900000000043", Price = 26616, Unit = "hộp" },
                new Product { CategoryId = 5, SupplierId = 2, ProductName = "Bún khô", Barcode = "8900000000044", Price = 350911, Unit = "gói" },
                new Product { CategoryId = 3, SupplierId = 1, ProductName = "Phở ăn liền", Barcode = "8900000000045", Price = 407779, Unit = "tuýp" },
                new Product { CategoryId = 1, SupplierId = 1, ProductName = "Nước ngọt Sprite", Barcode = "8900000000046", Price = 230083, Unit = "hộp" },
                new Product { CategoryId = 1, SupplierId = 3, ProductName = "Trà sữa đóng chai", Barcode = "8900000000047", Price = 15130, Unit = "cái" },
                new Product { CategoryId = 3, SupplierId = 3, ProductName = "Snack Oishi", Barcode = "8900000000048", Price = 43415, Unit = "cái" },
                new Product { CategoryId = 4, SupplierId = 2, ProductName = "Snack Lay's", Barcode = "8900000000049", Price = 83536, Unit = "tuýp" },
                new Product { CategoryId = 1, SupplierId = 2, ProductName = "Kẹo dẻo Haribo", Barcode = "8900000000050", Price = 328680, Unit = "cái" }
            };
            await context.Products.AddRangeAsync(products);
        }

        await context.SaveChangesAsync();

        // Seed Inventory
        if (!await context.Inventory.AnyAsync())
        {
            var inventory = new List<Inventory>
            {
                new Inventory { ProductId = 1, Quantity = 25 },
                new Inventory { ProductId = 2, Quantity = 169 },
                new Inventory { ProductId = 3, Quantity = 77 },
                new Inventory { ProductId = 4, Quantity = 169 },
                new Inventory { ProductId = 5, Quantity = 90 },
                new Inventory { ProductId = 6, Quantity = 105 },
                new Inventory { ProductId = 7, Quantity = 125 },
                new Inventory { ProductId = 8, Quantity = 37 },
                new Inventory { ProductId = 9, Quantity = 74 },
                new Inventory { ProductId = 10, Quantity = 149 },
                new Inventory { ProductId = 11, Quantity = 69 },
                new Inventory { ProductId = 12, Quantity = 23 },
                new Inventory { ProductId = 13, Quantity = 46 },
                new Inventory { ProductId = 14, Quantity = 144 },
                new Inventory { ProductId = 15, Quantity = 134 },
                new Inventory { ProductId = 16, Quantity = 182 },
                new Inventory { ProductId = 17, Quantity = 99 },
                new Inventory { ProductId = 18, Quantity = 72 },
                new Inventory { ProductId = 19, Quantity = 128 },
                new Inventory { ProductId = 20, Quantity = 123 },
                new Inventory { ProductId = 21, Quantity = 155 },
                new Inventory { ProductId = 22, Quantity = 78 },
                new Inventory { ProductId = 23, Quantity = 166 },
                new Inventory { ProductId = 24, Quantity = 117 },
                new Inventory { ProductId = 25, Quantity = 168 },
                new Inventory { ProductId = 26, Quantity = 197 },
                new Inventory { ProductId = 27, Quantity = 36 },
                new Inventory { ProductId = 28, Quantity = 145 },
                new Inventory { ProductId = 29, Quantity = 61 },
                new Inventory { ProductId = 30, Quantity = 139 },
                new Inventory { ProductId = 31, Quantity = 47 },
                new Inventory { ProductId = 32, Quantity = 154 },
                new Inventory { ProductId = 33, Quantity = 194 },
                new Inventory { ProductId = 34, Quantity = 41 },
                new Inventory { ProductId = 35, Quantity = 154 },
                new Inventory { ProductId = 36, Quantity = 71 },
                new Inventory { ProductId = 37, Quantity = 49 },
                new Inventory { ProductId = 38, Quantity = 165 },
                new Inventory { ProductId = 39, Quantity = 73 },
                new Inventory { ProductId = 40, Quantity = 176 },
                new Inventory { ProductId = 41, Quantity = 41 },
                new Inventory { ProductId = 42, Quantity = 34 },
                new Inventory { ProductId = 43, Quantity = 175 },
                new Inventory { ProductId = 44, Quantity = 59 },
                new Inventory { ProductId = 45, Quantity = 198 },
                new Inventory { ProductId = 46, Quantity = 106 },
                new Inventory { ProductId = 47, Quantity = 99 },
                new Inventory { ProductId = 48, Quantity = 55 },
                new Inventory { ProductId = 49, Quantity = 62 },
                new Inventory { ProductId = 50, Quantity = 33 }
            };
            await context.Inventory.AddRangeAsync(inventory);
        }

        // Seed Promotions
        if (!await context.Promotions.AnyAsync())
        {
            var promotions = new List<Promotion>
            {
                new Promotion { PromoCode = "SALE10", Description = "Giảm 10% cho mọi đơn hàng", DiscountType = "percent", DiscountValue = 10, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 0, UsageLimit = 0, Status = "active" },
                new Promotion { PromoCode = "FREESHIP50K", Description = "Giảm 50,000 cho đơn từ 300,000 trở lên", DiscountType = "fixed", DiscountValue = 50000, StartDate = new DateTime(2025, 3, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 300000, UsageLimit = 500, Status = "active" },
                new Promotion { PromoCode = "NEWUSER", Description = "Giảm 20% cho khách hàng mới", DiscountType = "percent", DiscountValue = 20, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 6, 30), MinOrderAmount = 0, UsageLimit = 1, Status = "active" },
                new Promotion { PromoCode = "SUMMER15", Description = "Giảm 15% mùa hè", DiscountType = "percent", DiscountValue = 15, StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 8, 31), MinOrderAmount = 50000, UsageLimit = 1000, Status = "active" },
                new Promotion { PromoCode = "VIP100K", Description = "Giảm 100,000 cho đơn từ 1 triệu", DiscountType = "fixed", DiscountValue = 100000, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), MinOrderAmount = 1000000, UsageLimit = 200, Status = "active" }
            };
            await context.Promotions.AddRangeAsync(promotions);
        }

        // Seed Orders
        if (!await context.Orders.AnyAsync())
        {
            var orders = new List<Order>
            {
                new Order { CustomerId = 5, UserId = 3, PromoId = 5, Status = "paid", TotalAmount = 1292330, DiscountAmount = 100000 },
                new Order { CustomerId = 17, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 1731608, DiscountAmount = 0 },
                new Order { CustomerId = 8, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 720782, DiscountAmount = 0 },
                new Order { CustomerId = 20, UserId = 3, PromoId = 5, Status = "paid", TotalAmount = 21686, DiscountAmount = 21686 },
                new Order { CustomerId = 1, UserId = 2, PromoId = null, Status = "paid", TotalAmount = 94180, DiscountAmount = 0 },
                new Order { CustomerId = 5, UserId = 3, PromoId = 2, Status = "paid", TotalAmount = 3888671, DiscountAmount = 100000 },
                new Order { CustomerId = 9, UserId = 3, PromoId = 4, Status = "paid", TotalAmount = 512594, DiscountAmount = 102518.8m },
                new Order { CustomerId = 11, UserId = 3, PromoId = 3, Status = "paid", TotalAmount = 1715029, DiscountAmount = 171502.9m },
                new Order { CustomerId = 11, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 2484051, DiscountAmount = 0 },
                new Order { CustomerId = 11, UserId = 3, PromoId = 2, Status = "paid", TotalAmount = 1070239, DiscountAmount = 100000 },
                new Order { CustomerId = 20, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 1532741, DiscountAmount = 0 },
                new Order { CustomerId = 10, UserId = 2, PromoId = null, Status = "paid", TotalAmount = 1785354, DiscountAmount = 0 },
                new Order { CustomerId = 10, UserId = 3, PromoId = 2, Status = "paid", TotalAmount = 1588276, DiscountAmount = 100000 },
                new Order { CustomerId = 6, UserId = 2, PromoId = 2, Status = "paid", TotalAmount = 2896096, DiscountAmount = 50000 },
                new Order { CustomerId = 10, UserId = 2, PromoId = 3, Status = "paid", TotalAmount = 186000, DiscountAmount = 27900 },
                new Order { CustomerId = 10, UserId = 2, PromoId = 5, Status = "paid", TotalAmount = 1024090, DiscountAmount = 50000 },
                new Order { CustomerId = 19, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 467148, DiscountAmount = 0 },
                new Order { CustomerId = 10, UserId = 2, PromoId = null, Status = "paid", TotalAmount = 394342, DiscountAmount = 0 },
                new Order { CustomerId = 8, UserId = 3, PromoId = 4, Status = "paid", TotalAmount = 1965637, DiscountAmount = 294845.55m },
                new Order { CustomerId = 3, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 2889813, DiscountAmount = 0 },
                new Order { CustomerId = 9, UserId = 2, PromoId = null, Status = "paid", TotalAmount = 2288406, DiscountAmount = 0 },
                new Order { CustomerId = 17, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 331008, DiscountAmount = 0 },
                new Order { CustomerId = 6, UserId = 3, PromoId = 1, Status = "paid", TotalAmount = 2154851, DiscountAmount = 323227.65m },
                new Order { CustomerId = 1, UserId = 3, PromoId = 1, Status = "paid", TotalAmount = 1138686, DiscountAmount = 170802.9m },
                new Order { CustomerId = 2, UserId = 2, PromoId = 5, Status = "paid", TotalAmount = 393847, DiscountAmount = 100000 },
                new Order { CustomerId = 15, UserId = 3, PromoId = 1, Status = "paid", TotalAmount = 260658, DiscountAmount = 52131.6m },
                new Order { CustomerId = 4, UserId = 2, PromoId = null, Status = "paid", TotalAmount = 933199, DiscountAmount = 0 },
                new Order { CustomerId = 16, UserId = 2, PromoId = null, Status = "paid", TotalAmount = 2609123, DiscountAmount = 0 },
                new Order { CustomerId = 4, UserId = 3, PromoId = 4, Status = "paid", TotalAmount = 2406292, DiscountAmount = 481258.4m },
                new Order { CustomerId = 1, UserId = 3, PromoId = null, Status = "paid", TotalAmount = 2912134, DiscountAmount = 0 }
            };
            await context.Orders.AddRangeAsync(orders);
        }

        await context.SaveChangesAsync();

        // Seed Order Items
        if (!await context.OrderItems.AnyAsync())
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, ProductId = 23, Quantity = 2, Price = 31265, Subtotal = 62530 },
                new OrderItem { OrderId = 1, ProductId = 5, Quantity = 2, Price = 205683, Subtotal = 411366 },
                new OrderItem { OrderId = 1, ProductId = 47, Quantity = 1, Price = 477948, Subtotal = 477948 },
                new OrderItem { OrderId = 1, ProductId = 25, Quantity = 2, Price = 170243, Subtotal = 340486 },
                new OrderItem { OrderId = 2, ProductId = 39, Quantity = 1, Price = 447059, Subtotal = 447059 },
                new OrderItem { OrderId = 2, ProductId = 14, Quantity = 1, Price = 51108, Subtotal = 51108 },
                new OrderItem { OrderId = 2, ProductId = 46, Quantity = 3, Price = 411147, Subtotal = 1233441 },
                new OrderItem { OrderId = 3, ProductId = 18, Quantity = 3, Price = 202167, Subtotal = 606501 },
                new OrderItem { OrderId = 3, ProductId = 34, Quantity = 1, Price = 44219, Subtotal = 44219 },
                new OrderItem { OrderId = 3, ProductId = 26, Quantity = 3, Price = 23354, Subtotal = 70062 },
                new OrderItem { OrderId = 4, ProductId = 24, Quantity = 2, Price = 10843, Subtotal = 21686 },
                new OrderItem { OrderId = 5, ProductId = 9, Quantity = 1, Price = 94180, Subtotal = 94180 },
                new OrderItem { OrderId = 6, ProductId = 18, Quantity = 3, Price = 186886, Subtotal = 560658 },
                new OrderItem { OrderId = 6, ProductId = 22, Quantity = 2, Price = 199267, Subtotal = 398534 },
                new OrderItem { OrderId = 6, ProductId = 42, Quantity = 3, Price = 215726, Subtotal = 647178 },
                new OrderItem { OrderId = 6, ProductId = 17, Quantity = 3, Price = 474268, Subtotal = 1422804 },
                new OrderItem { OrderId = 6, ProductId = 20, Quantity = 3, Price = 286499, Subtotal = 859497 },
                new OrderItem { OrderId = 7, ProductId = 8, Quantity = 2, Price = 256297, Subtotal = 512594 },
                new OrderItem { OrderId = 8, ProductId = 42, Quantity = 1, Price = 355116, Subtotal = 355116 },
                new OrderItem { OrderId = 8, ProductId = 43, Quantity = 2, Price = 129224, Subtotal = 258448 },
                new OrderItem { OrderId = 8, ProductId = 31, Quantity = 3, Price = 367155, Subtotal = 1101465 },
                new OrderItem { OrderId = 9, ProductId = 17, Quantity = 2, Price = 48755, Subtotal = 97510 },
                new OrderItem { OrderId = 9, ProductId = 12, Quantity = 2, Price = 381904, Subtotal = 763808 },
                new OrderItem { OrderId = 9, ProductId = 43, Quantity = 2, Price = 167445, Subtotal = 334890 },
                new OrderItem { OrderId = 9, ProductId = 19, Quantity = 3, Price = 429281, Subtotal = 1287843 },
                new OrderItem { OrderId = 10, ProductId = 25, Quantity = 1, Price = 232635, Subtotal = 232635 },
                new OrderItem { OrderId = 10, ProductId = 1, Quantity = 2, Price = 245362, Subtotal = 490724 },
                new OrderItem { OrderId = 10, ProductId = 23, Quantity = 2, Price = 127233, Subtotal = 254466 },
                new OrderItem { OrderId = 10, ProductId = 49, Quantity = 2, Price = 46207, Subtotal = 92414 },
                new OrderItem { OrderId = 11, ProductId = 3, Quantity = 2, Price = 347879, Subtotal = 695758 },
                new OrderItem { OrderId = 11, ProductId = 23, Quantity = 3, Price = 130215, Subtotal = 390645 },
                new OrderItem { OrderId = 11, ProductId = 4, Quantity = 1, Price = 64761, Subtotal = 64761 },
                new OrderItem { OrderId = 11, ProductId = 33, Quantity = 1, Price = 240159, Subtotal = 240159 },
                new OrderItem { OrderId = 11, ProductId = 7, Quantity = 1, Price = 141418, Subtotal = 141418 },
                new OrderItem { OrderId = 12, ProductId = 40, Quantity = 2, Price = 455428, Subtotal = 910856 },
                new OrderItem { OrderId = 12, ProductId = 46, Quantity = 2, Price = 75412, Subtotal = 150824 },
                new OrderItem { OrderId = 12, ProductId = 34, Quantity = 2, Price = 189856, Subtotal = 379712 },
                new OrderItem { OrderId = 12, ProductId = 25, Quantity = 3, Price = 114654, Subtotal = 343962 },
                new OrderItem { OrderId = 13, ProductId = 24, Quantity = 2, Price = 143251, Subtotal = 286502 },
                new OrderItem { OrderId = 13, ProductId = 23, Quantity = 2, Price = 381347, Subtotal = 762694 },
                new OrderItem { OrderId = 13, ProductId = 18, Quantity = 2, Price = 179146, Subtotal = 358292 },
                new OrderItem { OrderId = 13, ProductId = 9, Quantity = 2, Price = 90394, Subtotal = 180788 },
                new OrderItem { OrderId = 14, ProductId = 24, Quantity = 2, Price = 327016, Subtotal = 654032 },
                new OrderItem { OrderId = 14, ProductId = 2, Quantity = 1, Price = 403478, Subtotal = 403478 },
                new OrderItem { OrderId = 14, ProductId = 27, Quantity = 3, Price = 404474, Subtotal = 1213422 },
                new OrderItem { OrderId = 14, ProductId = 4, Quantity = 2, Price = 312582, Subtotal = 625164 },
                new OrderItem { OrderId = 15, ProductId = 18, Quantity = 1, Price = 105328, Subtotal = 105328 },
                new OrderItem { OrderId = 15, ProductId = 27, Quantity = 2, Price = 17303, Subtotal = 34606 },
                new OrderItem { OrderId = 15, ProductId = 50, Quantity = 2, Price = 23033, Subtotal = 46066 },
                new OrderItem { OrderId = 16, ProductId = 15, Quantity = 1, Price = 43160, Subtotal = 43160 },
                new OrderItem { OrderId = 16, ProductId = 16, Quantity = 2, Price = 18541, Subtotal = 37082 },
                new OrderItem { OrderId = 16, ProductId = 44, Quantity = 1, Price = 492698, Subtotal = 492698 },
                new OrderItem { OrderId = 16, ProductId = 41, Quantity = 1, Price = 451150, Subtotal = 451150 },
                new OrderItem { OrderId = 17, ProductId = 42, Quantity = 1, Price = 467148, Subtotal = 467148 },
                new OrderItem { OrderId = 18, ProductId = 30, Quantity = 1, Price = 64334, Subtotal = 64334 },
                new OrderItem { OrderId = 18, ProductId = 11, Quantity = 1, Price = 178454, Subtotal = 178454 },
                new OrderItem { OrderId = 18, ProductId = 20, Quantity = 3, Price = 50518, Subtotal = 151554 },
                new OrderItem { OrderId = 19, ProductId = 16, Quantity = 1, Price = 89280, Subtotal = 89280 },
                new OrderItem { OrderId = 19, ProductId = 23, Quantity = 3, Price = 404655, Subtotal = 1213965 },
                new OrderItem { OrderId = 19, ProductId = 11, Quantity = 2, Price = 331196, Subtotal = 662392 },
                new OrderItem { OrderId = 20, ProductId = 49, Quantity = 1, Price = 367325, Subtotal = 367325 },
                new OrderItem { OrderId = 20, ProductId = 32, Quantity = 2, Price = 264392, Subtotal = 528784 },
                new OrderItem { OrderId = 20, ProductId = 19, Quantity = 3, Price = 345903, Subtotal = 1037709 },
                new OrderItem { OrderId = 20, ProductId = 17, Quantity = 2, Price = 392028, Subtotal = 784056 },
                new OrderItem { OrderId = 20, ProductId = 19, Quantity = 1, Price = 171939, Subtotal = 171939 },
                new OrderItem { OrderId = 21, ProductId = 11, Quantity = 3, Price = 227666, Subtotal = 682998 },
                new OrderItem { OrderId = 21, ProductId = 25, Quantity = 2, Price = 436122, Subtotal = 872244 },
                new OrderItem { OrderId = 21, ProductId = 48, Quantity = 1, Price = 340400, Subtotal = 340400 },
                new OrderItem { OrderId = 21, ProductId = 10, Quantity = 2, Price = 58482, Subtotal = 116964 },
                new OrderItem { OrderId = 21, ProductId = 4, Quantity = 2, Price = 137900, Subtotal = 275800 },
                new OrderItem { OrderId = 22, ProductId = 40, Quantity = 2, Price = 165504, Subtotal = 331008 },
                new OrderItem { OrderId = 23, ProductId = 1, Quantity = 2, Price = 296698, Subtotal = 593396 },
                new OrderItem { OrderId = 23, ProductId = 16, Quantity = 3, Price = 384657, Subtotal = 1153971 },
                new OrderItem { OrderId = 23, ProductId = 40, Quantity = 3, Price = 135828, Subtotal = 407484 },
                new OrderItem { OrderId = 24, ProductId = 3, Quantity = 3, Price = 379562, Subtotal = 1138686 },
                new OrderItem { OrderId = 25, ProductId = 9, Quantity = 1, Price = 22063, Subtotal = 22063 },
                new OrderItem { OrderId = 25, ProductId = 16, Quantity = 2, Price = 185892, Subtotal = 371784 },
                new OrderItem { OrderId = 26, ProductId = 47, Quantity = 2, Price = 130329, Subtotal = 260658 },
                new OrderItem { OrderId = 27, ProductId = 37, Quantity = 1, Price = 448581, Subtotal = 448581 },
                new OrderItem { OrderId = 27, ProductId = 23, Quantity = 1, Price = 484618, Subtotal = 484618 },
                new OrderItem { OrderId = 28, ProductId = 20, Quantity = 3, Price = 357837, Subtotal = 1073511 },
                new OrderItem { OrderId = 28, ProductId = 34, Quantity = 1, Price = 161219, Subtotal = 161219 },
                new OrderItem { OrderId = 28, ProductId = 1, Quantity = 3, Price = 458131, Subtotal = 1374393 },
                new OrderItem { OrderId = 29, ProductId = 28, Quantity = 1, Price = 485514, Subtotal = 485514 },
                new OrderItem { OrderId = 29, ProductId = 7, Quantity = 3, Price = 487044, Subtotal = 1461132 },
                new OrderItem { OrderId = 29, ProductId = 42, Quantity = 1, Price = 235885, Subtotal = 235885 },
                new OrderItem { OrderId = 29, ProductId = 38, Quantity = 1, Price = 223761, Subtotal = 223761 },
                new OrderItem { OrderId = 30, ProductId = 25, Quantity = 1, Price = 426943, Subtotal = 426943 },
                new OrderItem { OrderId = 30, ProductId = 11, Quantity = 3, Price = 130209, Subtotal = 390627 },
                new OrderItem { OrderId = 30, ProductId = 5, Quantity = 2, Price = 73116, Subtotal = 146232 },
                new OrderItem { OrderId = 30, ProductId = 46, Quantity = 2, Price = 272220, Subtotal = 544440 },
                new OrderItem { OrderId = 30, ProductId = 23, Quantity = 3, Price = 467964, Subtotal = 1403892 }
            };
            await context.OrderItems.AddRangeAsync(orderItems);
        }

        // Seed Payments
        if (!await context.Payments.AnyAsync())
        {
            var payments = new List<Payment>
            {
                new Payment { OrderId = 1, Amount = 1192330, PaymentMethod = "cash" },
                new Payment { OrderId = 2, Amount = 1731608, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 3, Amount = 720782, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 4, Amount = 0, PaymentMethod = "card" },
                new Payment { OrderId = 5, Amount = 94180, PaymentMethod = "cash" },
                new Payment { OrderId = 6, Amount = 3788671, PaymentMethod = "cash" },
                new Payment { OrderId = 7, Amount = 410075.2m, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 8, Amount = 1543526.1m, PaymentMethod = "cash" },
                new Payment { OrderId = 9, Amount = 2484051, PaymentMethod = "cash" },
                new Payment { OrderId = 10, Amount = 970239, PaymentMethod = "card" },
                new Payment { OrderId = 11, Amount = 1532741, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 12, Amount = 1785354, PaymentMethod = "card" },
                new Payment { OrderId = 13, Amount = 1488276, PaymentMethod = "card" },
                new Payment { OrderId = 14, Amount = 2846096, PaymentMethod = "cash" },
                new Payment { OrderId = 15, Amount = 158100, PaymentMethod = "card" },
                new Payment { OrderId = 16, Amount = 974090, PaymentMethod = "cash" },
                new Payment { OrderId = 17, Amount = 467148, PaymentMethod = "cash" },
                new Payment { OrderId = 18, Amount = 394342, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 19, Amount = 1670791.45m, PaymentMethod = "card" },
                new Payment { OrderId = 20, Amount = 2889813, PaymentMethod = "card" },
                new Payment { OrderId = 21, Amount = 2288406, PaymentMethod = "cash" },
                new Payment { OrderId = 22, Amount = 331008, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 23, Amount = 1831623.35m, PaymentMethod = "cash" },
                new Payment { OrderId = 24, Amount = 967883.1m, PaymentMethod = "e-wallet" },
                new Payment { OrderId = 25, Amount = 293847, PaymentMethod = "cash" },
                new Payment { OrderId = 26, Amount = 208526.4m, PaymentMethod = "cash" },
                new Payment { OrderId = 27, Amount = 933199, PaymentMethod = "cash" },
                new Payment { OrderId = 28, Amount = 2609123, PaymentMethod = "card" },
                new Payment { OrderId = 29, Amount = 1925033.6m, PaymentMethod = "cash" },
                new Payment { OrderId = 30, Amount = 2912134, PaymentMethod = "card" }
            };
            await context.Payments.AddRangeAsync(payments);
        }

        await context.SaveChangesAsync();

        Console.WriteLine("✅ Seed data đã được thêm thành công!");
    }
}

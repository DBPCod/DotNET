-- ============================================
-- SQL Script để test thống kê
-- Chạy script này sau khi đã có dữ liệu cơ bản (customers, products, users)
-- ============================================

-- Xóa dữ liệu cũ (nếu cần)
-- DELETE FROM order_items;
-- DELETE FROM orders;

-- ============================================
-- INSERT ORDERS - Phân bố trong 6 tháng gần đây
-- ============================================

-- Lấy ID từ bảng hiện có (giả sử đã có dữ liệu)
-- Thay thế các UUID này bằng ID thực tế từ database của bạn

-- Orders tháng 5/2025 (tháng đầu tiên)
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111101', (SELECT id FROM customers WHERE name = 'Khách hàng 1' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-05-01 10:00:00', 'paid', 1500000.00, 0.00),
('11111111-1111-1111-1111-111111111102', (SELECT id FROM customers WHERE name = 'Khách hàng 2' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-05-05 14:30:00', 'paid', 2300000.00, 0.00),
('11111111-1111-1111-1111-111111111103', (SELECT id FROM customers WHERE name = 'Khách hàng 3' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-05-10 09:15:00', 'paid', 1800000.00, 0.00),
('11111111-1111-1111-1111-111111111104', (SELECT id FROM customers WHERE name = 'Khách hàng 4' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-05-15 16:45:00', 'paid', 3200000.00, 0.00),
('11111111-1111-1111-1111-111111111105', (SELECT id FROM customers WHERE name = 'Khách hàng 5' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-05-20 11:20:00', 'paid', 2100000.00, 0.00),
('11111111-1111-1111-1111-111111111106', (SELECT id FROM customers WHERE name = 'Khách hàng 6' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-05-25 13:00:00', 'pending', 1900000.00, 0.00);

-- Orders tháng 6/2025
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111107', (SELECT id FROM customers WHERE name = 'Khách hàng 7' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-06-02 10:30:00', 'paid', 2800000.00, 0.00),
('11111111-1111-1111-1111-111111111108', (SELECT id FROM customers WHERE name = 'Khách hàng 8' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-06-08 15:00:00', 'paid', 3500000.00, 0.00),
('11111111-1111-1111-1111-111111111109', (SELECT id FROM customers WHERE name = 'Khách hàng 9' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-06-12 09:45:00', 'paid', 2200000.00, 0.00),
('11111111-1111-1111-1111-111111111110', (SELECT id FROM customers WHERE name = 'Khách hàng 10' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-06-18 14:20:00', 'paid', 4100000.00, 0.00),
('11111111-1111-1111-1111-111111111111', (SELECT id FROM customers WHERE name = 'Khách hàng 1' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-06-22 11:10:00', 'paid', 1700000.00, 0.00),
('11111111-1111-1111-1111-111111111112', (SELECT id FROM customers WHERE name = 'Khách hàng 2' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-06-28 16:30:00', 'canceled', 2500000.00, 0.00);

-- Orders tháng 7/2025
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111113', (SELECT id FROM customers WHERE name = 'Khách hàng 11' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-07-03 10:15:00', 'paid', 3900000.00, 0.00),
('11111111-1111-1111-1111-111111111114', (SELECT id FROM customers WHERE name = 'Khách hàng 12' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-07-07 13:45:00', 'paid', 2600000.00, 0.00),
('11111111-1111-1111-1111-111111111115', (SELECT id FROM customers WHERE name = 'Khách hàng 13' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-07-11 09:30:00', 'paid', 3300000.00, 0.00),
('11111111-1111-1111-1111-111111111116', (SELECT id FROM customers WHERE name = 'Khách hàng 14' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-07-16 15:20:00', 'paid', 2700000.00, 0.00),
('11111111-1111-1111-1111-111111111117', (SELECT id FROM customers WHERE name = 'Khách hàng 15' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-07-21 11:00:00', 'paid', 4400000.00, 0.00),
('11111111-1111-1111-1111-111111111118', (SELECT id FROM customers WHERE name = 'Khách hàng 16' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-07-26 14:50:00', 'pending', 3100000.00, 0.00);

-- Orders tháng 8/2025
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111119', (SELECT id FROM customers WHERE name = 'Khách hàng 17' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-08-01 10:00:00', 'paid', 5000000.00, 0.00),
('11111111-1111-1111-1111-111111111120', (SELECT id FROM customers WHERE name = 'Khách hàng 18' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-08-05 13:30:00', 'paid', 3600000.00, 0.00),
('11111111-1111-1111-1111-111111111121', (SELECT id FROM customers WHERE name = 'Khách hàng 19' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-08-10 09:15:00', 'paid', 2900000.00, 0.00),
('11111111-1111-1111-1111-111111111122', (SELECT id FROM customers WHERE name = 'Khách hàng 20' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-08-15 15:45:00', 'paid', 4700000.00, 0.00),
('11111111-1111-1111-1111-111111111123', (SELECT id FROM customers WHERE name = 'Khách hàng 1' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-08-20 11:20:00', 'paid', 3800000.00, 0.00),
('11111111-1111-1111-1111-111111111124', (SELECT id FROM customers WHERE name = 'Khách hàng 2' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-08-25 14:10:00', 'paid', 4200000.00, 0.00);

-- Orders tháng 9/2025
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111125', (SELECT id FROM customers WHERE name = 'Khách hàng 3' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-09-02 10:30:00', 'paid', 5400000.00, 0.00),
('11111111-1111-1111-1111-111111111126', (SELECT id FROM customers WHERE name = 'Khách hàng 4' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-09-06 15:00:00', 'paid', 3100000.00, 0.00),
('11111111-1111-1111-1111-111111111127', (SELECT id FROM customers WHERE name = 'Khách hàng 5' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-09-10 09:45:00', 'paid', 4600000.00, 0.00),
('11111111-1111-1111-1111-111111111128', (SELECT id FROM customers WHERE name = 'Khách hàng 6' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-09-15 14:20:00', 'paid', 3700000.00, 0.00),
('11111111-1111-1111-1111-111111111129', (SELECT id FROM customers WHERE name = 'Khách hàng 7' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-09-20 11:10:00', 'paid', 4900000.00, 0.00),
('11111111-1111-1111-1111-111111111130', (SELECT id FROM customers WHERE name = 'Khách hàng 8' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-09-25 16:30:00', 'canceled', 2800000.00, 0.00);

-- Orders tháng 10/2025
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111131', (SELECT id FROM customers WHERE name = 'Khách hàng 9' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-10-01 10:15:00', 'paid', 5800000.00, 0.00),
('11111111-1111-1111-1111-111111111132', (SELECT id FROM customers WHERE name = 'Khách hàng 10' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-10-05 13:45:00', 'paid', 3400000.00, 0.00),
('11111111-1111-1111-1111-111111111133', (SELECT id FROM customers WHERE name = 'Khách hàng 11' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-10-09 09:30:00', 'paid', 5100000.00, 0.00),
('11111111-1111-1111-1111-111111111134', (SELECT id FROM customers WHERE name = 'Khách hàng 12' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-10-14 15:20:00', 'paid', 3900000.00, 0.00),
('11111111-1111-1111-1111-111111111135', (SELECT id FROM customers WHERE name = 'Khách hàng 13' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-10-19 11:00:00', 'paid', 5200000.00, 0.00),
('11111111-1111-1111-1111-111111111136', (SELECT id FROM customers WHERE name = 'Khách hàng 14' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-10-24 14:50:00', 'pending', 3500000.00, 0.00);

-- Orders tháng 11/2025 (tháng hiện tại)
INSERT INTO orders (id, customer_id, user_id, promo_id, order_date, status, total_amount, discount_amount) VALUES
('11111111-1111-1111-1111-111111111137', (SELECT id FROM customers WHERE name = 'Khách hàng 15' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-11-01 10:00:00', 'paid', 6200000.00, 0.00),
('11111111-1111-1111-1111-111111111138', (SELECT id FROM customers WHERE name = 'Khách hàng 16' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-11-03 13:30:00', 'paid', 3600000.00, 0.00),
('11111111-1111-1111-1111-111111111139', (SELECT id FROM customers WHERE name = 'Khách hàng 17' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-11-05 09:15:00', 'paid', 5500000.00, 0.00),
('11111111-1111-1111-1111-111111111140', (SELECT id FROM customers WHERE name = 'Khách hàng 18' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-11-08 15:45:00', 'paid', 4100000.00, 0.00),
('11111111-1111-1111-1111-111111111141', (SELECT id FROM customers WHERE name = 'Khách hàng 19' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff01' LIMIT 1), NULL, '2025-11-10 11:20:00', 'paid', 5300000.00, 0.00),
('11111111-1111-1111-1111-111111111142', (SELECT id FROM customers WHERE name = 'Khách hàng 20' LIMIT 1), (SELECT id FROM Users WHERE username = 'staff02' LIMIT 1), NULL, '2025-11-12 14:10:00', 'paid', 4800000.00, 0.00);

-- ============================================
-- INSERT ORDER ITEMS
-- Tạo dữ liệu để có ít nhất 5 sản phẩm bán chạy với doanh thu khác nhau
-- ============================================

-- Sản phẩm 1: Coca Cola lon - Sản phẩm bán chạy nhất (doanh thu cao nhất)
INSERT INTO order_items (id, order_id, product_id, quantity, price, subtotal) VALUES
('22222222-2222-2222-2222-222222222201', '11111111-1111-1111-1111-111111111101', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 5, 314838.00, 1574190.00),
('22222222-2222-2222-2222-222222222202', '11111111-1111-1111-1111-111111111107', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 8, 314838.00, 2518704.00),
('22222222-2222-2222-2222-222222222203', '11111111-1111-1111-1111-111111111113', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 10, 314838.00, 3148380.00),
('22222222-2222-2222-2222-222222222204', '11111111-1111-1111-1111-111111111119', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 12, 314838.00, 3778056.00),
('22222222-2222-2222-2222-222222222205', '11111111-1111-1111-1111-111111111125', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 15, 314838.00, 4722570.00),
('22222222-2222-2222-2222-222222222206', '11111111-1111-1111-1111-111111111131', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 18, 314838.00, 5667084.00),
('22222222-2222-2222-2222-222222222207', '11111111-1111-1111-1111-111111111137', (SELECT id FROM products WHERE product_name = 'Coca Cola lon' LIMIT 1), 20, 314838.00, 6296760.00);

-- Sản phẩm 2: Trà Xanh 0 độ - Sản phẩm bán chạy thứ 2
INSERT INTO order_items (id, order_id, product_id, quantity, price, subtotal) VALUES
('22222222-2222-2222-2222-222222222208', '11111111-1111-1111-1111-111111111102', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 4, 415725.00, 1662900.00),
('22222222-2222-2222-2222-222222222209', '11111111-1111-1111-1111-111111111108', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 6, 415725.00, 2494350.00),
('22222222-2222-2222-2222-222222222210', '11111111-1111-1111-1111-111111111114', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 5, 415725.00, 2078625.00),
('22222222-2222-2222-2222-222222222211', '11111111-1111-1111-1111-111111111120', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 7, 415725.00, 2910075.00),
('22222222-2222-2222-2222-222222222212', '11111111-1111-1111-1111-111111111126', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 6, 415725.00, 2494350.00),
('22222222-2222-2222-2222-222222222213', '11111111-1111-1111-1111-111111111132', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 8, 415725.00, 3325800.00),
('22222222-2222-2222-2222-222222222214', '11111111-1111-1111-1111-111111111138', (SELECT id FROM products WHERE product_name = 'Trà Xanh 0 độ' LIMIT 1), 7, 415725.00, 2910075.00);

-- Sản phẩm 3: Red Bull - Sản phẩm bán chạy thứ 3
INSERT INTO order_items (id, order_id, product_id, quantity, price, subtotal) VALUES
('22222222-2222-2222-2222-222222222215', '11111111-1111-1111-1111-111111111103', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 3, 402179.00, 1206537.00),
('22222222-2222-2222-2222-222222222216', '11111111-1111-1111-1111-111111111109', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 4, 402179.00, 1608716.00),
('22222222-2222-2222-2222-222222222217', '11111111-1111-1111-1111-111111111115', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 6, 402179.00, 2413074.00),
('22222222-2222-2222-2222-222222222218', '11111111-1111-1111-1111-111111111121', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 5, 402179.00, 2010895.00),
('22222222-2222-2222-2222-222222222219', '11111111-1111-1111-1111-111111111127', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 7, 402179.00, 2815253.00),
('22222222-2222-2222-2222-222222222220', '11111111-1111-1111-1111-111111111133', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 8, 402179.00, 3217432.00),
('22222222-2222-2222-2222-222222222221', '11111111-1111-1111-1111-111111111139', (SELECT id FROM products WHERE product_name = 'Red Bull' LIMIT 1), 9, 402179.00, 3619611.00);

-- Sản phẩm 4: Sting dâu - Sản phẩm bán chạy thứ 4
INSERT INTO order_items (id, order_id, product_id, quantity, price, subtotal) VALUES
('22222222-2222-2222-2222-222222222222', '11111111-1111-1111-1111-111111111104', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 6, 351670.00, 2110020.00),
('22222222-2222-2222-2222-222222222223', '11111111-1111-1111-1111-111111111110', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 8, 351670.00, 2813360.00),
('22222222-2222-2222-2222-222222222224', '11111111-1111-1111-1111-111111111116', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 5, 351670.00, 1758350.00),
('22222222-2222-2222-2222-222222222225', '11111111-1111-1111-1111-111111111122', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 9, 351670.00, 3165030.00),
('22222222-2222-2222-2222-222222222226', '11111111-1111-1111-1111-111111111128', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 7, 351670.00, 2461690.00),
('22222222-2222-2222-2222-222222222227', '11111111-1111-1111-1111-111111111134', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 8, 351670.00, 2813360.00),
('22222222-2222-2222-2222-222222222228', '11111111-1111-1111-1111-111111111140', (SELECT id FROM products WHERE product_name = 'Sting dâu' LIMIT 1), 10, 351670.00, 3516700.00);

-- Sản phẩm 5: Bánh Oreo - Sản phẩm bán chạy thứ 5
INSERT INTO order_items (id, order_id, product_id, quantity, price, subtotal) VALUES
('22222222-2222-2222-2222-222222222229', '11111111-1111-1111-1111-111111111105', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 8, 209283.00, 1674264.00),
('22222222-2222-2222-2222-222222222230', '11111111-1111-1111-1111-111111111111', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 6, 209283.00, 1255698.00),
('22222222-2222-2222-2222-222222222231', '11111111-1111-1111-1111-111111111117', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 10, 209283.00, 2092830.00),
('22222222-2222-2222-2222-222222222232', '11111111-1111-1111-1111-111111111123', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 9, 209283.00, 1883547.00),
('22222222-2222-2222-2222-222222222233', '11111111-1111-1111-1111-111111111129', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 12, 209283.00, 2511396.00),
('22222222-2222-2222-2222-222222222234', '11111111-1111-1111-1111-111111111135', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 11, 209283.00, 2302113.00),
('22222222-2222-2222-2222-222222222235', '11111111-1111-1111-1111-111111111141', (SELECT id FROM products WHERE product_name = 'Bánh Oreo' LIMIT 1), 13, 209283.00, 2720679.00);

-- Thêm các sản phẩm khác để đa dạng hóa dữ liệu
INSERT INTO order_items (id, order_id, product_id, quantity, price, subtotal) VALUES
('22222222-2222-2222-2222-222222222236', '11111111-1111-1111-1111-111111111102', (SELECT id FROM products WHERE product_name = 'Pepsi lon' LIMIT 1), 3, 114807.00, 344421.00),
('22222222-2222-2222-2222-222222222237', '11111111-1111-1111-1111-111111111103', (SELECT id FROM products WHERE product_name = 'Bánh Chocopie' LIMIT 1), 4, 212528.00, 850112.00),
('22222222-2222-2222-2222-222222222238', '11111111-1111-1111-1111-111111111104', (SELECT id FROM products WHERE product_name = 'Socola KitKat' LIMIT 1), 2, 139959.00, 279918.00),
('22222222-2222-2222-2222-222222222239', '11111111-1111-1111-1111-111111111108', (SELECT id FROM products WHERE product_name = 'Nước tương Maggi' LIMIT 1), 1, 462539.00, 462539.00),
('22222222-2222-2222-2222-222222222240', '11111111-1111-1111-1111-111111111110', (SELECT id FROM products WHERE product_name = 'Dầu ăn Tường An' LIMIT 1), 2, 281354.00, 562708.00),
('22222222-2222-2222-2222-222222222241', '11111111-1111-1111-1111-111111111115', (SELECT id FROM products WHERE product_name = 'Nồi cơm điện' LIMIT 1), 1, 405347.00, 405347.00),
('22222222-2222-2222-2222-222222222242', '11111111-1111-1111-1111-111111111120', (SELECT id FROM products WHERE product_name = 'Quạt máy' LIMIT 1), 2, 69968.00, 139936.00),
('22222222-2222-2222-2222-222222222243', '11111111-1111-1111-1111-111111111125', (SELECT id FROM products WHERE product_name = 'Máy xay sinh tố' LIMIT 1), 1, 334564.00, 334564.00),
('22222222-2222-2222-2222-222222222244', '11111111-1111-1111-1111-111111111131', (SELECT id FROM products WHERE product_name = 'Cà phê G7' LIMIT 1), 5, 201228.00, 1006140.00),
('22222222-2222-2222-2222-222222222245', '11111111-1111-1111-1111-111111111137', (SELECT id FROM products WHERE product_name = 'Sữa Vinamilk' LIMIT 1), 6, 252845.00, 1517070.00);

-- ============================================
-- Ghi chú:
-- 1. Tổng doanh thu theo tháng sẽ tăng dần từ tháng 5 đến tháng 11
-- 2. Top 5 sản phẩm bán chạy (theo doanh thu):
--    - Coca Cola lon: ~21,000,000 VNĐ
--    - Trà Xanh 0 độ: ~18,000,000 VNĐ  
--    - Red Bull: ~15,000,000 VNĐ
--    - Sting dâu: ~18,000,000 VNĐ
--    - Bánh Oreo: ~14,000,000 VNĐ
-- 3. Có đủ dữ liệu để test:
--    - Biểu đồ doanh thu theo tháng (6 tháng)
--    - Top 5 sản phẩm bán chạy
--    - Phân bố trạng thái đơn hàng (paid, pending, canceled)
-- ============================================


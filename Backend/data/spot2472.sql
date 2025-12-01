-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.4.3 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.8.0.6908
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for spot247
CREATE DATABASE IF NOT EXISTS `spot247` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `spot247`;

-- Dumping structure for table spot247.categories
CREATE TABLE IF NOT EXISTS `categories` (
  `description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `status` int NOT NULL,
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `category_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.categories: ~5 rows (approximately)
INSERT IGNORE INTO `categories` (`description`, `status`, `Id`, `category_name`) VALUES
	('Các loại gia vị nấu ăn, nước chấm, dầu ăn', 1, '430bc3ce-3204-4d66-84aa-4329ce060b2b', 'Gia vị'),
	('Các vật dụng sinh hoạt trong nhà', 1, '45efce19-3409-4ffe-beab-a501755b2982', 'Đồ gia dụng'),
	('Sản phẩm chăm sóc cá nhân và làm đẹp', 1, '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', 'Mỹ phẩm'),
	('Các loại bánh, kẹo, snack và đồ ăn vặt', 1, '8786a032-1cd3-46d4-94b3-e472a025dd0a', 'Bánh kẹo'),
	('Nước ngọt, nước có ga, cà phê, trà và các loại thức uống khác', 1, 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', 'Đồ uống');

-- Dumping structure for table spot247.customers
CREATE TABLE IF NOT EXISTS `customers` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `customer_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `created_at` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.customers: ~20 rows (approximately)
INSERT IGNORE INTO `customers` (`Id`, `customer_id`, `name`, `phone`, `email`, `address`, `status`, `created_at`) VALUES
	('02e6cb35-2d01-4653-b3c2-57612fb6c6c4', 'CUS004', 'Khách hàng 4', '0909000004', 'kh4@mail.com', 'Địa chỉ 4', 'PENDING', '2025-11-21 09:43:03.290694'),
	('06995357-948c-4c98-ab98-b5f4bfbf99df', 'CUS011', 'Khách hàng 11', '0909000011', 'kh11@mail.com', 'Địa chỉ 11', 'ACTIVE', '2025-11-21 09:43:03.290698'),
	('14107edd-3c25-4f0a-a12b-d879cc57cd3d', 'CUS007', 'Khách hàng 7', '0909000007', 'kh7@mail.com', 'Địa chỉ 7', 'ACTIVE', '2025-11-21 09:43:03.290697'),
	('212f7478-c8ff-4bf8-b7d7-78cc4a04acbc', 'CUS012', 'Khách hàng 12', '0909000012', 'kh12@mail.com', 'Địa chỉ 12', 'ACTIVE', '2025-11-21 09:43:03.290698'),
	('2a4dd388-eb6e-4db4-8702-7b4cad0ba367', 'CUS006', 'Khách hàng 6', '0909000006', 'kh6@mail.com', 'Địa chỉ 6', 'ACTIVE', '2025-11-21 09:43:03.290697'),
	('2f06df85-ce90-4aaf-a9ac-46b331127e99', 'CUS014', 'Khách hàng 14', '0909000014', 'kh14@mail.com', 'Địa chỉ 14', 'ACTIVE', '2025-11-21 09:43:03.290699'),
	('4463aba5-cb3e-40bc-9869-99ff5b8453a4', 'CUS017', 'Khách hàng 17', '0909000017', 'kh17@mail.com', 'Địa chỉ 17', 'ACTIVE', '2025-11-21 09:43:03.290700'),
	('503cf23e-155d-4000-a2a2-0f7372849811', 'CUS015', 'Khách hàng 15', '0909000015', 'kh15@mail.com', 'Địa chỉ 15', 'ACTIVE', '2025-11-21 09:43:03.290699'),
	('516d7b4f-41e8-4fbf-89ac-9014de9a0467', 'CUS005', 'Khách hàng 5', '0909000005', 'kh5@mail.com', 'Địa chỉ 5', 'ACTIVE', '2025-11-21 09:43:03.290695'),
	('520c6a17-e318-42f4-91b6-94b6eacc4f95', 'CUS001', 'Khách hàng 1', '0909000001', 'kh1@mail.com', 'Địa chỉ 1', 'ACTIVE', '2025-11-21 09:43:03.290547'),
	('522c9859-a900-4561-93fd-47d4eadaa9ea', 'CUS016', 'Khách hàng 16', '0909000016', 'kh16@mail.com', 'Địa chỉ 16', 'ACTIVE', '2025-11-21 09:43:03.290700'),
	('560fc16f-87b0-46f8-a524-99eb8f94c9be', 'CUS018', 'Khách hàng 18', '0909000018', 'kh18@mail.com', 'Địa chỉ 18', 'ACTIVE', '2025-11-21 09:43:03.290700'),
	('696c805f-5b52-4d16-a8e7-f84ccea0c088', 'CUS020', 'Khách hàng 20', '0909000020', 'kh20@mail.com', 'Địa chỉ 20', 'ACTIVE', '2025-11-21 09:43:03.290701'),
	('6db32054-5696-4b90-9488-71c4f0f38778', 'CUS010', 'Khách hàng 10', '0909000010', 'kh10@mail.com', 'Địa chỉ 10', 'ACTIVE', '2025-11-21 09:43:03.290698'),
	('78a8d856-060d-46ba-bf89-777e64b265f4', 'CUS003', 'Khách hàng 3', '0909000003', 'kh3@mail.com', 'Địa chỉ 3', 'ACTIVE', '2025-11-21 09:43:03.290694'),
	('8b7945ea-b4c0-4c48-a35d-13b1b135d72b', 'CUS009', 'Khách hàng 9', '0909000009', 'kh9@mail.com', 'Địa chỉ 9', 'ACTIVE', '2025-11-21 09:43:03.290697'),
	('a36817d1-ac84-42a8-83eb-afe568524a35', 'CUS013', 'Khách hàng 13', '0909000013', 'kh13@mail.com', 'Địa chỉ 13', 'PENDING', '2025-11-21 09:43:03.290699'),
	('b5cea98c-4112-424f-8c3f-ec68fe3a8a05', 'CUS019', 'Khách hàng 19', '0909000019', 'kh19@mail.com', 'Địa chỉ 19', 'PENDING', '2025-11-21 09:43:03.290701'),
	('d75edf65-4833-4b16-ac01-6db14e149fb1', 'CUS002', 'Khách hàng 2', '0909000002', 'kh2@mail.com', 'Địa chỉ 2', 'ACTIVE', '2025-11-21 09:43:03.290694'),
	('e5b36085-4815-4943-ae47-048fb5893bca', 'CUS008', 'Khách hàng 8', '0909000008', 'kh8@mail.com', 'Địa chỉ 8', 'PENDING', '2025-11-21 09:43:03.290697');

-- Dumping structure for table spot247.inventory
CREATE TABLE IF NOT EXISTS `inventory` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `product_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `quantity` int NOT NULL,
  `cost_price` decimal(10,2) NOT NULL,
  `updated_at` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_inventory_product_id` (`product_id`),
  CONSTRAINT `FK_inventory_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.inventory: ~50 rows (approximately)
INSERT IGNORE INTO `inventory` (`Id`, `product_id`, `quantity`, `cost_price`, `updated_at`) VALUES
	('075b16a7-32b4-44b9-91ec-4300ae6bf40e', 'a1302900-9ae1-4a79-879c-0a668fce71de', 169, 0.00, '2025-11-21 09:43:03.711167'),
	('103dce94-2e4d-4d76-95c2-f9409fe2bf9e', '4408112a-515d-4cb1-99a0-51d4b1c14d14', 165, 0.00, '2025-11-21 09:43:03.924385'),
	('16dc27f0-5fba-472a-8871-20d7062cccc4', '24f9640e-9b87-4127-8196-802b3ed82e0a', 23, 0.00, '2025-11-21 09:43:03.761015'),
	('2234ae7d-0254-450f-bb6e-fc7c4b4b58d3', '69607f01-2373-4d7a-ab95-e1bda0534f7d', 134, 0.00, '2025-11-21 09:43:03.780380'),
	('271eed10-5cf3-4d0b-bce7-9e96235dfb5e', '5179f9ab-f284-4fc1-b833-ce8ddcd9bda3', 25, 0.00, '2025-11-21 09:43:03.684825'),
	('2c631a24-fffb-482c-828d-8e545caa86bd', '2e0134cb-b392-4a45-8deb-9ce5477773ee', 41, 0.00, '2025-11-21 09:43:03.902007'),
	('2d806627-5963-4809-b8f7-7a5b2f968d14', '4cbf600f-7cf7-41e9-975f-36c36ce83c85', 194, 0.00, '2025-11-21 09:43:03.896466'),
	('30db6839-72f8-49a1-ae42-116d432008f8', 'a6ff3c70-4196-499b-9c68-38d1e0492e8c', 77, 0.00, '2025-11-21 09:43:03.705283'),
	('315b1e12-7487-4e21-8278-bb3e2218384d', 'fbe6d2c5-111f-450e-8ec4-aa793c2a07a8', 166, 0.00, '2025-11-21 09:43:03.835421'),
	('38930f4e-a403-4bef-9fb4-39fed203adfb', '6f8c23df-e308-4b03-9ee7-4c6a5f44b1f9', 33, 0.00, '2025-11-21 09:43:04.013806'),
	('3cf59717-52e4-4102-82b0-f88c787a9a7f', '7a7afdde-a46f-47d7-b05a-640293bcc91a', 59, 0.00, '2025-11-21 09:43:03.956765'),
	('40e8b8f8-423b-4ae4-b09a-eb5f72a40a57', '7ff6c244-b296-42d2-9e2d-3b51e9795662', 99, 0.00, '2025-11-21 09:43:03.988597'),
	('4737c718-7b0d-4a14-8a9d-ffe6a31d192c', '239025e6-3107-4176-b121-bc86f7e0a2ef', 182, 0.00, '2025-11-21 09:43:03.794906'),
	('4c00d3f3-ac85-4225-98f9-51dabb2a9925', 'e3c0ee08-fe23-4f97-94cc-0d02ff8fc1ee', 36, 0.00, '2025-11-21 09:43:03.858905'),
	('655f83d4-aa0b-47e1-897c-0fcd617cf2e1', '40ae74cb-afea-431d-b75d-fba5be4b4c69', 74, 0.00, '2025-11-21 09:43:03.742773'),
	('67e6a1f8-3d76-4271-b4c0-070d41312605', '24436c59-13e2-4f54-8a5b-9769c59c1641', 149, 0.00, '2025-11-21 09:43:03.748875'),
	('6bac5c3f-2d61-413f-9af4-1504ebb04fac', '8679b9a5-ead8-4e6c-bc39-2994121fd71d', 139, 0.00, '2025-11-21 09:43:03.878343'),
	('6d800828-d727-4a55-a1be-3214404a5a85', 'd43490c9-649c-473e-bed8-614d7465c343', 78, 0.00, '2025-11-21 09:43:03.829682'),
	('70f67002-7365-45ea-bea1-fa41da05ffab', 'db910255-be98-4d89-b020-10ef3b72bb36', 34, 0.00, '2025-11-21 09:43:03.945980'),
	('76a9201c-b5a3-4784-aaf5-3efbb41c6052', '6193d84e-7128-471e-a3e1-e764ab5f1671', 128, 0.00, '2025-11-21 09:43:03.811693'),
	('7967dabc-f9ac-42a8-90ba-f235a8fcd77a', '9a62f1b3-c89a-4d26-8813-b84bb79d00cd', 123, 0.00, '2025-11-21 09:43:03.817520'),
	('7a60caa8-e35c-4da3-80d6-cf2213ad38f8', '8bb66c60-0660-4313-b9b5-f12b295f6837', 144, 0.00, '2025-11-21 09:43:03.774455'),
	('7f7a9e2c-227b-45c3-bfeb-d425620e3228', '967cfa66-9fc2-4619-8419-92edd57d8f36', 37, 0.00, '2025-11-21 09:43:03.736983'),
	('845b6ea6-c8fc-47a7-9e7d-21fbf2c77fc7', 'ab7105fd-a02f-479a-a36e-ca6a177e777b', 105, 0.00, '2025-11-21 09:43:03.723399'),
	('8c95d478-4b70-4c4d-92cc-65516489e3a5', 'f8201745-40ad-49aa-82f1-5609bb8eed5b', 155, 0.00, '2025-11-21 09:43:03.823803'),
	('8de08b0d-c67b-4faf-a7cd-799ea4331b56', 'f853204d-45bd-4d7c-b1fc-ee5ceb61d761', 99, 0.00, '2025-11-21 09:43:03.800818'),
	('945636ff-4a9d-4a98-9cab-f6117428ac6d', '42555ebc-44de-4330-859e-3ebcf4cb90c0', 72, 0.00, '2025-11-21 09:43:03.806377'),
	('977852a7-0680-45e8-b36c-3d6245f93ac5', '4f3b4a99-9e2c-4d80-b1ca-3510c9f3120b', 169, 0.00, '2025-11-21 09:43:03.698814'),
	('9bb2243e-2e4c-43f3-80f7-c1c3f651efec', '21bea911-78d7-4865-b608-27de8662ba50', 176, 0.00, '2025-11-21 09:43:03.935029'),
	('9d0ccc13-f813-4ef9-8004-84170bf6460e', '64fddaa6-0803-49d5-bcee-06978e35d70e', 168, 0.00, '2025-11-21 09:43:03.847003'),
	('a1837040-39fb-44ad-b964-f852bb4d98cf', '8e30f965-bcbb-4136-9065-6757fc329bf7', 198, 0.00, '2025-11-21 09:43:03.962280'),
	('a202e425-eb8c-4ff3-a9d9-7550ffe1b2a2', '2dec680f-66af-46fd-95b9-459b6ea0cdca', 125, 0.00, '2025-11-21 09:43:03.729995'),
	('a532abf7-b057-4b33-8dc9-9d7b7b919f1d', 'f2ffe1ad-6fab-4273-becd-01869101a6e3', 117, 0.00, '2025-11-21 09:43:03.841203'),
	('ab7637a5-5229-420e-a93f-7d801ec094a8', '2c58e56d-c1ca-478e-a882-7d227f7644ef', 73, 0.00, '2025-11-21 09:43:03.929728'),
	('af30025e-f084-47fa-8975-e05064af21ad', '03d24f99-ad3b-41ec-a376-65e4ca61344a', 69, 0.00, '2025-11-21 09:43:03.755111'),
	('b25f2fc7-95bb-4b74-a73f-c469a90a1073', '2bfe3f44-4764-451e-9dd2-da326d26de0b', 197, 0.00, '2025-11-21 09:43:03.852578'),
	('b6b900be-39d8-4735-b3af-0ae9fea1dbb6', '938c551a-858b-41b0-9e2d-519d34b1497d', 154, 0.00, '2025-11-21 09:43:03.907593'),
	('c42747c1-fe3e-4908-80ee-ce4463e12272', 'bbdec094-2e54-4068-ac80-222004e6ddfb', 49, 0.00, '2025-11-21 09:43:03.918396'),
	('c590c8fc-d3ef-4195-85fb-7347801d11e2', '13c0a631-0821-44c8-b772-f900018768dd', 46, 0.00, '2025-11-21 09:43:03.768226'),
	('ccb6ec3f-19e5-4615-bd2f-2c4f121c2bd4', '0ca53603-f659-4066-8aa3-19354b93539b', 175, 0.00, '2025-11-21 09:43:03.951350'),
	('d2036eeb-6e2d-43b5-b7ed-5ee451fadf5c', 'd6bcf546-82ba-45fe-a5fd-daf67b27a8e6', 62, 0.00, '2025-11-21 09:43:04.006173'),
	('d39d281e-9a3f-47e6-85d0-7723d5d3466d', 'e3ab24c3-c8b3-4ec5-9a9d-e2010ebf2722', 41, 0.00, '2025-11-21 09:43:03.940389'),
	('d642cc39-d14d-4c02-9d82-aed65f10ff20', 'bcd93aa1-1b56-461c-83d7-d5f31ae3bc6d', 71, 0.00, '2025-11-21 09:43:03.913071'),
	('d90d1cc0-6ddc-4da0-a765-0eed80eec285', 'fa5959cc-8cf4-43c1-9fc3-358a05d33eb3', 154, 0.00, '2025-11-21 09:43:03.890462'),
	('d91d164a-3ec2-4e2b-8bbf-2eb673c38bd0', '4e0b422c-846f-494f-8fac-f976d179e74f', 47, 0.00, '2025-11-21 09:43:03.884211'),
	('ea53f3af-f001-4067-9611-0e7f1e5d481f', 'c9216837-ac36-4434-ba68-c2e4781cea1c', 106, 0.00, '2025-11-21 09:43:03.967661'),
	('ecb2444f-8c60-4d80-9e32-fc326309fdc6', 'fe96dff4-cbcf-414c-bf4b-5aa6c541642f', 61, 0.00, '2025-11-21 09:43:03.871177'),
	('ef1b8933-0dce-4397-a387-ea93aa446845', '6c2e8571-78f0-46ea-976d-11d734364654', 55, 0.00, '2025-11-21 09:43:03.998077'),
	('f0db85a2-eefd-4587-83cc-f5a4c2842be1', '8e7075bb-290a-45a6-85af-1a1972bcf904', 145, 0.00, '2025-11-21 09:43:03.865192'),
	('f6698938-9f5f-443e-ab37-06475eb655dd', '553ec95b-da72-400d-9422-ab29e3b4c057', 90, 0.00, '2025-11-21 09:43:03.717273');

-- Dumping structure for table spot247.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `customer_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `user_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `promo_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `order_date` datetime(6) NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `total_amount` decimal(10,2) DEFAULT NULL,
  `discount_amount` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_orders_customer_id` (`customer_id`),
  KEY `IX_orders_promo_id` (`promo_id`),
  KEY `IX_orders_user_id` (`user_id`),
  CONSTRAINT `FK_orders_customers_customer_id` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`Id`),
  CONSTRAINT `FK_orders_promotions_promo_id` FOREIGN KEY (`promo_id`) REFERENCES `promotions` (`Id`),
  CONSTRAINT `FK_orders_Users_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.orders: ~30 rows (approximately)
INSERT IGNORE INTO `orders` (`Id`, `customer_id`, `user_id`, `promo_id`, `order_date`, `status`, `total_amount`, `discount_amount`) VALUES
	('127d206a-f4b1-4058-9cc1-6d041e022b87', '78a8d856-060d-46ba-bf89-777e64b265f4', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153942', 'paid', 2889813.00, 0.00),
	('1c7bb362-d480-4955-a0ab-7b649e97473d', '02e6cb35-2d01-4653-b3c2-57612fb6c6c4', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', NULL, '2025-11-21 09:43:04.153951', 'paid', 933199.00, 0.00),
	('2927a480-dca5-4590-8c9b-1ac19250e355', '520c6a17-e318-42f4-91b6-94b6eacc4f95', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', NULL, '2025-11-21 09:43:04.153911', 'paid', 94180.00, 0.00),
	('2c2ea886-eb9e-4d0f-81b3-a2217f91db18', '520c6a17-e318-42f4-91b6-94b6eacc4f95', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153954', 'paid', 2912134.00, 0.00),
	('3b27e2f3-18dd-4e10-b614-508737793db5', '696c805f-5b52-4d16-a8e7-f84ccea0c088', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153929', 'paid', 1532741.00, 0.00),
	('4969f091-62b4-45a9-a78d-eeb0ff67b6d3', '8b7945ea-b4c0-4c48-a35d-13b1b135d72b', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', NULL, '2025-11-21 09:43:04.153943', 'paid', 2288406.00, 0.00),
	('5e2bb9e1-1887-4254-a0c6-c6fc2f08f7e2', '02e6cb35-2d01-4653-b3c2-57612fb6c6c4', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '40798f28-0cdc-40fa-86cb-bd3c59567f4c', '2025-11-21 09:43:04.153953', 'paid', 2406292.00, 481258.40),
	('5e7286c4-f690-44c2-b41d-60cbffa2ba35', '6db32054-5696-4b90-9488-71c4f0f38778', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', NULL, '2025-11-21 09:43:04.153930', 'paid', 1785354.00, 0.00),
	('5ef87772-dce4-45bb-bdfb-487f0b3b8196', '503cf23e-155d-4000-a2a2-0f7372849811', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', 'ada59b8e-4e54-428f-8a6f-a9fec7b2bb3f', '2025-11-21 09:43:04.153950', 'paid', 260658.00, 52131.60),
	('62fe80a0-794a-45fe-8608-9ae8da97e275', '520c6a17-e318-42f4-91b6-94b6eacc4f95', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', 'ada59b8e-4e54-428f-8a6f-a9fec7b2bb3f', '2025-11-21 09:43:04.153948', 'paid', 1138686.00, 170802.90),
	('6de1fecf-ce6c-431c-b6c2-75e5e66125d0', '06995357-948c-4c98-ab98-b5f4bfbf99df', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153923', 'paid', 2484051.00, 0.00),
	('735361cb-cd5e-4c64-ab0d-3500e26a6139', '2a4dd388-eb6e-4db4-8702-7b4cad0ba367', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', '1a064880-e41c-49f7-ae87-61dec5940ea4', '2025-11-21 09:43:04.153933', 'paid', 2896096.00, 50000.00),
	('73ffbb2a-5d78-45bd-b5c6-84af4b618bb2', '522c9859-a900-4561-93fd-47d4eadaa9ea', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', NULL, '2025-11-21 09:43:04.153952', 'paid', 2609123.00, 0.00),
	('8371938b-d578-412a-a3b7-6869c010faeb', '696c805f-5b52-4d16-a8e7-f84ccea0c088', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', 'b7fce67b-0b39-49ea-88fd-a2e633c4e9cb', '2025-11-21 09:43:04.153904', 'paid', 21686.00, 21686.00),
	('868dee75-a500-4c04-be68-5c9eb6e6481b', '6db32054-5696-4b90-9488-71c4f0f38778', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', NULL, '2025-11-21 09:43:04.153939', 'paid', 394342.00, 0.00),
	('86bd2604-a644-4b75-a0de-9df2205ce901', 'e5b36085-4815-4943-ae47-048fb5893bca', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153900', 'paid', 720782.00, 0.00),
	('8767a11b-5529-44ad-8154-43511fdcba8e', 'd75edf65-4833-4b16-ac01-6db14e149fb1', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', 'b7fce67b-0b39-49ea-88fd-a2e633c4e9cb', '2025-11-21 09:43:04.153949', 'paid', 393847.00, 100000.00),
	('8979462f-1bc5-4e2a-850f-36eefce59e03', '4463aba5-cb3e-40bc-9869-99ff5b8453a4', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153944', 'paid', 331008.00, 0.00),
	('8cd4a3a5-f284-4553-989b-4922e35d921c', 'e5b36085-4815-4943-ae47-048fb5893bca', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '40798f28-0cdc-40fa-86cb-bd3c59567f4c', '2025-11-21 09:43:04.153940', 'paid', 1965637.00, 294845.55),
	('8f27632b-4517-44a7-9e4c-9254ebfd6f52', '6db32054-5696-4b90-9488-71c4f0f38778', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', 'b7fce67b-0b39-49ea-88fd-a2e633c4e9cb', '2025-11-21 09:43:04.153935', 'paid', 1024090.00, 50000.00),
	('a5561875-3545-41de-b3cd-1756d536756f', '4463aba5-cb3e-40bc-9869-99ff5b8453a4', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153896', 'paid', 1731608.00, 0.00),
	('a841ae6d-de21-4237-aeaa-feaed701fe8d', '6db32054-5696-4b90-9488-71c4f0f38778', 'f8d3e94f-cf8b-425f-86dd-b8380c5b4797', '306050d8-9eaa-40de-8831-f253cc74eb86', '2025-11-21 09:43:04.153934', 'paid', 186000.00, 27900.00),
	('ad6655f1-8f7e-46dd-bb19-ba1ee2cd317c', '6db32054-5696-4b90-9488-71c4f0f38778', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '1a064880-e41c-49f7-ae87-61dec5940ea4', '2025-11-21 09:43:04.153931', 'paid', 1588276.00, 100000.00),
	('ae3c10fa-19c5-47bb-982b-2c98bf4b4c4c', '06995357-948c-4c98-ab98-b5f4bfbf99df', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '306050d8-9eaa-40de-8831-f253cc74eb86', '2025-11-21 09:43:04.153922', 'paid', 1715029.00, 171502.90),
	('babd09f7-dd33-486b-aaf8-c03c5c25d12c', '516d7b4f-41e8-4fbf-89ac-9014de9a0467', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', 'b7fce67b-0b39-49ea-88fd-a2e633c4e9cb', '2025-11-21 09:43:04.152727', 'paid', 1292330.00, 100000.00),
	('bef16206-1d14-45da-ad05-2a9febc6b349', 'b5cea98c-4112-424f-8c3f-ec68fe3a8a05', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', NULL, '2025-11-21 09:43:04.153936', 'paid', 467148.00, 0.00),
	('cba6802c-d8b3-4193-bc5c-3374844f0d91', '2a4dd388-eb6e-4db4-8702-7b4cad0ba367', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', 'ada59b8e-4e54-428f-8a6f-a9fec7b2bb3f', '2025-11-21 09:43:04.153945', 'paid', 2154851.00, 323227.65),
	('e18a098c-8d17-4da4-94df-e3d571ada901', '06995357-948c-4c98-ab98-b5f4bfbf99df', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '1a064880-e41c-49f7-ae87-61dec5940ea4', '2025-11-21 09:43:04.153924', 'paid', 1070239.00, 100000.00),
	('eed53693-ab00-43b3-81e6-09a308de2ef6', '516d7b4f-41e8-4fbf-89ac-9014de9a0467', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '1a064880-e41c-49f7-ae87-61dec5940ea4', '2025-11-21 09:43:04.153913', 'paid', 3888671.00, 100000.00),
	('efb29fa2-2b28-4605-abd2-8a36a34d285c', '8b7945ea-b4c0-4c48-a35d-13b1b135d72b', '1e7bdb0f-b474-452a-9c7f-019cbce0033c', '40798f28-0cdc-40fa-86cb-bd3c59567f4c', '2025-11-21 09:43:04.153916', 'paid', 512594.00, 102518.80);

-- Dumping structure for table spot247.order_items
CREATE TABLE IF NOT EXISTS `order_items` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `order_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `product_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `quantity` int NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_order_items_order_id` (`order_id`),
  KEY `IX_order_items_product_id` (`product_id`),
  CONSTRAINT `FK_order_items_orders_order_id` FOREIGN KEY (`order_id`) REFERENCES `orders` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_order_items_products_product_id` FOREIGN KEY (`product_id`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.order_items: ~2 rows (approximately)
INSERT IGNORE INTO `order_items` (`Id`, `order_id`, `product_id`, `quantity`, `price`, `subtotal`) VALUES
	('21be7628-8a29-4908-9138-9a567b985a02', '127d206a-f4b1-4058-9cc1-6d041e022b87', '4f3b4a99-9e2c-4d80-b1ca-3510c9f3120b', 1, 114807.00, 114807.00),
	('31ca3b25-0b31-4605-a86d-82b52372f5ae', '127d206a-f4b1-4058-9cc1-6d041e022b87', '5179f9ab-f284-4fc1-b833-ce8ddcd9bda3', 2, 314838.00, 629676.00);

-- Dumping structure for table spot247.payments
CREATE TABLE IF NOT EXISTS `payments` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `order_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `payment_method` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `payment_date` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_payments_order_id` (`order_id`),
  CONSTRAINT `FK_payments_orders_order_id` FOREIGN KEY (`order_id`) REFERENCES `orders` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.payments: ~30 rows (approximately)
INSERT IGNORE INTO `payments` (`Id`, `order_id`, `amount`, `payment_method`, `payment_date`) VALUES
	('0c915dd1-6269-4696-bc06-35d9a2cbe32e', '868dee75-a500-4c04-be68-5c9eb6e6481b', 186000.00, 'cash', '2025-11-21 09:43:04.299808'),
	('0dced4bd-43ca-4c59-89b1-7383f66547f9', 'bef16206-1d14-45da-ad05-2a9febc6b349', 260658.00, 'bank_transfer', '2025-11-21 09:43:04.312475'),
	('17d9d6b5-31d4-435f-98ef-0015830e7861', 'eed53693-ab00-43b3-81e6-09a308de2ef6', 2406292.00, 'bank_transfer', '2025-11-21 09:43:04.315787'),
	('2481e9bc-7318-43aa-9475-fc9d90fe3ea2', 'babd09f7-dd33-486b-aaf8-c03c5c25d12c', 393847.00, 'credit_card', '2025-11-21 09:43:04.311339'),
	('2814e460-da0d-4842-9bee-62dd9660dd5b', 'cba6802c-d8b3-4193-bc5c-3374844f0d91', 933199.00, 'cash', '2025-11-21 09:43:04.313812'),
	('2ea8df85-653e-4cef-a210-5f45ae26bbfd', '73ffbb2a-5d78-45bd-b5c6-84af4b618bb2', 1588276.00, 'credit_card', '2025-11-21 09:43:04.297348'),
	('2f7b9d70-295f-4942-9d56-c926ac2bbef9', '8979462f-1bc5-4e2a-850f-36eefce59e03', 394342.00, 'cash', '2025-11-21 09:43:04.303599'),
	('3463ade0-e22e-4fb5-8818-7ac5be2ec50a', '5ef87772-dce4-45bb-bdfb-487f0b3b8196', 2484051.00, 'cash', '2025-11-21 09:43:04.293334'),
	('347be3ed-8240-438c-b1c2-263841293a9a', '1c7bb362-d480-4955-a0ab-7b649e97473d', 1731608.00, 'bank_transfer', '2025-11-21 09:43:04.284409'),
	('3d360046-2317-442a-9174-a9121cc213e0', '6de1fecf-ce6c-431c-b6c2-75e5e66125d0', 1532741.00, 'bank_transfer', '2025-11-21 09:43:04.295102'),
	('47ce1225-fc0a-43e4-a496-f634dbfd56cb', '735361cb-cd5e-4c64-ab0d-3500e26a6139', 1785354.00, 'cash', '2025-11-21 09:43:04.296202'),
	('4b4ee8a5-fa7b-4a9f-ab56-b007ac9de4ce', '8f27632b-4517-44a7-9e4c-9254ebfd6f52', 2889813.00, 'bank_transfer', '2025-11-21 09:43:04.306260'),
	('5312f1e1-8650-4a12-a888-2a73dceb975c', '86bd2604-a644-4b75-a0de-9df2205ce901', 1024090.00, 'credit_card', '2025-11-21 09:43:04.300900'),
	('586523e0-6791-499f-bf02-678ae7f58824', '3b27e2f3-18dd-4e10-b614-508737793db5', 94180.00, 'bank_transfer', '2025-11-21 09:43:04.288451'),
	('5b7e160f-0013-4817-bfc9-f5ee4b810e55', '5e7286c4-f690-44c2-b41d-60cbffa2ba35', 1715029.00, 'bank_transfer', '2025-11-21 09:43:04.292222'),
	('6b83e1ac-2df1-46cb-9955-6f21bb251285', 'a841ae6d-de21-4237-aeaa-feaed701fe8d', 331008.00, 'credit_card', '2025-11-21 09:43:04.308369'),
	('715e85b4-5ed9-43e5-beac-f06f8e873918', 'ad6655f1-8f7e-46dd-bb19-ba1ee2cd317c', 2154851.00, 'bank_transfer', '2025-11-21 09:43:04.309343'),
	('775752f8-d41f-4dac-ad37-4b5942acf8ec', '5e2bb9e1-1887-4254-a0c6-c6fc2f08f7e2', 512594.00, 'credit_card', '2025-11-21 09:43:04.290707'),
	('791d3ec6-3c98-40da-a5f8-3301c99dcfdf', '2927a480-dca5-4590-8c9b-1ac19250e355', 720782.00, 'cash', '2025-11-21 09:43:04.286094'),
	('9e8a15d8-855c-4649-86fd-3d4f3bef1247', '2c2ea886-eb9e-4d0f-81b3-a2217f91db18', 21686.00, 'credit_card', '2025-11-21 09:43:04.287357'),
	('b260731b-b935-4f8b-a998-5da92cca1c38', '62fe80a0-794a-45fe-8608-9ae8da97e275', 1070239.00, 'credit_card', '2025-11-21 09:43:04.294272'),
	('c21ec216-edca-4b3a-b089-246d3e8942d1', '4969f091-62b4-45a9-a78d-eeb0ff67b6d3', 3888671.00, 'cash', '2025-11-21 09:43:04.289540'),
	('c719bd3c-5059-4b65-85fc-820e766976fb', 'e18a098c-8d17-4da4-94df-e3d571ada901', 2609123.00, 'credit_card', '2025-11-21 09:43:04.314865'),
	('c9a9a5d5-8a7c-489f-903e-12e2f9dadb30', '127d206a-f4b1-4058-9cc1-6d041e022b87', 1292330.00, 'credit_card', '2025-11-21 09:43:04.271413'),
	('c9e58ce3-9b1a-4ef8-89d9-888f197ebcf0', 'efb29fa2-2b28-4605-abd2-8a36a34d285c', 2912134.00, 'cash', '2025-11-21 09:43:04.316636'),
	('cab3ae14-2608-4b80-822f-7fd60560664e', '8cd4a3a5-f284-4553-989b-4922e35d921c', 1965637.00, 'credit_card', '2025-11-21 09:43:04.304994'),
	('cb92aeb6-c16c-493f-aa7b-1ad7607c86c4', '8767a11b-5529-44ad-8154-43511fdcba8e', 467148.00, 'bank_transfer', '2025-11-21 09:43:04.301922'),
	('ce89a953-ea1e-4861-8fac-3ebd20f7e48e', 'ae3c10fa-19c5-47bb-982b-2c98bf4b4c4c', 1138686.00, 'cash', '2025-11-21 09:43:04.310367'),
	('e06c380d-084c-4a28-99fb-2c23ca319b1d', '8371938b-d578-412a-a3b7-6869c010faeb', 2896096.00, 'bank_transfer', '2025-11-21 09:43:04.298642'),
	('f1543bd5-a7a2-44bf-bf88-877abf1e5f62', 'a5561875-3545-41de-b3cd-1756d536756f', 2288406.00, 'cash', '2025-11-21 09:43:04.307387');

-- Dumping structure for table spot247.products
CREATE TABLE IF NOT EXISTS `products` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `category_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `supplier_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `product_name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `barcode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `unit` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `image_path` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `status` tinyint(1) NOT NULL,
  `created_at` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_products_category_id` (`category_id`),
  KEY `IX_products_supplier_id` (`supplier_id`),
  CONSTRAINT `FK_products_categories_category_id` FOREIGN KEY (`category_id`) REFERENCES `categories` (`Id`),
  CONSTRAINT `FK_products_suppliers_supplier_id` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.products: ~52 rows (approximately)
INSERT IGNORE INTO `products` (`Id`, `category_id`, `supplier_id`, `product_name`, `barcode`, `price`, `unit`, `image_path`, `status`, `created_at`) VALUES
	('03d24f99-ad3b-41ec-a376-65e4ca61344a', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Nước mắm Nam Ngư', '8900000000011', 51792.00, 'chai', '/uploads/products/Nước mắm Nam Ngư.jpg', 1, '2025-11-21 09:43:03.516502'),
	('0ca53603-f659-4066-8aa3-19354b93539b', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '95d13804-016b-4b8a-813d-b246976433bf', 'Mì Omachi', '8900000000043', 26616.00, 'hộp', '/uploads/products/Mì Omachi.jpg', 1, '2025-11-21 09:43:03.581502'),
	('13c0a631-0821-44c8-b772-f900018768dd', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '0b087938-407d-4e31-b929-046e216143c1', 'Muối i-ốt', '8900000000013', 173302.00, 'cái', '/uploads/products/Muối i-ốt.jpg', 1, '2025-11-21 09:43:03.520251'),
	('21bea911-78d7-4865-b608-27de8662ba50', '45efce19-3409-4ffe-beab-a501755b2982', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Khẩu trang 3M', '8900000000040', 464252.00, 'gói', '/uploads/products/Khẩu trang 3M.jpg', 1, '2025-11-21 09:43:03.576359'),
	('239025e6-3107-4176-b121-bc86f7e0a2ef', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Nồi cơm điện', '8900000000016', 405347.00, 'hộp', '/uploads/products/Nồi cơm điện.jpg', 1, '2025-11-21 09:43:03.526937'),
	('24436c59-13e2-4f54-8a5b-9769c59c1641', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '95d13804-016b-4b8a-813d-b246976433bf', 'Socola KitKat', '8900000000010', 139959.00, 'chai', '/uploads/products/Socola KitKat.jpg', 1, '2025-11-21 09:43:03.515020'),
	('24f9640e-9b87-4127-8196-802b3ed82e0a', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '95d13804-016b-4b8a-813d-b246976433bf', 'Nước tương Maggi', '8900000000012', 462539.00, 'lon', '/uploads/products/Nước tương Maggi.jpg', 1, '2025-11-21 09:43:03.518419'),
	('2bfe3f44-4764-451e-9dd2-da326d26de0b', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Cà phê G7', '8900000000026', 201228.00, 'lon', '/uploads/products/Cà phê G7.jpg', 1, '2025-11-21 09:43:03.551298'),
	('2c58e56d-c1ca-478e-a882-7d227f7644ef', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '95d13804-016b-4b8a-813d-b246976433bf', 'Bông tẩy trang', '8900000000039', 317819.00, 'tuýp', '/uploads/products/Bông tẩy trang.jpg', 1, '2025-11-21 09:43:03.574213'),
	('2dec680f-66af-46fd-95b9-459b6ea0cdca', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '0b087938-407d-4e31-b929-046e216143c1', 'Bánh Chocopie', '8900000000007', 212528.00, 'lon', '/uploads/products/Bánh Chocopie.jpg', 1, '2025-11-21 09:43:03.503794'),
	('2e0134cb-b392-4a45-8deb-9ce5477773ee', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Hộp nhựa Tupperware', '8900000000034', 297415.00, 'cái', '/uploads/products/Hộp nhựa Tupperware.jpg', 1, '2025-11-21 09:43:03.565714'),
	('40ae74cb-afea-431d-b75d-fba5be4b4c69', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Kẹo bạc hà', '8900000000009', 316289.00, 'cái', '/uploads/products/Kẹo bạc hà.jpg', 1, '2025-11-21 09:43:03.513482'),
	('42555ebc-44de-4330-859e-3ebcf4cb90c0', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '95d13804-016b-4b8a-813d-b246976433bf', 'Quạt máy', '8900000000018', 69968.00, 'hộp', '/uploads/products/Quạt máy.jpg', 1, '2025-11-21 09:43:03.531899'),
	('4408112a-515d-4cb1-99a0-51d4b1c14d14', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '0b087938-407d-4e31-b929-046e216143c1', 'Nước súc miệng Listerine', '8900000000038', 223906.00, 'gói', '/uploads/products/Nước súc miệng Listerine.jpg', 1, '2025-11-21 09:43:03.572652'),
	('4cbf600f-7cf7-41e9-975f-36c36ce83c85', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '95d13804-016b-4b8a-813d-b246976433bf', 'Bình nước Lock&Lock', '8900000000033', 354771.00, 'gói', '/uploads/products/Bình nước Lock&Lock.jpg', 1, '2025-11-21 09:43:03.564032'),
	('4e0b422c-846f-494f-8fac-f976d179e74f', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '0b087938-407d-4e31-b929-046e216143c1', 'Khăn giấy Tempo', '8900000000031', 102525.00, 'chai', '/uploads/products/Khăn giấy Tempo.jpg', 1, '2025-11-21 09:43:03.560129'),
	('4f3b4a99-9e2c-4d80-b1ca-3510c9f3120b', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '0b087938-407d-4e31-b929-046e216143c1', 'Pepsi lon', '8900000000002', 114807.00, 'cái', '/uploads/products/Pepsi lon.jpg', 1, '2025-11-21 09:43:03.472509'),
	('5179f9ab-f284-4fc1-b833-ce8ddcd9bda3', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Coca Cola lon', '8900000000001', 314838.00, 'hộp', '/uploads/products/Coca Cola lon.jpg', 1, '2025-11-21 09:43:03.399938'),
	('553ec95b-da72-400d-9422-ab29e3b4c057', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '95d13804-016b-4b8a-813d-b246976433bf', 'Red Bull', '8900000000005', 402179.00, 'lon', '/uploads/products/Red Bull.jpg', 1, '2025-11-21 09:43:03.494333'),
	('6193d84e-7128-471e-a3e1-e764ab5f1671', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '0b087938-407d-4e31-b929-046e216143c1', 'Bếp gas mini', '8900000000019', 416845.00, 'lon', '/uploads/products/Bếp gas mini.jpg', 1, '2025-11-21 09:43:03.534147'),
	('64fddaa6-0803-49d5-bcee-06978e35d70e', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Nước hoa Romano', '8900000000025', 352508.00, 'cái', '/uploads/products/Nước hoa Romano.jpg', 1, '2025-11-21 09:43:03.549599'),
	('69607f01-2373-4d7a-ab95-e1bda0534f7d', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '95d13804-016b-4b8a-813d-b246976433bf', 'Dầu ăn Tường An', '8900000000015', 281354.00, 'tuýp', '/uploads/products/Dầu ăn Tường An.jpg', 1, '2025-11-21 09:43:03.525004'),
	('6c2e8571-78f0-46ea-976d-11d734364654', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '0b087938-407d-4e31-b929-046e216143c1', 'Snack Oishi', '8900000000048', 43415.00, 'cái', '/uploads/products/Snack Oishi.jpg', 1, '2025-11-21 09:43:03.591009'),
	('6f8c23df-e308-4b03-9ee7-4c6a5f44b1f9', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '95d13804-016b-4b8a-813d-b246976433bf', 'Kẹo dẻo Haribo', '8900000000050', 328680.00, 'cái', '/uploads/products/Kẹo dẻo Haribo.jpg', 1, '2025-11-21 09:43:03.594606'),
	('7a7afdde-a46f-47d7-b05a-640293bcc91a', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '95d13804-016b-4b8a-813d-b246976433bf', 'Bún khô', '8900000000044', 350911.00, 'gói', '/uploads/products/Bún khô.jpg', 1, '2025-11-21 09:43:03.583680'),
	('7ff6c244-b296-42d2-9e2d-3b51e9795662', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '0b087938-407d-4e31-b929-046e216143c1', 'Trà sữa đóng chai', '8900000000047', 15130.00, 'cái', '/uploads/products/trà sữa đóng chai.png', 1, '2025-11-21 09:43:03.589114'),
	('8679b9a5-ead8-4e6c-bc39-2994121fd71d', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '95d13804-016b-4b8a-813d-b246976433bf', 'Nước suối Lavie', '8900000000030', 331637.00, 'lon', '/uploads/products/Nước suối Lavie.jpg', 1, '2025-11-21 09:43:03.558298'),
	('8bb66c60-0660-4313-b9b5-f12b295f6837', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Bột ngọt Ajinomoto', '8900000000014', 443069.00, 'cái', '/uploads/products/Bột ngọt Ajinomoto.jpg', 1, '2025-11-21 09:43:03.522570'),
	('8e30f965-bcbb-4136-9065-6757fc329bf7', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Phở ăn liền', '8900000000045', 407779.00, 'tuýp', '/uploads/products/Phở ăn liền.jpg', 1, '2025-11-21 09:43:03.585572'),
	('8e7075bb-290a-45a6-85af-1a1972bcf904', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '95d13804-016b-4b8a-813d-b246976433bf', 'Sữa Vinamilk', '8900000000028', 252845.00, 'chai', '/uploads/products/Sữa Vinamilk.jpg', 1, '2025-11-21 09:43:03.554323'),
	('938c551a-858b-41b0-9e2d-519d34b1497d', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Dao Inox', '8900000000035', 47523.00, 'hộp', '/uploads/products/Dao Inox.jpg', 1, '2025-11-21 09:43:03.567512'),
	('967cfa66-9fc2-4619-8419-92edd57d8f36', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '95d13804-016b-4b8a-813d-b246976433bf', 'Kẹo Alpenliebe', '8900000000008', 34313.00, 'lon', '/uploads/products/Kẹo Alpenliebe.jpg', 1, '2025-11-21 09:43:03.511247'),
	('9a62f1b3-c89a-4d26-8813-b84bb79d00cd', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '0b087938-407d-4e31-b929-046e216143c1', 'Máy xay sinh tố', '8900000000020', 334564.00, 'hộp', '/uploads/products/Máy xay sinh tố.jpg', 1, '2025-11-21 09:43:03.536055'),
	('a06b4aa5-c042-42f8-8796-47141dbbd9a9', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '0b087938-407d-4e31-b929-046e216143c1', 'Bánh con gấu', 'xzxc', 1000000.00, 'pcs', '/uploads/products/14a98b3c-72bc-40d3-a444-aed2ae26f343.jpg', 1, '2025-11-21 09:45:56.748206'),
	('a1302900-9ae1-4a79-879c-0a668fce71de', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Sting dâu', '8900000000004', 351670.00, 'cái', '/uploads/products/Sting dâu.jpg', 1, '2025-11-21 09:43:03.491881'),
	('a6ff3c70-4196-499b-9c68-38d1e0492e8c', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '0b087938-407d-4e31-b929-046e216143c1', 'Trà Xanh 0 độ', '8900000000003', 415725.00, 'tuýp', '/uploads/products/Trà Xanh 0 độ.jpg', 1, '2025-11-21 09:43:03.482708'),
	('ab7105fd-a02f-479a-a36e-ca6a177e777b', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '95d13804-016b-4b8a-813d-b246976433bf', 'Bánh Oreo', '8900000000006', 209283.00, 'chai', '/uploads/products/Bánh Oreo.jpg', 1, '2025-11-21 09:43:03.501183'),
	('bbdec094-2e54-4068-ac80-222004e6ddfb', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '95d13804-016b-4b8a-813d-b246976433bf', 'Kem đánh răng P/S', '8900000000037', 93713.00, 'hộp', '/uploads/products/Kem đánh răng PS.jpg', 1, '2025-11-21 09:43:03.571136'),
	('bcd93aa1-1b56-461c-83d7-d5f31ae3bc6d', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Bàn chải Colgate', '8900000000036', 136417.00, 'chai', '/uploads/products/Bàn chải Colgate.jpg', 1, '2025-11-21 09:43:03.569269'),
	('c9216837-ac36-4434-ba68-c2e4781cea1c', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Nước ngọt Sprite', '8900000000046', 230083.00, 'hộp', '/uploads/products/Nước ngọt Sprite.jpg', 1, '2025-11-21 09:43:03.587226'),
	('d43490c9-649c-473e-bed8-614d7465c343', '45efce19-3409-4ffe-beab-a501755b2982', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Kem dưỡng da Pond\'s', '8900000000022', 413840.00, 'hộp', '/uploads/products/Kem dưỡng da Pond\'s.jpg', 1, '2025-11-21 09:43:03.539516'),
	('d4a96cbc-c501-4fc6-8d43-1bae429185cd', '45efce19-3409-4ffe-beab-a501755b2982', '0b087938-407d-4e31-b929-046e216143c1', 'zxczxc', 'zxczxc', 1111100.00, 'pcs', '/uploads/products/b8a18df3-b1fb-4be9-901d-c97773453b9d.jpg', 1, '2025-11-21 10:22:24.593379'),
	('d6bcf546-82ba-45fe-a5fd-daf67b27a8e6', '45efce19-3409-4ffe-beab-a501755b2982', '95d13804-016b-4b8a-813d-b246976433bf', 'Snack Lay\'s', '8900000000049', 83536.00, 'tuýp', '/uploads/products/Snack Lay\'s.jpg', 1, '2025-11-21 09:43:03.592758'),
	('db910255-be98-4d89-b020-10ef3b72bb36', '760ce3e9-cda2-4f05-84ba-cd3c4cdf4876', '95d13804-016b-4b8a-813d-b246976433bf', 'Mì gói Hảo Hảo', '8900000000042', 9413.00, 'hộp', '/uploads/products/Mì gói Hảo Hảo.jpg', 1, '2025-11-21 09:43:03.579729'),
	('e3ab24c3-c8b3-4ec5-9a9d-e2010ebf2722', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Bánh mì sandwich', '8900000000041', 279350.00, 'cái', '/uploads/products/Bánh mì sandwich.jpg', 1, '2025-11-21 09:43:03.578313'),
	('e3c0ee08-fe23-4f97-94cc-0d02ff8fc1ee', '8786a032-1cd3-46d4-94b3-e472a025dd0a', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Trà Lipton', '8900000000027', 38039.00, 'cái', '/uploads/products/Trà Lipton.jpg', 1, '2025-11-21 09:43:03.552818'),
	('f2ffe1ad-6fab-4273-becd-01869101a6e3', '45efce19-3409-4ffe-beab-a501755b2982', '95d13804-016b-4b8a-813d-b246976433bf', 'Sữa tắm Dove', '8900000000024', 336928.00, 'chai', '/uploads/products/Sữa tắm Dove.jpg', 1, '2025-11-21 09:43:03.547881'),
	('f8201745-40ad-49aa-82f1-5609bb8eed5b', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Sữa rửa mặt Hazeline', '8900000000021', 188475.00, 'lon', '/uploads/products/Sữa rửa mặt Hazeline.jpg', 1, '2025-11-21 09:43:03.537870'),
	('f853204d-45bd-4d7c-b1fc-ee5ceb61d761', 'f3e31fe2-58b5-4c4a-a3d6-866e0f178f16', '0b087938-407d-4e31-b929-046e216143c1', 'Ấm siêu tốc', '8900000000017', 113087.00, 'chai', '/uploads/products/Ấm siêu tốc.jpg', 1, '2025-11-21 09:43:03.529336'),
	('fa5959cc-8cf4-43c1-9fc3-358a05d33eb3', '45efce19-3409-4ffe-beab-a501755b2982', '0b087938-407d-4e31-b929-046e216143c1', 'Giấy vệ sinh Pulppy', '8900000000032', 495429.00, 'chai', '/uploads/products/Giấy vệ sinh Pulppy.jpg', 1, '2025-11-21 09:43:03.562058'),
	('fbe6d2c5-111f-450e-8ec4-aa793c2a07a8', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '0b087938-407d-4e31-b929-046e216143c1', 'Dầu gội Sunsilk', '8900000000023', 158950.00, 'tuýp', '/uploads/products/Dầu gội Sunsilk.jpg', 1, '2025-11-21 09:43:03.546227'),
	('fe96dff4-cbcf-414c-bf4b-5aa6c541642f', '430bc3ce-3204-4d66-84aa-4329ce060b2b', '191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Sữa TH True Milk', '8900000000029', 35278.00, 'hộp', '/uploads/products/Sữa TH True Milk.jpg', 1, '2025-11-21 09:43:03.556451');

-- Dumping structure for table spot247.promotions
CREATE TABLE IF NOT EXISTS `promotions` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `promo_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `discount_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `discount_value` decimal(10,2) NOT NULL,
  `promotion_type` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `min_order_amount` decimal(10,2) NOT NULL,
  `usage_limit` int NOT NULL,
  `used_count` int NOT NULL,
  `status` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.promotions: ~8 rows (approximately)
INSERT IGNORE INTO `promotions` (`Id`, `promo_code`, `description`, `discount_type`, `discount_value`, `promotion_type`, `start_date`, `end_date`, `min_order_amount`, `usage_limit`, `used_count`, `status`) VALUES
	('1a064880-e41c-49f7-ae87-61dec5940ea4', 'FREESHIP50K', 'Giảm 50,000 cho đơn từ 300,000 trở lên', 'fixed', 50000.00, 'promotion', '2025-03-01', '2025-12-31', 300000.00, 500, 0, 'active'),
	('21e2cab2-c45f-45a9-97e5-7300cde8b02f', 'WELCOME10', 'Giảm $10 cho khách hàng mới', 'fixed', 10.00, 'discountcode', '2025-01-01', '2025-12-31', 50000.00, 500, 0, 'active'),
	('2d9c37f7-8032-43f5-9af3-18870e26d496', 'BLACKFRIDAY50', 'Giảm giá 50% cho tất cả sản phẩm điện tử', 'percent', 50.00, 'discountcode', '2025-11-01', '2025-11-30', 100000.00, 1000, 0, 'active'),
	('306050d8-9eaa-40de-8831-f253cc74eb86', 'NEWUSER', 'Giảm 20% cho khách hàng mới', 'percent', 20.00, 'promotion', '2025-01-01', '2025-06-30', 0.00, 1, 0, 'active'),
	('40798f28-0cdc-40fa-86cb-bd3c59567f4c', 'SUMMER15', 'Giảm 15% mùa hè', 'percent', 15.00, 'promotion', '2025-06-01', '2025-08-31', 50000.00, 1000, 0, 'active'),
	('8636a111-751f-4d55-8b4f-c07c887f0ddb', 'FREESHIP', 'Miễn phí vận chuyển', 'freeshipping', 0.00, 'discountcode', '2025-01-01', '2025-12-31', 30000.00, 200, 0, 'active'),
	('ada59b8e-4e54-428f-8a6f-a9fec7b2bb3f', 'SALE10', 'Giảm 10% cho mọi đơn hàng', 'percent', 10.00, 'promotion', '2025-01-01', '2025-12-31', 0.00, 0, 0, 'active'),
	('b7fce67b-0b39-49ea-88fd-a2e633c4e9cb', 'VIP100K', 'Giảm 100,000 cho đơn từ 1 triệu', 'fixed', 100000.00, 'promotion', '2025-01-01', '2025-12-31', 1000000.00, 200, 0, 'active');

-- Dumping structure for table spot247.suppliers
CREATE TABLE IF NOT EXISTS `suppliers` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `phone` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `address` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `status` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.suppliers: ~3 rows (approximately)
INSERT IGNORE INTO `suppliers` (`Id`, `name`, `phone`, `email`, `address`, `status`) VALUES
	('0b087938-407d-4e31-b929-046e216143c1', 'Công ty 123', '0933123456', '123@gmail.com', 'Đà Nẵng', 1),
	('191d2570-f5e5-45e7-ad1b-667033d06ca4', 'Công ty ABC', '0909123456', 'abc@gmail.com', 'Hà Nội', 1),
	('95d13804-016b-4b8a-813d-b246976433bf', 'Công ty XYZ', '0912123456', 'xyz@gmail.com', 'TP HCM', 1);

-- Dumping structure for table spot247.users
CREATE TABLE IF NOT EXISTS `users` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `username` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `full_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `role` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT (_utf8mb4'ACTIVE'),
  `created_at` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Users_email` (`email`),
  UNIQUE KEY `IX_Users_username` (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.users: ~5 rows (approximately)
INSERT IGNORE INTO `users` (`Id`, `username`, `email`, `password`, `full_name`, `role`, `status`, `created_at`) VALUES
	('1e7bdb0f-b474-452a-9c7f-019cbce0033c', 'staff02', 'staff02@example.com', '12345678', 'Lê Thị B', 'STAFF', 'PENDING', '2025-11-21 09:43:03.076143'),
	('7eab201d-239b-4460-abda-5f38d7a60615', 'admin', 'admin@example.com', '12345678', 'Quản trị viên', 'ADMIN', 'PENDING', '2025-11-21 09:43:03.076003'),
	('bac334b8-b964-44ce-b517-878b381ceec2', 'dinhbaphong', 'dinhbaphong123@gmail.com', '$2a$11$89aM943k0KpcTKq8Ib7An.IzJAgKV4bV5yat2KcKzQriOichS8pHq', 'Administrator', 'ADMIN', 'ACTIVE', '2025-11-21 09:43:05.419220'),
	('c4e3552e-bbca-421e-9a0f-3cb67a58e6b2', 'dinhbanghia', 'dinhbaphong456@gmail.com', '$2a$11$5VwhqXsHzhdAGRh7GV.cPOAAOT7kX3jcRTZHOySVsxa9V8uWWQLoO', 'Đinh Bá Nghĩa', 'STAFF', 'ACTIVE', '2025-11-22 17:08:55.059385'),
	('f8d3e94f-cf8b-425f-86dd-b8380c5b4797', 'staff01', 'staff01@example.com', '12345678', 'Nguyễn Văn A', 'STAFF', 'PENDING', '2025-11-21 09:43:03.076142');

-- Dumping structure for table spot247.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table spot247.__efmigrationshistory: ~1 rows (approximately)
INSERT IGNORE INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20251121024203_Init', '9.0.8');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

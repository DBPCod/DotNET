
## Danh sách API

1. [Tạo khuyến mãi]
2. [Lấy danh sách khuyến mãi]
3. [Lấy chi tiết khuyến mãi]
4. [Cập nhật khuyến mãi]
5. [Xóa khuyến mãi]
6. [Validate mã khuyến mãi]
7. [Áp dụng mã khuyến mãi cho đơn hàng]

## 1. Tạo khuyến mãi

**Quyền**: ADMIN

**Method**: `POST`

**URL**: `http://localhost/4040/api/promotions`

**Headers**:
```
Authorization: Bearer {token}
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded):
```
PromoCode=SUMMER2024
Description=Giảm giá mùa hè 2024
DiscountType=percent
DiscountValue=20
StartDate=2024-06-01
EndDate=2024-08-31
MinOrderAmount=100000
UsageLimit=100
Status=active
```

## 2. Lấy danh sách khuyến mãi

**Quyền**: STAFF, ADMIN

**Method**: `GET`

**URL**: `http://localhost:4040/api/promotions`

**Headers**:
```
Authorization: Bearer {token}
```

**Query Parameters**:
```
page=1
pageSize=10
q=SUMMER          (tìm kiếm theo mã hoặc mô tả)
status=active     (lọc theo trạng thái: active/inactive)
from=2024-06-01   (lọc từ ngày)
to=2024-12-31     (lọc đến ngày)
```

**Ví dụ**:
- Lấy tất cả: `GET /api/promotions?page=1&pageSize=10`
- Lọc active: `GET /api/promotions?page=1&pageSize=10&status=active`
- Tìm kiếm: `GET /api/promotions?q=SUMMER&page=1&pageSize=10`
- Lọc theo ngày: `GET /api/promotions?from=2024-06-01&to=2024-12-31&page=1&pageSize=10`

---

## 3. Lấy chi tiết khuyến mãi

**Quyền**: STAFF, ADMIN

**Method**: `GET`

**URL**: `http://localhost:4040/api/promotions/{id}`

**Headers**:
```
Authorization: Bearer {token}
```

**Ví dụ**:
```
GET /api/promotions/123e4567-e89b-12d3-a456-426614174000
```

---

## 4. Cập nhật khuyến mãi

**Quyền**: ADMIN

**Method**: `PUT`

**URL**: `http://localhost:4040/api/promotions/{id}`

**Headers**:
```
Authorization: Bearer {token}
Content-Type: application/x-www-form-urlencoded
```

**Body** (form-urlencoded) - Tất cả các trường đều optional:
```
Description=Giảm giá mùa hè 2024 - Cập nhật
DiscountType=percent
DiscountValue=25
StartDate=2024-06-01
EndDate=2024-09-30
MinOrderAmount=150000
UsageLimit=150
Status=active
```

**Lưu ý**: 
- Không thể sửa `PromoCode` nếu đã có đơn hàng sử dụng mã này
- Trường `CanEdit` trong response cho biết có thể sửa mã hay không

---

## 5. Xóa khuyến mãi (Soft Delete)

**Quyền**: ADMIN

**Method**: `DELETE`

**URL**: `http://localhost:4040/api/promotions/{id}`

**Headers**:
```
Authorization: Bearer {token}
```

**Ví dụ**:
```
DELETE /api/promotions/123e4567-e89b-12d3-a456-426614174000
```

**Lưu ý**: Không xóa thật, chỉ chuyển `status` thành `inactive`

---

## 6. Validate mã khuyến mãi

**Quyền**: STAFF, ADMIN

**Method**: `GET`

**URL**: `http://localhost:4040/api/promotions/validate`

**Headers**:
```
Authorization: Bearer {token}
```

**Query Parameters**:
```
code=SUMMER2024
orderTotal=500000
```

**Ví dụ**:
```
GET /api/promotions/validate?code=SUMMER2024&orderTotal=500000
```

**Response**:
```json
{
  "valid": true,
  "reason": "ok",
  "discountAmount": 100000.00,
  "discountType": "percent"
}
```

**Các giá trị `reason`**:
- `ok`: Mã hợp lệ
- `not_found`: Mã không tồn tại
- `inactive`: Mã đã bị vô hiệu hóa
- `expired`: Mã hết hạn hoặc chưa bắt đầu
- `min_order`: Giá trị đơn hàng không đủ tối thiểu
- `usage_limit`: Đã đạt giới hạn số lần sử dụng

---

## 7. Áp dụng mã khuyến mãi cho đơn hàng

**Quyền**: STAFF, ADMIN

**Method**: `POST`

**URL**: `http://localhost:4040/api/orders/{orderId}/apply-promo`

**Headers**:
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Body** (JSON):
```json
{
  "code": "SUMMER2024"
}
```

**Ví dụ**:
```
POST /api/orders/123e4567-e89b-12d3-a456-426614174000/apply-promo
Body: { "code": "SUMMER2024" }
```

**Response**:
```json
{
  "orderId": "123e4567-e89b-12d3-a456-426614174000",
  "promoCode": "SUMMER2024",
  "discountAmount": 100000.00,
  "orderTotalBefore": 500000.00,
  "orderTotalAfter": 400000.00
}
```

**Lưu ý**:
- Mỗi đơn hàng chỉ áp dụng được 1 mã
- Có thể đổi sang mã khác (hệ thống tự động xử lý `used_count`)
- Sử dụng transaction để đảm bảo an toàn


## Ví dụ hoàn chỉnh

### 1. Login để lấy token
```
POST http://localhost:5000/api/v1/auth/login
Content-Type: application/x-www-form-urlencoded

usernameOrEmail=admin@example.com&password=admin123
```

### 2. Tạo mã giảm 20%
```
POST http://localhost:5000/api/promotions
Authorization: Bearer {token}
Content-Type: application/x-www-form-urlencoded

PromoCode=SALE20&Description=Giảm 20%&DiscountType=percent&DiscountValue=20&StartDate=2024-01-01&EndDate=2025-12-31&MinOrderAmount=100000&UsageLimit=0&Status=active
```

### 3. Tạo mã giảm cố định 50k
```
POST http://localhost:5000/api/promotions
Authorization: Bearer {token}
Content-Type: application/x-www-form-urlencoded

PromoCode=SAVE50K&Description=Giảm 50k&DiscountType=fixed&DiscountValue=50000&StartDate=2024-01-01&EndDate=2025-12-31&MinOrderAmount=500000&UsageLimit=100&Status=active
```

### 4. Validate mã
```
GET http://localhost:5000/api/promotions/validate?code=SALE20&orderTotal=200000
Authorization: Bearer {token}
```

### 5. Áp dụng mã cho order
```
POST http://localhost:5000/api/orders/{orderId}/apply-promo
Authorization: Bearer {token}
Content-Type: application/json

{ "code": "SALE20" }
```

---

## Validation Rules

### Khi tạo/cập nhật:
- `EndDate` phải >= `StartDate`
- Nếu `DiscountType` = `percent` thì `DiscountValue` <= 100
- `PromoCode` không được trùng

### Khi validate/apply:
- `Status` = `active`
- Ngày hiện tại trong khoảng [`StartDate`, `EndDate`]
- `OrderTotal` >= `MinOrderAmount`
- `UsageLimit` = 0 HOẶC `UsedCount` < `UsageLimit`

---

## Database Schema

### Bảng promotions
```sql
id                 Guid (PK)
promo_code         VARCHAR(50)
description        VARCHAR(255)
discount_type      VARCHAR(20)    -- 'percent' | 'fixed'
discount_value     DECIMAL(10,2)
start_date         DATE
end_date           DATE
min_order_amount   DECIMAL(10,2)
usage_limit        INT
used_count         INT            -- Tự động tăng/giảm khi apply/đổi promo
status             VARCHAR(20)    -- 'active' | 'inactive'
```

### Bảng orders
```sql
id                 Guid (PK)
promo_id           Guid (FK)      -- Tham chiếu đến promotions.id
discount_amount    DECIMAL(10,2)  -- Số tiền được giảm
total_amount       DECIMAL(10,2)  -- Tổng đơn hàng
```


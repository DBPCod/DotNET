-- Update Promotion table schema to fix data type issues
-- Run this script in your MySQL database

-- Update discount_type column to varchar(20) instead of longtext
ALTER TABLE promotions 
MODIFY COLUMN discount_type VARCHAR(20) NOT NULL;

-- Update status column to varchar(20) instead of longtext  
ALTER TABLE promotions 
MODIFY COLUMN status VARCHAR(20) NOT NULL DEFAULT 'active';

-- Add default values for existing records
UPDATE promotions 
SET 
    discount_type = LOWER(discount_type),
    status = LOWER(status),
    start_date = CASE 
        WHEN start_date = '0001-01-01' THEN CURDATE() 
        ELSE start_date 
    END,
    end_date = CASE 
        WHEN end_date = '0001-01-01' THEN DATE_ADD(CURDATE(), INTERVAL 30 DAY)
        ELSE end_date 
    END,
    discount_value = CASE 
        WHEN discount_value = 0 THEN 10.00 
        ELSE discount_value 
    END
WHERE 
    start_date = '0001-01-01' 
    OR end_date = '0001-01-01' 
    OR discount_value = 0;

-- Add constraints
ALTER TABLE promotions 
ADD CONSTRAINT chk_discount_type 
CHECK (discount_type IN ('percent', 'fixed'));

ALTER TABLE promotions 
ADD CONSTRAINT chk_status 
CHECK (status IN ('active', 'inactive'));

ALTER TABLE promotions 
ADD CONSTRAINT chk_discount_value 
CHECK (discount_value > 0);

ALTER TABLE promotions 
ADD CONSTRAINT chk_dates 
CHECK (end_date >= start_date);

-- Add index for better performance
CREATE INDEX idx_promotions_promo_code ON promotions(promo_code);
CREATE INDEX idx_promotions_status ON promotions(status);
CREATE INDEX idx_promotions_dates ON promotions(start_date, end_date);


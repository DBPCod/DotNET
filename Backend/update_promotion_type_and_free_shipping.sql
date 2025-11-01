-- Update Promotion table to add promotion_type column and support free_shipping
-- Run this script in your MySQL database

-- Check and add promotion_type column
-- Note: Run this manually if column doesn't exist
-- ALTER TABLE promotions 
-- ADD COLUMN promotion_type VARCHAR(20) DEFAULT 'promotion' AFTER discount_value;

-- Update existing records to have promotion_type = 'promotion'
UPDATE promotions 
SET promotion_type = 'promotion' 
WHERE promotion_type IS NULL OR promotion_type = '';

-- Note: MySQL doesn't support IF NOT EXISTS for ALTER TABLE
-- If column doesn't exist, manually run:
-- ALTER TABLE promotions ADD COLUMN promotion_type VARCHAR(20) DEFAULT 'promotion' AFTER discount_value;

-- Note: Constraints in MySQL work differently
-- The CHECK constraints will be enforced in application code
-- For database level, you may need to modify existing constraints manually


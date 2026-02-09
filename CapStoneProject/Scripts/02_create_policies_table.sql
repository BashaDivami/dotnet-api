-- Create policies table
CREATE TABLE IF NOT EXISTS policies (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    premium_amount DECIMAL(18,2) NOT NULL CHECK (premium_amount > 0),
    description TEXT,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
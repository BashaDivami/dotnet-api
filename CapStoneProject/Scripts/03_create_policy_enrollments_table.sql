-- Create policy_enrollments table
CREATE TABLE IF NOT EXISTS policy_enrollments (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    policy_id INTEGER NOT NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    requested_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    approved_at TIMESTAMP,
    approved_by INTEGER,
    rejection_reason TEXT,
    
    -- Foreign key constraints
    CONSTRAINT fk_enrollment_user FOREIGN KEY (user_id) 
        REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_enrollment_policy FOREIGN KEY (policy_id) 
        REFERENCES policies(id) ON DELETE CASCADE,
    CONSTRAINT fk_enrollment_approver FOREIGN KEY (approved_by) 
        REFERENCES users(id) ON DELETE SET NULL,
    
    -- Unique constraint: user can only enroll in a policy once
    CONSTRAINT uq_user_policy UNIQUE (user_id, policy_id),
    
    -- Check constraint for valid status values
    CONSTRAINT chk_enrollment_status CHECK (status IN ('Pending', 'Approved', 'Rejected'))
);
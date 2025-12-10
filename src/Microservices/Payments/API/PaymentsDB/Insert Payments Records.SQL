-- =========================================================
-- Drop tables in the reverse order of creation.
-- =========================================================
DROP TABLE IF EXISTS PaymentEvents;
DROP TABLE IF EXISTS PaymentTransactions;

-- =========================================================
-- Create table: PaymentTransactions
-- =========================================================
CREATE TABLE PaymentTransactions (
	PaymentId INTEGER PRIMARY KEY AUTOINCREMENT, 
	
	-- The link to the Orders Microservice. 
	-- Orders Microservice will use this ID to query Payment details.
	-- We use a simple INTEGER. A FOREIGN KEY constraint cannot be enforced because Orders Microservice has its own DB, and you cannot (and should not) make a FK reference.
	OrderId INTEGER NOT NULL UNIQUE, 
	
	Amount REAL NOT NULL, 
	Currency TEXT NOT NULL DEFAULT 'USD', 
	
	-- The current status of the payment (e.g., 'PENDING', 'COMPLETED', 'FAILED', 'REFUNDED')
	Status TEXT NOT NULL, 
	
	-- The method used for payment (e.g., 'Credit Card', 'PayPal', 'Wallet')
	PaymentMethod TEXT NOT NULL,
	
	-- Unique ID provided by the payment gateway (e.g., Stripe, PayPal transaction ID)
	GatewayTransactionId TEXT UNIQUE, 
	
	-- Timestamp for when the payment record was created
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
	-- Timestamp for when the status was last updated
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP 
);

-- =========================================================
-- Create table: PaymentEvents
-- Logs every state change or action for a given payment.
-- =========================================================
CREATE TABLE PaymentEvents (
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	
	-- Foreign Key to the main transaction in this microservice
	PaymentId INTEGER NOT NULL, 
	
	-- What kind of event occurred (e.g., 'INITIATED', 'GATEWAY_SUCCESS', 'WEBHOOK_RECEIVED', 'REFUND_REQUESTED')
	EventType TEXT NOT NULL, 
	
	-- The status after this event (e.g., 'PENDING', 'COMPLETED')
	Status TEXT NOT NULL, 
	
	-- Timestamp of the event
	EventTime DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
	-- Optional: Any relevant detail/payload from the gateway or service
	Details TEXT, 
	
	-- The ONLY foreign key constraint in this microservice database:
	FOREIGN KEY (PaymentId) REFERENCES PaymentTransactions(PaymentId) ON DELETE CASCADE
);

-- =========================================================
-- Insert data into table: PaymentTransactions
-- OrderId matches the primary key (Id) in the external Orders table.
-- =========================================================
INSERT INTO PaymentTransactions (OrderId, Amount, Currency, Status, PaymentMethod, GatewayTransactionId, CreatedAt, UpdatedAt) VALUES
-- Order 1: Alice Johnson, Total 150.75
(1, 150.75, 'USD', 'COMPLETED', 'Credit Card', 'GTID_452A', '2025-11-20 10:30:00', '2025-11-20 10:30:15'), 

-- Order 2: Bob Williams, Total 59.99
(2, 59.99, 'USD', 'REFUNDED', 'PayPal', 'GTID_101B', '2025-11-21 14:45:00', '2025-11-22 09:00:00'),         

-- Order 3: Alice Johnson, Total 89.50
(3, 89.50, 'USD', 'FAILED', 'Debit Card', 'GTID_223C', '2025-11-21 16:00:00', '2025-11-21 16:01:00');

-- =========================================================
-- Insert data into table: PaymentEvents
-- Logs the lifecycle of each payment transaction
-- =========================================================

-- Events for OrderId 1 (COMPLETED)
INSERT INTO PaymentEvents (PaymentId, EventType, Status, Details) VALUES
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 1), 'INITIATED', 'PENDING', 'Payment request sent.'),
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 1), 'GATEWAY_SUCCESS', 'COMPLETED', 'Transaction approved by bank.');

-- Events for OrderId 2 (REFUNDED)
INSERT INTO PaymentEvents (PaymentId, EventType, Status, Details) VALUES
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 2), 'INITIATED', 'PENDING', 'Payment request sent.'),
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 2), 'GATEWAY_SUCCESS', 'COMPLETED', 'Transaction approved.'),
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 2), 'REFUND_REQUESTED', 'REFUND_PENDING', 'Customer initiated return.'),
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 2), 'REFUND_COMPLETED', 'REFUNDED', 'Refund processed by gateway.');


-- Events for OrderId 3 (FAILED)
INSERT INTO PaymentEvents (PaymentId, EventType, Status, Details) VALUES
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 3), 'INITIATED', 'PENDING', 'Payment request sent.'),
((SELECT PaymentId FROM PaymentTransactions WHERE OrderId = 3), 'GATEWAY_ERROR', 'FAILED', 'Insufficient funds error code: 51.');

-- =========================================================
-- SELECT statement for Payments Microservice DB.
-- =========================================================
SELECT
    pt.PaymentId,
    pt.OrderId AS ExternalOrderId, -- The key to call the Orders Microservice
    pt.Amount,
    pt.Status AS CurrentPaymentStatus,
    pt.CreatedAt AS PaymentInitiatedTime,
    -- Concatenating the full event history for auditing
    GROUP_CONCAT(pe.EventType || ' (' || pe.Status || ') @ ' || pe.EventTime, ' | ') AS PaymentAuditTrail
FROM
    PaymentTransactions AS pt
LEFT JOIN
    PaymentEvents pe ON pt.PaymentId = pe.PaymentId
GROUP BY
    pt.PaymentId, pt.OrderId, pt.Amount, pt.Status, pt.CreatedAt
ORDER BY
    pt.PaymentId;
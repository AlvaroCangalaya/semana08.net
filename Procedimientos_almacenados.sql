use Semana07
CREATE PROCEDURE USP_listar_customers
AS
BEGIN
    SELECT customer_id, name, address, phone, active
    FROM customers
    WHERE active = 1;
END
GO

USP_listar_customers;

CREATE PROCEDURE USP_find_customers_by_name
    @nombre NVARCHAR(255)
AS
BEGIN
    SELECT customer_id, name, address, phone, active
    FROM customers
    WHERE active = 1 AND name LIKE '%' + @nombre + '%';
END
GO

USP_find_customers_by_name 'DOE'

CREATE OR ALTER PROCEDURE USP_insert_customer
    @name NVARCHAR(255),
    @address NVARCHAR(255),
    @phone NVARCHAR(15)
AS
BEGIN
    INSERT INTO customers (name, address, phone, active)
    VALUES (@name, @address, @phone, 1)
END


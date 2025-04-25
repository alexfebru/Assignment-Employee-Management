create table users(
	id int primary key identity(1,1),
	username varchar(max) null,
	password varchar(max) null,
	date_register date default getdate()
)

select *from users;

INSERT INTO Users (Username, Password)
VALUES ('admin', 'admin123')


create table employees(
	id int primary key identity (1,1),
	employee_id varchar(max) null,
	full_name varchar(max) null,
	gender varchar(max) null, 
	contact_number varchar(max) null,
	position varchar(max) null,
	image varchar(max) null,
	salary int null,
	status varchar (max),
	insert_date date null,
	update_date date null,
	delete_date date 
)

select *from employees;


create table Departments (
    Id int primary key identity(1,1),
    Name VARCHAR(MAX)
);

ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Departments
FOREIGN KEY (Department_Id) REFERENCES Departments(Id);


-- Insert some sample data
/*INSERT INTO Employees (Employee_Id, Full_Name, Gender, Contact_Number, Position, Salary)
VALUES
('EMP001', 'John Doe', 'Male', '123-456-7890', 'Software Engineer', 50000),
('EMP002', 'Jane Smith', 'Female', '987-654-3210', 'Project Manager', 70000),
('EMP003', 'Michael Johnson', 'Male', '555-555-5555', 'Sales Manager', 60000); */

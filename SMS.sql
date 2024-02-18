CREATE TABLE users(
id int PRIMARY KEY IDENTITY(1,1),
username varchar(max) NULL,
password varchar(max) NULL
);
SELECT * FROM users
insert users(username,password)values('abc','abc1');

/*
SELECT * FROM BOOKINGS;
SELECT * FROM ROOMTYPES;
SELECT * FROM ROOMS;
SELECT * FROM ROOMTYPEIMAGES;
SELECT * FROM EMPLOYEES;
SELECT * FROM CUSTOMERS;
SELECT * FROM FACILITIES;
SELECT * FROM FACILITYAPPLIES;
SELECT * FROM ACCOUNTS;
SELECT * FROM SERVICES;
SELECT * FROM BOOKINGSERVICES;
SELECT * FROM FEEDBACKS;
SELECT * FROM FEEDBACKIMAGES;


DELETE FROM FACILITYAPPLIES;
DELETE FROM FACILITIES;
DELETE FROM ROOMTYPEIMAGES;
DELETE FROM BOOKINGS;
DELETE FROM ROOMTYPES;
DELETE FROM ROOMS;
DELETE FROM EMPLOYEES;
DELETE FROM CUSTOMERS;
DELETE FROM ACCOUNTS;
DELETE FROM SERVICES;
DELETE FROM BOOKINGSERVICES;
DELETE FROM FEEDBACKS;
DELETE FROM FEEDBACKIMAGES;
*/
GO



CREATE OR ALTER TRIGGER TRG_ROOM_INSERT ON ROOMS AFTER INSERT AS
BEGIN
	UPDATE ROOMTYPES
	SET QUANTITY = QUANTITY + 1
	FROM INSERTED
	WHERE ROOMTYPES.ROOMTYPEID=INSERTED.ROOMTYPEID;
END
GO

CREATE OR ALTER TRIGGER TRG_ROOM_DELETE ON ROOMS FOR DELETE AS
BEGIN
	UPDATE ROOMTYPES
	SET QUANTITY = QUANTITY - (SELECT COUNT(*) FROM DELETED WHERE DELETED.ROOMTYPEID=ROOMTYPES.ROOMTYPEID)
END
GO

CREATE OR ALTER TRIGGER TRG_ROOM_UPDATE ON ROOMS AFTER UPDATE AS
BEGIN
	UPDATE ROOMTYPES
	SET QUANTITY = QUANTITY - 1
	FROM DELETED
	WHERE ROOMTYPES.ROOMTYPEID=DELETED.ROOMTYPEID;

	UPDATE ROOMTYPES
	SET QUANTITY = QUANTITY + 1
	FROM INSERTED
	WHERE ROOMTYPES.ROOMTYPEID=INSERTED.ROOMTYPEID;
END
GO


SET IDENTITY_INSERT ROOMTYPES ON;
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('1', 'Small Normal Room', 20, 0, 2, 2, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('2', 'Medium Normal Room', 35, 0, 4, 2, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('3', 'Large Normal Room', 65, 0, 6, 4, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('4', 'Small Vip Room', 30, 0, 2, 2, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('5', 'Medium Vip Room', 60, 0, 4, 4, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('6', 'Large Vip Room', 119, 0, 6, 6, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('7', 'Small King Room', 60, 0, 2, 2, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('8', 'Medium King Room', 119, 0, 4, 2, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('9', 'Large King Room', 199, 0, 6, 4, 'Description');
INSERT INTO ROOMTYPES ([RoomTypeId], [Name],[Price],[Quantity],[MaxAdult],[MaxChild],[Description]) VALUES ('10', 'President Room', 300, 0, 2, 2, 'Description');
SET IDENTITY_INSERT ROOMTYPES OFF;


INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('1', '1/img1.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('1', '1/img2.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('1', '1/img3.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('2', '2/img4.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('2', '2/img5.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('2', '2/img6.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('2', '2/img7.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('3', '3/img8.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('3', '3/img9.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('3', '3/img10.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('3', '3/img1.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('3', '3/img2.png', 'Image 5', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('4', '4/img1.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('4', '4/img2.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('4', '4/img3.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('5', '5/img4.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('5', '5/img5.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('5', '5/img6.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('5', '5/img7.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('6', '6/img8.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('6', '6/img9.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('6', '6/img10.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('6', '6/img1.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('6', '6/img2.png', 'Image 5', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('7', '7/img1.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('7', '7/img2.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('7', '7/img3.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('8', '8/img4.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('8', '8/img5.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('8', '8/img6.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('8', '8/img7.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('9', '9/img8.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('9', '9/img9.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('9', '9/img10.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('9', '9/img1.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('9', '9/img2.png', 'Image 5', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('10', '10/img8.png', 'Image 1', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('10', '10/img9.png', 'Image 2', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('10', '10/img10.png', 'Image 3', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('10', '10/img1.png', 'Image 4', GETDATE());
INSERT INTO ROOMTYPEIMAGES ([RoomTypeId],[ImageUrl],[ImageName],[CreateDate]) VALUES ('10', '10/img2.png', 'Image 5', GETDATE());



INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('101', '1', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('102', '1', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('103', '1', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('104', '1', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('105', '2', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('201', '2', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('202', '2', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('203', '3', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('204', '3', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('205', '4', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('301', '4', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('302', '4', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('303', '4', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('304', '5', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('305', '5', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('401', '5', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('402', '6', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('403', '6', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('404', '7', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('405', '7', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('501', '8', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('502', '8', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('503', '9', '');
INSERT INTO ROOMS ([RoomNo],[RoomTypeId],[Description]) VALUES ('504', '10', '');


SET IDENTITY_INSERT FACILITIES ON;
INSERT INTO FACILITIES ([FacId],[Name],[ImageUrl]) VALUES ('1', 'Bed', 'bed.png');
INSERT INTO FACILITIES ([FacId],[Name],[ImageUrl]) VALUES ('2', 'Air Conditioner', 'air-conditioner.png');
INSERT INTO FACILITIES ([FacId],[Name],[ImageUrl]) VALUES ('3', 'Sofa', 'sofa.png');
INSERT INTO FACILITIES ([FacId],[Name],[ImageUrl]) VALUES ('4', 'Television', 'television.png');
INSERT INTO FACILITIES ([FacId],[Name],[ImageUrl]) VALUES ('5', 'Refrigerator', 'refrigerator.png');
SET IDENTITY_INSERT FACILITIES OFF;

SET IDENTITY_INSERT FACILITIES ON;
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '1', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '2', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '3', '3');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '4', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '5', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '6', '3');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '7', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '8', '3');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '9', '4');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('1', '10', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '1', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '2', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '3', '3');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '4', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '5', '3');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '6', '4');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '7', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '8', '3');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '9', '4');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('2', '10', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '4', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '5', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '6', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '7', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '8', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '9', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('3', '10', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '1', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '2', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '3', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '4', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '5', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '6', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '7', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '8', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '9', '2');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('4', '10', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '1', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '2', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '3', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '4', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '5', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '6', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '7', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '8', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '9', '1');
INSERT INTO FACILITYAPPLIES ([FacId],[RoomTypeId],[Amount]) VALUES ('5', '10', '1');
SET IDENTITY_INSERT FACILITIES OFF;


SET IDENTITY_INSERT ACCOUNTS ON;
INSERT INTO ACCOUNTS ([AccId],[Username],[Password],[RoleName],[CreateDate]) VALUES ('1', 'manager', '1', 'manager', GETDATE());
INSERT INTO ACCOUNTS ([AccId],[Username],[Password],[RoleName],[CreateDate]) VALUES ('2', 'employee', '1', 'employee', GETDATE());
INSERT INTO ACCOUNTS ([AccId],[Username],[Password],[RoleName],[CreateDate]) VALUES ('3', 'customer', '1', 'customer', GETDATE());
SET IDENTITY_INSERT ACCOUNTS OFF;


SET IDENTITY_INSERT EMPLOYEES ON;
INSERT INTO EMPLOYEES ([EmpId],[AccId],[Name],[Gender],[Phone],[Address],[Salary]) VALUES ('1', '2', 'Manager 1', 'Female', '0902222222', 'HCM', 222);
INSERT INTO EMPLOYEES ([EmpId],[AccId],[Name],[Gender],[Phone],[Address],[Salary]) VALUES ('2', '3', 'Employee 1', 'Male', '0901111111', 'HCM', 111);
SET IDENTITY_INSERT EMPLOYEES OFF;

SET IDENTITY_INSERT CUSTOMERS ON;
INSERT INTO CUSTOMERS ([CusId],[AccId],[Name],[Gender],[Phone],[Email],[CitizencardId],[CountryCode]) VALUES ('1', '3', 'Nguyen Van A', 'Male', '0901111111', '20521627@gm.uit.edu.vn', '123', 'VN');
INSERT INTO CUSTOMERS ([CusId],[AccId],[Name],[Gender],[Phone],[Email],[CitizencardId],[CountryCode]) VALUES ('2', null, 'Nguyen Van B', 'Female', '0902222222', 'dodangkiennam@gmail.com', '123', 'VN');
SET IDENTITY_INSERT CUSTOMERS OFF;


SET IDENTITY_INSERT dbo.SERVICES ON;
INSERT INTO dbo.SERVICES ([ServiceId], [Name], [ImageUrl], [Price]) VALUES ('1', 'Breakfast', 'breakfast.png', '10');
INSERT INTO dbo.SERVICES ([ServiceId], [Name], [ImageUrl], [Price]) VALUES ('2', 'Lunch', 'lunch.png', '15');
INSERT INTO dbo.SERVICES ([ServiceId], [Name], [ImageUrl], [Price]) VALUES ('3', 'Dinner', 'dinner.png', '20');
INSERT INTO dbo.SERVICES ([ServiceId], [Name], [ImageUrl], [Price]) VALUES ('4', 'Drink', 'drink.png', '4');
INSERT INTO dbo.SERVICES ([ServiceId], [Name], [ImageUrl], [Price]) VALUES ('5', 'Fast food', 'fastfood.png', '10');
INSERT INTO dbo.SERVICES ([ServiceId], [Name], [ImageUrl], [Price]) VALUES ('6', 'Spa', 'spa.png', '20');
SET IDENTITY_INSERT dbo.SERVICES OFF;


SET IDENTITY_INSERT FEEDBACKS ON;
INSERT INTO FEEDBACKS ([FeedbackId], [Content],[Rating],[CreateDate]) VALUES ('1', 'Very Good', '5', '06/12/2022 07:30:00');
SET IDENTITY_INSERT FEEDBACKS OFF;


INSERT INTO FEEDBACKIMAGES ([FeedbackId],[ImageUrl]) VALUES ('1', 'random.jpg');


--All status: PENDING, BOOKING_SUCCESS, CANCEL, CHECKED_IN, CHECKED_OUT
SET DATEFORMAT dmy;
SET IDENTITY_INSERT BOOKINGS ON;
INSERT INTO BOOKINGS ([BookingId],[CusId],[EmpId],[RoomNo],[RoomTypeId],[RoomAmount],[CreateDate],[CheckIn],[CheckOut],[TotalPrice],[Status]) 
VALUES ('1', '1', '1', '101', '1', '1', '01/12/2022', '03/12/2022', '05/12/2022', '40', 'CHECKED_OUT');
INSERT INTO BOOKINGS ([BookingId],[CusId],[EmpId],[RoomNo],[RoomTypeId],[RoomAmount],[CreateDate],[CheckIn],[CheckOut],[TotalPrice],[Status]) 
VALUES ('2', '1', null, null, '1', '1', '15/12/2022', '31/12/2022', '07/1/2023', '140', 'PENDING');
INSERT INTO BOOKINGS ([BookingId],[CusId],[EmpId],[RoomNo],[RoomTypeId],[RoomAmount],[CreateDate],[CheckIn],[CheckOut],[TotalPrice],[Status]) 
VALUES ('3', '2', '1', null, '1', '1', '15/12/2022', '01/01/2023', '07/1/2023', '120', 'BOOKING_SUCCESS');
SET IDENTITY_INSERT BOOKINGS OFF;


INSERT INTO BOOKINGSERVICES ([ServiceId],[BookingId],[OrderTime],[Amount]) VALUES ('1', '1', '04/12/2022 07:30:00', '1');
INSERT INTO BOOKINGSERVICES ([ServiceId],[BookingId],[OrderTime],[Amount]) VALUES ('2', '1', '04/12/2022 11:30:00', '1');
INSERT INTO BOOKINGSERVICES ([ServiceId],[BookingId],[OrderTime],[Amount]) VALUES ('2', '1', '04/12/2022 17:00:00', '1');





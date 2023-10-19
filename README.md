# HotelManagement
This is an application for managing a hotel, built with ASP.NET Core MVC (.NET 6).
## Installation Prerequisites
- SQL server
- .NET (version 6 or higher)
- Environments (AppSettings.json):
  + Add your connection string at "ConnectionStrings__DbConnection" 
  + This project have used IronPdf to generate PDF, so set your license key provided by IronPdf for "IronPdf.LicenseKey"
  + For sending email with MailKit, you must configure username (email), password (email password) inside "EmailConfiguration"
## Installation
1. Clone project
```
git clone https://github.com/dodangkiennam/HotelManagement.git
```
2. Move to project folder
```
cd HotelManagement/HotelManagement
```
3. Generate database
```
dotnet ef database update
```
4. Run project
```
dotnet run
```
5. Seed database (optional)

Once the database is generated, you can run data.sql (located in database folder) to insert mocking data and create some triggers.
<br/>Otherwise, you can run DDL.sql for creating database and tables.
## Project info
### Actors
| Actor  | Description |
| ------------- | ------------- |
| Customer  | Search room, create/cancel/view booking, view booking history, send feedback |
| Employee  | Add/Search customer info, create/cancel booking for customer, view/approve booking requests, choose room for customer |
| Manager  | Manage (CRUD) employee, room, room type, booking |
### Database diagram
![Database diagram](https://github.com/dodangkiennam/HotelManagement/blob/master/files/db_diagram.drawio.png)




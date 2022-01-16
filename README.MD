# Introduction to Car Rental Management System

A car rental or car hire agencies are private companies that provide short-time leasing vehicles for a specified time with a fee to their customers. This is the definition of car renting.

In Sri Lanka, car rental service increasingly becomes the preferred option for most people, especially among students on campuses, universities, and families. But most of car rental organizations are using manual methods to store information of customers. And all of the work is been done in papers.

In order to make this paper works as computerized stuff, The Car Renting Management System was developed. The work of the renting process and other processes such as storing customer information, card details can be easily done using this car renting management system.

This provides a rich and fair working environment for the system admin. This Car Renting Management System is a desktop application that is used to handle paperwork inside the computer.

This software, called Car Renting Management System (CRMS), will allow the company to access its database securely and safely in a user-friendly environment.

The software can be customizable according to the business needs such as per day mileage units, driver salary and etc.

This application is pretty easy to get well on it since it has many easily understandable features. Therefore, the person who works in this system can quickly pick up the working methodologies.

There are many modules inside the system for different sorts of purposes.



### Programming Languages & Frameworks
- C#
- MySQL
- XML
- Bunifu
- Metro UI
- Mono Flat
- .NET Framework 4.6


## Configuration & Setup

**Step 01:-** Create a database on MySQL with the name of "rentcarsystem".

**Step 02:-** Export the database to the empty created database. Note: the database is found on the "1-database folder (rentcarsystem.sql)". Upload this database to the empty database on MySQL management panel.

**Step 03:-** Open the profile-files folder with Visual Studio and build then run the application.

### Login Information

#### Admin Login

- *Username:* kuna
- *Password:* 123

#### Staff Login

- *Username:* guna
- *Password:* guna123


## System Background

This Car Renting Management System has two types of accounts staff login and admin login. The admin has full power than the staff account.

The user interface of this system was created with the help of the Bunifu framework which is rich and good-looking user interface framework for C# windows from applications. This system also uses the Mono-Flat user interface Framework for textboxes and headers. Not only this but also, this system has used Metro framework for the First from loading. There are three types of user interface frameworks has been used in this system such as Bunifu, Mono-Flat and Metro.

This system is used MySQL database for data storing purpose. The name of the database is “rentcarsystem”. It has some tables inside it. XML is another technology that has been used inside the system.

In the MySQL database, only the data of cars, customers, booking details and other system data are stored. But in the XML, the system configuration such as settings and other system configurations data are stored.

The system uses .NET framework version 4.6. The reason for the 4.6 version of .NET framework is Bunifu user interface does not work on the earlier version of .NET framework. It works on newer version. Therefore, we have selected the newer version of .NET framework.

## Problem Statements

The process on searching the client details is slow if the company is using manual system and there are thousands of clients. Besides that, staffs have to record the booking manually and it is difficult to produce a monthly report or an annual report. Manual system does not allow client to booking online and hard to keep track on the record of rental cars.

### Objective

The main objective of this system is to provide convenience to the management team by developing a computerized system to make processes regarding car rental easier. In distinctive, the objectives of Car Renting Management System are:

- To enhance searching speed for all information such as client and rental.
- To provide customizable settings for the organization.  
- To provide report generation and analyses the rental of car to give better decision making.

### Project significance

CRMS sharply improve the searching speed of staffs, drivers and client’s details for the user because staff who works in the system can insert key word to search them. Besides that, Admin can get benefit because it manages the booking of cars by show available cars for client and always records every single booking to easy the company report so that admin and staff can just print the report. Admin also does not have to calculate the profit for cars owner because the system will produce report and calculate the profit sharing. CRMS also give client benefit because allow them make booking at anywhere and anytime before they want it.

## Entire function of the system

In this section, the entire function of the Car Renting Management System is going to explain from scratch. For the explanation purpose, the screenshots of the applications and some important code snippets will be used to demonstrate the function of the system.
This section will give a clear statement of the modules of the car renting management system with the perfect explanation. This car rental document has some more details information about the application.

### Essential Functions
- Car Management
- Customer Management
- Driver Management
- Booking Process
- Returning Car  
- Reports
- Profits Management
- Notifications => Notifies cars that have to return on the current date.

These are main functions that occurs inside the car renting management system. beyond this there are several other types of functions are occurred inside the system. This structure of this application is so different. Because this application uses MySQL and XML as the main component of the database. MySQL is used to store data about the car information or other bossiness details hence XML sore data about the car renting management system configurations.

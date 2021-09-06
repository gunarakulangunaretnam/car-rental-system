-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Aug 19, 2018 at 08:07 PM
-- Server version: 10.1.16-MariaDB
-- PHP Version: 7.0.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `rentcarsystem`
--

-- --------------------------------------------------------

--
-- Table structure for table `booking`
--

CREATE TABLE `booking` (
  `orderId` varchar(12) NOT NULL,
  `carId` varchar(11) NOT NULL,
  `cusId` varchar(11) NOT NULL,
  `driverId` varchar(50) NOT NULL,
  `rentedDate` varchar(20) NOT NULL,
  `returnDate` varchar(20) NOT NULL,
  `basis` varchar(12) NOT NULL,
  `duration` int(12) NOT NULL,
  `TotalAmount` double NOT NULL,
  `driverAmount` double NOT NULL,
  `advancedAmount` double NOT NULL,
  `blance` double NOT NULL,
  `startMileage` varchar(300) NOT NULL,
  `totalMileage` varchar(20) NOT NULL,
  `perAmount` double NOT NULL,
  `DiscountAmount` double NOT NULL,
  `DiscountPer` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `booking`
--

INSERT INTO `booking` (`orderId`, `carId`, `cusId`, `driverId`, `rentedDate`, `returnDate`, `basis`, `duration`, `TotalAmount`, `driverAmount`, `advancedAmount`, `blance`, `startMileage`, `totalMileage`, `perAmount`, `DiscountAmount`, `DiscountPer`) VALUES
('ORD-029', 'xp-002', 'CUS-001', 'DRI-002', '2018-08-19', '2018-08-24', 'Daily', 5, 5000, 2500, 1250, 3750, '99990', '500', 500, 0, '0');

-- --------------------------------------------------------

--
-- Table structure for table `car`
--

CREATE TABLE `car` (
  `carId` varchar(20) NOT NULL,
  `carModel` varchar(20) NOT NULL,
  `carBrand` varchar(25) NOT NULL,
  `carMakeYear` int(5) NOT NULL,
  `carColor` varchar(15) NOT NULL,
  `carDailyRate` double NOT NULL,
  `carMonthlyRate` double NOT NULL,
  `carStatus` text NOT NULL,
  `odometer` varchar(300) NOT NULL,
  `kmmeter_type` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `car`
--

INSERT INTO `car` (`carId`, `carModel`, `carBrand`, `carMakeYear`, `carColor`, `carDailyRate`, `carMonthlyRate`, `carStatus`, `odometer`, `kmmeter_type`) VALUES
('xp-001', 'b12', 'BMW ', 2018, 'Red', 500, 15000, 'IN', '00495', 'Normal'),
('xp-002', 'b12', 'BMW ', 2018, 'Red', 500, 15000, 'OUT', '99990', 'Digital');

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `customerId` varchar(100) NOT NULL,
  `customerFname` text NOT NULL,
  `customerLname` varchar(25) NOT NULL,
  `customerAddress` varchar(300) NOT NULL,
  `customerPhoneNo` int(25) NOT NULL,
  `customerIdentityCard` varchar(50) NOT NULL,
  `customerLicence` varchar(50) NOT NULL,
  `customerGender` char(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`customerId`, `customerFname`, `customerLname`, `customerAddress`, `customerPhoneNo`, `customerIdentityCard`, `customerLicence`, `customerGender`) VALUES
('CUS-001', 'Kuna', 'Rakulan', 'Kallady 05', 756800519, '97720110V', '5565656565', 'M'),
('CUS-002', 'Maker', 'doer', 'daldlakdk', 565656, '565656', '5656', 'M'),
('CUS-003', 'Doer', 'Maker', 'dadadas', 565656, '6565656', '5656', 'M'),
('CUS-004', 'JOthn', 'dasldla', '5fdas5d6a56d6', 656565656, '565656', '565656', 'M'),
('CUS-005', 'Huth', 'katk', 'dasdaslkdlas', 565656, '56565656', '56556', 'F'),
('CUS-006', 'Jathu', 'meo', 'kalla', 2147483647, '565656565', '565656656', 'F');

-- --------------------------------------------------------

--
-- Table structure for table `driver`
--

CREATE TABLE `driver` (
  `driverId` varchar(100) NOT NULL,
  `driverFname` text NOT NULL,
  `driverLname` varchar(25) NOT NULL,
  `driverAddress` varchar(300) NOT NULL,
  `driverPhoneNo` int(25) NOT NULL,
  `driverIdentityCard` varchar(50) NOT NULL,
  `driverLicence` varchar(50) NOT NULL,
  `driverGender` char(2) NOT NULL,
  `driverStatus` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `driver`
--

INSERT INTO `driver` (`driverId`, `driverFname`, `driverLname`, `driverAddress`, `driverPhoneNo`, `driverIdentityCard`, `driverLicence`, `driverGender`, `driverStatus`) VALUES
('DRI-001', 'athu', 'men', 'dasdlald', 5656565, '565656', '5656', 'M', 'IN'),
('DRI-002', 'iahan', 'dlald', 'hdashdhaskd', 56565656, '56565656', '5656', 'M', 'OUT'),
('DRI-003', 'ginger', 'kuna', '5d5a6d56a5d656asdadafffad', 6565656, '565656', '565656', 'M', 'IN'),
('DRI-004', 'Pathu', 'kuthu', 'dad6a5d656ad56', 5656565, '6565656', '565656', 'F', 'IN'),
('DRI-005', 'jothy', 'dladklad', 'ldklakdl', 565656, '565656', '5656', 'F', 'IN'),
('DRI-006', 'Mathu', 'menan', 'Kllllaoidoa', 2147483647, '56565656', '656556', 'M', 'IN'),
('DRI-007', 'huge', 'lattien', 'kallady', 656556, '565666', '56556', 'M', 'IN'),
('DRI-008', 'jhon', 'guna', 'kallady', 756800519, '5277189', '56789', 'M', 'IN');

-- --------------------------------------------------------

--
-- Table structure for table `oldbookdata`
--

CREATE TABLE `oldbookdata` (
  `oderId` varchar(20) NOT NULL,
  `carId` varchar(30) NOT NULL,
  `cusId` varchar(30) NOT NULL,
  `driverId` varchar(25) NOT NULL,
  `rentalDate` varchar(20) NOT NULL,
  `returnedDate` varchar(20) NOT NULL,
  `duration` int(11) NOT NULL,
  `driversalary` double NOT NULL,
  `advancedAmount` double NOT NULL,
  `balanceAmount` double NOT NULL,
  `extraDay` int(11) NOT NULL,
  `extraDayAmount` double NOT NULL,
  `extradriverSalary` double NOT NULL,
  `extraMileage` int(11) NOT NULL,
  `extraMileageAmount` double NOT NULL,
  `TotalAmount` double NOT NULL,
  `basis` varchar(10) NOT NULL,
  `DiscountAmount` double NOT NULL,
  `DiscountPer` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `oldbookdata`
--

INSERT INTO `oldbookdata` (`oderId`, `carId`, `cusId`, `driverId`, `rentalDate`, `returnedDate`, `duration`, `driversalary`, `advancedAmount`, `balanceAmount`, `extraDay`, `extraDayAmount`, `extradriverSalary`, `extraMileage`, `extraMileageAmount`, `TotalAmount`, `basis`, `DiscountAmount`, `DiscountPer`) VALUES
('ORD-001', 'Car-65656', 'CUS-001', 'No-Driver', '2018-06-03', '2018-08-04', 2, 0, 15500, 44000, 1, 500, 0, 1, 5, 60005, 'Monthly', 500, '10%'),
('ORD-002', 'poiyt-5656', 'CUS-002', 'No-Driver', '2018-06-03', '2018-08-03', 2, 0, 1000, 3000, 1, 0, 0, 1, 2, 4002, 'Monthly', 0, '0'),
('ORD-003', 'Hu-9885-PO', 'CUS-002', 'DRI-004', '2018-06-03', '2018-06-06', 2, 1500, 10250, 30750, 1, 20000, 500, 93716, 18743200, 18804700, 'Daily', 0, '0'),
('ORD-004', 'UO-9875WP', 'CUS-002', 'No-Driver', '2018-06-03', '2018-08-03', 2, 0, 5000, 13000, 0, 0, 0, 69707, 348535, 366535, 'Monthly', 6000, '25%'),
('ORD-005', 'poiyt-5656', 'CUS-001', 'No-Driver', '2018-08-01', '2018-08-07', 5, 0, 50, 600, 1, 200, 0, 93399, 186798, 187648, 'Daily', 350, '25%'),
('ORD-006', 'UY-5545', 'CUS-001', 'No-Driver', '2018-06-03', '2018-08-03', 2, 0, 20250, 30750, 0, 0, 0, 16108, 161080, 212080, 'Monthly', 9000, '10%'),
('ORD-007', 'gh-56565', 'CUS-002', 'No-Driver', '2018-06-03', '2018-06-09', 6, 0, 1500, 1500, 0, 0, 0, 0, 0, 3000, 'Daily', 0, '0'),
('ORD-008', 'Car-65656', 'CUS-001', 'No-Driver', '2018-06-03', '2018-06-09', 5, 0, 1250, 1250, 1, 500, 0, 26632, 133160, 136160, 'Daily', 0, '0'),
('ORD-009', 'Hu-9885-PO', 'CUS-001', 'DRI-001', '2018-07-14', '2018-09-15', 2, 30500, 29250, 87750, 1, 20000, 500, 0, 0, 137500, 'Monthly', 13000, '10%'),
('ORD-010', 'Car-65656', 'CUS-001', 'DRI-001', '2018-08-19', '2018-08-22', 2, 1500, 475, 1425, 1, 500, 500, 66367, 331835, 334735, 'Daily', 100, '5%'),
('ORD-011', 'gh-56565', 'CUS-003', 'DRI-002', '2018-08-19', '2018-08-25', 5, 3000, 625, 1875, 1, 500, 500, 0, 0, 3500, 'Daily', 2500, '50%'),
('ORD-012', 'poiyt-5656', 'CUS-001', 'DRI-002', '2018-08-19', '2018-08-24', 4, 2500, 700, 2100, 1, 200, 500, 0, 0, 3500, 'Daily', 0, '0'),
('ORD-013', 'Car-65656', 'CUS-002', 'DRI-003', '2018-08-19', '2018-10-20', 2, 30500, 10500, 31500, 1, 500, 500, 60566, 302830, 345830, 'Monthly', 18000, '30%'),
('ORD-014', 'Hu-9885-PO', 'CUS-003', 'No-Driver', '2018-08-19', '2018-08-25', 5, 0, 25000, 75000, 1, 20000, 0, 0, 0, 120000, 'Daily', 0, '0'),
('ORD-015', 'Car-65656', 'CUS-001', 'DRI-002', '2018-08-19', '2018-08-25', 6, 3000, 1500, 4500, 0, 0, 0, 32734, 163670, 169670, '', 0, '0'),
('ORD-016', 'Car-65656', 'CUS-001', 'DRI-001', '2018-08-19', '2018-08-24', 5, 2500, 1250, 3750, 0, 0, 0, 0, 0, 5000, 'Daily', 0, '0'),
('ORD-017', 'gh-56565', 'CUS-001', 'DRI-001', '2018-08-19', '2018-08-25', 6, 3000, 1500, 4500, 0, 0, 0, 99312, 496560, 502560, 'Daily', 0, '0'),
('ORD-018', 'Hu-9885-PO', 'CUS-003', 'No-Driver', '2018-08-19', '2018-08-28', 9, 0, 45000, 135000, 0, 0, 0, 0, 0, 180000, 'Daily', 0, '0'),
('ORD-019', 'gh-56565', 'CUS-002', 'DRI-002', '2018-08-19', '2018-08-27', 8, 4000, 2000, 6000, 0, 0, 0, 0, 0, 8000, 'Daily', 0, '0'),
('ORD-020', 'gh-56565', 'CUS-001', 'DRI-002', '2018-08-19', '2018-08-24', 5, 2500, 1250, 3750, 0, 0, 0, 0, 0, 5000, 'Daily', 0, '0'),
('ORD-021', 'xp-001', 'CUS-001', 'No-Driver', '2018-08-19', '2018-08-24', 5, 0, 625, 1875, 0, 0, 0, 99433, 497165, 499665, 'Daily', 0, '0'),
('ORD-022', 'xp-002', 'CUS-001', 'DRI-002', '2018-08-19', '2018-08-27', 8, 4000, 2000, 6000, 0, 0, 0, 99199, 495995, 503995, 'Daily', 0, '0'),
('ORD-023', 'xp-002', 'CUS-002', 'No-Driver', '2018-08-19', '2018-08-24', 5, 0, 625, 1875, 0, 0, 0, 54044, 270220, 272720, 'Daily', 0, '0'),
('ORD-024', 'xp-002', 'CUS-002', 'No-Driver', '2018-08-19', '2018-08-27', 8, 0, 1000, 3000, 0, 0, 0, 0, 0, 4000, 'Daily', 0, '0'),
('ORD-025', 'xp-001', 'CUS-001', 'No-Driver', '2018-08-19', '2018-08-24', 5, 0, 625, 1875, 0, 0, 0, 0, 0, 2500, 'Daily', 0, '0'),
('ORD-026', 'xp-001', 'CUS-001', 'No-Driver', '2018-08-19', '2018-08-24', 5, 0, 625, 1875, 0, 0, 0, 5, 25, 2525, 'Daily', 0, '0'),
('ORD-027', 'xp-002', 'CUS-001', 'DRI-001', '2018-08-19', '2018-12-19', 4, 60000, 30000, 90000, 0, 0, 0, 0, 0, 120000, 'Monthly', 0, '0'),
('ORD-028', 'xp-002', 'CUS-001', 'No-Driver', '2018-08-19', '2018-08-24', 5, 0, 625, 1875, 0, 0, 0, 0, 0, 2500, 'Daily', 0, '0');

-- --------------------------------------------------------

--
-- Table structure for table `systemcontroller`
--

CREATE TABLE `systemcontroller` (
  `deleteCustomer` int(11) NOT NULL,
  `deleteDriver` int(11) NOT NULL,
  `deleteOrder` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `systemcontroller`
--

INSERT INTO `systemcontroller` (`deleteCustomer`, `deleteDriver`, `deleteOrder`) VALUES
(0, 0, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `booking`
--
ALTER TABLE `booking`
  ADD PRIMARY KEY (`orderId`);

--
-- Indexes for table `car`
--
ALTER TABLE `car`
  ADD PRIMARY KEY (`carId`);

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`customerId`);

--
-- Indexes for table `driver`
--
ALTER TABLE `driver`
  ADD PRIMARY KEY (`driverId`);

--
-- Indexes for table `oldbookdata`
--
ALTER TABLE `oldbookdata`
  ADD PRIMARY KEY (`oderId`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

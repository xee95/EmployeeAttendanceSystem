-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 11, 2021 at 03:20 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `employee_data`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `country_hos` (IN `rec_id` INT, IN `attendance` INT, IN `empid` INT, IN `attendance_date` DATE)  NO SQL
BEGIN
INSERT INTO record VALUES(rec_id,attendance,empid,attendance_date);
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `attendance_id` int(11) NOT NULL,
  `attendance` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`attendance_id`, `attendance`) VALUES
(1, 'Present'),
(2, 'Absent'),
(3, 'Leave');

-- --------------------------------------------------------

--
-- Table structure for table `employees_info`
--

CREATE TABLE `employees_info` (
  `Id` int(32) NOT NULL,
  `FirstName` varchar(30) NOT NULL,
  `LastName` varchar(30) NOT NULL,
  `Address` varchar(80) NOT NULL,
  `Contact` varchar(15) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Designation` varchar(30) NOT NULL,
  `Department` varchar(30) NOT NULL,
  `JoinDate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `employees_info`
--

INSERT INTO `employees_info` (`Id`, `FirstName`, `LastName`, `Address`, `Contact`, `Email`, `Designation`, `Department`, `JoinDate`) VALUES
(1, 'Zeeshan', 'Nawaz', 'steel town', '03048008401', 'zeeshan@gmail.com', 'CEO', 'Administrative', '2020-08-28'),
(2, 'Faraz', 'Ahmed', 'karachi', '03001234567', 'faraz@gmail.com', 'Manager', 'Marketing', '2020-07-01'),
(3, 'Moin', 'Khan', 'karachi', '0312345612', 'moin@gmail.com', 'Deputy Manager', 'Administrative', '2020-07-01'),
(4, 'Raheel', 'Janjua', 'karachi', '03048888001', 'raheel@gmail.com', 'Manager', 'Finance', '2020-07-01'),
(5, 'Tariq', 'Ali', 'Karachi', '03001112222', 'tariq@gmail.com', 'Director', 'Customer service', '2020-07-01'),
(6, 'Naeem', 'Hassan', 'Karachi', '03001234987', 'naeem@gmail.com', 'Assistant Manager', 'Administrative', '2020-07-01'),
(7, 'Ali', 'Raza', 'Karachi', '03004441501', 'ali@gmail.com', 'Manager', 'Marketing', '2020-08-06'),
(8, 'Abadullah', 'Khan', 'karachi', '03447778881', 'khan@gmail.com', 'Manager', 'IT', '2020-07-01'),
(9, 'Ali', 'Khattak', 'Karachi', '03004321781', 'saddam@gmail.com', 'Sales Manager', 'Marketing', '2020-08-13'),
(10, 'Billal', 'Saeed', 'Karachi', '03213407552', 'billal@gmail.com', 'Director', 'Administrative', '2020-08-01'),
(11, 'abc', 'xyz', 'karachi', '03001234567', 'abc@gmail.com', 'Manager', 'Finance', '2020-06-03');

-- --------------------------------------------------------

--
-- Table structure for table `login_admin`
--

CREATE TABLE `login_admin` (
  `username` varchar(11) NOT NULL,
  `password` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `login_admin`
--

INSERT INTO `login_admin` (`username`, `password`) VALUES
('admin', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `record`
--

CREATE TABLE `record` (
  `REC_ID` int(11) NOT NULL,
  `attendance_type` int(10) NOT NULL,
  `REC_EMP_ID` int(11) DEFAULT NULL,
  `attendance_date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `record`
--

INSERT INTO `record` (`REC_ID`, `attendance_type`, `REC_EMP_ID`, `attendance_date`) VALUES
(1, 1, 1, '2020-09-01'),
(2, 1, 2, '2020-09-01'),
(3, 1, 3, '2020-09-01'),
(4, 1, 4, '2020-09-01'),
(5, 1, 5, '2020-09-01'),
(6, 1, 6, '2020-09-01'),
(7, 1, 7, '2020-09-01'),
(8, 1, 8, '2020-09-01'),
(9, 1, 9, '2020-09-01'),
(10, 1, 10, '2020-09-01'),
(11, 2, 2, '2020-09-02'),
(12, 2, 3, '2020-09-02'),
(13, 2, 8, '2020-09-02'),
(14, 3, 9, '2020-09-02'),
(15, 3, 10, '2020-09-02'),
(16, 2, 4, '2020-09-03'),
(17, 2, 6, '2020-09-03'),
(18, 1, 8, '2020-09-03'),
(19, 2, 9, '2020-09-03'),
(20, 1, 1, '2020-09-04'),
(21, 1, 2, '2020-09-04'),
(22, 2, 3, '2020-09-04'),
(23, 2, 4, '2020-09-04'),
(24, 3, 5, '2020-09-04'),
(25, 3, 6, '2020-09-04'),
(26, 3, 7, '2020-09-04'),
(27, 2, 8, '2020-09-04'),
(28, 1, 9, '2020-09-04'),
(29, 2, 10, '2020-09-04'),
(30, 1, 1, '2020-09-05'),
(31, 1, 2, '2020-09-05'),
(32, 1, 3, '2020-09-05'),
(33, 2, 4, '2020-09-05'),
(34, 2, 5, '2020-09-05'),
(35, 1, 6, '2020-09-05'),
(36, 3, 7, '2020-09-05'),
(37, 3, 8, '2020-09-05'),
(38, 2, 9, '2020-09-05'),
(39, 1, 10, '2020-09-05'),
(40, 1, 1, '2020-09-14'),
(41, 1, 2, '2020-09-14'),
(42, 2, 3, '2020-09-14'),
(43, 1, 4, '2020-09-14'),
(44, 2, 5, '2020-09-14'),
(45, 1, 6, '2020-09-14'),
(46, 2, 7, '2020-09-14'),
(47, 1, 8, '2020-09-14'),
(48, 3, 9, '2020-09-14'),
(49, 3, 10, '2020-09-14'),
(50, 1, 1, '2020-09-16'),
(51, 1, 2, '2020-09-16'),
(52, 2, 3, '2020-09-16'),
(54, 1, 5, '2020-09-16'),
(55, 2, 6, '2020-09-16'),
(56, 2, 7, '2020-09-16'),
(57, 2, 8, '2020-09-16'),
(58, 3, 9, '2020-09-16'),
(59, 3, 10, '2020-09-16'),
(60, 1, 1, '2020-09-18'),
(61, 1, 2, '2020-09-18'),
(62, 2, 3, '2020-09-18'),
(63, 2, 4, '2020-09-18'),
(64, 1, 1, '0000-00-00'),
(65, 1, 2, '0000-00-00'),
(66, 1, 3, '0000-00-00'),
(67, 1, 1, '0000-00-00'),
(68, 1, 2, '0000-00-00'),
(69, 1, 3, '0000-00-00'),
(70, 2, 1, '0000-00-00'),
(71, 2, 2, '0000-00-00'),
(72, 3, 3, '0000-00-00'),
(73, 1, 1, '0000-00-00'),
(74, 1, 2, '0000-00-00'),
(75, 1, 3, '0000-00-00'),
(76, 1, 1, '2020-10-10'),
(77, 1, 2, '2020-10-10'),
(78, 1, 3, '2020-10-10');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `employees_info`
--
ALTER TABLE `employees_info`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`);

--
-- Indexes for table `record`
--
ALTER TABLE `record`
  ADD PRIMARY KEY (`REC_ID`),
  ADD KEY `record_ibfk_1` (`REC_EMP_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `employees_info`
--
ALTER TABLE `employees_info`
  MODIFY `Id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=50;

--
-- AUTO_INCREMENT for table `record`
--
ALTER TABLE `record`
  MODIFY `REC_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=79;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `record`
--
ALTER TABLE `record`
  ADD CONSTRAINT `record_ibfk_1` FOREIGN KEY (`REC_EMP_ID`) REFERENCES `employees_info` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

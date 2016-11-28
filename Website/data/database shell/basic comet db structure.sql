-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 23, 2016 at 02:00 PM
-- Server version: 5.7.16-0ubuntu0.16.04.1
-- PHP Version: 7.0.13-1+deb.sury.org~xenial+1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `comet`
--
CREATE DATABASE IF NOT EXISTS `comet` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `comet`;

-- --------------------------------------------------------

--
-- Table structure for table `account_types`
--

CREATE TABLE `account_types` (
  `id` int(16) NOT NULL,
  `type` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `account_types`
--

INSERT INTO `account_types` (`id`, `type`) VALUES
(1, 'admin'),
(2, 'moderator'),
(3, 'enduser');

-- --------------------------------------------------------

--
-- Table structure for table `cards`
--

CREATE TABLE `cards` (
  `id` int(16) NOT NULL,
  `userid` int(16) NOT NULL,
  `cardname` varchar(32) NOT NULL,
  `cardimage` longtext NOT NULL,
  `effect` int(16) NOT NULL,
  `attack` int(16) NOT NULL,
  `health` int(16) NOT NULL,
  `created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `editted` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `cards_decks_rel`
--

CREATE TABLE `cards_decks_rel` (
  `id` int(16) NOT NULL,
  `deckid` int(16) NOT NULL,
  `cardid` int(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `decks`
--

CREATE TABLE `decks` (
  `id` int(16) NOT NULL,
  `userid` int(16) NOT NULL,
  `deckname` varchar(32) NOT NULL,
  `created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `editted` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `effect_types`
--

CREATE TABLE `effect_types` (
  `id` int(16) NOT NULL,
  `effect` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `effect_types`
--

INSERT INTO `effect_types` (`id`, `effect`) VALUES
(1, 'damage'),
(2, 'healing');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(16) NOT NULL,
  `email` varchar(64) NOT NULL,
  `username` varchar(16) NOT NULL,
  `password` varchar(42) NOT NULL,
  `account_type` int(1) NOT NULL,
  `created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `editted` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `email`, `username`, `password`, `account_type`, `created`, `editted`) VALUES
(1, 'admin@careforsometeasaboteur.com', 'admin', '*4ACFE3202A5FF5CF467898FC58AAB1D615029441', 1, '2016-11-23 13:44:33', '2016-11-23 13:44:33');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account_types`
--
ALTER TABLE `account_types`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cards`
--
ALTER TABLE `cards`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userid` (`userid`),
  ADD KEY `effect` (`effect`);

--
-- Indexes for table `cards_decks_rel`
--
ALTER TABLE `cards_decks_rel`
  ADD PRIMARY KEY (`id`),
  ADD KEY `deckid` (`deckid`),
  ADD KEY `cardid` (`cardid`);

--
-- Indexes for table `decks`
--
ALTER TABLE `decks`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userid` (`userid`);

--
-- Indexes for table `effect_types`
--
ALTER TABLE `effect_types`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `account_type` (`account_type`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account_types`
--
ALTER TABLE `account_types`
  MODIFY `id` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `cards`
--
ALTER TABLE `cards`
  MODIFY `id` int(16) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `cards_decks_rel`
--
ALTER TABLE `cards_decks_rel`
  MODIFY `id` int(16) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `decks`
--
ALTER TABLE `decks`
  MODIFY `id` int(16) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `effect_types`
--
ALTER TABLE `effect_types`
  MODIFY `id` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `cards`
--
ALTER TABLE `cards`
  ADD CONSTRAINT `cards_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `cards_ibfk_2` FOREIGN KEY (`effect`) REFERENCES `effect_types` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `cards_decks_rel`
--
ALTER TABLE `cards_decks_rel`
  ADD CONSTRAINT `cards_decks_rel_ibfk_1` FOREIGN KEY (`deckid`) REFERENCES `decks` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `cards_decks_rel_ibfk_2` FOREIGN KEY (`cardid`) REFERENCES `cards` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Constraints for table `decks`
--
ALTER TABLE `decks`
  ADD CONSTRAINT `decks_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`account_type`) REFERENCES `account_types` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: 21-Fev-2019 às 11:12
-- Versão do servidor: 5.5.52-MariaDB
-- PHP Version: 7.0.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `flex`
--
CREATE DATABASE IF NOT EXISTS `flex` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `flex`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `DIVIDAS`
--

DROP TABLE IF EXISTS `DIVIDAS`;
CREATE TABLE `DIVIDAS` (
  `ID` int(11) NOT NULL,
  `CLIENTE_ID` int(11) NOT NULL,
  `MOTIVO` varchar(4000) NOT NULL,
  `VALOR` decimal(11,2) NOT NULL,
  `DATA_DIVIDA` date NOT NULL,
  `CRIADO_EM` datetime NOT NULL,
  `MODIFICADO_EM` datetime NOT NULL,
  `EXCLUIDO_EM` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `DIVIDAS`
--
ALTER TABLE `DIVIDAS`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `DIVIDAS`
--
ALTER TABLE `DIVIDAS`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

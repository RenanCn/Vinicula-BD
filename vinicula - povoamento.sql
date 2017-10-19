-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: 24-Ago-2017 às 02:44
-- Versão do servidor: 5.7.14
-- PHP Version: 5.6.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `vinicula`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `cepa`
--

CREATE TABLE `cepa` (
  `Nome` varchar(45) NOT NULL,
  `Parreiral_idParreiral` int(11) NOT NULL,
  `Região de Origem` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `cepa`
--

INSERT INTO `cepa` (`Nome`, `Parreiral_idParreiral`, `Região de Origem`) VALUES
('Malbec', 3, 'Nordeste'),
('Sauvignon Blanc', 1, 'Centro-Oeste'),
('Syrah', 1, 'Nordeste'),
('Tempranillo', 3, 'Sudeste');

-- --------------------------------------------------------

--
-- Estrutura da tabela `colheita`
--

CREATE TABLE `colheita` (
  `idColheita` int(11) NOT NULL,
  `Parreiral_idParreiral` int(11) NOT NULL,
  `diaColheita` int(11) NOT NULL,
  `mesColheita` int(11) NOT NULL,
  `anoColheita` int(11) NOT NULL,
  `Material` varchar(45) NOT NULL,
  `Maturação` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `colheita`
--

INSERT INTO `colheita` (`idColheita`, `Parreiral_idParreiral`, `diaColheita`, `mesColheita`, `anoColheita`, `Material`, `Maturação`) VALUES
(5, 1, 1, 1, 2017, 'Barricas de Carvalho', '51'),
(9, 3, 23, 5, 2013, 'Inox', '40'),
(11, 1, 30, 12, 2014, 'Barricas de Carvalho', '4'),
(13, 5, 1, 1, 2015, 'Barricas de Carvalho', '6');

-- --------------------------------------------------------

--
-- Estrutura da tabela `contato`
--

CREATE TABLE `contato` (
  `Propriedade_Nome` varchar(50) NOT NULL,
  `Endereço` varchar(45) NOT NULL,
  `Telefone` varchar(45) DEFAULT NULL,
  `E-mail` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `contato`
--

INSERT INTO `contato` (`Propriedade_Nome`, `Endereço`, `Telefone`, `E-mail`) VALUES
('Dragonstone', 'Algum Lugar', NULL, NULL),
('Iron Islands', 'Pyke', '0000-0001', 'teste2@email.com.br'),
('Kings Landing', 'South Westeros', '1230-4005; 0567-0084', 'teste@criatividade.com'),
('Propriedade Tres', 'Praia', NULL, NULL),
('Winterfell', 'North Westeros', '4002-8922', 'jon@knownothing.com');

-- --------------------------------------------------------

--
-- Estrutura da tabela `emails`
--

CREATE TABLE `emails` (
  `idEmail` int(11) NOT NULL,
  `Contato_Propriedade_Nome` varchar(50) NOT NULL,
  `Email` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `emails`
--

INSERT INTO `emails` (`idEmail`, `Contato_Propriedade_Nome`, `Email`) VALUES
(1, 'Winterfell', 'sabenada@jaodasneve.com.br'),
(3, 'Winterfell', 'teste@teste.co'),
(4, 'Dragonstone', 'abc@efg.com'),
(5, 'Kings Landing', 'boom@aaa.com'),
(6, 'Propriedade Tres', 'dsfsdjifdoi@cansei.com');

-- --------------------------------------------------------

--
-- Estrutura da tabela `parreiral`
--

CREATE TABLE `parreiral` (
  `idParreiral` int(11) NOT NULL,
  `Propriedade_Nome` varchar(50) NOT NULL,
  `Vinhas` int(11) NOT NULL,
  `Área (m²)` float NOT NULL,
  `Plantio` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `parreiral`
--

INSERT INTO `parreiral` (`idParreiral`, `Propriedade_Nome`, `Vinhas`, `Área (m²)`, `Plantio`) VALUES
(1, 'Dragonstone', 15000, 5000, '2017-07-01 00:00:00'),
(3, 'Winterfell', 9000, 500, '2016-07-01 00:00:00'),
(5, 'Dragonstone', 50000, 12000, '2014-12-08 00:00:00'),
(6, 'Propriedade Tres', 6554, 6565, '2013-05-05 00:00:00');

-- --------------------------------------------------------

--
-- Estrutura da tabela `propriedade`
--

CREATE TABLE `propriedade` (
  `Nome` varchar(50) NOT NULL,
  `Administrador` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `propriedade`
--

INSERT INTO `propriedade` (`Nome`, `Administrador`) VALUES
('Dragonstone', 'Daenerys Targaryen'),
('Fazenda', 'Zé João'),
('Iron Islands', 'Euron Greyjoy'),
('Kings Landing', 'Cersei Lannister'),
('Propriedade Dois', 'Juvenal'),
('Propriedade Tres', 'Ademir'),
('Propriedade Um', 'Agripino'),
('Winterfell', 'Jão das Neves');

-- --------------------------------------------------------

--
-- Estrutura da tabela `safra`
--

CREATE TABLE `safra` (
  `Colheita_idColheita` int(11) NOT NULL,
  `Ano` int(11) NOT NULL,
  `Garrafas` int(45) NOT NULL,
  `Vinho_codVinho` int(11) NOT NULL,
  `Vinho_Nome` varchar(45) NOT NULL,
  `Avaliação` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `safra`
--

INSERT INTO `safra` (`Colheita_idColheita`, `Ano`, `Garrafas`, `Vinho_codVinho`, `Vinho_Nome`, `Avaliação`) VALUES
(5, 2017, 40, 10, 'Vinho1', 96),
(9, 2014, 99, 12, 'Vinho2', 93),
(11, 2014, 75, 12, 'Vinho2', 41);

-- --------------------------------------------------------

--
-- Estrutura da tabela `telefones`
--

CREATE TABLE `telefones` (
  `idTelefones` int(11) NOT NULL,
  `Contato_Propriedade_Nome` varchar(50) NOT NULL,
  `Telefone` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `telefones`
--

INSERT INTO `telefones` (`idTelefones`, `Contato_Propriedade_Nome`, `Telefone`) VALUES
(2, 'Winterfell', '4002-8922'),
(3, 'Dragonstone', '0000-0001'),
(4, 'Winterfell', '0000-9999'),
(9, 'Propriedade Tres', '5464-1000');

-- --------------------------------------------------------

--
-- Estrutura da tabela `terroir`
--

CREATE TABLE `terroir` (
  `Propriedade_Nome` varchar(50) NOT NULL,
  `Tipo de Solo` varchar(50) NOT NULL,
  `Altitude (m)` float NOT NULL,
  `Umidade` float NOT NULL,
  `Índice Pluviométrico (%)` float NOT NULL,
  `Clima (°C)` float NOT NULL,
  `Região` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `terroir`
--

INSERT INTO `terroir` (`Propriedade_Nome`, `Tipo de Solo`, `Altitude (m)`, `Umidade`, `Índice Pluviométrico (%)`, `Clima (°C)`, `Região`) VALUES
('Dragonstone', 'Seco', 27, 6, 45, 25, 'Leste'),
('Kings Landing', 'Árido', 60, 40, 61, 23, 'Nordeste'),
('Propriedade Tres', 'Solado', 32, 43, 32, 33, 'Sul'),
('Winterfell', 'Congelado', 45, 75, 60, -15, 'Norte');

-- --------------------------------------------------------

--
-- Estrutura da tabela `vinho`
--

CREATE TABLE `vinho` (
  `codVinho` int(11) NOT NULL,
  `Nome` varchar(45) NOT NULL,
  `Rótulo` varchar(50) NOT NULL,
  `Classificação` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `vinho`
--

INSERT INTO `vinho` (`codVinho`, `Nome`, `Rótulo`, `Classificação`) VALUES
(10, 'Vinho1', 'C:\\Users\\Renan\\Desktop\\Banco D\\vinho1.png', 'Varietal'),
(12, 'Vinho2', 'C:\\Users\\Renan\\Desktop\\Banco D\\vinho2.png', 'Assemblage'),
(13, 'Vinho3', 'C:\\Users\\Renan\\Desktop\\Banco D\\vinho3.png', 'Assemblage'),
(15, 'Vinho4', 'C:\\Users\\Renan\\Desktop\\Banco D\\vinho4.png', 'Varietal'),
(16, 'Vinho5', 'C:\\Users\\Renan\\Desktop\\Banco D\\vinho5.png', 'Assemblage');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cepa`
--
ALTER TABLE `cepa`
  ADD PRIMARY KEY (`Nome`,`Parreiral_idParreiral`),
  ADD KEY `fk_Cepa_Parreiral1_idx` (`Parreiral_idParreiral`);

--
-- Indexes for table `colheita`
--
ALTER TABLE `colheita`
  ADD PRIMARY KEY (`idColheita`,`Parreiral_idParreiral`),
  ADD UNIQUE KEY `idColheita_UNIQUE` (`idColheita`),
  ADD UNIQUE KEY `anoColheita_UNIQUE` (`anoColheita`),
  ADD KEY `fk_Colheita_Parreiral1_idx` (`Parreiral_idParreiral`);

--
-- Indexes for table `contato`
--
ALTER TABLE `contato`
  ADD PRIMARY KEY (`Propriedade_Nome`),
  ADD UNIQUE KEY `Propriedade_Nome_UNIQUE` (`Propriedade_Nome`),
  ADD UNIQUE KEY `Endereço_UNIQUE` (`Endereço`);

--
-- Indexes for table `emails`
--
ALTER TABLE `emails`
  ADD PRIMARY KEY (`idEmail`),
  ADD UNIQUE KEY `idEmail_UNIQUE` (`idEmail`),
  ADD UNIQUE KEY `Email_UNIQUE` (`Email`),
  ADD KEY `fk_Emails_Contato1_idx` (`Contato_Propriedade_Nome`);

--
-- Indexes for table `parreiral`
--
ALTER TABLE `parreiral`
  ADD PRIMARY KEY (`idParreiral`,`Propriedade_Nome`),
  ADD KEY `fk_Parreiral_Propriedade1_idx` (`Propriedade_Nome`);

--
-- Indexes for table `propriedade`
--
ALTER TABLE `propriedade`
  ADD PRIMARY KEY (`Nome`),
  ADD UNIQUE KEY `Nome_UNIQUE` (`Nome`);

--
-- Indexes for table `safra`
--
ALTER TABLE `safra`
  ADD PRIMARY KEY (`Colheita_idColheita`,`Vinho_codVinho`,`Vinho_Nome`),
  ADD UNIQUE KEY `Colheita_idColheita_UNIQUE` (`Colheita_idColheita`),
  ADD KEY `fk_Safra_Colheita1_idx` (`Colheita_idColheita`),
  ADD KEY `fk_Safra_Vinho1_idx` (`Vinho_codVinho`,`Vinho_Nome`);

--
-- Indexes for table `telefones`
--
ALTER TABLE `telefones`
  ADD PRIMARY KEY (`idTelefones`),
  ADD UNIQUE KEY `idTelefone_UNIQUE` (`idTelefones`),
  ADD UNIQUE KEY `Telefone_UNIQUE` (`Telefone`),
  ADD KEY `fk_Telefones_Contato1_idx` (`Contato_Propriedade_Nome`);

--
-- Indexes for table `terroir`
--
ALTER TABLE `terroir`
  ADD PRIMARY KEY (`Propriedade_Nome`),
  ADD UNIQUE KEY `Região_UNIQUE` (`Região`);

--
-- Indexes for table `vinho`
--
ALTER TABLE `vinho`
  ADD PRIMARY KEY (`codVinho`,`Nome`),
  ADD UNIQUE KEY `codVinho_UNIQUE` (`codVinho`),
  ADD UNIQUE KEY `Nome_UNIQUE` (`Nome`),
  ADD UNIQUE KEY `Rótulo_UNIQUE` (`Rótulo`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `colheita`
--
ALTER TABLE `colheita`
  MODIFY `idColheita` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT for table `emails`
--
ALTER TABLE `emails`
  MODIFY `idEmail` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `parreiral`
--
ALTER TABLE `parreiral`
  MODIFY `idParreiral` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `telefones`
--
ALTER TABLE `telefones`
  MODIFY `idTelefones` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- AUTO_INCREMENT for table `vinho`
--
ALTER TABLE `vinho`
  MODIFY `codVinho` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `cepa`
--
ALTER TABLE `cepa`
  ADD CONSTRAINT `fk_Cepa_Parreiral1` FOREIGN KEY (`Parreiral_idParreiral`) REFERENCES `parreiral` (`idParreiral`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `colheita`
--
ALTER TABLE `colheita`
  ADD CONSTRAINT `fk_Colheita_Parreiral1` FOREIGN KEY (`Parreiral_idParreiral`) REFERENCES `parreiral` (`idParreiral`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `contato`
--
ALTER TABLE `contato`
  ADD CONSTRAINT `fk_Contato_Propriedade1` FOREIGN KEY (`Propriedade_Nome`) REFERENCES `propriedade` (`Nome`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `emails`
--
ALTER TABLE `emails`
  ADD CONSTRAINT `fk_Emails_Contato1` FOREIGN KEY (`Contato_Propriedade_Nome`) REFERENCES `contato` (`Propriedade_Nome`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `parreiral`
--
ALTER TABLE `parreiral`
  ADD CONSTRAINT `fk_Parreiral_Propriedade1` FOREIGN KEY (`Propriedade_Nome`) REFERENCES `propriedade` (`Nome`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `safra`
--
ALTER TABLE `safra`
  ADD CONSTRAINT `fk_Safra_Colheita1` FOREIGN KEY (`Colheita_idColheita`) REFERENCES `colheita` (`idColheita`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Safra_Vinho1` FOREIGN KEY (`Vinho_codVinho`,`Vinho_Nome`) REFERENCES `vinho` (`codVinho`, `Nome`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `telefones`
--
ALTER TABLE `telefones`
  ADD CONSTRAINT `fk_Telefones_Contato1` FOREIGN KEY (`Contato_Propriedade_Nome`) REFERENCES `contato` (`Propriedade_Nome`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `terroir`
--
ALTER TABLE `terroir`
  ADD CONSTRAINT `fk_Terroar_Propriedade1` FOREIGN KEY (`Propriedade_Nome`) REFERENCES `propriedade` (`Nome`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

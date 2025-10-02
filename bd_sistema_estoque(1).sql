-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 02-Out-2025 às 19:49
-- Versão do servidor: 10.4.6-MariaDB
-- versão do PHP: 7.3.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `bd_sistema_estoque`
--
CREATE DATABASE IF NOT EXISTS `bd_sistema_estoque` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `bd_sistema_estoque`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `categoria`
--

CREATE TABLE IF NOT EXISTS `categoria` (
  `idcategoria` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  PRIMARY KEY (`idcategoria`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `categoria`
--

INSERT INTO `categoria` (`idcategoria`, `nome`) VALUES
(1, 'Alimentos'),
(2, 'Bebidas'),
(3, 'Higiene'),
(4, 'Matéria-prima'),
(5, 'Ferramentas'),
(6, 'Eletronicos');

-- --------------------------------------------------------

--
-- Estrutura da tabela `endereco`
--

CREATE TABLE IF NOT EXISTS `endereco` (
  `idendereco` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_endereco` varchar(45) NOT NULL,
  `logradouro` varchar(100) NOT NULL,
  `numero` int(11) NOT NULL,
  `bairro` varchar(50) NOT NULL,
  `cidade` varchar(45) NOT NULL,
  `estado` char(2) NOT NULL,
  `cep` char(9) NOT NULL,
  `complemento` varchar(45) DEFAULT NULL,
  `fk_fornecedor_idfornecedor` int(11) NOT NULL,
  PRIMARY KEY (`idendereco`,`fk_fornecedor_idfornecedor`),
  KEY `fk_endereco_fornecedor1_idx` (`fk_fornecedor_idfornecedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `fornecedor`
--

CREATE TABLE IF NOT EXISTS `fornecedor` (
  `idfornecedor` int(11) NOT NULL AUTO_INCREMENT,
  `razao_social` varchar(100) NOT NULL,
  `nome_fantasia` varchar(100) NOT NULL,
  `cnpj` varchar(45) NOT NULL,
  `email` varchar(60) NOT NULL,
  PRIMARY KEY (`idfornecedor`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `cnpj_UNIQUE` (`cnpj`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `movimentacao`
--

CREATE TABLE IF NOT EXISTS `movimentacao` (
  `idmovimentacao` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` varchar(45) NOT NULL,
  `quantidade` int(11) NOT NULL,
  `preco` decimal(10,0) NOT NULL,
  `date` datetime NOT NULL,
  `fk_produto_idproduto` int(11) NOT NULL,
  PRIMARY KEY (`idmovimentacao`,`fk_produto_idproduto`),
  KEY `fk_movimentacao_produto1_idx` (`fk_produto_idproduto`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `movimentacao`
--

INSERT INTO `movimentacao` (`idmovimentacao`, `tipo`, `quantidade`, `preco`, `date`, `fk_produto_idproduto`) VALUES
(1, 'Saida', 1, '1', '2025-10-02 14:33:37', 1),
(2, 'Saida', 3, '500', '2025-10-02 14:39:31', 2),
(3, 'Saida', 1, '500', '2025-10-02 14:40:16', 2);

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto`
--

CREATE TABLE IF NOT EXISTS `produto` (
  `idproduto` int(11) NOT NULL AUTO_INCREMENT,
  `nome_produto` varchar(60) NOT NULL,
  `descricao` varchar(150) DEFAULT NULL,
  `quantidade` int(11) NOT NULL,
  `preco_custo` decimal(10,0) NOT NULL,
  `preco_venda` decimal(10,0) DEFAULT NULL,
  `estoque_minimo` int(11) NOT NULL,
  `fk_categoria_idcategoria` int(11) NOT NULL,
  PRIMARY KEY (`idproduto`,`fk_categoria_idcategoria`),
  KEY `fk_produto_categoria1_idx` (`fk_categoria_idcategoria`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `produto`
--

INSERT INTO `produto` (`idproduto`, `nome_produto`, `descricao`, `quantidade`, `preco_custo`, `preco_venda`, `estoque_minimo`, `fk_categoria_idcategoria`) VALUES
(1, 'ps5', NULL, 0, '0', '1', 1, 6),
(2, 'Maça', NULL, 96, '0', '500', 1, 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto_fornecedor`
--

CREATE TABLE IF NOT EXISTS `produto_fornecedor` (
  `idproduto_fornecedor` int(11) NOT NULL AUTO_INCREMENT,
  `fk_produto_idproduto` int(11) NOT NULL,
  `fk_fornecedor_idfornecedor` int(11) NOT NULL,
  PRIMARY KEY (`idproduto_fornecedor`,`fk_produto_idproduto`,`fk_fornecedor_idfornecedor`),
  KEY `fk_produto_has_fornecedor_fornecedor1_idx` (`fk_fornecedor_idfornecedor`),
  KEY `fk_produto_has_fornecedor_produto_idx` (`fk_produto_idproduto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

CREATE TABLE IF NOT EXISTS `usuario` (
  `idusuario` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `login` varchar(50) NOT NULL,
  `senha` varchar(255) NOT NULL,
  PRIMARY KEY (`idusuario`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`idusuario`, `nome`, `login`, `senha`) VALUES
(1, '', 'admin', '123');

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `endereco`
--
ALTER TABLE `endereco`
  ADD CONSTRAINT `fk_endereco_fornecedor1` FOREIGN KEY (`fk_fornecedor_idfornecedor`) REFERENCES `fornecedor` (`idfornecedor`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `movimentacao`
--
ALTER TABLE `movimentacao`
  ADD CONSTRAINT `fk_movimentacao_produto1` FOREIGN KEY (`fk_produto_idproduto`) REFERENCES `produto` (`idproduto`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `produto`
--
ALTER TABLE `produto`
  ADD CONSTRAINT `fk_produto_categoria1` FOREIGN KEY (`fk_categoria_idcategoria`) REFERENCES `categoria` (`idcategoria`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `produto_fornecedor`
--
ALTER TABLE `produto_fornecedor`
  ADD CONSTRAINT `fk_produto_has_fornecedor_fornecedor1` FOREIGN KEY (`fk_fornecedor_idfornecedor`) REFERENCES `fornecedor` (`idfornecedor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_produto_has_fornecedor_produto` FOREIGN KEY (`fk_produto_idproduto`) REFERENCES `produto` (`idproduto`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;






-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/03/2015 19:24:10
-- Generated from EDMX file: C:\Users\BryceAshey\Documents\GitHub\Disty\Disty.Model.MySql\DistyModel.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Lists` DROP CONSTRAINT `FK_DeptList`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Lists`;
    DROP TABLE IF EXISTS `Depts`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Lists`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (50) NOT NULL, 
	`Owner` varchar (50) NOT NULL, 
	`Creator` varchar (50) NOT NULL, 
	`Dept_Id` int NOT NULL);

ALTER TABLE `Lists` ADD PRIMARY KEY (Id);




CREATE TABLE `Depts`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (50) NOT NULL);

ALTER TABLE `Depts` ADD PRIMARY KEY (Id);




CREATE TABLE `Emails`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Address` longtext NOT NULL, 
	`List_Id` int NOT NULL);

ALTER TABLE `Emails` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `Dept_Id` in table 'Lists'

ALTER TABLE `Lists`
ADD CONSTRAINT `FK_DeptList`
    FOREIGN KEY (`Dept_Id`)
    REFERENCES `Depts`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeptList'

CREATE INDEX `IX_FK_DeptList` 
    ON `Lists`
    (`Dept_Id`);

-- Creating foreign key on `List_Id` in table 'Emails'

ALTER TABLE `Emails`
ADD CONSTRAINT `FK_ListEmail`
    FOREIGN KEY (`List_Id`)
    REFERENCES `Lists`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ListEmail'

CREATE INDEX `IX_FK_ListEmail` 
    ON `Emails`
    (`List_Id`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

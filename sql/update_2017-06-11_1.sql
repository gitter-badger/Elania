ALTER TABLE `characters` ADD `barrackLayer` TINYINT(3) NOT NULL;

SET SQL_SAFE_UPDATES = 0;
UPDATE `characters` SET `barrackLayer` = 1;
SET SQL_SAFE_UPDATES = 1;

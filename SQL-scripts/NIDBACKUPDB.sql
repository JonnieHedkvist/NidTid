CREATE TABLE `kund` (
  `KundID` int(11) NOT NULL AUTO_INCREMENT,
  `Namn` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `OrgNr` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Adress` text COLLATE utf8_unicode_ci,
  `Privat` varchar(45) COLLATE utf8_unicode_ci DEFAULT 'j',
  PRIMARY KEY (`KundID`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `medarbetare` (
  `AnstNr` int(11) NOT NULL AUTO_INCREMENT,
  `Namn` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `username` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `color` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `admin` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'n',
  PRIMARY KEY (`AnstNr`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `projekt` (
  `ProjektID` int(11) NOT NULL AUTO_INCREMENT,
  `Namn` varchar(45) COLLATE utf8_unicode_ci NOT NULL,
  `KundID` int(11) NOT NULL,
  `Kontaktperson` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Tel1` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Tel2` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Epost` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Brf` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `lghNr` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Konto` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Beskrivning` longtext COLLATE utf8_unicode_ci,
  `ansvarig` varchar(45) COLLATE utf8_unicode_ci DEFAULT '-',
  `Debit` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'j',
  `Aktivt` varchar(45) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'j',
  PRIMARY KEY (`ProjektID`)
) ENGINE=InnoDB AUTO_INCREMENT=163 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `tidrapporter` (
  `idTidrapporter` int(11) NOT NULL AUTO_INCREMENT,
  `Datum` int(11) NOT NULL,
  `ProjektID` int(11) NOT NULL,
  `AnstNr` int(11) NOT NULL,
  `Timmar` varchar(45) COLLATE utf8_unicode_ci DEFAULT '0',
  `ejDeb` varchar(45) COLLATE utf8_unicode_ci DEFAULT '0',
  `Kilometer` varchar(45) COLLATE utf8_unicode_ci DEFAULT '0',
  `Notering` longtext COLLATE utf8_unicode_ci,
  PRIMARY KEY (`idTidrapporter`)
) ENGINE=InnoDB AUTO_INCREMENT=2895 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


USE NidTidData
SET IDENTITY_INSERT Customers ON
GO
INSERT INTO [NidTidData].[dbo].[Customers] (Id)
    SELECT KundID FROM [nidbackupdb].[dbo].[kund]
GO


MERGE INTO [NidTidData].[dbo].[Customers]
   USING [nidbackupdb].[dbo].[Kund]
      ON [NidTidData].[dbo].[Customers].[Id] = [nidbackupdb].[dbo].[Kund].[KundID]
WHEN MATCHED THEN
   UPDATE 
      SET Name = Namn;


MERGE INTO [NidTidData].[dbo].[Customers]
   USING [nidbackupdb].[dbo].[Kund]
      ON [NidTidData].[dbo].[Customers].[Id] = [nidbackupdb].[dbo].[Kund].[KundID]
WHEN MATCHED THEN
   UPDATE 
      SET OrgNr = [nidbackupdb].[dbo].[Kund].OrgNr;


MERGE INTO [NidTidData].[dbo].[Customers]
   USING [nidbackupdb].[dbo].[Kund]
      ON [NidTidData].[dbo].[Customers].[Id] = [nidbackupdb].[dbo].[Kund].[KundID]
WHEN MATCHED THEN
   UPDATE 
      SET Adress = [nidbackupdb].[dbo].[Kund].Adress;


 ***** Plocka columner från projekt in till Customers *******

 MERGE INTO [NidTidData].[dbo].[Customers] AS TGT
   USING 
   (SELECT Epost, KundID FROM [nidbackupdb].[dbo].[projekt] Where len(Epost) > 2 GROUP BY KundID, Epost) AS SRC
    ON TGT.[Id] = SRC.KundID
WHEN MATCHED THEN
   UPDATE 
      SET TGT.Email = SRC.Epost;

 MERGE INTO [NidTidData].[dbo].[Customers] AS TGT
   USING 
   (SELECT Tel1, KundID FROM [nidbackupdb].[dbo].[projekt] Where len(Tel1) > 2 GROUP BY KundID, Tel1) AS SRC
    ON TGT.[Id] = SRC.KundID
WHEN MATCHED THEN
   UPDATE 
      SET TGT.Phone1 = SRC.Tel1;


 MERGE INTO [NidTidData].[dbo].[Customers] AS TGT
   USING 
   (SELECT Tel2, KundID FROM [nidbackupdb].[dbo].[projekt] Where len(Tel2) > 2 GROUP BY KundID, Tel2) AS SRC
    ON TGT.[Id] = SRC.KundID
WHEN MATCHED THEN
   UPDATE 
      SET TGT.Phone2 = SRC.Tel2;





******* Projekt ******

USE NidTidData
SET IDENTITY_INSERT Projects ON
GO
INSERT INTO [NidTidData].[dbo].[Projects] (Id, Name, CustomerId, Referens, FastBtn, KontoStr, Description, Active, UserId)
    SELECT ProjektID, Namn, KundID, Kontaktperson, Brf, Konto, Beskrivning, 0, 1 FROM [nidbackupdb].[dbo].[projekt]
GO


****** USERS ******
USE NidTidData
SET IDENTITY_INSERT Users ON
GO
INSERT INTO [NidTidData].[dbo].[Users] (Id, Name, Email, Password)
    SELECT AnstNr, Namn, username, Password
	FROM [nidbackupdb].[dbo].[medarbetare];
GO



****** REPORTS ******

USE NidTidData
SET IDENTITY_INSERT Reports ON
GO
INSERT INTO [NidTidData].[dbo].[Reports] (Id, Notes, Date, UserId, ProjectId)
    SELECT idTidrapporter, Notering, CONVERT(DATETIME, CONVERT(CHAR(8), [Datum])), AnstNr, ProjektID 
	FROM [nidbackupdb].[dbo].[tidrapporter]
	WHERE Datum > 20000000 AND Datum < 22000000;
GO

  --->>>>> timmar >>
UPDATE [nidbackupdb].[dbo].tidrapporter
SET Timmar = REPLACE(timmar,'-','0')
GO
UPDATE [nidbackupdb].[dbo].tidrapporter
SET Timmar = REPLACE(timmar,' ','')
GO
UPDATE [nidbackupdb].[dbo].tidrapporter
SET Timmar = REPLACE(timmar,'h','')
GO

MERGE INTO [NidTidData].[dbo].[Reports] AS TGT
   USING 
   (SELECT timmar, idTidrapporter FROM [nidbackupdb].[dbo].[tidrapporter] 
   WHERE ISNUMERIC(Timmar) != 0) AS SRC
    ON TGT.[Id] = SRC.idTidrapporter
WHEN MATCHED THEN
   UPDATE 
   SET TGT.Deb = CAST(SRC.Timmar as DECIMAL(18,2));
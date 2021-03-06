-- Create table dbo.User
CREATE TABLE dbo.[User]
    (
      Id INT NOT NULL
             IDENTITY ,
      Email NVARCHAR(100) NOT NULL ,
      [Password] NVARCHAR(50) NOT NULL ,
      Firstname NVARCHAR(50) NOT NULL ,
      Lastname NVARCHAR(50) NOT NULL ,
      CreatedDate DATETIME NOT NULL ,
      LastLogin DATETIME NOT NULL ,
      AccountType INT NOT NULL ,
      Roles INT NOT NULL ,
      Displayname NVARCHAR(25) NOT NULL ,
      Avatar VARCHAR(250) NOT NULL ,
      ThirdPartyId VARCHAR(25) NOT NULL ,
      [Status] INT NOT NULL ,
      Ip VARCHAR(25) NOT NULL ,
      IsAdmin BIT  NOT NULL DEFAULT 0,
      CONSTRAINT PK_User PRIMARY KEY ( Id )
    );
    
GO

CREATE TABLE dbo.[FoodGroup](
	FdGrp_CD nvarchar(4) NOT NULL,
	GroupDesc nvarchar(60) NULL,
	CONSTRAINT PK_FoodGroup PRIMARY KEY (FdGrp_CD)
);

GO

-- copy group data from old database to smoothie
INSERT INTO dbo.FoodGroup
SELECT FdGrp_CD, FdGrp_Desc FROM sr24SQL.dbo.FD_GROUP
WHERE FdGrp_CD IN ('0100', '0200', '0400', '0800', '0900', '1100', '1200', '1400', '1600', '1800', '1900', '2100');

GO

CREATE TABLE [dbo].[FoodDescription](
	Id INT NOT NULL IDENTITY,
	[NDB_No] [nvarchar](5) NOT NULL,
	[FdGrp_CD] [nvarchar](4) NULL,
	[Long_Desc] [nvarchar](200) NULL,
	[Shrt_Desc] [nvarchar](60) NULL,
	[ComName] [nvarchar](100) NULL,
	[ManufacName] [nvarchar](65) NULL,
	[Survey] [nvarchar](1) NULL,
	[Ref_Desc] [nvarchar](135) NULL,
	[Refuse] [smallint] NULL,
	[SciName] [nvarchar](65) NULL,
	[N_Factor] [real] NULL,
	[Pro_Factor] [real] NULL,
	[Fat_Factor] [real] NULL,
	[CHO_Factor] [real] NULL,
	CONSTRAINT PK_FoodDescription PRIMARY KEY (Id)
);

GO

ALTER TABLE dbo.FoodDescription
ADD CONSTRAINT FK_FoodDescription_FdGrp_CD
FOREIGN KEY (FdGrp_CD)
REFERENCES dbo.FoodGroup(FdGrp_CD);

GO

-- copy food desc data from old database to smoothie
INSERT INTO dbo.FoodDescription
SELECT * FROM sr24SQL.dbo.FOOD_DES
WHERE FdGrp_CD IN ('0100', '0200', '0400', '0800', '0900', '1100', '1200', '1400', '1600', '1800', '1900', '2100');

GO

CREATE TABLE [dbo].[FoodAbbrev](
	[NDB_No] [nvarchar](5) NOT NULL,
	[Shrt_Desc] [nvarchar](255) NULL,
	[Water] [float] NULL,
	[Energ_Kcal] [float] NULL,
	[Protein] [float] NULL,
	[Lipid_Tot] [float] NULL,
	[Ash] [float] NULL,
	[Carbohydrt] [float] NULL,
	[Fiber_TD] [float] NULL,
	[Sugar_Tot] [float] NULL,
	[Calcium] [float] NULL,
	[Iron] [float] NULL,
	[Magnesium] [float] NULL,
	[Phosphorus] [float] NULL,
	[Potassium] [float] NULL,
	[Sodium] [float] NULL,
	[Zinc] [float] NULL,
	[Copper] [float] NULL,
	[Manganese] [float] NULL,
	[Selenium] [float] NULL,
	[Vit_C] [float] NULL,
	[Thiamin] [float] NULL,
	[Riboflavin] [float] NULL,
	[Niacin] [float] NULL,
	[Panto_Acid] [float] NULL,
	[Vit_B6] [float] NULL,
	[Folate_Tot] [float] NULL,
	[Folic_Acid] [float] NULL,
	[Food_Folate] [float] NULL,
	[Folate_DFE] [float] NULL,
	[Choline_Tot] [float] NULL,
	[Vit_B12] [float] NULL,
	[Vit_A_IU] [float] NULL,
	[Vit_A_RAE] [float] NULL,
	[Retinol] [float] NULL,
	[Alpha_Carot] [float] NULL,
	[Beta_Carot] [float] NULL,
	[Beta_Crypt] [float] NULL,
	[Lycopene] [float] NULL,
	[Lut_Zea] [float] NULL,
	[Vit_E] [float] NULL,
	[Vit_D] [float] NULL,
	[ViVit_D] [float] NULL,
	[Vit_K] [float] NULL,
	[FA_Sat] [float] NULL,
	[FA_Mono] [float] NULL,
	[FA_Poly] [float] NULL,
	[Cholestrl] [float] NULL,
	[GmWt_1] [float] NULL,
	[GmWt_Desc1] [nvarchar](255) NULL,
	[GmWt_2] [float] NULL,
	[GmWt_Desc2] [nvarchar](255) NULL,
	[Refuse_Pct] [float] NULL,
	CONSTRAINT PK_FoodAbbrev PRIMARY KEY (NDB_No)
);

GO



-- copy Abbrev data from old database to smoothie
INSERT INTO dbo.FoodAbbrev
SELECT sr24SQL.dbo.Abbrev.* FROM sr24SQL.dbo.Abbrev
	INNER JOIN sr24SQL.dbo.FOOD_DES ON sr24SQL.dbo.Abbrev.NDB_No = sr24SQL.dbo.FOOD_DES.NDB_No
WHERE FdGrp_CD IN ('0100', '0200', '0400', '0800', '0900', '1100', '1200', '1400', '1600', '1800', '1900', '2100');

GO

-- Adding New Column GroupCd
ALTER TABLE dbo.FoodAbbrev
ADD GroupCd [nvarchar](4) NULL

GO

-- Adding New Column GroupId
ALTER TABLE dbo.FoodAbbrev
ADD GroupId INT NULL

GO


-- Adding New Column Status
ALTER TABLE dbo.FoodAbbrev
ADD Status INT NULL

GO


-- Adding New Column Name
ALTER TABLE dbo.FoodAbbrev
ADD Name NVARCHAR(50) NOT NULL DEFAULT ''

GO


-- Adding New Column Image
ALTER TABLE dbo.FoodAbbrev
ADD Image NVARCHAR(255) NOT NULL DEFAULT ''

GO


-- Adding New Column BaseUnit
ALTER TABLE dbo.FoodAbbrev
ADD BaseUnit int NOT NULL DEFAULT 1

GO


-- Adding New Column GmWt_3
ALTER TABLE dbo.FoodAbbrev
ADD GmWt_3 float NULL

GO

-- Adding New Column GmWt_Desc3
ALTER TABLE dbo.FoodAbbrev
ADD GmWt_Desc3 NVARCHAR(255) NULL

GO



UPDATE dbo.FoodAbbrev
SET STATUS = 1;

GO


-- Update GroupCd Field
UPDATE dbo.FoodAbbrev
SET GroupCd = ( SELECT dbo.FoodDescription.FdGrp_CD
				FROM dbo.FoodDescription
				WHERE dbo.FoodDescription.NDB_No = dbo.FoodAbbrev.NDB_No)
				
GO


-- Update GmWt_3, GmWt_Desc3 Field
UPDATE dbo.FoodAbbrev
SET GmWt_3 = GmWt_1, GmWt_Desc3 = GmWt_Desc1
				
GO


CREATE TABLE dbo.Category(
	Id INT NOT NULL IDENTITY,
	Name nvarchar(50) NULL,
	ReOrder INT NOT NULL,
	CONSTRAINT PK_Category PRIMARY KEY (Id)
);

GO

INSERT INTO  dbo.Category
        ( Name, ReOrder )
VALUES  ( N'Fruit and Berries', 1),
		( N'Veggies', 2),
		( N'Grains', 3),
		( N'Proteins', 4),
		( N'Dairy', 5),
		( N'Flavoring', 6),
		( N'Supplements', 7),
		( N'Baked', 8),
		( N'Other', 9);
		
GO


-- Create table dbo.Smoothie
CREATE TABLE dbo.Smoothie
    (
      Id INT NOT NULL
             IDENTITY ,
      Name NVARCHAR(255) NOT NULL ,
      CreatedDate DATETIME NOT NULL,
      [Status] INT NOT NULL ,
      UserId INT NOT NULL ,
      CONSTRAINT PK_Smoothie PRIMARY KEY ( Id )
    );
    
GO


-- ALTER Column UserId
ALTER TABLE dbo.Smoothie
ALTER COLUMN UserId int NULL

GO


ALTER TABLE dbo.Smoothie
ADD CONSTRAINT FK_Smoothie_UserId
FOREIGN KEY(UserId) REFERENCES dbo.[User](Id)

GO



-- Create table dbo.Smoothie
CREATE TABLE dbo.SmoothieIngredients
    (
      Id INT NOT NULL
             IDENTITY ,
      [NDB_No] [nvarchar](5) NOT NULL,
      SmoothieId INT NOT NULL ,
      Quantity INT NOT NULL,
      CONSTRAINT PK_SmoothieIngredients PRIMARY KEY ( Id )
    );
    
GO


ALTER TABLE dbo.SmoothieIngredients
ADD CONSTRAINT FK_SmoothieIngredients_SmoothieId
FOREIGN KEY(SmoothieId) REFERENCES dbo.Smoothie(Id)

GO



-- 9/16/2012


-- Adding New Column Status
ALTER TABLE dbo.Category
ADD Status INT NULL

GO

UPDATE dbo.Category SET STATUS = 3
WHERE Status IS NULL

Go


-- 9/18/2012

ALTER TABLE dbo.FoodAbbrev
ADD Id INT NULL

GO



ALTER TABLE dbo.SmoothieIngredients
ADD FoodId INT NULL

GO


UPDATE dbo.SmoothieIngredients
SET FoodId = CAST(NDB_No AS INT)

GO


UPDATE dbo.FoodAbbrev
SET Id = CAST(NDB_No AS INT)

GO

ALTER TABLE dbo.FoodAbbrev DROP CONSTRAINT PK_FoodAbbrev

GO

ALTER TABLE dbo.FoodAbbrev
ALTER COLUMN Id int NOT NULL

GO

ALTER TABLE dbo.FoodAbbrev ADD CONSTRAINT PK_FoodAbbrev
PRIMARY KEY CLUSTERED (Id);

GO

ALTER TABLE dbo.SmoothieIngredients
ADD CONSTRAINT FK_SmoothieIngredients_FoodId
FOREIGN KEY(FoodId) REFERENCES dbo.FoodAbbrev(Id)

GO











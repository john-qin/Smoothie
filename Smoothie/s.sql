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
    


CREATE TABLE dbo.[FoodGroup](
	FdGrp_CD nvarchar(4) NOT NULL,
	GroupDesc nvarchar(60) NULL,
	CONSTRAINT PK_FoodGroup PRIMARY KEY (FdGrp_CD)
);

-- copy group data from old database to smoothie
INSERT INTO dbo.FoodGroup
SELECT FdGrp_CD, FdGrp_Desc FROM sr24SQL.dbo.FD_GROUP
WHERE FdGrp_CD IN ('0100', '0200', '0400', '0800', '0900', '1100', '1200', '1400', '1600', '1800', '1900', '2100');



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

ALTER TABLE dbo.FoodDescription
ADD CONSTRAINT FK_FoodDescription_FdGrp_CD
FOREIGN KEY (FdGrp_CD)
REFERENCES dbo.FoodGroup(FdGrp_CD);


-- copy food desc data from old database to smoothie
INSERT INTO dbo.FoodDescription
SELECT * FROM sr24SQL.dbo.FOOD_DES
WHERE FdGrp_CD IN ('0100', '0200', '0400', '0800', '0900', '1100', '1200', '1400', '1600', '1800', '1900', '2100');



CREATE TABLE [dbo].[FoodAbbrev](
	[NDB_No] [nvarchar](255) NOT NULL,
	[Shrt_Desc] [nvarchar](255) NULL,
	[Water_(g)] [float] NULL,
	[Energ_Kcal] [float] NULL,
	[Protein_(g)] [float] NULL,
	[Lipid_Tot_(g)] [float] NULL,
	[Ash_(g)] [float] NULL,
	[Carbohydrt_(g)] [float] NULL,
	[Fiber_TD_(g)] [float] NULL,
	[Sugar_Tot_(g)] [float] NULL,
	[Calcium_(mg)] [float] NULL,
	[Iron_(mg)] [float] NULL,
	[Magnesium_(mg)] [float] NULL,
	[Phosphorus_(mg)] [float] NULL,
	[Potassium_(mg)] [float] NULL,
	[Sodium_(mg)] [float] NULL,
	[Zinc_(mg)] [float] NULL,
	[Copper_mg)] [float] NULL,
	[Manganese_(mg)] [float] NULL,
	[Selenium_(µg)] [float] NULL,
	[Vit_C_(mg)] [float] NULL,
	[Thiamin_(mg)] [float] NULL,
	[Riboflavin_(mg)] [float] NULL,
	[Niacin_(mg)] [float] NULL,
	[Panto_Acid_mg)] [float] NULL,
	[Vit_B6_(mg)] [float] NULL,
	[Folate_Tot_(µg)] [float] NULL,
	[Folic_Acid_(µg)] [float] NULL,
	[Food_Folate_(µg)] [float] NULL,
	[Folate_DFE_(µg)] [float] NULL,
	[Choline_Tot_ (mg)] [float] NULL,
	[Vit_B12_(µg)] [float] NULL,
	[Vit_A_IU] [float] NULL,
	[Vit_A_RAE] [float] NULL,
	[Retinol_(µg)] [float] NULL,
	[Alpha_Carot_(µg)] [float] NULL,
	[Beta_Carot_(µg)] [float] NULL,
	[Beta_Crypt_(µg)] [float] NULL,
	[Lycopene_(µg)] [float] NULL,
	[Lut+Zea_ (µg)] [float] NULL,
	[Vit_E_(mg)] [float] NULL,
	[Vit_D_µg] [float] NULL,
	[ViVit_D_IU] [float] NULL,
	[Vit_K_*(µg)] [float] NULL,
	[FA_Sat_(g)] [float] NULL,
	[FA_Mono_(g)] [float] NULL,
	[FA_Poly_(g)] [float] NULL,
	[Cholestrl_(mg)] [float] NULL,
	[GmWt_1] [float] NULL,
	[GmWt_Desc1] [nvarchar](255) NULL,
	[GmWt_2] [float] NULL,
	[GmWt_Desc2] [nvarchar](255) NULL,
	[Refuse_Pct] [float] NULL,
	CONSTRAINT PK_FoodAbbrev PRIMARY KEY (NDB_No)
);

-- copy Abbrev data from old database to smoothie
INSERT INTO dbo.FoodAbbrev
SELECT sr24SQL.dbo.Abbrev.* FROM sr24SQL.dbo.Abbrev
	INNER JOIN sr24SQL.dbo.FOOD_DES ON sr24SQL.dbo.Abbrev.NDB_No = sr24SQL.dbo.FOOD_DES.NDB_No
WHERE FdGrp_CD IN ('0100', '0200', '0400', '0800', '0900', '1100', '1200', '1400', '1600', '1800', '1900', '2100');



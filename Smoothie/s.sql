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
      CONSTRAINT PK_User PRIMARY KEY ( Id )
    );
    
ALTER TABLE <tablename> ADD CONSTRAINT
            <constraintname> UNIQUE NONCLUSTERED
    (
                <columnname>
    )

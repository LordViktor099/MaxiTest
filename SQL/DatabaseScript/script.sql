USE [lifeInsurance]
GO
/****** Object:  User [ApiUser]    Script Date: 2/13/2024 4:00:47 PM ******/
CREATE USER [ApiUser] FOR LOGIN [ApiUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ApiUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ApiUser]
GO
/****** Object:  Table [dbo].[Beneficiaries]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beneficiaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[FirstName] [nchar](200) NULL,
	[LastName] [nchar](200) NULL,
	[Birthday] [date] NULL,
	[EmployeeNumber] [int] NULL,
	[CURP] [nchar](18) NULL,
	[SSN] [int] NULL,
	[PhoneNumber] [int] NULL,
	[Nationality] [nchar](200) NULL,
	[ParticipationPercentage] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](200) NULL,
	[LastName] [nchar](200) NULL,
	[Birthday] [date] NULL,
	[EmployeeNumber] [int] NULL,
	[CURP] [nchar](18) NULL,
	[SSN] [int] NULL,
	[PhoneNumber] [int] NULL,
	[Nationality] [nchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Beneficiaries]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[CreateBeneficiary]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateBeneficiary](
	@EmployeeId int = 0,
    @FirstName nchar(200) = NULL,
    @LastName nchar(200) = NULL,
    @Birthday date = NULL,
    @EmployeeNumber int = 0,
    @CURP nchar(200) = NULL,
    @SSN int = 0,
    @PhoneNumber int = 0,
    @Nationality nchar(200) = NULL
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @beneficiaries AS int
	DECLARE @percentage AS int

	SET @beneficiaries = ( SELECT COUNT(*) FROM Beneficiaries WHERE EmployeeId = @EmployeeId );

	SET @percentage = (100)/(@beneficiaries+1);

	UPDATE Beneficiaries SET 
		ParticipationPercentage = @percentage
		WHERE EmployeeId = @EmployeeId;

    -- Insert statements for procedure here
	INSERT Beneficiaries (EmployeeId, FirstName, LastName, Birthday, EmployeeNumber, CURP, SSN, PhoneNumber, Nationality, ParticipationPercentage)
	OUTPUT 
		INSERTED.Id, INSERTED.EmployeeId, INSERTED.FirstName, INSERTED.LastName, INSERTED.Birthday, INSERTED.EmployeeNumber, INSERTED.CURP, INSERTED.SSN, INSERTED.PhoneNumber, INSERTED.Nationality, INSERTED.ParticipationPercentage
	VALUES
		(@EmployeeId, @FirstName,@LastName, @Birthday, @EmployeeNumber, @CURP, @SSN, @PhoneNumber, @Nationality, @percentage);

	

END
GO
/****** Object:  StoredProcedure [dbo].[CreateEmployee]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateEmployee](
    @FirstName nchar(200) = NULL,
    @LastName nchar(200) = NULL,
    @Birthday date = NULL,
    @EmployeeNumber int = 0,
    @CURP nchar(200) = NULL,
    @SSN int = 0,
    @PhoneNumber int = 0,
    @Nationality nchar(200) = NULL
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT Employees (FirstName, LastName, Birthday, EmployeeNumber, CURP, SSN, PhoneNumber, Nationality)
	OUTPUT 
		INSERTED.Id, INSERTED.FirstName, INSERTED.LastName, INSERTED.Birthday, INSERTED.EmployeeNumber, INSERTED.CURP, INSERTED.SSN, INSERTED.PhoneNumber, INSERTED.Nationality
	VALUES
		(@FirstName,@LastName, @Birthday, @EmployeeNumber, @CURP, @SSN, @PhoneNumber, @Nationality)

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteBeneficiary]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteBeneficiary](
    @Param1 int = 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Beneficiaries
		WHERE Beneficiaries.Id = @Param1;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteEmployee](
    @Param1 int = 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Employees
		WHERE Employees.Id = @Param1;

END
GO
/****** Object:  StoredProcedure [dbo].[FindBeneficiary]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FindBeneficiary](
	@Param1 int = 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Beneficiaries WHERE Id = @Param1;
END
GO
/****** Object:  StoredProcedure [dbo].[FindEmployee]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[FindEmployee](
	@Param1 int = 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Employees WHERE Id = @Param1;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 2/13/2024 4:00:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateEmployee](
    @FirstName nchar(200) = NULL,
    @LastName nchar(200) = NULL,
    @Birthday date = NULL,
    @EmployeeNumber int = 0,
    @CURP nchar(200) = NULL,
    @SSN int = 0,
    @PhoneNumber int = 0,
    @Nationality nchar(200) = NULL,
	@Id int = 0
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Employees
		SET 
		FirstName = @FirstName,
		LastName = @LastName,
		Birthday = @Birthday,
		EmployeeNumber = @EmployeeNumber, 
		CURP = @CURP, 
		SSN = @SSN, 
		PhoneNumber = @PhoneNumber,
		Nationality = @Nationality 
	WHERE Id = @Id;

	SELECT * FROM Employees WHERE Id = @Id;
END
GO

use CoxAutomotive
GO

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[tbl_DealerTrack]') AND type in (N'U'))
BEGIN
CREATE TABLE tbl_DealerTrack
(
DealId Bigint identity(1,1),
GivenFilename char(100),
DealNumber BIGINT,
CustomerName char(100),
DealershipName char(100),
Vehicle char(100),
Price Decimal(18,2),
Date_Added Datetime,
Primary key (DealId)
)
END

GO

 IF  EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'FetchRecords')
                    AND type IN ( N'P', N'PC' ) ) 
drop proc FetchRecords 
GO
CREATE PROCEDURE FetchRecords 
( @myfilename varchar(50)  
)
as
SELECT GivenFilename, 
DealNumber ,  
CustomerName,  
DealershipName,  
Vehicle,  
Price,  
Date_Added  
from tbl_DealerTrack  
where GivenFilename = @myfilename  

  

 GO
 
  
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'CalculateMostSold')
                    AND type IN ( N'P', N'PC' ) ) 
drop proc CalculateMostSold 
GO

CREATE PROC CalculateMostSold
AS
BEGIN

SELECT tt.*
FROM tbl_DealerTrack tt
INNER JOIN
		(SELECT Top 1 (Vehicle)
		FROM tbl_DealerTrack
		GROUP BY Vehicle
		order by count(Vehicle)  DESC) groupedtt 
ON tt.vehicle = groupedtt.vehicle 


END
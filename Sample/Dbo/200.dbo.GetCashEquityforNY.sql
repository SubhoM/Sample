SELECT 'GuardAppEvent:Start', 'GuardAppEventStrValue:CHG0088272'
GO


IF OBJECT_ID('[dbo].[GetCashEquityforNY]', 'P') IS NOT NULL
DROP PROC [dbo].[GetCashEquityforNY]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCashEquityforNY]
	@key varchar(50), 
	@EMPLID varchar(6)	
AS

/******************************************************************************
Name:	 [dbo].[GetCashEquityforNY]
Desc:	 Get Comp Statement Information
Return:   
Author:   
Date:	  
Notes:    
Examples: 
	Exec [dbo].[GetCashEquityforNY]	@key ='ECR', @EMPLID = '028411'
	Exec [dbo].[GetCashEquityforNY]	@key ='ECR', @EMPLID = '008412'
	Exec [dbo].[GetCashEquityforNY]	@key ='ECR', @EMPLID = '018331'
	Exec [dbo].[GetCashEquityforNY]	@key ='ECR', @EMPLID = '004719'

	Exec [dbo].[GetCashEquityforNY]	@key ='ECR', @EMPLID = null	
	
*******************************************************************************
Revision History:
Date		Author			Description
*******************************************************************************
12/03/2018   Subho		   Modified. Added Special Cash Field 
******************************************************************************/

BEGIN


Create Table #tmp (EmplID varchar(10) Primary Key, 
EQUITY_IS numeric, 
PLUS_THIS numeric, 
OF_THE_AMOUNT_OVER numeric, 
Total_Comp varchar(Max), 
Total_Incentive_USD Decimal(20,2), 
Equity_USD Decimal(20,4), 
Cash_Incentive Decimal(10,0),
MaximumDeferralPct Decimal(10,2), 
MinimumDeferralPct Decimal(10,2), 
PCT_Equity_TI Decimal (30,20), 
Exchange_Rate decimal(10,5) )

Insert into #tmp
SELECT CRE.EMPLID, E.EQUITY_IS, E.PLUS_THIS, E.OF_THE_AMOUNT_OVER, 
Cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Base_USD)) as numeric) + cast( CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Officer_title)) as numeric) * EX.Exchange_Rate,
cast( CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Officer_title)) as numeric) * EX.Exchange_Rate,
Case When E.PLUS_THIS != 0 Then 
  
  (Cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Base_USD)) as decimal(10,2)) + cast( CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Officer_title)) as decimal(10,2)) * EX.Exchange_Rate - CONVERT(decimal(10,2), E.Of_The_Amount_Over))
  * (CONVERT(decimal(10,2), E.Plus_This) /100) + Convert(decimal(10,2), E.Equity_Is)

Else 0 End ,

0,
cast (DR.MaximumDeferralPct as decimal(10,2)), 
cast( DR.MinimumDeferralPct as decimal(10,2)),0, Ex.Exchange_Rate
 FROM COMP_REC_ENC CRE 
 Inner join Exchange_Rates EX 
  ON ISNULL(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Local_Currency_Code)), 'USD') = Ex.Currency_Code AND EX.[Year] = CRE.Year
  inner Join
 dbo.EQUITY_GRID E on
   E.IS_OVER <= Cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Base_USD)) as numeric) + cast( CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Officer_title)) as numeric) * EX.Exchange_Rate
  and E.IS_NOT_OVER > Cast( CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Base_USD)) as numeric) + cast( CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Officer_title)) as numeric) * Ex.Exchange_Rate
  LEFT JOIN DeferralRules DR ON CRE.RULEID = DR.RULEID
  Where CRE.Year = dbo.getCurrentYear()
  and  CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.New_Officer_title)) != '0'
  and (@Emplid is Null Or @Emplid is Not Null and emplid = @Emplid)



Update #tmp
Set Equity_USD = Total_Incentive_USD 
Where Equity_USD >= Total_Incentive_USD


Update #tmp
Set PCT_Equity_TI = (Equity_USD/Total_Incentive_USD) * 100
Where Total_Incentive_USD > 0


Update #tmp
Set PCT_Equity_TI = MaximumDeferralPct
Where PCT_Equity_TI > MaximumDeferralPct


Update #tmp
Set PCT_Equity_TI = MinimumDeferralPct
Where PCT_Equity_TI < MinimumDeferralPct


Update #tmp
Set Equity_USD = PCT_Equity_TI * Total_Incentive_USD / 100


Update #tmp
Set Equity_USD = 
Case When Cast(Equity_USD as Int)% 2 = 0 and (Equity_USD - Cast(Equity_USD as Int)) <= 0.5   Then Cast(Equity_USD as Int) 
Else ROUND(Equity_USD, 0)
End



Select EmplID, 
Cast((Total_Incentive_USD - Equity_USD)/Exchange_Rate as decimal(10,0)) as Cash_Incentive,
Cast(Equity_USD/Exchange_Rate as decimal(10,0))
as Equity_Incentive from #tmp 



Drop Table #tmp

End
GO


SELECT 'GuardAppEvent:Released'
GO

SELECT 'GuardAppEvent:Start', 'GuardAppEventStrValue:CHG0088272'
GO


IF OBJECT_ID('[dbo].[CompMixRecalculate]', 'P') IS NOT NULL
DROP PROC [dbo].[CompMixRecalculate]
GO

SET ANSI_Warnings ON
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CompMixRecalculate]	
	@key varchar(10) ,
	@emplid int = null,
	@MeritPromotionFlag varchar(10) = 'Comp Mix'

	
/******************************************************************************
Name:	 [dbo].[CompMixRecalculate]
Desc:	 Calcuate Employee Salary increase based on CompMix Configuration
Return:   
Author:   Subho
Date:	  10/01/2018
Notes:    
Examples: 
	Exec [dbo].[CompMixRecalculate]	@key ='ECR', @emplid = '020409', @MeritPromotionFlag = 'Comp Mix'

	Exec [dbo].[CompMixRecalculate]	@key ='ECR', @emplid = null, @MeritPromotionFlag = 'Comp Mix'
		
*******************************************************************************
Revision History:
Date		Author			Description
*******************************************************************************
10/1/2018   Subho		  Created
12/15/2018  Subho		  Updated NY PTIE for Employees having Comp Mix
******************************************************************************/
AS
BEGIN

Update Comp_Rec_Enc
SET
New_Base_Salary_Local = EncryptByPassPhrase(@key, Convert(Varchar, Cast( CONVERT(varchar, DECRYPTBYPASSPHRASE(@Key, New_Base_Salary_Local)) as numeric) - CAST(CONVERT(varchar, DECRYPTBYPASSPHRASE(@key, MP_Increase_Amount)) as numeric))),
New_Base_USD = EncryptByPassPhrase(@key, Convert(Varchar, Cast(CONVERT(varchar, DECRYPTBYPASSPHRASE(@key, New_Base_USD)) as numeric) - Cast(CONVERT(varchar, DECRYPTBYPASSPHRASE(@key, MP_Increase_Amount)) as numeric))),
Merit_Promotion_Flag = EncryptByPassPhrase(@key,''),
MP_Increase_Amount = EncryptByPassPhrase(@key, '0'),
MP_Increase_Percent = EncryptByPassPhrase(@key, ''),
Increase_Amount_USD =  EncryptByPassPhrase(@key, '0'),
SalaryIncrease_OverWrite =  Null
--New_Officer_title =  EncryptByPassPhrase(@key,'')
From Comp_Rec_Enc cre
Where cre.Year = dbo.GetCurrentYear()
and (DECRYPTBYPASSPHRASE(@key, Merit_Promotion_Flag) = 'Comp Mix' or DECRYPTBYPASSPHRASE(@key, Merit_Promotion_Flag) = 'CompMix')
and (CRE.SalaryIncrease_OverWrite is Null Or (CRE.SalaryIncrease_OverWrite is Not Null and CRE.SalaryIncrease_OverWrite !=  'Y'))
and (@emplid is null Or (@emplid is not Null and CRE.EMPLID = @emplid))

--OffCycle_Salary_Increase

Select emplid, 
ct.MinSalary + 
 (Case When Cast(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Eff_Date)) as date) > Cast(('12/31/' + Convert(Varchar,CRE.Year)) as Date)
Then
 cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[OffCycle_Salary_Increase])) as numeric)
Else 0 
End)
 as New_Base_Salary_USD,
ct.MinSalary - ( cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[Base_Salary_USD])) as numeric) + 
Case When Cast(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Eff_Date)) as date) <= Cast(('12/31/' + Convert(Varchar,CRE.Year)) as Date)
Then
 cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[OffCycle_Salary_Increase])) as numeric)
Else 0 
End)
as Salary_Increase,
(ct.MinSalary - ( cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[Base_Salary_USD])) as numeric) + 
Case When Cast(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Eff_Date)) as date) <= Cast(('12/31/' + Convert(Varchar,CRE.Year)) as Date)
Then
 cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[OffCycle_Salary_Increase])) as numeric)
Else 0 
End))/
( cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[Base_Salary_USD])) as numeric) + 
Case When Cast(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Eff_Date)) as date) <= Cast(('12/31/' + Convert(Varchar,CRE.Year)) as Date)
Then
 cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[OffCycle_Salary_Increase])) as numeric)
Else 0 
End) 
* 100 as increase_percentage,
er.[Exchange_Rate]
into #tmp
from [dbo].[COMP_REC_ENC] CRE
inner join [dbo].[CompMixGrid] cg on CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,Org_Level_1))  = cg.OrgLevel1
inner join EXCHANGE_RATES er on er.CURRENCY_CODE = RTRIM(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[Local_Currency_Code]))) and CRE.Year = er.Year
outer apply (
Select ct.Tier, ct.TCUSDMin, ct.TCUSDMax,ct.MinSalary  from [dbo].[CompMixTier] ct
Where   ct.[ApplicableGrid] = cg.[ApplicableGrid] 
and ct.TCUSDMax >= cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,Total_Compensation_USD)) as numeric) 
and ct.TCUSDMin <= cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,Total_Compensation_USD)) as numeric) 
) ct
Where cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,Total_Compensation_USD)) as numeric)   >= 250000
and ct.MinSalary > cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[Base_Salary_USD])) as numeric) +
Case When Cast(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Eff_Date)) as date) <= Cast(('12/31/' + Convert(Varchar,CRE.Year)) as Date)
Then
 cast(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[OffCycle_Salary_Increase])) as numeric)
Else 0 
End
and CRE.Year = dbo.GetCurrentYear()
and CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Perf_Rtg)) in ('1','2','3','7')
and CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Incentive_plan)) = 'Annual'
and CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.[Local_Currency_Code])) = 'USD'
and (@emplid is null Or (@emplid is not Null and CRE.EMPLID = @emplid))
and (CRE.SalaryIncrease_OverWrite is Null Or (CRE.SalaryIncrease_OverWrite is Not Null and CRE.SalaryIncrease_OverWrite !=  'Y'))


Update Comp_Rec_Enc
SET
Merit_Promotion_Flag = EncryptByPassPhrase(@key,@MeritPromotionFlag),
MP_Increase_Amount = EncryptByPassPhrase(@key, cast(b.Salary_Increase * b.[Exchange_Rate] as Varchar)),
MP_Increase_Percent = EncryptByPassPhrase(@key, cast(b.Increase_Percentage as Varchar)),
New_Base_Salary_Local = EncryptByPassPhrase(@key, cast(b.New_Base_Salary_USD*b.[Exchange_Rate] as Varchar)) ,
New_Base_USD = EncryptByPassPhrase(@key, cast(b.New_Base_Salary_USD as varchar)),
Increase_Amount_USD =  EncryptByPassPhrase(@key, cast(b.Salary_Increase as varchar)),
SalaryIncrease_OverWrite =  'N',
Summary_TEMPLATEID = EncryptByPassPhrase(@key, 'Y')
From Comp_Rec_Enc cre
inner join #tmp b on cre.emplid = b.emplid
inner join EXCHANGE_RATES er on er.CURRENCY_CODE = RTRIM(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,cre.[Local_Currency_Code]))) and cre.Year = er.Year
Where cre.Year = dbo.GetCurrentYear()

-- Next Year PTIE value for Comp Mix Users

Update Comp_Rec_Enc
Set New_Officer_title =
EncryptByPassPhrase(@key,
Case When CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, Total_Incentive_Local)) != '0' 
and Convert(numeric,CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, Total_Incentive_Local)))  > 
Convert(numeric,CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, MP_Increase_Amount)))  
Then  
Cast(
Convert(numeric,CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, Total_Incentive_Local)))  - 
Convert(numeric,CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, MP_Increase_Amount))) 
as Varchar) Else '0' End)
Where CONVERT(Varchar(max), DECRYPTBYPASSPHRASE('ECR', Merit_Promotion_Flag)) = 'Comp Mix'
and Year = dbo.GetCurrentYear()
and CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, Total_Incentive_Local)) != ''
and (@emplid is null Or (@emplid is not Null and EMPLID = @emplid))
and (NY_PTIE_OverWrite Is Null Or NY_PTIE_OverWrite != 'Y')



-- Next Year PTIE value for non Comp Mix Users
Update Comp_Rec_Enc
Set New_Officer_title =
Case When Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key, [Incentive_plan])) = 'Annual' Then
Total_Incentive_Local
Else EncryptByPassPhrase(@key, '0') End
Where CONVERT(Varchar(max), DECRYPTBYPASSPHRASE('ECR', Merit_Promotion_Flag)) != 'Comp Mix'
and Year = dbo.GetCurrentYear()
and CONVERT(Varchar(max), DECRYPTBYPASSPHRASE(@key, Total_Incentive_Local)) != ''
and (@emplid is null Or (@emplid is not Null and EMPLID = @emplid))
and (NY_PTIE_OverWrite Is Null Or NY_PTIE_OverWrite != 'Y')


Drop Table #tmp

End
GO


SELECT 'GuardAppEvent:Released'
GO
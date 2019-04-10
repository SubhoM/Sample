
SELECT 'GuardAppEvent:Start', 'GuardAppEventStrValue:CHG0088272'
GO

IF OBJECT_ID('[dbo].[GenerateReport]', 'P') IS NOT NULL
DROP PROC [dbo].[GenerateReport]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GenerateReport]
 @Report_ID int,
 @Criteria_Type varchar(20),
 @HRPBID VARCHAR(10) = Null,
 @Planning_Manager_ID Varchar(10) = Null,
 @Updated_By Varchar(50) = Null,
 @Key varchar(5)
AS

/******************************************************************************
Name:	  [dbo].[GenerateReport]
Desc:	  Generate Report
Return:   
Author:   
Date:	  
Notes:    
Examples: Exec [dbo].[GenerateReport] @Report_ID =8, @Criteria_Type = 'Generate', @key ='ECR'
		
*******************************************************************************
Revision History:
Date		Author			Description
*******************************************************************************
10/3/2018   Subho		    Returns Dataset for TI Report Data
******************************************************************************/


IF OBJECT_ID('#tmp', 'Table') IS NOT NULL
Begin
Drop Table #tmp
End


Select distinct
CRE.[EMPLID], 
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Name])) as 'Employee Name(Last, First)',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[EMC_Flag])) as 'EMC Flag',
Convert(varchar(500), CRE.Planning_Manager_ID) as 'Planning Manager Name',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Perf_Rtg])) as 'CY Performance Rating',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Hire_Date])) as 'Latest Hire Date',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Incentive_plan])) as 'Incentive Plan',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Status])) as 'Status',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Bonus_Eligible])) as 'Eligibility',	
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Population_Type])) as 'Population_Type',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Band])) as 'Grade',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Local_Currency_Code])) as 'CY Currency Code',
Cast('' as varchar(500)) as 'PY Total_Incentive_Local',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Total_Incentive_Local])) as 'CY Total_Incentive_Local',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[PTIE_Local])) as 'CY PTIE_Local',
CASE ISNULL(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Incentive_vs_Previous_Year)),'') 
		WHEN '' THEN '0%' 
		When 'NA' THEN 'NA'
ELSE CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Incentive_vs_Previous_Year)) + '%' END AS 'Total Incentive % Chg YOY',
CASE ISNULL(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_vs_Previous_Year)),'') 
		WHEN '' THEN '0%' 
		When 'NA' THEN 'NA'
		ELSE CONVERT(VARCHAR(500),CONVERT(NUMERIC(11,1),CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_vs_Previous_Year)))) + '%' 
	END AS 'Total Comp % Chg YOY',
	 CASE
		WHEN ISNULL(convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Increase)),'') = '' 			
			THEN CASE ISNULL(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_Local)),'') 
				WHEN '' THEN '' 
				ELSE dbo.fn_FormatWithCommas(ROUND(CONVERT(NUMERIC,CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_Local))),0)) 
			END
		ELSE 
			CASE ISNULL(CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_Local)),'') 
				WHEN '' THEN '' 
				--ELSE dbo.fn_FormatWithCommas(ROUND(CONVERT(NUMERIC,CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CY.Total_Compensation_Local))) + CONVERT(NUMERIC,convert(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CY.OffCycle_Salary_Increase))),0)) 
				ELSE dbo.fn_FormatWithCommas(ROUND(CONVERT(NUMERIC,CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_Local))),0)) 

			END 
		END As 'CY Total Comp (Local)',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[MP_Increase_Amount])) as 'Salary Increase Amount (Local)',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[MP_Increase_Percent])) as 'Salary Increase %',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Merit_Promotion_Flag])) as 'Type of Salary Increase',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[New_Base_Salary_Local])) as 'New Base Salary (Local)',
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Increase)) as 'Off-Cycle Salary Increase (Local)',
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.OffCycle_Salary_Eff_Date)) as 'Off-Cycle Salary Eff Date(1)',
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Special_29)) as 'Off-Cycle Incr, excl. Merit (Mar CY - Oct CY)',
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Special_30)) as 'Off-Cycle Salary Eff Date(2)',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Internal_TC_Position])) as 'Mkt Sal Position',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Mkt_TC_Position])) as 'Mkt TC Position',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Covered])) as '(C) Covered',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[C2_Risk_Form])) as 'C2 Risk Form Completed',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Special_25)) as 'Prior Year Promos',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.New_Band)) as 'Promo - New Grade',
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Manager_Comments)) AS 'Manager Comments',
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.HRG_Notes)) as 'HRG Comments',
cast(
(Cast(Coalesce(Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.New_Base_Salary_Local)), Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Base_Salary_Local))) 
as numeric)  
/ (Cast(Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Standard_Hours)) as decimal(10,2))  * 52)) as decimal(10,2)) as 'Hourly Rate',
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Standard_Hours)) as 'Standard Hours'
into #tmp
from COMP_REC_ENC CRE
inner join [dbo].[HRCMS_USER] HRUsr on CRE.[Planning_Manager_ID] = HrUsr.[EMPLID] 
left outer join [dbo].[Report_SavedCriteria] RSC
on 
RSC.Report_ID = @Report_ID
and RSC.SavedCriteria_Type = @Criteria_Type
left outer JOIN HRBP_Empl_Xref hr ON hr.EMPLID = CRE.Planning_Manager_ID 
Where 
(@HRPBID Is Null or (@HRPBID Is Not Null and hr.HRBPID = @HRPBID))
and (@Planning_Manager_ID is Null or (@Planning_Manager_ID is Not Null and CRE.Planning_Manager_ID = @Planning_Manager_ID))
and CRE.Year = dbo.getCurrentYear()
and Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Standard_Hours)) != ''
and (RSC.[Perf_Rtg] Is Null Or  RSC.[Perf_Rtg] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Perf_Rtg]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Perf_Rtg]))))  
and (RSC.[Incentive_plan] Is Null Or  RSC.[Incentive_plan] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Incentive_plan]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Incentive_plan])))) 
and (RSC.[Bonus_Eligible] Is Null Or  RSC.[Bonus_Eligible] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Bonus_Eligible]) lst Where lst.ListTableID =  Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Bonus_Eligible]))))
and (RSC.[Population_Type] Is Null Or  RSC.[Population_Type] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Population_Type]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Population_Type))))
and (RSC.[Merit_Promotion_Flag] Is Null Or  RSC.[Merit_Promotion_Flag] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Merit_Promotion_Flag]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Merit_Promotion_Flag))))
and (RSC.[Covered] Is Null Or  RSC.[Covered] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Covered]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Covered))))
and (RSC.[C2_Risk_Form] Is Null Or  RSC.[C2_Risk_Form] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[C2_Risk_Form]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.C2_Risk_Form))))
and (RSC.Total_Comp_Chg_YOY Is Null Or  RSC.Total_Comp_Chg_YOY Is Not Null and 
Exists(Select lst.ListTableID from dbo.GetListTable(RSC.[Total_Comp_Chg_YOY]) lst 
Where (CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Incentive_vs_Previous_Year)) != 'NA' Or 
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Incentive_vs_Previous_Year)) != 'NA') and
(
ABS(CONVERT(numeric,
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Incentive_vs_Previous_Year)) 
)) >= lst.ListTableID 
Or 
ABS(CONVERT(numeric,
CONVERT(VARCHAR(500),DECRYPTBYPASSPHRASE(@key,CRE.Total_Compensation_vs_Previous_Year))
)) >= lst.ListTableID)
)
)
and (RSC.Min_Hourly_Rate is Null Or (RSC.Min_Hourly_Rate is Not Null and (
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Standard_Hours)) != '' and
Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Local_Currency_Code)) = 'USD' and
(Cast(Coalesce(Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.New_Base_Salary_Local)), Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Base_Salary_Local))) as numeric)  
/ (Cast(Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Standard_Hours)) as numeric)  * 52)) < Cast(RSC.Min_Hourly_Rate as numeric)	
)) )
and (RSC.[Internal_TC_Position] Is Null Or  RSC.[Internal_TC_Position] Is Not Null and Exists (Select lst.ListTableID from dbo.GetListTable(RSC.[Internal_TC_Position]) lst Where lst.ListTableID = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.Internal_TC_Position))))	 
and (@Updated_By Is Null Or  @Updated_By Is Not Null and RSC.Updated_By = @Updated_By)	 


Update #tmp
Set [PY Total_Incentive_Local] = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,PYCRE.[Total_Incentive_Local]))
from #tmp t 
left outer join COMP_REC_ENC PYCRE On t.EMPLID = PYCRE.EMPLID 
and PYCRE.Year = dbo.getPreviousYear()


Update #tmp
Set [Planning Manager Name] = Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CRE.[Name]))
from #tmp t 
left outer join COMP_REC_ENC CRE On t.[Planning Manager Name] = CRE.EMPLID 
and CRE.Year = dbo.getPreviousYear()



If @Report_ID = 1
Begin
	Select * from #tmp t 
	Where Cast(t.[CY Total_Incentive_Local] as numeric) = '0'
End 

If @Report_ID = 2
Begin
	Select * from #tmp t 
	Where Cast(t.[CY Total_Incentive_Local] as numeric) < Cast(t.[CY PTIE_Local] as numeric)
End 



If @Report_ID = 3
Begin	


	Select 
	--t.Grade,
	--Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CY.[New_Band])),
	t.* from #tmp t 
	Inner join COMP_REC_ENC CY on t.EMPLID = CY.EMPLID
	Where 
	CY.Year = dbo.getCurrentYear()
	and Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CY.[New_Band])) != ''
	and t.Grade != ''
	and
	(Cast(Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CY.[New_Band])) as numeric) >  Cast(t.Grade as numeric) + 2 
	Or Cast(Convert(Varchar(500), DECRYPTBYPASSPHRASE(@key,CY.[New_Band])) as numeric) < Cast(t.Grade as numeric) -1 )
End 
 
If @Report_ID = 4
Begin
	Select * from #tmp t 
	Where (cast(t.[Salary Increase Amount (Local)] as numeric) > 0	Or cast(t.[CY Total_Incentive_Local] as numeric) > 0)	
End 

If @Report_ID = 5
Begin
	Select * from #tmp t 
	Where (cast(t.[Salary Increase Amount (Local)] as numeric) > 0	and 
	(cast(t.[Off-Cycle Incr, excl. Merit (Mar CY - Oct CY)] as numeric) > 0 Or
	cast(t.[Off-Cycle Salary Increase (Local)] as numeric) > 0) 
	)	
End 



If @Report_ID = 7
Begin
	Select * from #tmp t 
	Where cast(t.[Salary Increase Amount (Local)] as numeric) = 0		
	
End 




If @Report_ID in (6,8, 9, 10,11, 12)
Begin
	Select t.* from #tmp t 	
End 



Drop Table #tmp
Go

SELECT 'GuardAppEvent:Released'
GO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.BLL
{
    public class CompMixInfo
    {

        public static List<CompMixGrid> getCompMixGrid()
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {

                var compMixGridList = context.CompMixGrids.ToList();

                return compMixGridList;
            }

        }

        public static void updateCompMixGrid(List<CompMixGrid> lstCompMixGrid)
        {
            if (lstCompMixGrid == null)
                return;

            using (HRCMSEntities context = new HRCMSEntities())
            {
                HashSet<int> diffids = new HashSet<int>(lstCompMixGrid.Select(s => s.CompMixGridID));

                List<CompMixGrid> results = context.CompMixGrids.Where(m => diffids.Contains(m.CompMixGridID)).ToList();
                                             

                foreach (CompMixGrid cg in results)
                {
                    cg.ApplicableGrid = lstCompMixGrid.Where(m => m.CompMixGridID == cg.CompMixGridID).FirstOrDefault().ApplicableGrid;
                }

                context.SaveChanges();
            }
        }


        public static void deleteCompMixGrid(string deletedCompMixGridIds)
        {
            if (deletedCompMixGridIds == null)
                return;

            using (HRCMSEntities context = new HRCMSEntities())
            {
                var asd = deletedCompMixGridIds.Split(',');

                HashSet<string> diffids = new HashSet<string>(deletedCompMixGridIds.Split(','));

                List<CompMixGrid> results = context.CompMixGrids.Where(m => diffids.Contains(m.CompMixGridID.ToString())).ToList();
                
                context.CompMixGrids.RemoveRange(results);

                context.SaveChanges();

            }
        }


        public static List<CompMixTier> getCompMixTier()
        {
            using (HRCMSEntities context = new HRCMSEntities())
            {

                var compMixTierList = context.CompMixTiers.OrderBy(m=> m.ApplicableGrid).ThenBy(n=> n.TCUSDMin).ToList();

                return compMixTierList;
            }
        }

        public static void updateCompMixTier(List<CompMixTier> lstCompMixTier)
        {
            if (lstCompMixTier == null)
                return;

            using (HRCMSEntities context = new HRCMSEntities())
            {
                HashSet<int> diffids = new HashSet<int>(lstCompMixTier.Select(s => s.CompMixTierID));

                List<CompMixTier> results = context.CompMixTiers.Where(m => diffids.Contains(m.CompMixTierID)).ToList();


                foreach (CompMixTier cg in results)
                {
                    var _updatedRec = lstCompMixTier.Where(m => m.CompMixTierID == cg.CompMixTierID).FirstOrDefault();
                    cg.Tier = _updatedRec.Tier;
                    cg.TCUSDMin = _updatedRec.TCUSDMin;
                    cg.TCUSDMax = _updatedRec.TCUSDMax;
                    cg.MinSalary = _updatedRec.MinSalary;
                }

                context.SaveChanges();
            }
        }


        public static void deleteCompMixTier(string deletedCompMixGridIds)
        {
            if (deletedCompMixGridIds == null)
                return;

            using (HRCMSEntities context = new HRCMSEntities())
            {
                var asd = deletedCompMixGridIds.Split(',');

                HashSet<string> diffids = new HashSet<string>(deletedCompMixGridIds.Split(','));

                List<CompMixTier> results = context.CompMixTiers.Where(m => diffids.Contains(m.CompMixTierID.ToString())).ToList();

                context.CompMixTiers.RemoveRange(results);

                context.SaveChanges();

            }
        }

        public static void insertCompMixTier(List<CompMixTier> lstCompMixTier)
        {
            if (lstCompMixTier == null)
                return;

            using (HRCMSEntities context = new HRCMSEntities())
            {                
                context.CompMixTiers.AddRange(lstCompMixTier);

                context.SaveChanges();

            }
        }


        public static void calculateCompMix(int? emplID)
        {
            try
            {
                using (HRCMSEntities context = new HRCMSEntities())
                {

                    context.RecalculateCompByCompMix("ECR", emplID, "Comp Mix");

                }
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }
}

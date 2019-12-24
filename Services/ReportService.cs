using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Dynamic.Core;

using FamilyBudget.Application.Interface;
using FamilyBudget.Application.Model;

using System.Threading.Tasks;

namespace FamilyBudget.Application.Services
{
    public class ReportService : IReportService
    {
        readonly IPurchaseRepo _purchRepo;
        readonly IProductRepo _prodRepo;
        readonly ICategoryXrefRepo _catxrefRepo;
        protected int _count;
        public int Count {get { return _count; } }


        public ReportService(IPurchaseRepo purchRepo, IProductRepo prodRepo, ICategoryXrefRepo catxrefRepo)
        {
            _purchRepo = purchRepo;
            _prodRepo = prodRepo;
            _catxrefRepo = catxrefRepo;
        }

        public IEnumerable<Report1> GetAll(string pagingInfo)
        {
            //select Products.Title, Purchases.Count, Purchases.PurchaseDate,(CASE WHEN Purchases.CostOverride IS NULL THEN Products.Cost * Purchases.Count ELSE Purchases.CostOverride END) Cost 
            //from Purchases inner join Products on Products.ProductID  = Purchases.ProductID 
            //inner join CategoryXrefs on CategoryXrefs.ProductID  = Products.ProductID 
            //where CategoryXrefs.CategoryID=? order by Purchases.PurchaseDate", [catid], (err, rows) => {

            IEnumerable<Report1> results = null;
            if (pagingInfo!=null) {
                var purchases = _purchRepo.ReadAll();
                var products = _prodRepo.ReadAll();
                var catxrefs = _catxrefRepo.ReadAll();
                var prs = pagingInfo.ParseSearchRequest();

//http://localhost:5002/api/report?page=0&pageSize=99&sort[0][field]=Title&sort[0][dir]=asc&filter[logic]=and&filter[filters][0][field]=CategoryID&filter[filters][0][operator]=eq&filter[filters][0][value]=1
                  //if (prs.Page>0) prs.Page--;
                  string filtering=prs.GetFiltering<CategoryXrefVM>();
                  var sorting = prs.GetSorting();

                var rptPurch = (from pu in purchases.OrderBy(sorting)
                             join pr in products on pu.ProductID equals pr.ProductID
                             join xrf in catxrefs.Where (filtering) on pr.ProductID equals xrf.ProductID
                             select new {
                                    monthyear = $"{pu.PurchaseDate.Month}/{pu.PurchaseDate.Year}",
                                    Cost = (pu.CostOverride==null? pr.Cost.Value*pu.Count: pu.CostOverride.Value)
                                }).ToList();

                IEnumerable<Report1> query = rptPurch.GroupBy(p => p.monthyear)
                            .Select(cl => new Report1{
                                monthyear = cl.Key,
                                Cost = cl.Sum(c => c.Cost)
                            });

                // var rptPurch = (from pu in purchases
                //              join pr in products on pu.ProductID equals pr.ProductID
                //              join xrf in catxrefs.Where (filtering) on pr.ProductID equals xrf.ProductID
                //             // where xrf.CategoryID == 1
                //              select new {
                //                     monthyear = $"{pu.PurchaseDate.Month}/{pu.PurchaseDate.Year}",
                //                     Title = pr.Title,
                //                     Cost = (pu.CostOverride==null? pr.Cost.Value*pu.Count: pu.CostOverride.Value)
                //                 }).ToList();

                // IEnumerable<Report1> query = rptPurch.GroupBy(p => new { p.monthyear, p.Title})
                //             .Select(cl => new Report1{
                //                 Title = cl.Key.Title,
                //                 monthyear = cl.Key.monthyear,
                //                 Cost = cl.Sum(c => c.Cost)
                //             });


                //   IQueryable<Report1> query = from pu in purchases
                //                                     join pr in products.OrderBy(sorting) on pu.ProductID equals pr.ProductID
                //                                     join xrf in catxrefs.Where(filtering) on pr.ProductID equals xrf.ProductID
                //                                  select new Report1 {
                //                                      monthyear = $"{pu.PurchaseDate.Month}/{pu.PurchaseDate.Year}",
                //                                      Cost = (pu.CostOverride==null? pr.Cost.Value*pu.Count: pu.CostOverride.Value)
                //                                  };
                    try {
                        _count = query.Count();
                        int skip = prs.Page * prs.PageSize;
                        results = query.Skip(skip).Take(prs.PageSize).ToList();
                    }
                    catch {}


            }
            else {
                var purchases = _purchRepo.ReadAll();
                var products = _prodRepo.ReadAll();
                var catxrefs = _catxrefRepo.ReadAll();

                var rptPurch = (from pu in purchases.OrderBy(x => x.PurchaseDate)
                             join pr in products on pu.ProductID equals pr.ProductID
                             join xrf in catxrefs on pr.ProductID equals xrf.ProductID
                             select new {
                                    monthyear = $"{pu.PurchaseDate.Month}/{pu.PurchaseDate.Year}",
                                    Cost = (pu.CostOverride==null? pr.Cost.Value*pu.Count: pu.CostOverride.Value)
                                }).ToList();

                results = rptPurch.GroupBy(p => p.monthyear)
                            .Select(cl => new Report1{
                                monthyear = cl.Key,
                                Cost = cl.Sum(c => c.Cost)
                            });

                _count = results.Count();
            }
            return results;
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Dynamic.Core;

using FamilyBudget.Application.Interface;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        readonly IPurchaseRepo _purchRepo;
        readonly IProductRepo _prodRepo;

        protected int _count;
        public int Count {get { return _count; } }


        public PurchaseService(IPurchaseRepo purchRepo, IProductRepo prodRepo)
        {
            _purchRepo = purchRepo;
            _prodRepo = prodRepo;
        }

        public IEnumerable<PurchaseVM> GetAll(string pagingInfo)
        {
            IEnumerable<PurchaseVM> results = null;
            if (pagingInfo!=null) {
                var purchases = _purchRepo.ReadAll();
                var products = _prodRepo.ReadAll();
                var prs = pagingInfo.ParseSearchRequest();

                  if (prs.Page>0) prs.Page--;
                  string filtering=prs.GetFiltering<ProductVM>();
                  var sorting = prs.GetSorting();
                  IQueryable<PurchaseVM> query = from pu in purchases.Where(filtering).OrderBy(sorting)
                                                    join pr in products on pu.ProductID equals pr.ProductID
                                                 select new PurchaseVM(pu, pr);
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
                results = from pu in purchases
                                join pr in products on pu.ProductID equals pr.ProductID
                            select new PurchaseVM(pu, pr) ; 
                _count = results.Count();
            }
            return results;
        }

    }
}
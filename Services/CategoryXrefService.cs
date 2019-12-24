using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Dynamic.Core;

using FamilyBudget.Application.Interface;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Services
{
    public class CategoryXrefService : ICategoryXrefService
    {
        readonly ICategoryXrefRepo _Repo;

        protected int _count;
        public int Count {get { return _count; } }


        public CategoryXrefService(ICategoryXrefRepo Repo)
        {
            _Repo = Repo;
        }

        public IEnumerable<CategoryXrefVM> GetAll(string pagingInfo)
        {
            IEnumerable<CategoryXrefVM> results = null;
            if (pagingInfo!=null) {
                var pvm = _Repo.ReadAll();
                var prs = pagingInfo.ParseSearchRequest();

          //        if (prs.Page>0) prs.Page--;
                  string filtering=prs.GetFiltering<CategoryXrefVM>();
                  var sorting = prs.GetSorting();
                  IQueryable<CategoryXrefVM> query = from p in pvm.Where(filtering).OrderBy(sorting)
                                                 select new CategoryXrefVM(p) ;
                    try {
                        _count = query.Count();
                        int skip = prs.Page * prs.PageSize;
                        results = query.Skip(skip).Take(prs.PageSize).ToList();
                    }
                    catch {}


            }
            else {
                results = from p in _Repo.ReadAll()
                            select new CategoryXrefVM(p) ;
                _count = results.Count();
            }
            return results;
        }



        public void Update(int ProductID, CategoryVM [] updobj)
        {
           _Repo.Delete(ProductID);

           foreach(var c in updobj) {
            var cx = new CategoryXrefVM();
            cx.ProductID=ProductID;
            cx.CategoryID=c.CategoryID;

            var prd = _Repo.Add(cx);
           }
           _Repo.Save();
        }

    }
}
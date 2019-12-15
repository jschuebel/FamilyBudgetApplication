using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Dynamic.Core;

using FamilyBudget.Application.Interface;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepo _Repo;

        protected int _count;
        public int Count {get { return _count; } }


        public CategoryService(ICategoryRepo Repo)
        {
            _Repo = Repo;
        }

        public IEnumerable<CategoryVM> GetAll(string pagingInfo)
        {
            IEnumerable<CategoryVM> results = null;
            if (pagingInfo!=null) {
                var pvm = _Repo.ReadAll();
                var prs = pagingInfo.ParseSearchRequest();

          //        if (prs.Page>0) prs.Page--;
                  string filtering=prs.GetFiltering<CategoryVM>();
                  var sorting = prs.GetSorting();
                  IQueryable<CategoryVM> query = from p in pvm.Where(filtering).OrderBy(sorting)
                                                 select new CategoryVM(p) ;
                    try {
                        _count = query.Count();
                        int skip = prs.Page * prs.PageSize;
                        results = query.Skip(skip).Take(prs.PageSize).ToList();
                    }
                    catch {}


            }
            else {
                results = from p in _Repo.ReadAll()
                            select new CategoryVM(p) ;
                _count = results.Count();
            }
            return results;
        }

    }
}
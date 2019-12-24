using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public CategoryVM FindById(int ID)
        {
            var prd = _Repo.ReadAll().Where(x => x.CategoryID==ID).First();
            return new CategoryVM(prd);
        }


        public CategoryVM Create(CategoryVM obj)
        {
           var prd = _Repo.Create(obj);
            return new CategoryVM(prd);
        }

        public CategoryVM Update(CategoryVM obj)
        {
            //if there is a Date of Birth event, update it
            // var dob = _Repo.ReadAll().Where(x=> x.ProductID=obj.ProductID).FirstOrDefault();
            // if (dob!=null) {
            //     dob.Date=cust.DateOfBirth;
            //     _eventRepo.Update(dob);
            // }
            obj = (CategoryVM)_Repo.Update(obj);
            return obj;
        }

        public Task<bool> Delete(int ID)
        {
            //Func<bool> function = new Func<bool>(() => _Repo.Delete(ProductID));
            //return Task.Run<bool>(function);
             return Task.Run(() => 
             {
                 return _Repo.Delete(ID);
             });
            //return Task.FromResult<bool>(_Repo.Delete(ProductID));
        }



    }
}
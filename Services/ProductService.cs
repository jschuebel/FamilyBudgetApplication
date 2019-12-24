using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Linq.Dynamic.Core;

using FamilyBudget.Application.Interface;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Services
{
    public class ProductService : IProductService
    {
        readonly IProductRepo _Repo;

        protected int _count;
        public int Count {get { return _count; } }


        public ProductService(IProductRepo Repo)
        {
            _Repo = Repo;
        }

        public IEnumerable<ProductVM> GetAll(string pagingInfo)
        {
            IEnumerable<ProductVM> results = null;
            if (pagingInfo!=null) {
                var pvm = _Repo.ReadAll();
                var prs = pagingInfo.ParseSearchRequest();

          //        if (prs.Page>0) prs.Page--;
                  string filtering=prs.GetFiltering<ProductVM>();
                  var sorting = prs.GetSorting();
                  IQueryable<ProductVM> query = from p in pvm.Where(filtering).OrderBy(sorting)
                                                 select new ProductVM(p) ;
                    try {
                        _count = query.Count();
                        int skip = prs.Page * prs.PageSize;
                        results = query.Skip(skip).Take(prs.PageSize).ToList();
                    }
                    catch {}


            }
            else {
                results = from p in _Repo.ReadAll()
                            select new ProductVM(p) ;
                _count = results.Count();
            }
            return results;
        }
        public ProductVM FindById(int ProductID)
        {
            var prd = _Repo.ReadAll().Where(x => x.ProductID==ProductID).First();
            return new ProductVM(prd);
        }


        public ProductVM Create(ProductVM obj)
        {
           var prd = _Repo.Create(obj);
            return new ProductVM(prd);
        }

        public ProductVM Update(ProductVM obj)
        {
            //if there is a Date of Birth event, update it
            // var dob = _Repo.ReadAll().Where(x=> x.ProductID=obj.ProductID).FirstOrDefault();
            // if (dob!=null) {
            //     dob.Date=cust.DateOfBirth;
            //     _eventRepo.Update(dob);
            // }
            obj = (ProductVM)_Repo.Update(obj);
            return obj;
        }

        public Task<bool> Delete(int ProductID)
        {
            //Func<bool> function = new Func<bool>(() => _Repo.Delete(ProductID));
            //return Task.Run<bool>(function);
             return Task.Run(() => 
             {
                 return _Repo.Delete(ProductID);
             });
            //return Task.FromResult<bool>(_Repo.Delete(ProductID));
        }


    }
}
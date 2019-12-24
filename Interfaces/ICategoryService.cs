using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface ICategoryService
    {
        int Count {get;}

        CategoryVM Create(CategoryVM newobj);
        //Read //GET
        CategoryVM FindById(int id);
        IEnumerable<CategoryVM> GetAll(string pagingInfo);
        //Update //PUT
        CategoryVM Update(CategoryVM updobj);
        
        //Delete //DELETE
        Task<bool> Delete(int id);        
    }
}
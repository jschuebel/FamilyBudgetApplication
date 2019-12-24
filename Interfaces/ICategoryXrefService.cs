using System;
using System.Collections.Generic;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface ICategoryXrefService
    {
        int Count {get;}

        //CategoryXrefVM NewPerson(string firstName,  string lastName, string address);
        //CategoryXrefVM CreatePerson(CategoryXrefVM newobj);
        //Read //GET
        //CategoryXrefVM FindById(int id);
        //Person FindPersonByIdIncludeOrders(int id);
        IEnumerable<CategoryXrefVM> GetAll(string pagingInfo);
        //Update //PUT
        void Update(int ProductID, CategoryVM [] updobj);
        
        //Delete //DELETE
        //bool DeletePerson(int id);        
    }
}
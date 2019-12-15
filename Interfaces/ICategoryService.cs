using System;
using System.Collections.Generic;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface ICategoryService
    {
        int Count {get;}

        //CategoryVM NewPerson(string firstName,  string lastName, string address);
        //CategoryVM CreatePerson(CategoryVM newobj);
        //Read //GET
        //CategoryVM FindById(int id);
        //Person FindPersonByIdIncludeOrders(int id);
        IEnumerable<CategoryVM> GetAll(string pagingInfo);
        //Update //PUT
        //CategoryVM UpdatePerson(CategoryVM updobj);
        
        //Delete //DELETE
        //bool DeletePerson(int id);        
    }
}
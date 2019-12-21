using System;
using System.Collections.Generic;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface IProductService
    {
        int Count {get;}

        //ProductVM NewPerson(string firstName,  string lastName, string address);
        ProductVM Create(ProductVM newobj);
        //Read //GET
        ProductVM FindById(int ProductID);
        //Person FindPersonByIdIncludeOrders(int id);
        IEnumerable<ProductVM> GetAll(string pagingInfo);
        //Update //PUT
        ProductVM Update(ProductVM updobj);
        
        //Delete //DELETE
        bool Delete(int ProductID);        
    }
}
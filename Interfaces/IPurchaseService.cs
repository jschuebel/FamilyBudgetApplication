using System;
using System.Collections.Generic;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface IPurchaseService
    {
        int Count {get;}

        //ProductVM NewPerson(string firstName,  string lastName, string address);
        //ProductVM CreatePerson(ProductVM newobj);
        //Read //GET
        //ProductVM FindById(int id);
        //Person FindPersonByIdIncludeOrders(int id);
        IEnumerable<PurchaseVM> GetAll(string pagingInfo);
        //Update //PUT
        //ProductVM UpdatePerson(ProductVM updobj);
        
        //Delete //DELETE
        //bool DeletePerson(int id);        
    }
}
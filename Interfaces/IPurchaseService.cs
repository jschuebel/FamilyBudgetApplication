using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface IPurchaseService
    {
        int Count {get;}
        PurchaseVM Create(PurchaseVM newobj);
        //Read //GET
        PurchaseVM FindById(int id);
        IEnumerable<PurchaseVM> GetAll(string pagingInfo);
        //Update //PUT
        PurchaseVM Update(PurchaseVM updobj);
        
        //Delete //DELETE
        Task<bool> Delete(int id);        
    }
}
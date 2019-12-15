using System;
using System.Collections.Generic;
using FamilyBudget.Domain.Model;
using System.Linq;

namespace FamilyBudget.Application.Interface
{
    public interface IPurchaseRepo
    {
        //Create Data
        //No Id when enter, but Id when exits
        Purchase Create(Purchase newobj);
        //Read Data
        Purchase GetById(int id);
        IQueryable<Purchase> ReadAll();
        //Update Data
        Purchase Update(Purchase evt);
        //Delete Data
        bool Delete(int id);
        
    }
}
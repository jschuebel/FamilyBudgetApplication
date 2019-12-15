using System;
using System.Collections.Generic;
using FamilyBudget.Domain.Model;
using System.Linq;

namespace FamilyBudget.Application.Interface
{
    public interface IProductRepo
    {
        //Create Data
        //No Id when enter, but Id when exits
        Product Create(Product evt);
        //Read Data
        Product GetById(int id);
        IQueryable<Product> ReadAll();
        //Update Data
        Product Update(Product evt);
        //Delete Data
        bool Delete(int id);
        
    }
}
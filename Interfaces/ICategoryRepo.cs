using System;
using System.Collections.Generic;
using FamilyBudget.Domain.Model;
using System.Linq;

namespace FamilyBudget.Application.Interface
{
    public interface ICategoryRepo
    {
        //Create Data
        //No Id when enter, but Id when exits
        Category Create(Category evt);
        //Read Data
        Category GetById(int id);
        IQueryable<Category> ReadAll();
        //Update Data
        Category Update(Category evt);
        //Delete Data
        bool Delete(int id);
        
    }
}
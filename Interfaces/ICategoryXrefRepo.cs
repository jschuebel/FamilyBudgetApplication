using System;
using System.Collections.Generic;
using FamilyBudget.Domain.Model;
using System.Linq;

namespace FamilyBudget.Application.Interface
{
    public interface ICategoryXrefRepo
    {
        //Create Data
        //No Id when enter, but Id when exits
        CategoryXref Create(CategoryXref evt);
        //Read Data
        CategoryXref GetById(int id);
        IQueryable<CategoryXref> ReadAll();
        //Update Data
        CategoryXref Update(CategoryXref evt);
        //Delete Data
        bool Delete(int id);
        
    }
}
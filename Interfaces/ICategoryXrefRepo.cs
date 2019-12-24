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
        CategoryXref Add(CategoryXref obj);
        void Save();
        //Read Data
        IQueryable<CategoryXref> ReadAll();
        //Update Data
        //Delete Data
        bool Delete(int id);
        
    }
}
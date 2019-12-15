using System;
using FamilyBudget.Domain.Model;

namespace FamilyBudget.Application.Model
{
    public class CategoryXrefVM : CategoryXref
    {

        public CategoryXrefVM(){}
        public CategoryXrefVM(CategoryXref p) {
            this.CategoryID=p.CategoryID;
            this.ProductID=p.ProductID;
        }
    }

}
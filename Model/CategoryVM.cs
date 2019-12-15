using System;
using FamilyBudget.Domain.Model;

namespace FamilyBudget.Application.Model
{
    public class CategoryVM : Category
    {

        public CategoryVM(){}
        public CategoryVM(Category p) {
            this.CategoryID=p.CategoryID;
            this.Title=p.Title;
        }
    }

}
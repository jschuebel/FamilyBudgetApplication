using System;
using FamilyBudget.Domain.Model;

namespace FamilyBudget.Application.Model
{
    public class ProductVM : Product
    {

        public ProductVM(){}
        public ProductVM(Product p) {
            this.ProductID=p.ProductID;
            this.Title=p.Title;
            this.Cost=p.Cost!=null? p.Cost/100.0:null;
            this.Count=p.Count;
        }
    }

}
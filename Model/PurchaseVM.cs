using System;
using FamilyBudget.Domain.Model;

namespace FamilyBudget.Application.Model
{
    public class PurchaseVM : Purchase
    {
        public PurchaseVM(){}
        public PurchaseVM(Purchase pu, Product pr) {
            this.PurchaseID=pu.PurchaseID;
            this.ProductID=pu.ProductID;
            this.Count=pu.Count;
            this.CostOverride=pu.CostOverride;
            this.PurchaseDate=pu.PurchaseDate;

            if (pr!=null) {
                this.ProductTitle = pr.Title;
                this.UnitCost = pr.Cost;
                this.UnitCount= pr.Count;
            }

            if (this.CostOverride!=null)
                this.Cost=this.CostOverride.Value;
            else {
                if (pr!=null && this.UnitCost!=null) {
                    this.Cost = this.Count * this.UnitCost.Value;
                }
            }



        }

        
        public string ProductTitle { get ; set;}
        public double? UnitCost { get ; set;}
        public int UnitCount { get ; set;}
        public double Cost {get; set; }
    }

}
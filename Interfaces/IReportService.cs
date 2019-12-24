using System;
using System.Collections.Generic;
using FamilyBudget.Application.Model;

namespace FamilyBudget.Application.Interface
{
    public interface IReportService
    {
        int Count {get;}

        IEnumerable<Report1> GetAll(string pagingInfo);
    }
}
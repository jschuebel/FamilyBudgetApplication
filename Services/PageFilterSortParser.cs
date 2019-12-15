using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using System.Threading.Tasks;

using FamilyBudget.Domain.Model;
using FamilyBudget.Domain.Services;
using FamilyBudget.Application.Interface;

namespace FamilyBudget.Application.Services
{
    public static class PageFilterSortParser
    {

       private static ICollection<SortObject> GetSortObjects(StringValueProvider svp)
        {
            var list = new List<SortObject>();
            var filterKeys = new List<KeyValuePair<string, string>>();
            //sort[0][field]=Date&sort[0][dir]=asc
            for(int i=0;i<10;i++)
            {
                var fieldName = svp.ValueFor($"sort[{i}][field]").First();
                if (fieldName==null)
                    break;
                else {
                    var srtObj = new SortObject(fieldName,svp.ValueFor($"sort[{i}][dir]").First());

                    list.Add(srtObj);
                }
            }
            return list;
        }

        private static FilterObjectWrapper GetFilterObjects(StringValueProvider svp, string filterLogic)
        {
            var list = new List<FilterObject>();
            var filterKeys = new List<KeyValuePair<string, string>>();
        //&filter[logic]=and&filter[filters][0][field]=Date&filter[filters][0][operator]=contains&filter[filters][0][value]=4/1/2018
        //&filter[logic]=and&filter[filters][0][field]=Date&filter[filters][0][operator]=gte&filter[filters][0][value]=Mon+Apr+23+2018+00%3A00%3A00+GMT-0500+(Central+Daylight+Time)&filter[filters][1][field]=Date&filter[filters][1][operator]=lte&filter[filters][1][value]=Mon+Apr+30+2018+00%3A00%3A00+GMT-0500+(Central+Daylight+Time)&_=1562110553341
            for(int i=0;i<10;i++)
            {
                var fieldName = svp.ValueFor($"filter[filters][{i}][field]").First();
                if (fieldName==null)
                    break;
                else {
                    var fltObj = new FilterObject();
                    fltObj.Field1 = fieldName;
                    fltObj.Operator1 = svp.ValueFor($"filter[filters][{i}][operator]").First();
                    fltObj.Value1 = svp.ValueFor($"filter[filters][{i}][value]").First();

                    list.Add(fltObj);
                }
            }
            return new FilterObjectWrapper(filterLogic, list);
        }



        public static SearchRequest ParseSearchRequest(this string NameValueString)
        {
            if (NameValueString == null)
             throw new ArgumentNullException(nameof(NameValueString));  
            var svp = new StringValueProvider(NameValueString);
            var filterLogic = svp.ValueFor("filter[logic]");
//           if (filterLogic == ValueProviderResult.None)
//                      return Task.CompletedTask; 

//?page=1&pageSize=20&sort[0][field]=Date&sort[0][dir]=asc&filter[logic]=and&filter[filters][0][field]=Date&filter[filters][0][operator]=gte&filter[filters][0][value]=4/1/2018
//?page=1&pageSize=20&sort[0][field]=Name&sort[0][dir]=asc&filter[logic]=and&filter[filters][0][field]=Name&filter[filters][0][operator]=contains&filter[filters][0][value]=schuebel
//&filter[logic]=and&filter[filters][0][field]=Name&filter[filters][0][operator]=contains&filter[filters][0][value]=schuebel
            var page = svp.ValueFor("page").First();// bindingContext.ModelName);
            var pageSize = svp.ValueFor("pageSize").First();
            var filtering =  GetFilterObjects(svp, filterLogic.First());
            var sorting = GetSortObjects(svp);

            var result = new SearchRequest {
                Page=int.Parse(page??"0"),
                PageSize=int.Parse(pageSize??"25"),
                FilterObjectWrapper = filtering,
                SortObjects = sorting
            };
            return result;
        }




    }

}
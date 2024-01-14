using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTree.Interface;

namespace TechTree.Extentions
{
    public static class ConvertExtentions
    {
        public static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> collection,int selectedValue)where T: IPrimaryProperties

        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = (item.Id == selectedValue)
                    }).ToList();
        }
    }
}

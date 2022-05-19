using Common.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using CVBank.Dto.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.DataLakeExtensions
{
    public static class Extensions
    {
        public static SelectList ToSelectList(this PipelineResponseModel model)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (model.HasData())
            {
                if (model.value.HasData())
                {
                    foreach(var item in model.value)
                    {
                        list.Add(new SelectListItem()
                        {
                            Text = item.name,
                            Value = item.etag
                        });
                    }
                }
            }
            return new SelectList(list, "Value", "Text");
        }
    }
}

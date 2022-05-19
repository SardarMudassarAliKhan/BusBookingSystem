using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper.EmailTemplate
{
    public class EmailTemplate
    {
        public static string GetTemplate(string path)
        {
            try
            {
                string templateString = System.IO.File.ReadAllText(path);
                return templateString;
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }
    }
}

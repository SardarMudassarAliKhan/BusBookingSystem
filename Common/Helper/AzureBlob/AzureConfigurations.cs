using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper.AzureBlob
{
    public class AzureConfigurations
    {
        public static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=vvmcvbankstracc;AccountKey=/8sM6AsaP5N5q3T4TjVdBCb+Bv4TR30HjZlvyvktO6jwCnwGatmPrsPGyuI3SibF8+ub+9RSzW7F99RMjj/+8Q==;EndpointSuffix=core.windows.net";
        public static string Container = "cvbank";
        public static string Directory = "cvfiles";
        public static string RootPath = "https://vvmcvbankstracc.blob.core.windows.net";
    }
}

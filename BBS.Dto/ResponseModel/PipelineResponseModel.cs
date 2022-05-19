using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.Dto.ResponseModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    //public class DependsOn
    //{
    //    public string activity { get; set; }
    //    public List<string> dependencyConditions { get; set; }
    //}

    //public class Policy
    //{
    //    public string timeout { get; set; }
    //    public int retry { get; set; }
    //    public int retryIntervalInSeconds { get; set; }
    //    public bool secureOutput { get; set; }
    //    public bool secureInput { get; set; }
    //}

    //public class BaseParameters
    //{
    //    public string filename { get; set; }
    //}

    //public class TypeProperties
    //{
    //    public string notebookPath { get; set; }
    //    public BaseParameters baseParameters { get; set; }
    //}

    //public class LinkedServiceName
    //{
    //    public string referenceName { get; set; }
    //    public string type { get; set; }
    //}

    //public class Activity
    //{
    //    public string name { get; set; }
    //    public string type { get; set; }
    //    public List<DependsOn> dependsOn { get; set; }
    //    public Policy policy { get; set; }
    //    public List<object> userProperties { get; set; }
    //    public TypeProperties typeProperties { get; set; }
    //    public LinkedServiceName linkedServiceName { get; set; }
    //}

    //public class Properties
    //{
    //    public List<Activity> activities { get; set; }
    //    public List<object> annotations { get; set; }
    //    public DateTime lastPublishTime { get; set; }
    //}

    //public class Value
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string type { get; set; }
    //    public Properties properties { get; set; }
    //    public string etag { get; set; }
    //}

    //public class PipelineResponseModel
    //{
    //    public List<Value> value { get; set; }
    //}
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class DependsOn
    {
        public string activity { get; set; }
        public List<string> dependencyConditions { get; set; }
    }

    public class Policy
    {
        public string timeout { get; set; }
        public int retry { get; set; }
        public int retryIntervalInSeconds { get; set; }
        public bool secureOutput { get; set; }
        public bool secureInput { get; set; }
    }

    public class BaseParameters
    {
        public object filename { get; set; }
    }

    public class TypeProperties
    {
        public string notebookPath { get; set; }
        public BaseParameters baseParameters { get; set; }
    }

    public class LinkedServiceName
    {
        public string referenceName { get; set; }
        public string type { get; set; }
    }

    public class Activity
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<DependsOn> dependsOn { get; set; }
        public Policy policy { get; set; }
        public List<object> userProperties { get; set; }
        public TypeProperties typeProperties { get; set; }
        public LinkedServiceName linkedServiceName { get; set; }
    }

    public class FileName
    {
        public string type { get; set; }
        public string defaultValue { get; set; }
    }

    public class Parameters
    {
        public FileName FileName { get; set; }
    }

    public class Properties
    {
        public List<Activity> activities { get; set; }
        public List<object> annotations { get; set; }
        public DateTime lastPublishTime { get; set; }
        public Parameters parameters { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
        public string etag { get; set; }
    }

    public class PipelineResponseModel
    {
        public List<Value> value { get; set; }
    }


}

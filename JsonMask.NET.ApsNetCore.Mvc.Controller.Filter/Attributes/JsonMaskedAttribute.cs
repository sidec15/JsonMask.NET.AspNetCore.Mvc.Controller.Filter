﻿namespace JsonMask.NET.ApsNetCore.Mvc.Controller.Filter.Attributes
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class JsonMaskedAttribute : Attribute
  {
    public string QueryParameterName { get; private set; }

    public JsonMaskedAttribute(string queryParameterName = GlobalVariables.PROJECTION_QUERY_PARAM)
    {
      QueryParameterName = queryParameterName;
    }
  }

}

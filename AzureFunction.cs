// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Azure.AppServices;
using RESTClient;
using UnityEngine;
using System.Collections;
using System;

namespace Azure.Functions {
  public class AzureFunction {
    private AzureFunctionClient client;

    private string name; // Azure Function name
    private string key; // Azure Function key
    private string apiPath; // Azure Functions API path

    private const string API = "api";
    private const string PARAM_CODE = "code";

    public AzureFunction(string name, AzureFunctionClient client, string key = null, string apiPath = API) {
      this.client = client;
      this.name = name;
      this.key = key;
      this.apiPath = apiPath;
    }

    public override string ToString() {
      return name;
    }

    public IEnumerator Get<T>(Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<T>(Method.GET, callback, path, query);
    }

    public IEnumerator Post<T>(Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<T>(Method.POST, callback, path, query);
    }

    public IEnumerator Patch<T>(Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<T>(Method.PATCH, callback, path, query);
    }

    public IEnumerator Put<T>(Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<T>(Method.PUT, callback, path, query);
    }

    public IEnumerator Delete<T>(Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<T>(Method.DELETE, callback, path, query);
    }

    public IEnumerator Post<B, T>(B body, Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<B, T>(body, Method.POST, callback, path, query);
    }

    public IEnumerator Patch<B, T>(B body, Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<B, T>(body, Method.PATCH, callback, path, query);
    }

    public IEnumerator Put<B, T>(B body, Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<B, T>(body, Method.PUT, callback, path, query);
    }

    public IEnumerator Delete<B, T>(B body, Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApi<B, T>(body, Method.DELETE, callback, path, query);
    }

    #region Variant methods for parsing JSON arrays

    public IEnumerator GetArray<T>(Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<T>(Method.GET, callback, path, query);
    }

    public IEnumerator PostArray<T>(Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<T>(Method.POST, callback, path, query);
    }

    public IEnumerator PatchArray<T>(Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<T>(Method.PATCH, callback, path, query);
    }

    public IEnumerator PutArray<T>(Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<T>(Method.PUT, callback, path, query);
    }

    public IEnumerator DeleteArray<T>(Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<T>(Method.DELETE, callback, path, query);
    }

    public IEnumerator PostArray<B, T>(B body, Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<B, T>(body, Method.POST, callback, path, query);
    }

    public IEnumerator PatchArray<B, T>(B body, Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<B, T>(body, Method.PATCH, callback, path, query);
    }

    public IEnumerator PutArray<B, T>(B body, Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<B, T>(body, Method.PUT, callback, path, query);
    }

    public IEnumerator DeleteArray<B, T>(B body, Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      yield return InvokeApiArray<B, T>(body, Method.DELETE, callback, path, query);
    }

    #endregion

    #region Async methods for parsing JSON objects

    private IEnumerator InvokeApi<T>(Method httpMethod = Method.GET, Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      var request = new ZumoRequest(ApiUrl(path), httpMethod, false, client.User);
      request.SetQueryParams(query);
      if (!string.IsNullOrEmpty(key)) {
        request.AddQueryParam(PARAM_CODE, key, true);
      }
      yield return request.Request.Send();
      if (typeof(T) == typeof(string)) {
        request.GetText(callback);
      } else {
        request.ParseJson<T>(callback);
      }
    }

    private IEnumerator InvokeApi<B, T>(B body, Method httpMethod = Method.POST, Action<IRestResponse<T>> callback = null, string path = null, QueryParams query = null) {
      var request = new ZumoRequest(ApiUrl(path), httpMethod, false, client.User);
      request.SetQueryParams(query);
      if (!string.IsNullOrEmpty(key)) {
        request.AddQueryParam(PARAM_CODE, key, true);
      }
      if (body != null) {
        request.AddBody(body);
      }
      yield return request.Request.Send();
      if (typeof(T) == typeof(string)) {
        request.GetText(callback);
      } else {
        request.ParseJson<T>(callback);
      }
    }

    #endregion

    #region Async methods for parsing JSON arrays

    private IEnumerator InvokeApiArray<T>(Method httpMethod = Method.GET, Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      var request = new ZumoRequest(ApiUrl(path), httpMethod, false, client.User);
      if (!string.IsNullOrEmpty(key)) {
        request.AddQueryParam(PARAM_CODE, key, true);
      }
      yield return request.Request.Send();
      request.ParseJsonArray<T>(callback);
    }

    private IEnumerator InvokeApiArray<B, T>(B body, Method httpMethod = Method.POST, Action<IRestResponse<T[]>> callback = null, string path = null, QueryParams query = null) {
      var request = new ZumoRequest(ApiUrl(path), httpMethod, false, client.User);
      if (!string.IsNullOrEmpty(key)) {
        request.AddQueryParam(PARAM_CODE, key, true);
      }
      if (body != null) {
        request.AddBody(body);
      }
      yield return request.Request.Send();
      request.ParseJsonArray<T>(callback);
    }

    #endregion

    public string ApiUrl(string path = null) {
      string customRoute = string.IsNullOrEmpty(path) ? "" : "/" + path;
      return string.Format("{0}/{1}/{2}{3}", client.Url, apiPath, name, customRoute);
    }
  }
}

using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class CentralParkApiService : Singleton<CentralParkApiService>
{
    private string scheme = "http";
    private string domain = "central-park-api.herokuapp.com";

    public string GetScheme()
    {
        return this.scheme;
    }

    public void SetScheme(string scheme)
    {
        this.scheme = scheme;
    }

    public string GetDomain()
    {
        return this.domain;
    }

    public void SetDomain(string domain)
    {
        this.domain = domain;
    }


    public HTTPRequest AddUser(string username, int score, Action<ApiUserResponse> successCallback, Action<Exception> errorCallback)
    {
        string path = "/user";

        Dictionary<string, string> postData = new Dictionary<string, string>();

        postData.Add("username", username);
        postData.Add("score", score.ToString());

        return this.DoRequest(HTTPMethods.Post, path, successCallback, errorCallback, postData);
    }

    public HTTPRequest GetLeaderboard(int limit, Action<ApiLeaderboardResponse> onSuccess, Action<Exception> onError)
    {
        string path = string.Format("/leaderboard?limit={0}", limit);

        return this.DoRequest(HTTPMethods.Get, path, onSuccess, onError);
    }

    private HTTPRequest DoRequest<T>(HTTPMethods method, string path, Action<T> onSuccess, Action<Exception> onError, Dictionary<string, string> postData = null)
    {
        HTTPRequest rawRequest = this.CreateRequest(method, path, onSuccess, onError);

        if (postData != null) {
            foreach (var item in postData) {
                rawRequest.AddField(item.Key, item.Value);
            }
        }

        rawRequest.Send();

        return rawRequest;
    }

    private HTTPRequest CreateRequest<T>(HTTPMethods method, string path, Action<T> onSuccess, Action<Exception> onError)
    {
        string url = "http://" + this.domain + (path[0] == '/' ? "" : "/") + path;

        //Logger.MessageFormat("Sending HTTP request to: {0}", url);

        HTTPRequest rawRequest = new HTTPRequest(new Uri(url), delegate (HTTPRequest request, HTTPResponse response) {
            if (response == null) {
                Debug.LogErrorFormat("Empty HTTP response from: {0}", request.Uri.PathAndQuery);

                if (onError != null) {
                    onError(new Exception("Unable to connect to server"));
                }

                return;
            }

            if (!response.IsSuccess) {
                Debug.LogErrorFormat("Failed HTTP request with code {0} from: {1}", response.StatusCode, request.Uri.PathAndQuery);

                if (onError != null) {
                    onError(new Exception(response.Message));
                }

                return;
            }

            string data = response.DataAsText;
            T decodedData = default(T);

            //Logger.MessageFormat("HTTP Response: {0}", data);

            try {
                decodedData = JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception e) {
                Debug.LogErrorFormat("Unable to parse JSON from {0} response: {1}...", request.Uri.PathAndQuery, e);
            }

            if (decodedData == null) {
                Debug.LogErrorFormat("Invalid HTTP response '{0}' from: {1}", request.Uri.PathAndQuery, data);

                if (onError != null) {
                    onError(new Exception("We encountered an unknown issue, please try again later"));
                }

                return;
            }

            ApiResponse basicData = decodedData as ApiResponse;

            if (!basicData.success) {
                ApiErrorResponse errorData = JsonConvert.DeserializeObject<ApiErrorResponse>(data);

                Debug.LogErrorFormat("Failed HTTP request with API error '{0}' from: {1}", errorData.err, request.Uri.PathAndQuery);

                if (onError != null) {
                    onError(new Exception(errorData.err));
                }

                return;
            }

            if (onSuccess != null) {
                onSuccess(decodedData);
            }
        });

        rawRequest.MethodType = method;

        return rawRequest;
    }

}

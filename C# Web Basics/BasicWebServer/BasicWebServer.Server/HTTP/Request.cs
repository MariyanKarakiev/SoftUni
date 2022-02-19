﻿using BasicWebServer.Server.Common;
using BasicWebServer.Server.HTTP.Cookies;
using BasicWebServer.Server.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicWebServer.Server.HTTP
{
    public class Request
    {
        private static Dictionary<string, Session> Sessions = new();
        public string Body { get; private set; }
        public string Url { get; private set; }
        public Method Method { get; private set; }
        public Session Session { get; private set; }
        public HeaderCollection Headers { get; private set; }
        public CookieCollection Cookies { get; private set; }
        public IReadOnlyDictionary<string, string> Form { get; private set; }
        public IReadOnlyDictionary<string, string> Query { get; private set; }
        public static IServiceCollection ServiceCollection { get; private set; }

        public static Request Parse(string request, IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;

            var lines = request.Split("\r\n");
            var firstLine = lines.First().Split(" ");

            var method = ParseMethod(firstLine[0]);

            (string url, Dictionary<string, string> query) = ParseUrl(firstLine[1]);

            var headers = ParseHeaders(lines.Skip(1));
            var cookies = ParseCookies(headers);
            var session = GetSession(cookies);

            var bodyLines = lines.Skip(headers.Count + 2).ToArray();

            var body = string.Join("\r\n", bodyLines);

            var form = ParseForm(headers, body);


            return new Request
            {
                Method = method,
                Url = url,
                Headers = headers,
                Cookies = cookies,
                Session = session,
                Body = body,
                Form = form,
                Query = query
            };
        }

        private static (string url, Dictionary<string, string> query) ParseUrl(string queryString)
        {
            string url = String.Empty;
            Dictionary<string, string> query = new Dictionary<string, string>();

            var parts = queryString.Split("?", 2);

            if (parts.Length > 1)
            {
                var queryParams = parts[1].Split("&");

                foreach (var pair in queryParams)
                {
                    var param = pair.Split('=');

                    if (param.Length == 2)
                    {
                        query.Add(param[0], param[1]);
                    }
                }
            }

            url = parts[0];

            return (url, query);
        }

        private static Session GetSession(CookieCollection cookies)
        {
            var sessionId = cookies.Contains(Session.SessionCookieName)
                   ? cookies[Session.SessionCookieName]
                   : Guid.NewGuid().ToString();

            if (!Sessions.ContainsKey(sessionId))
            {
                Sessions[sessionId] = new Session(sessionId);
            }
            return Sessions[sessionId];
        }

        private static CookieCollection ParseCookies(HeaderCollection headers)
        {
            var cookieCollection = new CookieCollection();

            if (headers.Contains(Header.Cookie))
            {
                var cookieHeader = headers[Header.Cookie];

                var allCookies = cookieHeader.Split(';');

                foreach (var cookieText in allCookies)
                {
                    var cookieParts = cookieText.Split('=');

                    var cookieName = cookieParts[0].Trim();
                    var cookieValue = cookieParts[1].Trim();

                    cookieCollection.Add(cookieName, cookieValue);
                }
            }

            return cookieCollection;
        }

        public static HeaderCollection ParseHeaders(IEnumerable<string> lines)
        {
            var headerCollection = new HeaderCollection();

            foreach (var line in lines)
            {
                if (line == String.Empty)
                {
                    break;
                }

                var headersParts = line.Split(':', 2);

                if (headersParts.Length != 2)
                {
                    throw new InvalidOperationException("Request headers is not valid");
                }

                headerCollection.Add(headersParts[0], headersParts[1].Trim());
            }
            return headerCollection;
        }

        public static Method ParseMethod(string method)
        {
            try
            {
                return (Method)Enum.Parse(typeof(Method), method, true);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Method '{method}' is not supported");
            }
        }

        public static Dictionary<string, string> ParseForm(HeaderCollection headers, string body)
        {
            var formCollection = new Dictionary<string, string>();

            if (headers.Contains(Header.ContentType)
                && headers[Header.ContentType] == ContentType.FormUrlEncoded)
            {
                var parsedResult = ParseFormData(body);

                foreach (var (name, value) in parsedResult)
                {
                    formCollection.Add(name, value);
                }
            }

            return formCollection;
        }

        private static Dictionary<string, string> ParseFormData(string bodyLines)
         => HttpUtility.UrlDecode(bodyLines)
             .Split('&')
             .Select(part => part.Split('='))
             .Where(part => part.Length == 2)
             .ToDictionary(
             part => part[0],
             part => part[1],
             StringComparer.InvariantCultureIgnoreCase);


    }
}

using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;

namespace HelpDesk.Helpers;

public static class LinkHelper
{
    private static readonly Regex UrlRegex = new(
        @"https?://[^\s<]+",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static IHtmlContent Linkify(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return HtmlString.Empty;
        }

        int currentPosition = 0;
        var result = new System.Text.StringBuilder();

        foreach (Match match in UrlRegex.Matches(text))
        {
            // Safely encode the normal text before the URL.
            string precedingText = text[currentPosition..match.Index];
            result.Append(WebUtility.HtmlEncode(precedingText));

            string url = match.Value;

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) &&
                (uri.Scheme == Uri.UriSchemeHttp ||
                 uri.Scheme == Uri.UriSchemeHttps))
            {
                string safeUrl = WebUtility.HtmlEncode(url);

                result.Append(
                    $"\"{safeUrl}\"{safeUrl}</a>");
            }
            else
            {
                result.Append(WebUtility.HtmlEncode(url));
            }

            currentPosition = match.Index + match.Length;
        }

        // Encode any remaining text.
        result.Append(WebUtility.HtmlEncode(text[currentPosition..]));

        return new HtmlString(result.ToString());
    }
}
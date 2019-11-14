// ***********************************************************************
// Assembly         : Epi.Libraries.Social.Tags
// Author           : Jeroen Stemerdink
// Created          : 2019-08-05
// //
// Last Modified By : Jeroen Stemerdink
// Last Modified On : 2019-08-05
// ***********************************************************************
// <copyright file="TwitterTags.cs" company="Jeroen Stemerdink">
//      Copyright © 2019 Jeroen Stemerdink.
//      Permission is hereby granted, free of charge, to any person
//      obtaining a copy of this software and associated documentation
//      files (the "Software"), to deal in the Software without
//      restriction, including without limitation the rights to use,
//      copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the
//      Software is furnished to do so, subject to the following
//      conditions:
//      The above copyright notice and this permission notice shall be
//      included in all copies or substantial portions of the Software.
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//      EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//      OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//      NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//      HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//      WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//      FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//      OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Epi.Libraries.Social.Tags
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Boilerplate.Web.Mvc.Twitter;

    using EPiServer.Commerce.Catalog.ContentTypes;
    using EPiServer.Core;
    using EPiServer.Logging;

    /// <summary>
    /// Class TwitterData.
    /// </summary>
    public static class TwitterTags
    {
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Log = LogManager.GetLogger();

        /// <summary>
        /// Renders the Twitter header tags for <see cref="ITwitterTags"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString SummaryLargeImageTwitterCard(this HtmlHelper htmlHelper, ITwitterTags content)
        {
            SummaryLargeImageTwitterCard twitterContent = content.ToSummaryLargeImageTwitterCard();

            return twitterContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.TwitterCard(twitterCard: twitterContent);
        }

        /// <summary>
        /// Renders the Twitter header tags for <see cref="EntryContentBase" />
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="IHtmlString" />.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString SummaryLargeImageTwitterCard(
            this HtmlHelper htmlHelper,
            EntryContentBase content,
            string twitterUsername)
        {
            return htmlHelper.TwitterCard(content.ToSummaryLargeImageTwitterCard(twitterUsername: twitterUsername));
        }

        /// <summary>
        /// Renders the Twitter header tags for <see cref="NodeContent" />
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="IHtmlString" />.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString SummaryLargeImageTwitterCard(
            this HtmlHelper htmlHelper,
            NodeContent content,
            string twitterUsername)
        {
            return htmlHelper.TwitterCard(content.ToSummaryLargeImageTwitterCard(twitterUsername: twitterUsername));
        }

        /// <summary>
        /// Renders the Twitter header tags for <see cref="ITwitterTags"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString SummaryTwitterCard(this HtmlHelper htmlHelper, ITwitterTags content)
        {
            SummaryTwitterCard twitterContent = content.ToSummaryTwitterCard();
            return twitterContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.TwitterCard(twitterCard: twitterContent);
        }

        /// <summary>
        /// Renders the Twitter header tags for <see cref="EntryContentBase" />
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="IHtmlString" />.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString SummaryTwitterCard(
            this HtmlHelper htmlHelper,
            EntryContentBase content,
            string twitterUsername)
        {
            return htmlHelper.TwitterCard(content.ToSummaryTwitterCard(twitterUsername: twitterUsername));
        }

        /// <summary>
        /// Renders the Twitter header tags for <see cref="NodeContent" />
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="IHtmlString" />.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString SummaryTwitterCard(
            this HtmlHelper htmlHelper,
            NodeContent content,
            string twitterUsername)
        {
            return htmlHelper.TwitterCard(content.ToSummaryTwitterCard(twitterUsername: twitterUsername));
        }

        /// <summary>
        /// Converts the <param name="content"></param> to a <see cref="Boilerplate.Web.Mvc.Twitter.SummaryLargeImageTwitterCard"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.Twitter.SummaryLargeImageTwitterCard"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static SummaryLargeImageTwitterCard ToSummaryLargeImageTwitterCard(this ITwitterTags content)
        {
            string imageUrl = content.TeaserImage.GetUrl();

            if (!string.IsNullOrWhiteSpace(value: imageUrl))
            {
                return new SummaryLargeImageTwitterCard(username: content.TwitterUsername)
                           {
                               Description = content.Description ?? string.Empty,
                               Image = new TwitterImage(imageUrl: imageUrl),
                               Title = content.DisplayName
                           };
            }

            Log.Debug($"[Social Tags] Page with id '{content.ContentLink.ID}' has no teaser image defined.");
            return null;
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.Twitter.SummaryLargeImageTwitterCard" />.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.Twitter.SummaryLargeImageTwitterCard" /> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static SummaryLargeImageTwitterCard ToSummaryLargeImageTwitterCard(
            this EntryContentBase content,
            string twitterUsername)
        {
            return new SummaryLargeImageTwitterCard(username: twitterUsername)
                       {
                           Description = content.SeoInformation.Description ?? string.Empty,
                           Image = new TwitterImage(content.GetDefaultAsset<IContentMedia>()),
                           Title = content.DisplayName
                       };
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.Twitter.SummaryLargeImageTwitterCard" />.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.Twitter.SummaryLargeImageTwitterCard" /> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static SummaryLargeImageTwitterCard ToSummaryLargeImageTwitterCard(
            this NodeContent content,
            string twitterUsername)
        {
            return new SummaryLargeImageTwitterCard(username: twitterUsername)
                       {
                           Description = content.SeoInformation.Description ?? string.Empty,
                           Image = new TwitterImage(content.GetDefaultAsset<IContentMedia>()),
                           Title = content.DisplayName
                       };
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.Twitter.SummaryTwitterCard"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.Twitter.SummaryTwitterCard"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static SummaryTwitterCard ToSummaryTwitterCard(this ITwitterTags content)
        {
            string imageUrl = content.TeaserImage.GetUrl();

            if (!string.IsNullOrWhiteSpace(value: imageUrl))
            {
                return new SummaryTwitterCard(username: content.TwitterUsername)
                           {
                               Description = content.Description ?? string.Empty,
                               Image = new TwitterImage(imageUrl: imageUrl),
                               Title = content.DisplayName
                           };
            }

            Log.Debug($"[Social Tags] Page with id '{content.ContentLink.ID}' has no teaser image defined.");
            return null;
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.Twitter.SummaryTwitterCard" />.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.Twitter.SummaryTwitterCard" /> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static SummaryTwitterCard ToSummaryTwitterCard(this EntryContentBase content, string twitterUsername)
        {
            return new SummaryTwitterCard(username: twitterUsername)
                       {
                           Description = content.SeoInformation.Description ?? string.Empty,
                           Image = new TwitterImage(content.GetDefaultAsset<IContentMedia>()),
                           Title = content.DisplayName
                       };
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.Twitter.SummaryTwitterCard" />.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="twitterUsername">The twitter username.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.Twitter.SummaryTwitterCard" /> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static SummaryTwitterCard ToSummaryTwitterCard(this NodeContent content, string twitterUsername)
        {
            return new SummaryTwitterCard(username: twitterUsername)
                       {
                           Description = content.SeoInformation.Description ?? string.Empty,
                           Image = new TwitterImage(content.GetDefaultAsset<IContentMedia>()),
                           Title = content.DisplayName
                       };
        }
    }
}
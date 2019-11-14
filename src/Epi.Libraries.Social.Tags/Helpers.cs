// ***********************************************************************
// Assembly         : Epi.Libraries.Social.Tags
// Author           : Jeroen Stemerdink
// Created          : 2019-08-05
// //
// Last Modified By : Jeroen Stemerdink
// Last Modified On : 2019-08-05
// ***********************************************************************
// <copyright file="Helpers.cs" company="Jeroen Stemerdink">
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
    using System.Linq;
    using System.Text;
    using System.Web;

    using EPiServer;
    using EPiServer.Commerce.Catalog;
    using EPiServer.Commerce.Catalog.ContentTypes;
    using EPiServer.Commerce.Catalog.Linking;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Routing;

    using Mediachase.Commerce;
    using Mediachase.Commerce.Catalog;
    using Mediachase.Commerce.Pricing;

    /// <summary>
    /// Class Helpers.
    /// </summary>
    internal static class Helpers
    {
        /// <summary>
        /// The asset URL resolver
        /// </summary>
        private static readonly Lazy<AssetUrlResolver> AssetUrlResolver = new Lazy<AssetUrlResolver>(() => ServiceLocator.Current.GetInstance<AssetUrlResolver>());

        /// <summary>
        /// The current market
        /// </summary>
        private static readonly Lazy<ICurrentMarket> CurrentMarket = new Lazy<ICurrentMarket>(() => ServiceLocator.Current.GetInstance<ICurrentMarket>());

        /// <summary>
        /// The price service
        /// </summary>
        private static readonly Lazy<IPriceService> PriceService = new Lazy<IPriceService>(() => ServiceLocator.Current.GetInstance<IPriceService>());

        /// <summary>
        /// The relation repository
        /// </summary>
        private static readonly Lazy<IRelationRepository> RelationRepository = new Lazy<IRelationRepository>(() => ServiceLocator.Current.GetInstance<IRelationRepository>());

        /// <summary>
        /// The URL resolver
        /// </summary>
        private static readonly Lazy<UrlResolver> UrlResolver = new Lazy<UrlResolver>(() => ServiceLocator.Current.GetInstance<UrlResolver>());

        /// <summary>
        /// Gets the default asset url.
        /// </summary>
        /// <typeparam name="TContentMedia">The type of the content media.</typeparam>
        /// <param name="assetContainer">The asset container.</param>
        /// <returns>The Url for the default asset.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetDefaultAsset<TContentMedia>(this IAssetContainer assetContainer)
            where TContentMedia : IContentMedia
        {
            string url = AssetUrlResolver.Value.GetAssetUrl<TContentMedia>(assetContainer: assetContainer);
           
            UrlBuilder urlBuilder = new UrlBuilder(UrlResolver.Value.GetUrl(internalUrl: url));

            Global.UrlRewriteProvider.ConvertToExternal(url: urlBuilder, null, toEncoding: Encoding.UTF8);

            string externalUrl = HttpContext.Current == null
                                     ? UriSupport.AbsoluteUrlBySettings(urlBuilder.ToString())
                                     : $"{HttpContext.Current.Request.Url.GetLeftPart(part: UriPartial.Authority)}{urlBuilder}";
            return externalUrl;
        }

        /// <summary>
        /// Gets the default price.
        /// </summary>
        /// <param name="variationContent">Content of the variation.</param>
        /// <returns>The default price value.</returns>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static IPriceValue GetDefaultPrice(this VariationContent variationContent)
        {
            return PriceService.Value.GetDefaultPrice(
                market: CurrentMarket.Value.GetCurrentMarket().MarketId,
                validOn: DateTime.Now,
                new CatalogKey(catalogEntryCode: variationContent.Code),
                currency: CurrentMarket.Value.GetCurrentMarket().DefaultCurrency);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="language">The language.</param>
        /// <returns>The Url for the <param name="entry"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetUrl(this EntryContentBase entry, string language)
        {
            return GetUrl(
                entry: entry,
                relationRepository: RelationRepository.Value,
                urlResolver: UrlResolver.Value,
                language: language);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>The Url for the <param name="entry"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetUrl(this EntryContentBase entry)
        {
            return GetUrl(
                entry: entry,
                relationRepository: RelationRepository.Value,
                urlResolver: UrlResolver.Value);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The Url for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetUrl(this IContent content)
        {
            return GetUrl(content: content, null);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="language">The language.</param>
        /// <returns>The Url for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetUrl(this IContent content, string language)
        {
            return GetUrl(contentReference: content.ContentLink, language: language);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="contentReference">The content reference.</param>
        /// <returns>The Url for the <param name="contentReference"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetUrl(this ContentReference contentReference)
        {
            return GetUrl(contentReference: contentReference, null);
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="contentReference">The content reference.</param>
        /// <param name="language">The language.</param>
        /// <returns>The Url for the <param name="contentReference"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        internal static string GetUrl(this ContentReference contentReference, string language)
        {
            if (ContentReference.IsNullOrEmpty(contentLink: contentReference))
            {
                return string.Empty;
            }

            UrlBuilder urlBuilder = string.IsNullOrEmpty(value: language)
                                        ? new UrlBuilder(UrlResolver.Value.GetUrl(contentLink: contentReference))
                                        : new UrlBuilder(
                                            UrlResolver.Value.GetUrl(
                                                contentLink: contentReference,
                                                language: language));

            Global.UrlRewriteProvider.ConvertToExternal(url: urlBuilder, null, toEncoding: Encoding.UTF8);

            string externalUrl = HttpContext.Current == null
                                     ? UriSupport.AbsoluteUrlBySettings(urlBuilder.ToString())
                                     : $"{HttpContext.Current.Request.Url.GetLeftPart(part: UriPartial.Authority)}{urlBuilder}";
            return externalUrl;
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="relationRepository">The relation repository.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <returns>The Url for the <param name="entry"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        internal static string GetUrl(
            this EntryContentBase entry,
            IRelationRepository relationRepository,
            UrlResolver urlResolver) =>
            GetUrl(entry: entry, relationRepository: relationRepository, urlResolver: urlResolver, null);

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="relationRepository">The relation repository.</param>
        /// <param name="urlResolver">The URL resolver.</param>
        /// <param name="language">The language.</param>
        /// <returns>The Url for the <param name="entry"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        internal static string GetUrl(
            this EntryContentBase entry,
            IRelationRepository relationRepository,
            UrlResolver urlResolver,
            string language)
        {
            ContentReference parentLink = entry.GetParentProducts(relationRepository: relationRepository).FirstOrDefault();

            ContentReference productLink = entry is VariationContent
                                               ? entry.GetParentProducts(relationRepository: relationRepository)
                                                   .FirstOrDefault() ?? entry.ContentLink
                                               : entry.ContentLink;

            UrlBuilder urlBuilder = string.IsNullOrEmpty(value: language)
                                        ? new UrlBuilder(urlResolver.GetUrl(contentLink: productLink))
                                        : new UrlBuilder(
                                            urlResolver.GetUrl(contentLink: productLink, language: language));

            if (parentLink != null && entry.Code != null)
            {
                urlBuilder.QueryCollection.Add("variationCode", value: entry.Code);
            }

            Global.UrlRewriteProvider.ConvertToExternal(url: urlBuilder, null, toEncoding: Encoding.UTF8);

            string externalUrl = HttpContext.Current == null
                                     ? UriSupport.AbsoluteUrlBySettings(urlBuilder.ToString())
                                     : $"{HttpContext.Current.Request.Url.GetLeftPart(part: UriPartial.Authority)}{urlBuilder}";
            return externalUrl;
        }
    }
}
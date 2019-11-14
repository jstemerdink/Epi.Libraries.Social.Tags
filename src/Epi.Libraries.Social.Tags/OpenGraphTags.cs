// ***********************************************************************
// Assembly         : Epi.Libraries.Social.Tags
// Author           : Jeroen Stemerdink
// Created          : 2019-08-05
// //
// Last Modified By : Jeroen Stemerdink
// Last Modified On : 2019-08-05
// ***********************************************************************
// <copyright file="OpenGraphTags.cs" company="Jeroen Stemerdink">
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
// ***********************************************************************

namespace Epi.Libraries.Social.Tags
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Boilerplate.Web.Mvc.OpenGraph;

    using EPiServer;
    using EPiServer.Commerce.Catalog.ContentTypes;
    using EPiServer.Core;
    using EPiServer.Logging;
    using EPiServer.ServiceLocation;
    using EPiServer.Web;

    using Mediachase.Commerce;
    using Mediachase.Commerce.Catalog;
    using Mediachase.Commerce.Pricing;

    /// <summary>
    /// Class OpenGraphData.
    /// </summary>
    public static class OpenGraphTags
    {
        /// <summary>
        /// The content loader
        /// </summary>
        private static readonly Lazy<IContentLoader> ContentLoader = new Lazy<IContentLoader>(() => ServiceLocator.Current.GetInstance<IContentLoader>());

        /// <summary>
        /// The current market
        /// </summary>
        private static readonly Lazy<ICurrentMarket> CurrentMarket = new Lazy<ICurrentMarket>(() => ServiceLocator.Current.GetInstance<ICurrentMarket>());

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Log = LogManager.GetLogger();

        /// <summary>
        /// The price service
        /// </summary>
        private static readonly Lazy<IPriceService> PriceService = new Lazy<IPriceService>(() => ServiceLocator.Current.GetInstance<IPriceService>());

        /// <summary>
        /// Renders the OpenGraph header tags for an article.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphArticle(this HtmlHelper htmlHelper, ISocialMediaTags content)
        {
            OpenGraphArticle openGraphContent = content.ToOpenGraphArticle();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for <see cref="ProductContent"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphProduct(this HtmlHelper htmlHelper, ProductContent content)
        {
            OpenGraphProduct openGraphContent = content.ToOpenGraphProduct();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for <see cref="NodeContent"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphProductGroup(this HtmlHelper htmlHelper, NodeContent content)
        {
            OpenGraphProductGroup openGraphContent = content.ToOpenGraphProductGroup();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for <see cref="BundleContent"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphProductGroup(this HtmlHelper htmlHelper, BundleContent content)
        {
            OpenGraphProductGroup openGraphContent = content.ToOpenGraphProductGroup();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for <see cref="PackageContent"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphProductItem(this HtmlHelper htmlHelper, PackageContent content)
        {
            OpenGraphProductItem openGraphContent = content.ToOpenGraphProductItem();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for <see cref="VariationContent"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphProductItem(this HtmlHelper htmlHelper, VariationContent content)
        {
            OpenGraphProductItem openGraphContent = content.ToOpenGraphProductItem();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for <see cref="ProductContent"/>
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.ArgumentNullException">The variants are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static IHtmlString OpenGraphProductItem(this HtmlHelper htmlHelper, ProductContent content)
        {
            OpenGraphProductItem openGraphContent = content.ToOpenGraphProductItem();

            return openGraphContent == null
                       ? MvcHtmlString.Empty
                       : htmlHelper.OpenGraph(openGraphMetadata: openGraphContent);
        }

        /// <summary>
        /// Renders the OpenGraph header tags for the website
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        public static IHtmlString OpenGraphWebsite(this HtmlHelper htmlHelper, string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(value: imageUrl))
            {
                return htmlHelper.OpenGraph(
                    new OpenGraphWebsite(
                        title: SiteDefinition.Current.Name,
                        new OpenGraphImage(imageUrl: imageUrl),
                        SiteDefinition.Current.SiteUrl.ToString()));
            }

            Log.Debug("[Social Tags] Website has no teaser image defined.");
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphArticle"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphArticle"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphArticle ToOpenGraphArticle(this ISocialMediaTags content)
        {
            string imageUrl = content.TeaserImage.GetUrl();

            if (!string.IsNullOrWhiteSpace(value: imageUrl))
            {
                return new OpenGraphArticle(
                    !string.IsNullOrWhiteSpace(value: content.DisplayName) ? content.DisplayName : content.Name,
                    new OpenGraphImage(imageUrl: imageUrl),
                    content.GetUrl());
            }

            Log.Debug($"[Social Tags] Page with id '{content.ContentLink.ID}' has no teaser image defined.");
            return null;
        }

        /// <summary>
        ///  Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProduct"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProduct"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProduct ToOpenGraphProduct(this ProductContent content)
        {
            return new OpenGraphProduct(
                title: content.DisplayName,
                new OpenGraphImage(content.GetDefaultAsset<IContentMedia>()),
                content.GetUrl());
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductGroup"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductGroup"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductGroup ToOpenGraphProductGroup(this NodeContent content)
        {
            return new OpenGraphProductGroup(
                title: content.DisplayName,
                new OpenGraphImage(content.GetDefaultAsset<IContentMedia>()),
                content.GetUrl());
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductGroup"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductGroup"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductGroup ToOpenGraphProductGroup(this BundleContent content)
        {
            return new OpenGraphProductGroup(
                title: content.DisplayName,
                new OpenGraphImage(content.GetDefaultAsset<IContentMedia>()),
                content.GetUrl());
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductItem ToOpenGraphProductItem(this VariationContent content)
        {
            return content.ToOpenGraphProductItem(openGraphCondition: OpenGraphCondition.New);
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductItem ToOpenGraphProductItem(this PackageContent content)
        {
            return content.ToOpenGraphProductItem(openGraphCondition: OpenGraphCondition.New);
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.ArgumentNullException">The variants are <see langword="null" />.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductItem ToOpenGraphProductItem(this ProductContent content)
        {
            ContentReference variantReference = content.GetVariants().FirstOrDefault();

            VariationContent variation;

            if (ContentLoader.Value.TryGet(contentLink: variantReference, content: out variation))
            {
                return variation.ToOpenGraphProductItem(openGraphCondition: OpenGraphCondition.New);
            }

            Log.Debug($"[Social Tags] Product with id '{content.ContentLink.ID}' has no variations.");
            return null;
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="openGraphCondition">The open graph condition.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductItem ToOpenGraphProductItem(
            this VariationContent content,
            OpenGraphCondition openGraphCondition)
        {
            return new OpenGraphProductItem(
                title: content.DisplayName,
                new OpenGraphImage(content.GetDefaultAsset<IContentMedia>()),
                content.GetOpenGraphAvailability(),
                condition: openGraphCondition,
                content.GetOpenGraphCurrencies(),
                retailerItemId: content.Code,
                content.GetUrl());
        }

        /// <summary>
        /// Converts the <param name="content"></param> to an <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="openGraphCondition">The open graph condition.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphProductItem"/> for the <param name="content"></param>.</returns>
        /// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
        /// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Uri" /> instance is not an absolute instance.</exception>
        /// <exception cref="T:System.ArgumentException">The specified <see cref="UriPartial.Authority"/> is not valid.</exception>
        /// <exception cref="T:System.ArgumentNullException">Parent products are <see langword="null" />.</exception>
        /// <exception cref="T:System.NotSupportedException">The query collection is read-only.</exception>
        /// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing.</exception>
        /// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor.</exception>
        public static OpenGraphProductItem ToOpenGraphProductItem(
            this PackageContent content,
            OpenGraphCondition openGraphCondition)
        {
            return new OpenGraphProductItem(
                title: content.DisplayName,
                new OpenGraphImage(content.GetDefaultAsset<IContentMedia>()),
                content.GetOpenGraphAvailability(),
                condition: openGraphCondition,
                content.GetOpenGraphCurrencies(),
                retailerItemId: content.Code,
                content.GetUrl());
        }

        /// <summary>
        /// Gets the <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphAvailability"/> for the <param name="variationContent"></param>.
        /// </summary>
        /// <param name="variationContent">Content of the variation.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphAvailability"/> for the <param name="variationContent"></param>.</returns>
        private static OpenGraphAvailability GetOpenGraphAvailability(this VariationContent variationContent)
        {
            return variationContent.GetDefaultPrice() != null
                       ? OpenGraphAvailability.InStock
                       : OpenGraphAvailability.OutOfStock;
        }

        /// <summary>
        /// Gets the <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphAvailability"/> for the <param name="packageContent"></param>.
        /// </summary>
        /// <param name="packageContent">Content of the package.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphAvailability"/> for the <param name="packageContent"></param>.</returns>
        private static OpenGraphAvailability GetOpenGraphAvailability(this PackageContent packageContent)
        {
            return packageContent.GetDefaultPrice() != null
                       ? OpenGraphAvailability.InStock
                       : OpenGraphAvailability.OutOfStock;
        }

        /// <summary>
        /// Gets the <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphCurrency"/> list for the <param name="variationContent"></param>.
        /// </summary>
        /// <param name="variationContent">Content of the variation.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphCurrency"/> list for the <param name="variationContent"></param>.</returns>
        private static IEnumerable<OpenGraphCurrency> GetOpenGraphCurrencies(this VariationContent variationContent)
        {
            return PriceService.Value.GetPrices(
                market: CurrentMarket.Value.GetCurrentMarket().MarketId,
                validOn: DateTime.Now,
                new CatalogKey(catalogEntryCode: variationContent.Code),
                new PriceFilter()).GetOpenGraphCurrencies();
        }

        /// <summary>
        /// Gets the <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphCurrency"/> list for the <param name="packageContent"></param>.
        /// </summary>
        /// <param name="packageContent">Content of the package.</param>
        /// <returns>The <see cref="Boilerplate.Web.Mvc.OpenGraph.OpenGraphCurrency"/> list for the <param name="packageContent"></param>.</returns>
        private static IEnumerable<OpenGraphCurrency> GetOpenGraphCurrencies(this PackageContent packageContent)
        {
            return PriceService.Value.GetPrices(
                market: CurrentMarket.Value.GetCurrentMarket().MarketId,
                validOn: DateTime.Now,
                new CatalogKey(catalogEntryCode: packageContent.Code),
                new PriceFilter()).GetOpenGraphCurrencies();
        }

        /// <summary>
        /// Converts the <see cref="IEnumerable{IPriceValue}"/> to an <see cref="IEnumerable{OpenGraphCurrency}"/>.
        /// </summary>
        /// <param name="priceValues">The price values.</param>
        /// <returns>An <see cref="IEnumerable{OpenGraphCurrency}"/>.</returns>
        private static IEnumerable<OpenGraphCurrency> GetOpenGraphCurrencies(this IEnumerable<IPriceValue> priceValues)
        {
            return priceValues.Select(
                priceValue => new OpenGraphCurrency(
                    decimal.ToDouble(d: priceValue.UnitPrice.Amount),
                    currency: priceValue.UnitPrice.Currency.CurrencyCode));
        }
    }
}
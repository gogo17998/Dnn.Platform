﻿#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;

namespace Dnn.DynamicContent
{
    public class DynamicContentTypeController : ControllerBase<DynamicContentType, IDynamicContentTypeController, DynamicContentTypeController>, IDynamicContentTypeController
    {
        internal const string StructuredWhereClause = "WHERE PortalID = @0 AND IsStructured = 1";

        protected override Func<IDynamicContentTypeController> GetFactory()
        {
            return () => new DynamicContentTypeController();
        }

        public DynamicContentTypeController() : this(DotNetNuke.Data.DataContext.Instance()) { }

        public DynamicContentTypeController(IDataContext dataContext) : base(dataContext) { }

        /// <summary>
        /// Adds the type of the content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>content type id.</returns>
        /// <exception cref="System.ArgumentNullException">content type is null.</exception>
        /// <exception cref="System.ArgumentException">contentType.ContentType is empty.</exception>
        public int AddContentType(DynamicContentType contentType)
        {
            //Argument Contract
            Requires.PropertyNotNullOrEmpty(contentType, "ContentType");

            Add(contentType);

            //Save Field Definitions
            foreach (var definition in contentType.FieldDefinitions)
            {
                definition.ContentTypeId = contentType.ContentTypeId;
                FieldDefinitionController.Instance.AddFieldDefinition(definition);
            }

            //Save Content Templates
            foreach (var template in contentType.Templates)
            {
                template.ContentTypeId = contentType.ContentTypeId;
                ContentTemplateController.Instance.AddContentTemplate(template);
            }

            return contentType.ContentTypeId;
        }

        /// <summary>
        /// Deletes the type of the content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <exception cref="System.ArgumentNullException">content type is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">content type id is less than 0.</exception>
        public void DeleteContentType(DynamicContentType contentType)
        {
            Delete(contentType);

            //Delete Field Definitions
            foreach (var definition in contentType.FieldDefinitions)
            {
                FieldDefinitionController.Instance.DeleteFieldDefinition(definition);
            }

            //Delete Content Templates
            foreach (var template in contentType.Templates)
            {
                ContentTemplateController.Instance.DeleteContentTemplate(template);
            }
        }

        /// <summary>
        /// Gets the content types for a specific portal.
        /// </summary>
        /// <param name="portalId">The portalId</param>
        /// <returns>content type collection.</returns>
        public IQueryable<DynamicContentType> GetContentTypes(int portalId)
        {
            IQueryable<DynamicContentType> contentTypes;
            using (DataContext)
            {
                var rep = DataContext.GetRepository<DynamicContentType>();

                contentTypes = rep.Get(portalId).AsQueryable();
            }

            return contentTypes;
        }

        /// <summary>
        /// Gets a page of content types for a specific portal.
        /// </summary>
        /// <param name="portalId">The portalId</param>
        /// <param name="pageIndex">The page index to return</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>content type collection.</returns>
        public IPagedList<DynamicContentType> GetContentTypes(int portalId, int pageIndex, int pageSize)
        {
            IPagedList<DynamicContentType> contentTypes;
            using (DataContext)
            {
                var rep = DataContext.GetRepository<DynamicContentType>();

                contentTypes = rep.GetPage(portalId, pageIndex, pageSize);
            }

            return contentTypes;
        }

        /// <summary>
        /// Updates the type of the content.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <exception cref="System.ArgumentNullException">content type is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">content type id is less than 0.</exception>
        /// <exception cref="System.ArgumentException">contentType.ContentType is empty.</exception>
        public void UpdateContentType(DynamicContentType contentType)
        {
            //Argument Contract
            Requires.PropertyNotNullOrEmpty(contentType, "ContentType");

            Update(contentType);

            //Save Field Definitions
            foreach (var definition in contentType.FieldDefinitions)
            {
                if (definition.FieldDefinitionId == -1)
                {
                    definition.ContentTypeId = contentType.ContentTypeId;
                    FieldDefinitionController.Instance.AddFieldDefinition(definition);
                }
                else
                {
                    FieldDefinitionController.Instance.UpdateFieldDefinition(definition);
                }
            }

            //Save Content Templates
            foreach (var template in contentType.Templates)
            {
                if (template.TemplateId == -1)
                {
                    template.ContentTypeId = contentType.ContentTypeId;
                    ContentTemplateController.Instance.AddContentTemplate(template);
                }
                else
                {
                    ContentTemplateController.Instance.UpdateContentTemplate(template);
                }
            }

        }
    }
}

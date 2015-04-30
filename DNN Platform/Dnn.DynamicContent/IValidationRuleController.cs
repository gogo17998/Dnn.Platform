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

using System.Collections.Generic;
using System.Linq;

namespace Dnn.DynamicContent
{
    public interface IValidationRuleController
    {
        /// <summary>
        /// Adds a new rule for use with Structured(Dynamic) Content Types.
        /// </summary>
        /// <param name="rule">The rule to add.</param>
        /// <returns>rule id.</returns>
        /// <exception cref="System.ArgumentNullException">rule is null.</exception>
        int AddValidationRule(ValidationRule rule);

        /// <summary>
        /// Deletes the rule for use with Structured(Dynamic) Content Types.
        /// </summary>
        /// <param name="rule">The rule to delete.</param>
        /// <exception cref="System.ArgumentNullException">rule is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">rule id is less than 0.</exception>
        void DeleteValidationRule(ValidationRule rule);

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <param name="fieldDefinitionId">The Id of the parent Field Definition</param>
        /// <returns>rule collection.</returns>
        IQueryable<ValidationRule> GetValidationRules(int fieldDefinitionId);

        /// <summary>
        /// Gets the settings for a validation rule.
        /// </summary>
        /// <param name="validationRuleId">The Id of the parent Validation Rule</param>
        /// <returns>setting dictionary.</returns>
        IDictionary<string, ValidatorSetting> GetValidationSettings(int validationRuleId);

        /// <summary>
        /// Updates the rule.
        /// </summary>
        /// <param name="rule">The rule to update.</param>
        /// <exception cref="System.ArgumentNullException">rule is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">rule id is less than 0.</exception>
        void UpdateValidationRule(ValidationRule rule);
    }
}

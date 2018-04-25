using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustERP.MetronicTable.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace JustERP.MetronicTable.ModelBinder
{
    public class MetronicModelBinder : ComplexTypeModelBinder
    {
        public MetronicModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinders) : base(propertyBinders)
        {
        }

        protected override Task BindProperty(ModelBindingContext bindingContext)
        {
            if (!string.IsNullOrWhiteSpace(bindingContext.BinderModelName))
                return base.BindProperty(bindingContext);

            var modelName = bindingContext.HttpContext
                .Request
                .Query
                .Keys
                .SingleOrDefault(q => q.ToLower() == $"query[{bindingContext.ModelMetadata.PropertyName.ToLower()}]");

            if (string.IsNullOrWhiteSpace(modelName) ||
                bindingContext.HttpContext.Request.Query[modelName].All(string.IsNullOrWhiteSpace))
                return base.BindProperty(bindingContext);

            bindingContext.ModelName = modelName;
            bindingContext.FieldName = modelName;
            bindingContext.BindingSource = BindingSource.Query;
            bindingContext.BinderModelName = modelName;

            return base.BindProperty(bindingContext);
        }
    }

    public class MetronicModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (!context.Metadata.ModelType.IsSubclassOf(typeof(MetronicPagedResultRequestDto)))
                return null;
            Dictionary<ModelMetadata, IModelBinder> dictionary = new Dictionary<ModelMetadata, IModelBinder>();
            foreach (ModelMetadata property in context.Metadata.Properties)
            {
                dictionary.Add(property, context.CreateBinder(property));
            }
            return new MetronicModelBinder(dictionary);
        }
    }
}

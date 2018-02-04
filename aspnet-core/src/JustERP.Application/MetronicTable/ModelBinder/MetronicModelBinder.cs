using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JustERP.MetronicTable.ModelBinder
{
    public class MetronicModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //ComplexTypeModelBinder
            var modelCreator = ((Expression<Func<object>>)(() => Expression.New(bindingContext.ModelType))).Compile();
            bindingContext.Model = modelCreator();
            foreach (var property in bindingContext.ModelMetadata.Properties)
            {
                var modelName = property.BinderModelName ?? $"query[{property.PropertyName.ToLower()}]";
                var value = bindingContext.ValueProvider.GetValue(modelName);
                property.PropertySetter(bindingContext.Model, value.FirstValue);
            }
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}

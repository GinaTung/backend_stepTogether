using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Annotations;

public class CustomTagDescriptions : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // 收集 Controller 上的 SwaggerTag
        var controllerTags = context.ApiDescriptions
            .Select(desc => desc.ActionDescriptor as ControllerActionDescriptor)
            .Where(desc => desc != null)
            .GroupBy(desc => desc.ControllerName)
            .ToDictionary(
                g => g.Key,
                g =>
                {
                    var tagAttribute = g.First().ControllerTypeInfo.GetCustomAttribute<SwaggerTagAttribute>();
                    return tagAttribute?.Description ?? g.Key;
                });

        // 重新設定 Tags 顯示名稱
        swaggerDoc.Tags = controllerTags.Select(tag =>
            new OpenApiTag
            {
                Name = tag.Key,
                Description = tag.Value
            }).ToList();
    }
}

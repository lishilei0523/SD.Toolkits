using System.Collections.Generic;

namespace SD.Toolkits.WebApi.Models
{
    /// <summary>
    /// Http请求文件列表
    /// </summary>
    public sealed class FormFileCollection : List<IFormFile>, IFormFileCollection
    {

    }
}

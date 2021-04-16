namespace SD.Toolkits.WebApi.Models
{
    /// <summary>
    /// Http请求文件
    /// </summary>
    internal sealed class FormFile : IFormFile
    {
        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 内容长度
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// 内容处置
        /// </summary>
        public string ContentDisposition { get; set; }

        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] Datas { get; set; }
    }
}

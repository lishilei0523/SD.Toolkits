using Xamarin.Essentials;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.MonoAndroid
{
    /// <summary>
    /// 条码扫描设置
    /// </summary>
    public static class BarcodeSetting
    {
        /// <summary>
        /// 条码广播字段名称
        /// </summary>
        public static string BarcodeActionName
        {
            get => SecureStorage.GetAsync(AndroidConstants.BarcodeActionName).Result;
            set => SecureStorage.SetAsync(AndroidConstants.BarcodeActionName, value).Wait();
        }

        /// <summary>
        /// 条码数据字段名称
        /// </summary>
        public static string BarcodeExtraName
        {
            get => SecureStorage.GetAsync(AndroidConstants.BarcodeExtraName).Result;
            set => SecureStorage.SetAsync(AndroidConstants.BarcodeExtraName, value).Wait();
        }
    }
}

using Xamarin.Essentials;

// ReSharper disable once CheckNamespace
namespace SD.Toolkits.Xamarin
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
            get => Preferences.Get(AndroidConstants.BarcodeActionName, null);
            set => Preferences.Set(AndroidConstants.BarcodeActionName, value);
        }

        /// <summary>
        /// 条码数据字段名称
        /// </summary>
        public static string BarcodeExtraName
        {
            get => Preferences.Get(AndroidConstants.BarcodeExtraName, null);
            set => Preferences.Set(AndroidConstants.BarcodeExtraName, value);
        }
    }
}

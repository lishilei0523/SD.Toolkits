// ReSharper disable once CheckNamespace
namespace System.ServiceModel.NetCore
{
    /// <summary>
    /// WCF扩展工具类
    /// </summary>
    public static class ServiceModelExtension
    {
        /// <summary>
        /// 关闭信道扩展方法
        /// </summary>
        /// <param name="channel">信道实例</param>
        public static void CloseChannel(this object channel)
        {
            if (!(channel is ICommunicationObject communicationObject))
            {
                return;
            }
            try
            {
                if (communicationObject.State == CommunicationState.Faulted)
                {
                    communicationObject.Abort();
                }
                else
                {
                    communicationObject.Close();
                }
            }
            catch (TimeoutException)
            {
                communicationObject.Abort();
            }
            catch (CommunicationException)
            {
                communicationObject.Abort();
            }
            catch (Exception)
            {
                communicationObject.Abort();
                throw;
            }
        }
    }
}
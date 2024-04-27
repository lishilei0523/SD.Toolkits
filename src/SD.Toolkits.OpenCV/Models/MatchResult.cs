using OpenCvSharp;

namespace SD.Toolkits.OpenCV.Models
{
    /// <summary>
    /// 匹配结果
    /// </summary>
    public class MatchResult
    {
        #region # 构造器

        #region 00.无参构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        private MatchResult() { }
        #endregion

        #region 01.创建匹配结果构造器
        /// <summary>
        /// 创建匹配结果构造器
        /// </summary>
        /// <param name="matchedSourceKeyPoints">匹配的源关键点列表</param>
        /// <param name="matchedTargetKeyPoints">匹配的目标关键点列表</param>
        public MatchResult(KeyPoint[] matchedSourceKeyPoints, KeyPoint[] matchedTargetKeyPoints)
            : this()
        {
            this.MatchedSourceKeyPoints = matchedSourceKeyPoints;
            this.MatchedTargetKeyPoints = matchedTargetKeyPoints;
        }
        #endregion 

        #endregion

        #region # 属性

        #region 匹配的源关键点列表 —— KeyPoint[] MatchedSourceKeyPoints
        /// <summary>
        /// 匹配的源关键点列表
        /// </summary>
        public KeyPoint[] MatchedSourceKeyPoints { get; private set; }
        #endregion

        #region 匹配的目标关键点列表 —— KeyPoint[] MatchedTargetKeyPoints
        /// <summary>
        /// 匹配的目标关键点列表
        /// </summary>
        public KeyPoint[] MatchedTargetKeyPoints { get; private set; }
        #endregion 

        #endregion
    }
}

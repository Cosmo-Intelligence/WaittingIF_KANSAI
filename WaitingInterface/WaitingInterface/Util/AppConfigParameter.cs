
namespace WaitingInterface.Util
{
    /// <summary>
    /// アプリケーションコンフィグ定義
    /// </summary>
    public sealed class AppConfigParameter
    {
        #region 共通

        /// <summary>
        /// USER接続文字列
        /// </summary>
        public static string USER_Conn = "USER_ConnectionString";

        /// <summary>
        /// ログフォルダ保持期間(日数)
        /// </summary>
        public static string LogKeepDays = "LogKeepDays";

        /// <summary>
        /// 定周期実行フラグ 0：1回実行 1：定周期実行
        /// </summary>
        public static string ThreadLoopFlg = "ThreadLoopFlg";

        /// <summary>
        /// スレッド待機時間(ミリ秒)
        /// </summary>
        public static string ThreadInterval = "ThreadInterval";

        /// <summary>
        /// CSV出力フォルダ
        /// </summary>
        public static string CSVFolder = "CSVFolder";

        /// <summary>
        /// CSVファイル名
        /// </summary>
        public static string CSVFile = "CSVFile";

        /// <summary>
        /// 検査進捗
        /// </summary>
        public static string KensaStatus = "KensaStatus";

        /// <summary>
        /// 検査種別
        /// </summary>
        public static string KensaType = "KensaType";

        #endregion

    }
}

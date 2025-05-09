using System;
using System.Data;
using System.Reflection;
using WaitingInterface.Data.Export.Common;
using WaitingInterface.Util;

namespace WaitingInterface.Data.Export
{
    /// <summary>
    /// 連携データ処理
    /// </summary>
    class LinkageData
    {

        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 検査進捗
        /// </summary>
        private static string kensa_status =
                       AppConfigController.GetInstance().GetValueString(AppConfigParameter.KensaStatus).Replace(" ", "").Replace(",", "','");

        /// <summary>
        /// 検査種別
        /// </summary>
        private static string kensa_type =
                       AppConfigController.GetInstance().GetValueString(AppConfigParameter.KensaType).Replace(" ", "").Replace(",", "','");

        /// <summary>
        /// 検査室（出力対象外）
        /// </summary>
        private static string kensaSitu_NotIn =
        AppConfigController.GetInstance().GetValueString(AppConfigParameter.KensaSitu_NotIn).Replace(" ", "").Replace(",", "','");

        #endregion


        #region メソッド、ファンクション

        /// <summary>
        /// 連携対象データ取得
        /// </summary>
        /// <param name="linkageDt"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Select(ref DataTable linkageDt, OracleDataBase db)
        {
            try
            {
                string sql = ConstQuery.DATA_SELECT;
                
                // 出力対象外の検査室：なし
                if(kensaSitu_NotIn == "0")
                {
                    // SQL実行
                    db.GetDataTable(sql, ref linkageDt, "'" + kensa_status + "'", "'" + kensa_type + "'");
                }
                // 出力対象外の検査室：あり
                else
                {
                    // 抽出条件に出力対象外検査室を追加
                    sql += ConstQuery.KENSASITU_ID_NOT_IN;
                    // SQL実行
                    db.GetDataTable(sql, ref linkageDt, "'" + kensa_status + "'", "'" + kensa_type + "'", "'" + kensaSitu_NotIn + "'");
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
            return true;
        }

        #endregion
    }
}

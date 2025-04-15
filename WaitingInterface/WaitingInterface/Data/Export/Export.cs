using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using WaitingInterface.Data.Export.Entity;
using WaitingInterface.Util;

namespace WaitingInterface.Data.Export
{
    class Export
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 出力ファイル文字コードを取得
        /// </summary>
        private static Encoding OutputEnocode = CommonParameter.CommonEnocode;


        /// <summary>
        /// CSV出力ファイル名
        /// </summary>
        private static string csvFile = AppConfigController.GetInstance().GetValueString(AppConfigParameter.CSVFile) + ".csv";


        /// <summary>
        /// CSV出力処理
        /// </summary>
        /// <param name="outputDt">CSV出力データ</param>
        /// <param name="work">共有フォルダパス</param>
        /// <returns></returns>
        public static bool OutputCSV(DataTable outputDt, string work)
        {
            StreamWriter write = null;
            StringBuilder sb = new StringBuilder();

            // 出力件数カウント
            int dtCnt = 0;

            try
            {
                // 文字コードを指定して書き込む
                write = new StreamWriter(Path.Combine(work, csvFile), false, OutputEnocode);

                // ヘッダー作成
                sb.Append(CustomizeColumn("KJID") + ",");
                sb.Append(CustomizeColumn("SDATE") + ",");
                sb.Append(CustomizeColumn("ODNO") + ",");
                sb.Append(CustomizeColumn("ODSEQ") + ",");
                sb.Append(CustomizeColumn("IDATE") + ",");
                sb.Append(CustomizeColumn("SRNO") + ",");
                sb.Append(CustomizeColumn("RYSID") + ",");
                sb.Append(CustomizeColumn("ODRNO") + ",");
                sb.Append(CustomizeColumn("IM_NGKBN") + ",");
                sb.Append(CustomizeColumn("IM_KACD") + ",");
                sb.Append(CustomizeColumn("IM_BYTCD") + ",");
                sb.Append(CustomizeColumn("IM_BYST") + ",");
                sb.Append(CustomizeColumn("KZ_NGKBN") + ",");
                sb.Append(CustomizeColumn("KZ_KACD") + ",");
                sb.Append(CustomizeColumn("KZ_BYTCD") + ",");
                sb.Append(CustomizeColumn("KZ_BYST") + ",");
                sb.Append(CustomizeColumn("HKPNO") + ",");
                sb.Append(CustomizeColumn("XKSCD") + ",");
                sb.Append(CustomizeColumn("XKGCD") + ",");
                sb.Append(CustomizeColumn("NENKBN") + ",");
                sb.Append(CustomizeColumn("SRCD") + ",");
                sb.Append(CustomizeColumn("YW_HEYA") + ",");
                sb.Append(CustomizeColumn("YW_DATE") + ",");
                sb.Append(CustomizeColumn("YW_WSEQ") + ",");
                sb.Append(CustomizeColumn("YTIME") + ",");
                sb.Append(CustomizeColumn("SYJKN") + ",");
                sb.Append(CustomizeColumn("ODKBN") + ",");
                sb.Append(CustomizeColumn("IDCNM") + ",");
                sb.Append(CustomizeColumn("IDCCD") + ",");
                sb.Append(CustomizeColumn("KNDCD") + ",");
                sb.Append(CustomizeColumn("JIDCD") + ",");
                sb.Append(CustomizeColumn("GISCD") + ",");
                sb.Append(CustomizeColumn("UKJKBN") + ",");
                sb.Append(CustomizeColumn("UTIME") + ",");
                sb.Append(CustomizeColumn("HKFLG") + ",");
                sb.Append(CustomizeColumn("JSINF") + ",");
                sb.Append(CustomizeColumn("KTKBN") + ",");
                sb.Append(CustomizeColumn("JGFLG") + ",");
                sb.Append(CustomizeColumn("CMTCD") + ",");
                sb.Append(CustomizeColumn("RI_DATE") + ",");
                sb.Append(CustomizeColumn("RI_TIME") + ",");
                sb.Append(CustomizeColumn("RI_UKJKBN") + ",");
                sb.Append(CustomizeColumn("RI_SUKJKBN") + ",");
                sb.Append(CustomizeColumn("RI_SITSU") + ",");
                sb.Append(CustomizeColumn("ENTER") + ",");
                sb.Append(CustomizeColumn("LEAVE") + ",");
                sb.Append(CustomizeColumn("STAY") + ",");
                sb.Append(CustomizeColumn("JTIME") + ",");
                sb.Append(CustomizeColumn("FILLER") + ",");
                sb.Append(CustomizeColumn("ACCESSION_NO") + ",");

                // CSVファイルに書き出し
                write.WriteLine(sb.ToString());

                // 解放処理
                sb.Remove(0, sb.Length);

                // 取得データの件数分出力
                foreach (DataRow dtRow in outputDt.Rows)
                {
                    try
                    {
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KANJA_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KENSA_DATE].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.ORDERNO].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.ODSEQ) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.HIS_HAKKO_DATE].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.RECEIPTNUMBER].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.RYSID) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.ORDERNO].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.DENPYO_NYUGAIKBN].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.IRAI_SECTION_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.DENPYO_BYOUTOU_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.DENPYO_BYOSITU_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KANJA_NYUGAIKBN].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.SECTION_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.BYOUTOU_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.BYOUSITU_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.ADDENDUM02].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KENSATYPE_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.XKGCD) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.NENKBN) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.SRCD) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KENSASITU_ID].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KENSA_DATE].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.YW_WSEQ) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.KENSA_STARTTIME].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.SYJKN) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.ODKBN) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.IRAI_DOCTOR_NAME].ToString()) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.IRAI_DOCTOR_NO].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.KNDCD) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.JIDCD) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.GISCD) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.UKJKBN) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.RECEIPTDATE].ToString()) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.HKFLG) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.JSINF) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.KTKBN) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.JGFLG) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.CMTCD) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.RI_DATE) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.RI_TIME) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.RI_UKJKBN) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.RI_SUKJKBN) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.RI_SITSU) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.ENTER) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.LEAVE) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.STAY) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.JTIME) + ",");
                        sb.Append(CustomizeColumn(LinkageDataEntity.FILLER) + ",");
                        sb.Append(CustomizeColumn(dtRow[LinkageDataEntity.ACCESSIONNO].ToString()) + ",");

                        // 出力件数カウントインクリメント
                        dtCnt++;

                        // CSVファイルに書き出し
                        write.WriteLine(sb.ToString());

                        // 解放処理
                        sb.Remove(0, sb.Length);
                    }
                    catch (Exception ex)
                    {
                        _log.Warn(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
            finally
            {
                if (write != null)
                {
                    // 解放処理
                    write.Close();
                }
            }
            _log.Debug(csvFile + " 連携データ出力件数：" + dtCnt + "件");
            return true;
        }


        /// <summary>
        /// ダブルコーテーションを付ける
        /// </summary>
        /// <param name="column"></param>
        /// <returns>カスタマイズしたカラム名</returns>
        private static string CustomizeColumn(string column)
        {
            column = @"""" + column + @"""";
            return column;
        }

    }
}

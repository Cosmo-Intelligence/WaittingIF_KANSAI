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
                sb.Append("KJID" + ",");
                sb.Append("SDATE" + ",");
                sb.Append("ODNO" + ",");
                sb.Append("ODSEQ" + ",");
                sb.Append("IDATE" + ",");
                sb.Append("SRNO" + ",");
                sb.Append("RYSID" + ",");
                sb.Append("ODRNO" + ",");
                sb.Append("IM_NGKBN" + ",");
                sb.Append("IM_KACD" + ",");
                sb.Append("IM_BYTCD" + ",");
                sb.Append("IM_BYST" + ",");
                sb.Append("KZ_NGKBN" + ",");
                sb.Append("KZ_KACD" + ",");
                sb.Append("KZ_BYTCD" + ",");
                sb.Append("KZ_BYST" + ",");
                sb.Append("HKPNO" + ",");
                sb.Append("XKSCD" + ",");
                sb.Append("XKGCD" + ",");
                sb.Append("NENKBN" + ",");
                sb.Append("SRCD" + ",");
                sb.Append("YW_HEYA" + ",");
                sb.Append("YW_DATE" + ",");
                sb.Append("YW_WSEQ" + ",");
                sb.Append("YTIME" + ",");
                sb.Append("SYJKN" + ",");
                sb.Append("ODKBN" + ",");
                sb.Append("IDCNM" + ",");
                sb.Append("IDCCD" + ",");
                sb.Append("KNDCD" + ",");
                sb.Append("JIDCD" + ",");
                sb.Append("GISCD" + ",");
                sb.Append("UKJKBN" + ",");
                sb.Append("UTIME" + ",");
                sb.Append("HKFLG" + ",");
                sb.Append("JSINF" + ",");
                sb.Append("KTKBN" + ",");
                sb.Append("JGFLG" + ",");
                sb.Append("CMTCD" + ",");
                sb.Append("RI_DATE" + ",");
                sb.Append("RI_TIME" + ",");
                sb.Append("RI_UKJKBN" + ",");
                sb.Append("RI_SUKJKBN" + ",");
                sb.Append("RI_SITSU" + ",");
                sb.Append("ENTER" + ",");
                sb.Append("LEAVE" + ",");
                sb.Append("STAY" + ",");
                sb.Append("JTIME" + ",");
                sb.Append("FILLER" + ",");
                sb.Append("ACCESSION_NO" + ",");
                sb.Append("XCODE");

                // CSVファイルに書き出し
                write.WriteLine(sb.ToString());

                // 解放処理
                sb.Remove(0, sb.Length);

                // 取得データの件数分出力
                foreach (DataRow dtRow in outputDt.Rows)
                {
                    try
                    {
                        sb.Append(dtRow[LinkageDataEntity.KANJA_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.KENSA_DATE].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.ORDERNO].ToString() + ",");
                        sb.Append((LinkageDataEntity.ODSEQ) + ",");
                        sb.Append(dtRow[LinkageDataEntity.HIS_HAKKO_DATE].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.RECEIPTNUMBER].ToString() + ",");
                        sb.Append((LinkageDataEntity.RYSID) + ",");
                        sb.Append(dtRow[LinkageDataEntity.ORDERNO].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.DENPYO_NYUGAIKBN].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.IRAI_SECTION_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.DENPYO_BYOUTOU_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.DENPYO_BYOSITU_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.KANJA_NYUGAIKBN].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.SECTION_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.BYOUTOU_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.BYOUSITU_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.ADDENDUM02].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.KENSATYPE_ID].ToString() + ",");
                        sb.Append((LinkageDataEntity.XKGCD) + ",");
                        sb.Append((LinkageDataEntity.NENKBN) + ",");
                        sb.Append((LinkageDataEntity.SRCD) + ",");
                        sb.Append(dtRow[LinkageDataEntity.KENSASITU_ID].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.KENSA_DATE].ToString() + ",");
                        sb.Append((LinkageDataEntity.YW_WSEQ) + ",");
                        sb.Append(dtRow[LinkageDataEntity.KENSA_STARTTIME].ToString() + ",");
                        sb.Append((LinkageDataEntity.SYJKN) + ",");
                        sb.Append((LinkageDataEntity.ODKBN) + ",");
                        sb.Append(dtRow[LinkageDataEntity.IRAI_DOCTOR_NAME].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.IRAI_DOCTOR_NO].ToString() + ",");
                        sb.Append((LinkageDataEntity.KNDCD) + ",");
                        sb.Append((LinkageDataEntity.JIDCD) + ",");
                        sb.Append((LinkageDataEntity.GISCD) + ",");
                        sb.Append((LinkageDataEntity.UKJKBN) + ",");
                        sb.Append(dtRow[LinkageDataEntity.RECEIPTDATE].ToString() + ",");
                        sb.Append((LinkageDataEntity.HKFLG) + ",");
                        sb.Append((LinkageDataEntity.JSINF) + ",");
                        sb.Append((LinkageDataEntity.KTKBN) + ",");
                        sb.Append((LinkageDataEntity.JGFLG) + ",");
                        sb.Append((LinkageDataEntity.CMTCD) + ",");
                        sb.Append((LinkageDataEntity.RI_DATE) + ",");
                        sb.Append((LinkageDataEntity.RI_TIME) + ",");
                        sb.Append((LinkageDataEntity.RI_UKJKBN) + ",");
                        sb.Append((LinkageDataEntity.RI_SUKJKBN) + ",");
                        sb.Append((LinkageDataEntity.RI_SITSU) + ",");
                        sb.Append((LinkageDataEntity.ENTER) + ",");
                        sb.Append((LinkageDataEntity.LEAVE) + ",");
                        sb.Append((LinkageDataEntity.STAY) + ",");
                        sb.Append((LinkageDataEntity.JTIME) + ",");
                        sb.Append((LinkageDataEntity.FILLER) + ",");
                        sb.Append(dtRow[LinkageDataEntity.ACCESSIONNO].ToString() + ",");
                        sb.Append(dtRow[LinkageDataEntity.BUI_ID].ToString());

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
        //private static string CustomizeColumn(string column)
        //{
        //    column = @"""" + column + @"""";
        //    return column;
        //}

    }
}

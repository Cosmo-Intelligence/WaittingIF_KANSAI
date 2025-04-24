using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using WaitingInterface.Ctrl;
using WaitingInterface.Util;

namespace WaitingInterface
{
    class Program
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// スレッド
        /// </summary>
        // private Thread thread = null;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            // 二重起動にならないか確認する
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                _log.Error("アプリケーションを多重起動しようとした為、アプリケーションを強制終了します。");
                //処理を終了する
                return;
            }

            Program proc = new Program();
            proc.StartApplication();
        }

        /// <summary>
        /// 起動処理
        /// </summary>
        private void StartApplication()
        {
            _log.Info("アプリケーションを起動します。");

            try
            {
                // WaitingInterface.exe.config読込み
                Hashtable appConfigTable = new Hashtable();
                if (!CreateAppConfigParameter(appConfigTable))
                {
                    return;
                }

                AppConfigController.GetInstance().SetEAppConfigTableImpl(appConfigTable);

                _log.Info("待合機能データ連携処理を開始します。");

                // スレッド処理へ
                WaitingInterfaceController waitingInterface = new WaitingInterfaceController();
                // thread = new Thread(waitingInterface.LinkageThread);
                // thread.Start();
                waitingInterface.LinkageThread();

            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                return;
            }
            finally
            {
                _log.Info("アプリケーションを終了します。");
            }
        }

        /// <summary>
        /// 設定値をHashtableに保存
        /// </summary>
        /// <param name="param">設定ファイルテーブル</param>
        /// <returns>false : 不正</returns>
        private bool CreateAppConfigParameter(Hashtable table)
        {
            // アプリ必須項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.USER_Conn, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.ThreadLoopFlg, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.ThreadInterval, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.KensaStatus, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.KensaType, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.CSVFolder, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.CSVFile, table)) { return false; }

            // アプリ項目
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.LogKeepDays, table)) { return false; }

            return true;
        }
    }
}

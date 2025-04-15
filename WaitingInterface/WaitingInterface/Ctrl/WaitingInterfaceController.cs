using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WaitingInterface.Data.Export;
using WaitingInterface.Util;

namespace WaitingInterface.Ctrl
{
    class WaitingInterfaceController
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///  定周期実行フラグ
        /// </summary>
        private int loopflg =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.ThreadLoopFlg));

        /// <summary>
        /// 処理待機時間(単位 : ミリ秒)
        /// </summary>
        private int interval =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.ThreadInterval));

        /// <summary>
        /// USER接続文字列を取得
        /// </summary>
        private static string userConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.USER_Conn);

        /// <summary>
        /// 出力パス
        /// </summary>
        private static string todayWork = string.Empty;

        /// <summary>
        /// システム日付
        /// </summary>
        private static DateTime today = DateTime.Now;

        /// <summary>
        /// ログ出力日付フォルダ
        /// </summary>
        private static string dateFolder = today.ToString("yyyyMMdd");

        #endregion

        #region public

        /// <summary>
        /// AP終了指示フラグ(true : 終了指示)
        /// </summary>
        public bool appStopOrder = false;

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 待合機能データ連携スレッド
        /// </summary>
        public void LinkageThread()
        {
            _log.Info("初期処理を実行します。");

            // 初期処理
            if (!this.Init())
            {
                throw new Exception("初期処理でエラーが発生しました。");
            }

            // 終了指示があるか判定
            while (!this.appStopOrder)
            {
                _log.Debug("ループを開始します。");

                // 制御処理
                this.Controller();

                // 定周期実行フラグが0：１回の場合は終了
                if (this.loopflg == 0)
                {
                    break;
                }

                _log.Debug("待機処理に入ります。");
                // 待機処理
                this.WaitApplication();
            }

            // APを終了する
            this.ExitApplication();
        }

        /// <summary>
        /// 制御処理
        /// </summary>
        public void Controller()
        {
            // ユーザ DBクラス
            OracleDataBase db = null;

            // 待合機能データ連携用データ
            DataTable linkageDt = new DataTable();

            try
            {
                // ユーザ インスタンス生成
                db = new OracleDataBase(userConn);

                _log.Info("待合機能連携データ取得処理を実行します。");

                // 連携データ取得処理
                if (!LinkageData.Select(ref linkageDt, db))
                {
                    throw new Exception("待合機能連携データ取得でエラーが発生しました。");
                }

                // データ出力用共有フォルダの取得
                string folder = AppConfigController.GetInstance().GetValueString(AppConfigParameter.CSVFolder);
                todayWork = folder + @"\";

                _log.Debug("連携データファイル出力処理を実行します。（出力対象：" + linkageDt.Rows.Count + "件）");

                // 連携データファイル出力処理
                if (!Export.OutputCSV(linkageDt, todayWork))
                {
                    throw new Exception("連携データファイル出力でエラーが発生しました。");
                }
                _log.Debug("ファイル出力処理が正常に完了しました。");

                // 終了指示があるか判定
                if (this.appStopOrder)
                {
                    _log.Debug("終了指示が出されました。");
                    // 終了を中断
                    return;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            finally
            {
                // 破棄
                linkageDt.Clear();
                GC.Collect();
            }
        }


        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        private bool Init()
        {
            // ログフォルダ削除
            Logger.Delete();

            //システム日付フォルダのパス取得
            string csvFolder = AppConfigController.GetInstance().GetValueString(AppConfigParameter.CSVFolder);
            todayWork = csvFolder;

            // 連携データ出力フォルダ存在チェック
            if (!Directory.Exists(todayWork))
            {
                try
                {
                    // 存在しない場合は作成する
                    Directory.CreateDirectory(todayWork);
                    _log.DebugFormat("連携データファイル出力フォルダを作成しました。：{0}", todayWork);
                }
                catch (Exception ex)
                {
                    // フォルダが作成でエラーが発生した場合
                    _log.Error(ex.Message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// スレッド待機処理
        /// </summary>
        private void WaitApplication()
        {
            _log.Debug("スレッド待機処理を開始します。");

            // 現在日時にスレッド待機時間を加算し、スレッド待機日時を取得
            DateTime sleepDateTime = DateTime.Now.AddMilliseconds(interval);

            _log.DebugFormat("現在日時 : {0}、スレッド待機日時 : {1}", DateTime.Now, sleepDateTime);

            // 現在日時をスレッド待機日時が上回っているか判定
            while (DateTime.Now < sleepDateTime)
            {
                // 終了指示があるか判定
                if (this.appStopOrder)
                {
                    // 現在日時とスレッド待機日時を比較しているループを終了
                    break;
                }
                // スレッドを1秒間待機
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// アプリケーション終了処理
        /// </summary>
        private void ExitApplication()
        {
            _log.Info("待合機能データ連携処理を終了します。");

            // アプリケーションの終了
            Application.Exit();
        }

        #endregion
    }
}

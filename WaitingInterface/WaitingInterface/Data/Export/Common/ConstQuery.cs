
namespace WaitingInterface.Data.Export.Common
{
    class ConstQuery
    {
        #region QUEUEテーブル

        /// <summary>
        /// 連携対象データ取得
        /// </summary>
        public const string DATA_SELECT =
                  " select"
                + "  EXMAINTABLE.kanja_id "
                + " ,EXMAINTABLE.kensa_date "
                + " ,ORDERMAINTABLE.orderno "
                + " ,TO_CHAR(ExtendOrderInfo.HIS_HAKKO_DATE ,'YYYYMMDD') as HIS_HAKKO_DATE"
                + " ,ExMainTable.ReceiptNumber"
                + " ,OrderMainTable.Denpyo_Nyugaikbn"
                + " ,OrderMainTable.Irai_Section_ID"
                + " ,OrderMainTable.Denpyo_Byoutou_ID"
                + " ,OrderMainTable.DENPYO_BYOSITU_ID"
                + " ,PatientInfo.Kanja_NyugaiKbn"
                + " ,PatientInfo.Section_ID"
                + " ,PatientInfo.Byoutou_ID"
                + " ,PatientInfo.Byousitu_ID"
                + " ,EXTENDORDERINFO.ADDENDUM02"
                + " ,OrderMainTable.KensaType_ID"
                + " ,ExMainTable.Kensasitu_ID"
                + " ,OrderMainTable.Kensa_StartTime"
                + " ,OrderMainTable.Irai_Doctor_Name"
                + " ,OrderMainTable.Irai_Doctor_No"
                + " ,ExMainTable.ReceiptDate"
                + " ,OrderMainTable.AccessionNo"
                + " ,ORDERBUITABLE.BUI_ID"
                + " from"
                + " EXMAINTABLE"
                + " left join ORDERMAINTABLE on"
                + " EXMAINTABLE.RIS_ID = ORDERMAINTABLE.RIS_ID"
                + " left join EXTENDEXAMINFO on"
                + " EXMAINTABLE.RIS_ID = EXTENDEXAMINFO.RIS_ID"
                + " left join EXTENDORDERINFO on"
                + " EXMAINTABLE.RIS_ID = EXTENDORDERINFO.RIS_ID"
                + " left join PATIENTINFO on"
                + " EXMAINTABLE.KANJA_ID = PATIENTINFO.KANJA_ID"
                + " left join ORDERBUITABLE on"
                + " EXMAINTABLE.RIS_ID = ORDERBUITABLE.RIS_ID"
                + " where"
                + " EXMAINTABLE.KENSA_DATE = TO_CHAR(SYSDATE, 'YYYYMMDD')"
                + " and "
                + " EXMAINTABLE.Status in ({0})"
                + " and"
                + " EXMAINTABLE.KENSATYPE_ID in ({1})";

        #endregion

    }
}

using System;
using Common.Utilities;
using Common.Securities;
using System.Text.RegularExpressions;


namespace Entity
{
    public class SystemConfigEntity
    {
        public string DbSetting { get; set; }
        public DatabaseConfigInfoEntity DbSettingInfo {
            get {
                return GetDbConfig(DbSetting);
            }
        }
        public string DbPurchase { get; set; }
        public DatabaseConfigInfoEntity DbPurchaseInfo
        {
            get
            {
                return GetDbConfig(DbPurchase);
            }
        }
        public string DbSales { get; set; }
        public DatabaseConfigInfoEntity DbSalesInfo
        {
            get
            {
                return GetDbConfig(DbSales);
            }
        }
        public string DbWms { get; set; }
        public DatabaseConfigInfoEntity DbWmsInfo
        {
            get
            {
                return GetDbConfig(DbWms);
            }
        }
        public string DbInventory { get; set; }
        public DatabaseConfigInfoEntity DbInventoryInfo
        {
            get
            {
                return GetDbConfig(DbInventory);
            }
        }
        public string DbTransportation { get; set; }
        public DatabaseConfigInfoEntity DbTransportationInfo
        {
            get
            {
                return GetDbConfig(DbTransportation);
            }
        }
        public string DbProduction { get; set; }
        public DatabaseConfigInfoEntity DbProductionInfo
        {
            get
            {
                return GetDbConfig(DbProduction);
            }
        }
        public string DbPlanning { get; set; }
        public DatabaseConfigInfoEntity DbPlanningInfo
        {
            get
            {
                return GetDbConfig(DbPlanning);
            }
        }


        private DatabaseConfigInfoEntity GetDbConfig(string DbConfig) {
            DatabaseConfigInfoEntity ret = new DatabaseConfigInfoEntity();

            //UserFacade _u = new UserFacade();

            string[] tmp = Regex.Split(DbConfig, "###");
            if (tmp.Length == 3) {
                ret.ServerName = tmp[0];
                ret.DbUserID = tmp[1];
                ret.DbPssWd = StringCipher.DeCripTxt(tmp[2]);
            }

            return ret;
        }
    }

    public class DatabaseConfigInfoEntity {
        public string ServerName { get; set; }
        public string DbUserID { get; set; }
        public string DbPssWd { get; set; }
    }
}

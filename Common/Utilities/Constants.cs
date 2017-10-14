﻿using Common.Resources;

///<summary>
/// Class Name : Constants
/// Purpose    : -
/// Author     : Neda Peyrone
///</summary>
///<remarks>
/// Change History:
/// Date         Author           Description
/// ----         ------           -----------
///</remarks>
namespace Common.Utilities
{
    public static class Constants
    {
        public const int CompanyStartYear = 2013;
        public const string ConfigUrlPath = "~/Templates/ConfigUrl/";
        public const string NoImage50 = "~/Images/no_image_50.png";
        public const string NoImage30 = "~/Images/no_image_30.png";
        public const string PassPhrase = "gdupi9bok8bo";
        public const string UnknownFileExt = ".unknown";
        public const string NotKnown = "NA";

        public const int BatchInboundActivityTypeId = 2;
        public const int EmailInboundActivityTypeId = 6;
        public const int EmailOutboundActivityTypeId = 7;
        public const int CallCenterChannelId = 2;
        public const int DisplayMaxLength = 60;
        public const int DisplaySenderName = 35;
        public const int ActivityDescriptionMaxLength = 8000;
        public const int EmailBodyMaxLength = 8000;

        public const string DefaultSubscriptionTypeForUser = "18"; //"19";
        public const string RemarkLink = "<input id=\"remarkLink\" value=\"แสดงรายละเอียด\" class=\"btn btn-info btn-sm\" onclick=\"onlinkRemarkClick(); return false;\" type=\"button\">";

        public static class ApplicationStatus
        {
            public const string All = "";
            public const string Active = "Y";
            public const string Inactive = "N";

            public static string GetMessage(string status)
            {
                //if (status == null)
                //{
                //    return "Draft";
                //}

                if (status == Inactive)
                {
                    return Resources.Resources.Ddl_Status_Inactive;
                }

                if (status == Active)
                {
                    return Resources.Resources.Ddl_Status_Active;
                }

                if (status == All)
                {
                    return Resources.Resources.Ddl_Status_All;
                }

                return string.Empty;
            }
        }

        public static class EmployeeStatus
        {
            public const int Active = 1;
            public const int Termiated = 0;

            public static string GetMessage(short? status)
            {
                if (status.HasValue)
                {
                    if (status == Active)
                    {
                        return Resources.Resources.Emp_Status_Active;
                    }
                    if (status == Termiated)
                    {
                        return Resources.Resources.Emp_Status_Termiate;
                    }
                }

                return string.Empty;
            }
        }

        public static class ReportSRStatus
        {
            public const bool Pass = true;
            public const bool Fail = false;

            public static string GetMessage(string status)
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    if (VerifyResultStatus.Pass.Equals(status))
                    {
                        return Resources.Resources.Ddl_VerifyResult_Pass;
                    }
                    else if (VerifyResultStatus.Fail.Equals(status))
                    {
                        return Resources.Resources.Ddl_VerifyResult_Fail;
                    }
                    else
                    {
                        return Resources.Resources.Ddl_VerifyResult_Skip;
                    }
                }

                return "N/A";
            }
        }

        public static class AccountStatus
        {
            public const string Active = "A";
        }

        public static class ReportVerify
        {
            public const string Pass = VerifyResultStatus.Pass;
            public const string Fail = VerifyResultStatus.Fail;

            public static string GetMessage(string status)
            {
                if (status == Pass)
                {
                    return "ถูก";
                }
                if (status == Fail)
                {
                    return "ผิด";
                }
                return "ข้าม";
            }
        }

        public static class AuditLogStatus
        {
            public const int Success = 1;
            public const int Fail = 0;
        }

        public static class VerifyResultStatus
        {
            public const string Pass = "PASS";
            public const string Fail = "FAIL";
            public const string Skip = "SKIP";
        }

        public static class NCBCheckStatus
        {
            public const string Found = "Found";
            public const string NotFound = "Not Found";
        }

        public static class AttachFile
        {
            public const int Yes = 1;
            public const int No = 0;
        }

        public static class Module
        {
            public const string Batch = "Batch";
            public const string Authentication = "Authentication";
            public const string Customer = "Customer";
            public const string WebService = "WebService";
            public const string ServiceRequest = "ServiceRequest";
        }

        public static class AuditAction
        {
            public const string Login = "Login";
            public const string Logout = "Logout";
            public const string CreateJobs = "Create Jobs";
            public const string CloseSR = "Close SR";
            public const string ImportAFS = "Import AFS files";
            public const string CreateCommPool = "Create communication pool";
            public const string Export = "Export activity AFS";
            public const string RecommendedCampaign = "Recommended Campaign";
            public const string ExportMarketing = "Export marketing data";
            public const string ExistingLead = "Existing Lead";
            public const string ImportBDW = "Import BDW files";
            public const string ImportCIS = "Import CIS files";
            public const string ExportCIS = "Export CIS files";
            public const string ActivityLog = "Activity Log";
            public const string ImportHP = "Import HP files";
            public const string CreateProductMaster = "CreateProductMaster";
            public const string CreateBranch = "CreateBranch";

            public const string SyncSRStatusFromReplyEmail = "Sync SR Status from Reply Email";
            public const string ReSubmitActivityToCARSystem = "Re-Submit Activity to CAR System";
            public const string ReSubmitActivityToCBSHPSystem = "Re-Submit Activity to CBS-HP System";
            public const string SubmitActivityToCARSystem = "Submit Activity to CAR System";

            public const string Search = "Search";
        }

        public static class StatusType
        {
            public const string Job = "JOB";
            public const string SR = "SR";
        }

        public static class JobStatus
        {
            public const int Open = 0;
            public const int Refer = 1;
            public const int Done = 2;

            public static string GetMessage(int? status)
            {
                if (status == Open)
                {
                    return Resources.Resources.Lbl_JobStatusOpen;
                }

                if (status == Refer)
                {
                    return Resources.Resources.Lbl_JobStatusRefer;
                }

                if (status == Done)
                {
                    return Resources.Resources.Lbl_JobStatusDone;
                }

                return string.Empty;
            }
        }

        public static class CacheKey
        {
            public const string AllParameters = "CACHE_PARAMETERS"; // List of Parameters
            public const string MainMenu = "CACHE_MAINMENU";
            public const string ScreenRoles = "CACHE_SCREEN_ROLES";
            public const string CustomerTab = "CACHE_CUSTOMER_TAB";
            public const string PageSizeList = "CACHE_PAGESIZE_LIST";
        }

        public static class DateTimeFormat
        {
            public const string ShortTime = "HH:mm";
            public const string FullTime = "HH:mm:ss";
            public const string ShortDate = "dd MMM yyyy";
            public const string FullDateTime = "dd MMM yyyy HH:mm:ss";
            public const string DefaultShortDate = "dd/MM/yyyy";
            public const string DefaultFullDateTime = "dd/MM/yyyy HH:mm:ss";
            public const string CalendarShortDate = "dd-MM-yyyy";
            public const string CalendarFullDateTime = "dd-MM-yyyy HH:mm:ss";
            public const string StoreProcedureDate = "yyyy-MM-dd";
            public const string StoreProcedureDateTime = "yyyy-MM-dd HH:mm:ss";
            public const string ReportDateTime = "dd/MM/yyyy HH:mm:ss";
            public const string ExportDateTime = "yyyyMMdd_HHmm";
            public const string ExportCISDatetime = "dd-MMM-yyyy HH:mm:ss";
            public const string ExportAfsDateTime = "yyyyMMdd";
        }

        public static class ErrorCode
        {
            public const string CSM0001 = "CSM0001";      // CSM0001 *Connot connect to the system
            public const string CSM0002 = "CSM0002";      // CSM0002 *End point not found
            public const string CSM0003 = "CSM0003";      // CSM0003  Unknown Error

            public const string CSMProd001 = "001";
            public const string CSMProd002 = "002";
            public const string CSMProd003 = "003";

            public const string CSMCust001 = "001";
            public const string CSMCust002 = "002";
            public const string CSMCust003 = "003";

            public const string CSMBranch001 = "001";       //001 header bad request
            public const string CSMBranch002 = "002";       //002 body bad request
            public const string CSMBranch003 = "003";       //003 save or update error
            public const string CSMBranch004 = "004";       //004 Unknown error

            public const string CSMCalendar001 = "001";     //001 header bad request
            public const string CSMCalendar002 = "002";     //002 body bad request
            public const string CSMCalendar003 = "003";       //003 save or update error
            public const string CSMCalendar004 = "004";       //004 body invalid branch code

            public const string CSMSaveSr001 = "001"; //001 header bad request
            public const string CSMGetSr001 = "001"; //001 header bad request
            public const string CSMUpdateSr001 = "001"; //001 header bad request
            public const string CSMSearchSr001 = "001"; //001 header bad request


        }

        public static class KnownCulture
        {
            public const string EnglishUS = "en-US";
            public const string Thai = "th-TH";
        }

        public static class MailSubject
        {
            public const string NotifySyncEmailFailed = "NotifySyncEmailFailed";
            public const string NotifySyncEmailSuccess = "NotifySyncEmailSuccess";
            public const string NotifyImportAssetFailed = "NotifyImportAssetFailed";
            public const string NotifyImportAssetSuccess = "NotifyImportAssetSuccess";
            public const string NotifyExportActivityFailed = "NotifyExportActivityFailed";
            public const string NotifyExportActivitySuccess = "NotifyExportActivitySuccess";
            public const string NotifyFailExportActvity = "NotifyFailExportActvity";
            public const string NotifyImportContactFailed = "NotifyImportContactFailed";
            public const string NotifyImportContactSuccess = "NotifyImportContactSuccess";
            public const string NotifyImportCISSuccess = "NotifyImportCISSuccess";
            public const string NotifyImportCISFailed = "NotifyImportCISFailed";
            public const string NotifyImportHPSuccess = "NotifyImportHPSuccess";
            public const string NotifyImportHPFailed = "NotifyImportHPFailed";
            public const string NotifyCreateSrFromReplyEmailSuccess = "NotifyCreateSrFromReplyEmailSuccess";
            public const string NotifyCreateSrFromReplyEmailFailed = "NotifyCreateSrFromReplyEmailFailed";
            public const string NotifyReSubmitActivityToCARSystemSuccess = "NotifyReSubmitActivityToCARSystemSuccess";
            public const string NotifyReSubmitActivityToCARSystemFailed = "NotifyReSubmitActivityToCARSystemFailed";
            public const string NotifyReSubmitActivityToCBSHPSystemSuccess = "NotifyReSubmitActivityToCBSHPSystemSuccess";
            public const string NotifyReSubmitActivityToCBSHPSystemFailed = "NotifyReSubmitActivityToCBSHPSystemFailed";
        }

        public static class MaxLength
        {
            public const int CardNo = 20;
            public const int PhoneNo = 20;
            public const int Username = 50;
            public const int Password = 20;
            public const int AttachName = 100;
            public const int AttachDesc = 500;
            public const int IfRowStat = 50;
            public const int IfRowBatchNum = 50;
            public const int AssetNum = 50;
            public const int AssetType = 50;
            public const int AssetTradeInType = 50;
            public const int AssetStatus = 1;
            public const int AssetDesc = 200;
            public const int AssetName = 200;
            public const int AssetComments = 500;
            public const int AssetRefNo1 = 100;
            public const int AssetLot = 100;
            public const int AssetPurch = 100;
            public const int Amphur = 100;
            public const int Province = 100;
            public const int SaleName = 100;
            public const int EmployeeId = 10;
            public const int Email = 100; //50;
            public const int NewsContent = 8000;
            public const int Note = 1000;
            public const int PoolName = 200;
            public const int PoolDesc = 500;
            public const int ConfigName = 100;
            public const int ConfigUrl = 100;
            public const int ConfigImage = 100;
            public const int RelationshipName = 100;
            public const int RelationshipDesc = 255;
            public const int FirstName = 255; //100;
            public const int LastName = 255; //100;
            public const int RemarkCloseJob = 1000;

            #region "Import BwdContact"

            public const int BdwCardNo = 50;
            public const int BdwTitleTH = 50;
            public const int BdwNameTH = 255;
            public const int BdwSurnameTH = 255;
            public const int BdwTitleEN = 50;
            public const int BdwNameEN = 255;
            public const int BdwSurnameEN = 255;
            public const int BdwAccountNo = 100;
            public const int BdwLoanMain = 255;
            public const int BdwProductGroup = 255;
            public const int BdwProduct = 255;
            public const int BdwRelationship = 100;
            public const int BdwPhone = 255;
            public const int BdwCampaign = 255;
            public const int BdwCardTypeCode = 10;
            public const int BdwAccountStatus = 10;

            #endregion

            // import Hp Activity
            public const int Channel = 30;
            public const int Type = 30;
            public const int Area = 30;
            public const int Status = 30;
            public const int Description = 150;
            public const int Comment = 1500;
            public const int AssetInfo = 15;
            public const int ContactInfo = 15;
            public const int Ano = 40;
            public const int CallId = 30;
            public const int ContactName = 50;
            public const int ContactLastName = 50;
            public const int ContactPhone = 40;
            public const int DoneFlg = 1;
            public const int CreateDate = 20;
            public const int CreateBy = 15;
            public const int StartDate = 20;
            public const int EndDate = 20;
            public const int OwnerLogin = 50;
            public const int OwnerPerId = 1;
            public const int UpdateDate = 20;
            public const int UpdateBy = 1;
            public const int SrId = 15;
            public const int CallFlg = 1;
            public const int EnqFlg = 1;
            public const int LocEnqFlg = 1;
            public const int DocReqFlg = 1;
            public const int PriIssuedFlg = 1;
            public const int AssetInspectFlg = 1;
            public const int PlanStartDate = 20;
            public const int ContactFax = 50;
            public const int ContactEmail = 50;

            public const int MailSubject = 1000;
        }

        public static class MinLenght
        {
            public const int SearchTerm = 2;
            public const int AutoComplete = 0;
        }

        public static class CisMaxLength
        {
            public const int AddressTypeName = 50;
            public const int NameTH = 255;
            public const int NameEN = 255;
            public const int TaxId = 50;
            public const int HostBusinessCountryCode = 10;
            public const int ValuePerShare = 50;
            public const int AuthorizedShareCapital = 50;
            public const int RegisterDate = 50;
            public const int IdCountryIssue = 10;
            public const int BusinessCatCode = 10;
            public const int Stock = 50;
            public const int CountryNameTH = 255;
            public const int CountryNameEN = 255;
            public const int MailTypeCode = 10;
            public const int DistrictNameTH = 100;
            public const int DistrictNameEN = 100;
            public const int EmailTypeDesc = 100;
            public const int TitleNameCustom = 50;
            public const int FirstNameTH = 255;
            public const int MidNameTH = 255;
            public const int LastNameTH = 255;
            public const int FirstNameEN = 255;
            public const int MidNameEN = 255;
            public const int LastNameEN = 255;
            public const int BirthDate = 50;
            public const int MaritalCode = 10;
            public const int Nationality1Code = 10;
            public const int Nationality2Code = 10;
            public const int Nationality3Code = 10;
            public const int ReligionCode = 10;
            public const int EducationCode = 10;
            public const int Position = 255;
            public const int CompanyName = 255;
            public const int AnnualIncome = 50;
            public const int SourceIncome = 50;
            public const int TotalWealthPeriod = 50;
            public const int ChannelHome = 50;
            public const int AnnualIncomePeriod = 10;
            public const int OccupationCode = 10;
            public const int OccupationSubtypeCode = 10;
            public const int CountryIncome = 50;
            public const int TotalWealth = 50;
            public const int SourceIncomeRem = 100; //50;
            public const int PhoneTypeDesc = 100;
            public const int ProductCode = 50;
            public const int ProductType = 50;
            public const int ProductDesc = 100;
            public const int System = 50;
            public const int ProductFlag = 1;
            public const int SubscrDesc = 255;
            public const int ProvinceNameTH = 100;
            public const int ProvinceNameEN = 100;
            public const int SubdistrictNameTH = 100;
            public const int SubdistrictNameEN = 100;
            public const int AddressNumber = 255;
            public const int Village = 255;
            public const int Building = 255;
            public const int FloorNo = 100;
            public const int RoomNo = 100;
            public const int Moo = 100;
            public const int Street = 255;
            public const int Soi = 255;
            public const int SubDistrictValue = 255;
            public const int DistrictValue = 255;
            public const int ProvinceValue = 255;
            //public const int EmailTypeName = 100;
            public const int RefNo = 50;
            public const int Text1 = 255;
            public const int Text2 = 255;
            public const int Text3 = 255;
            public const int Text4 = 255;
            public const int Text5 = 255;
            public const int Text6 = 255;
            public const int Text7 = 255;
            public const int Text8 = 255;
            public const int Text9 = 255;
            public const int Text10 = 255;
            public const int Number1 = 50;
            public const int Number2 = 50;
            public const int Number3 = 50;
            public const int Number4 = 50;
            public const int Number5 = 50;
            public const int Date1 = 50;
            public const int Date2 = 50;
            public const int Date3 = 50;
            public const int Date4 = 50;
            public const int Date5 = 50;
            public const int SubscrStatus = 1;
            public const int CreatedChannel = 50;
            //public const int AccountNo = 50;
            public const int UpdatedChannel = 50;
            public const int BranchName = 255;
            public const int CustTypeTH = 100;
            public const int CustTypeEN = 100;
            public const int CardTypeEN = 100;
            public const int CardTypeTH = 100;
            public const int TitleNameTH = 100;
            public const int TitleNameEN = 100;
            public const int TitleTypeGroup = 10;

            public const int AddressTypeCode = 10;
            public const int Status = 1;
            public const int CardId = 50;
            public const int CustTypeCode = 10;
            public const int IsicCode = 10;
            public const int BusinessCode = 10;
            public const int FixedAsset = 50;
            public const int FixedAssetExcludeLand = 50;
            public const int NumberOfEmployee = 10;
            public const int ShareInfoFlag = 10;
            public const int FlgMstApp = 10;
            public const int FirstBranch = 50;
            public const int PlaceCustUpdated = 20;
            public const int DateCustUpdated = 20;
            public const int MarketingId = 10;
            public const int CountryCode = 10;
            public const int MailAccount = 100;
            public const int EmailTypeCode = 10;
            public const int GenderCode = 10;
            public const int KKCisId = 50;
            public const int EntityCode = 10;
            public const int PostCode = 10;
            public const int PostalValue = 10;
           
            public const int CardTypeCode = 10;
            public const int CustId = 50;
            public const int CustTypeGroup = 2;
            public const int DistrictCode = 10;
            public const int PhoneExt = 100;
            public const int PhoneNum = 100;
            public const int PhoneTypeCode = 10;
            public const int ProdGroup = 50;
            public const int ProdType = 50;
            public const int ProvinceCode = 10;
            public const int SubdistrictCode = 10;
            public const int SubscrCode = 50;
            public const int TitleId = 10;
            
            public const int CreatedDate = 20;
            public const int UpdatedDate = 20;
            public const int CreatedBy = 100;
            public const int UpdatedBy = 100;

            public const int SysCustSubscrId = 50;

        }

        public static class ParameterName
        {
            public const string AFSPathImport = "AFS_PATH_IMPORT";
            public const string AFSPathExport = "AFS_PATH_EXPORT";
            public const string AFSPathError = "AFS_PATH_ERROR";
            public const string CICPathExport = "CIC_PATH_EXPORT";
            //public const string CICPathExport = "CIC_PATH_SOURCE";
            public const string RegexFileExt = "REGEX_FILE_EXT";    // Regular Expression to validate the file extension
            //public const string RegexConfigIcon = "REGEX_CONFIG_ICON";
            public const string MaxRetrieveMail = "MAXIMUM_RETRIEVE_MAIL"; // Maximum retrieve emails by communication pool
            public const string AttachmentPathJob = "ATTACHMENT_PATH_JOB";
            public const string AttachmentPathNews = "ATTACHMENT_PATH_NEWS";
            public const string AttachmentPathCustomer = "ATTACHMENT_PATH_CUSTOMER";
            public const string AttachmentPathSr = "ATTACHMENT_PATH_SR";
            public const string ReportTimeStart = "REPORT_TIME_START";
            public const string ReportTimeEnd = "REPORT_TIME_END";
            public const string BDWPathImport = "BDW_PATH_IMPORT";
            public const string CISPathImport = "CIS_PATH_IMPORT";
            public const string CISPathError = "CIS_PATH_ERROR";
            public const string BDWPathError = "BDW_PATH_ERROR";
            public const string PageSizeStart = "PAGE_SIZE_START";
            public const string HPPathImport = "HP_PATH_IMPORT";
            public const string HPPathError = "HP_PATH_ERROR";
            public const string NumMonthsActivity = "NUM_MONTHS_ACTIVITY";
            public const string SingleFileSize = "SINGLE_FILE_SIZE";
            public const string TotalFileSize = "TOTAL_FILE_SIZE";
            public const string OfficePhoneNo = "OFFICE_PHONE_NO";
            public const string OfficeHour = "OFFICE_HOUR";
            public const string ProductGroupSubmitCBSHP = "PRODUCTGROUP_SUBMIT_CBSHP";
            public const string TextDummyAccountNo = "TEXT_DUMMY_ACCOUNT_NO";
            public const string CisPathSource = "CIS_PATH_SOURCE";
            public const string AfsPathSource = "AFS_PATH_SOURCE";
            public const string BdwPathSource = "BDW_PATH_SOURCE";
            public const string HpPathSource = "HP_PATH_SOURCE";

            public const string MaxMinuteBatchCreateSRActivityFromReplyEmail = "MAX_MINUTE_BATCH_CREATE_SR_ACTIVITY_FROM_REPLY_EMAIL";
            public const string MaxMinuteBatchReSubmitActivityToCARSystem = "MAX_MINUTE_BATCH_RESUBMIT_ACTIVITY_TO_CAR_SYSTEM";
            public const string MaxMinuteBatchReSubmitActivityToCBSHPSystem = "MAX_MINUTE_BATCH_RESUBMIT_ACTIVITY_TO_CBSHP_SYSTEM";

            public const string SLMUrlNewLead = "SLM_URL_NEW_LEAD";
            public const string SLMUrlViewLead = "SLM_URL_VIEW_LEAD";

            public const string ReportExportDate = "REPORT_EXPORT_DATE";

            public const string BatchInterval = "BATCH_INTERVAL";

            public const string CBSWebServiceProxy = "CS_WEBSERVICE_PROXY";
        }

        public static class ServiceName
        {
            public const string CampaignByCustomer = "CampaignByCustomer";
            public const string UpdateCustomerFlags = "UpdateCustomerFlags";
            public const string InsertLead = "InsertLead";
            public const string SearchLead = "SearchLead";
            public const string CreateActivityLog = "CreateActivityLog";
            public const string InquiryActivityLog = "InquiryActivityLog";
        }

        public static class ServicesNamespace
        {
            public const string MailService = "http://www.kiatnakinbank.com/services/CSM/CSMMailService";
            public const string FileService = "http://www.kiatnakinbank.com/services/CSM/CSMFileService";
            public const string MasterService = "http://www.kiatnakinbank.com/services/CSM/CSMMasterService";
            public const string BranchService = "http://www.kiatnakinbank.com/services/CSM/CSMBranchService";
            public const string UserService = "http://www.kiatnakinbank.com/services/CSM/CSMUserService";
            public const string SRService = "http://www.kiatnakinbank.com/services/CSM/CSMSRService";
            public const string CustomerService = "http://www.kiatnakinbank.com/services/CSM/CSMCustomerService";
        }

        public static class StackTraceError
        {
            public const string InnerException = "[Source={0}]<br>[Message={1}]<br>[Stack trace={2}]";
            public const string Exception = "<font size='1.7'>Application Error<br>{0}</font>";
        }

        public static class StatusResponse
        {
            public const string Success = "SUCCESS";
            public const string Failed = "FAILED";
            public const string NotProcess = "NOTPROCESS";
        }

        public static class TicketResponse
        {
            public const string SLMSuccess = "10000";
            public const string COCSuccess = "30000";
        }

        public static class ActivityResponse
        {
            public const string Success = "CAS-I-000";
        }

        public static class SystemName
        {
            public const string CSM = "CSM";
            public const string CMT = "CMT";
            public const string SLM = "SLM";
            public const string COC = "COC";
            public const string CAR = "CAR";
            public const string BDW = "BDW";
        }

        public static class PhoneTypeCode
        {
            public const string Mobile  = "02";
            public const string Fax     = "05"; //"FAX";
        }

        public static class DocumentCategory
        {
            public const int Customer = 1;
            public const int ServiceRequest = 2;
            public const int Announcement = 3;
        }

        public static class CustomerType
        {
            public const int Customer = 1;
            public const int Prospect = 2;
            public const int Employee = 3;

            public static string GetMessage(int? customerType)
            {
                if (customerType.HasValue)
                {
                    switch (customerType.Value)
                    {
                        case Customer:
                            return Resources.Resources.Ddl_CustomerType_Customer;
                        case Prospect:
                            return Resources.Resources.Ddl_CustomerType_Prospect;
                        case Employee:
                            return Resources.Resources.Ddl_CustomerType_Employee;
                        default:
                            return string.Empty;
                    }
                }

                return string.Empty;
            }
        }

        public static class CustomerProduct {
            public const string Loan = "Loan";
            public const string Funding = "Funding";
            public const string HP = "HP";
            public const string Insurance = "Insurance";

            //public static string GetMessage(string customerProduct) {
            //    switch (customerProduct)
            //    {
            //        case Loan:
            //            return Resource.Ddl_CustomerProduct_Loan;
            //        case Funding:
            //            return Resource.Ddl_CustomerProduct_Funding;
            //        case HP:
            //            return Resource.Ddl_CustomerProduct_HP;
            //        case Insurance:
            //            return Resource.Ddl_CustomerProduct_Insurance;
            //        default:
            //            return string.Empty;
            //    }

            //    return string.Empty;
            //}
        }

        public static class SubscriptTypeCode
        {
            public const string Personal = "18"; //"01";
        }

        public static class ChannelCode
        {
            public const string Email = "EMAIL";
            public const string Fax = "FAX";
            public const string KKWebSite = "KKWEB";
        }

        public static class DocumentLevel
        {
            public const string Customer = "Customer";
            public const string Sr = "SR";
        }

        public static class SRPage
        {
            public const int DefaultPageId = 1;
            public const int AFSPageId = 2;
            public const int NCBPageId = 3;

            public const string DefaultPageCode = "DEFAULT";
            public const string AFSPageCode = "AFS";
            public const string NCBPageCode = "NCB";
        }

        public static class SrLogAction
        {
            public const string ChangeStatus = "Change Status";
            public const string ChangeOwner = "Change Owner";
            public const string Delegate = "Delegate";
        }


        public static class SRStatusId
        {
            public const int Draft = 1;
            public const int Open = 2;
            public const int WaitingCustomer = 3;
            public const int InProgress = 4;
            public const int RouteBack = 5;
            public const int Cancelled = 6;
            public const int Closed = 7;

            public static int[] JobOnHandStatuses { get { return new int[] { Open, WaitingCustomer, InProgress, RouteBack }; } }

            public static string GetStatusName(int id)
            {
                switch (id)
                {
                    case Draft:
                        return "Draft";
                    case Open:
                        return "Open";
                    case WaitingCustomer:
                        return "Waiting Customer";
                    case InProgress:
                        return "In Progress";
                    case RouteBack:
                        return "Route Back";
                    case Cancelled:
                        return "Cancelled";
                    case Closed:
                        return "Closed";
                    default:
                        return "";
                }
            }
        }

        public static class SRStatusCode
        {
            public const string Draft = "DR";
            public const string Open = "OP";
            public const string WaitingCustomer = "WA";
            public const string InProgress = "IP";
            public const string RouteBack = "RB";
            public const string Cancelled = "CC";
            public const string Closed = "CL";
        }

        public static class SrRoleCode
        {
            public const string ITAdministrator = "IT";
            public const string UserAdministrator = "UA";
            public const string ContactCenterManager = "CM";
            public const string ContactCenterSupervisor = "CS";
            public const string ContactCenterFollowUp = "FL";
            public const string ContactCenterAgent = "CA";
            public const string BranchManager = "BM";
            public const string Branch = "BA";
            public const string NCB = "NCB";
        }

        public static class AddressType
        {
            public const string SendingDoc = "ที่อยู่ส่งเอกสาร";
        }

        public static class CMTParamConfig
        {
            public const string Offered = "Y";
            public const string NoOffered = "N";
            public const string Interested = "Y";
            public const string NoInterested = "N";
            public const string RecommendCampaign = "AND";
            public const string RecommendedCampaign = "OR";
            public const int NumRecommendCampaign = 5;
            public const int NumRecommendedCampaign = 30;

            public static string GetInterestedMessage(string interested)
            {
                if (Interested.Equals(interested))
                {
                    return Resources.Resources.Msg_Interested;
                }

                if (NoInterested.Equals(interested))
                {
                    return Resources.Resources.Msg_NoInterested;
                }

                return string.Empty;
            }
        }

        public static class CustomerLog
        {
            public const string AddCustomer = "เพิ่มข้อมูลลูกค้า";
            public const string EditCustomer = "แก้ไขข้อมูลลูกค้า";
            public const string AddDocument = "เพิ่มเอกสาร";
            public const string EditDocument = "แก้ไขเอกสาร";
            public const string DeleteDocument = "ลบเอกสาร";
            public const string AddContact = "เพิ่มผู้ติดต่อ";
            public const string EditContact = "แก้ไขผู้ติดต่อ";
            public const string DeleteContact = "ลบผู้ติดต่อ";
        }

        public static class Page
        {
            public const string CommunicationPage = "Commu";
            public const string CustomerPage = "Customer";
            public const string ServiceRequestPage = "ServiceRequest";
        }

        public static class Sla
        {
            public const int Due = 1;
            public const int OverDue = 2;
        }

        public static class CallType
        {
            public const string NCB = "NCB";
            public const string ContactCenter = "CC";
        }

        public static class ImportBDWContact
        {
            public const string DataTypeHeader = "H";
            public const string DataTypeDetail = "D";
            public const int LengthOfHeader = 3;
            public const int LengthOfDetail = 19; //18;
        }

        public static class ImportCisData
        {
            public const string DataTypeHeader = "H";
            public const string DataTypeDetail = "D";

            public const int LengthOfHeaderCisCorporate = 35;
            public const int LengthOfHeaderCisIndividual = 52;
            public const int LengthOfHeaderCisProductGroup = 12;
            public const int LengthOfHeaderCisSubscription = 40; //39;
            public const int LengthOfHeaderCisTitle = 9;
            public const int LengthOfHeaderCisProvince = 7;
            public const int LengthOfHeaderCisDistrict = 8;
            public const int LengthOfHeaderCisSubDistrict = 9;
            public const int LengthOfHeaderCisPhoneType = 6;
            public const int LengthOfHeaderEmailType = 6;
            public const int LengthOfHeaderCisSubscriptionAddress = 33; //32;
            public const int LengthOfHeaderCisSubscribePhone = 18; //17;
            public const int LengthOfHeaderCisSubscribeMail = 17; //16;
            public const int LengthOfHeaderCisAddressType = 6;
            public const int LengthOfHeaderCisCisSubscriptionType = 11;
            public const int LengthOfHeaderCisCustomerPhone = 15;
            public const int LengthOfHeaderCisCustomerEmail = 14;
            public const int LengthOfHeaderCisCountry = 7;

            public const int LengthOfDetailCisCorporate = 33;
            public const int LengthOfDetailCisIndividual = 50;
            public const int LengthOfDetailCisProductGroup = 10;
            public const int LengthOfDetailCisSubscription = 38; //37;
            public const int LengthOfDetailCisTitle = 7;
            public const int LengthOfDetailCisProvince = 5;
            public const int LengthOfDetailCisDistrict = 6;
            public const int LengthOfDetailCisSubDistrict = 7;
            public const int LengthOfDetailCisPhoneType = 4;
            public const int LengthOfDetailEmailType = 4;
            public const int LengthOfDetailCisSubscriptionAddress = 31; //30;
            public const int LengthOfDetailCisSubscribePhone = 16; //15;
            public const int LengthOfDetailCisSubscribeMail = 15; //14;
            public const int LengthOfDetailCisAddressType = 4;
            public const int LengthOfDetailCisCisSubscriptionType = 9;
            public const int LengthOfDetailCisCustomerPhone = 13;
            public const int LengthOfDetailCisCustomerEmail = 12;
            public const int LengthOfDetailCisCountry = 5;
        }

        public static class ImportAfs
        {
            public const int LengthOfProperty = 13;
            public const int LengthOfSaleZone = 7;
        }

        public static class ImportHp
        {
            public const int LengthOfDetail = 33;
        }

        public static class TitleLanguage
        {
            public const string TitleTh = "TH";
            public const string TitleEn = "EN";
        }

        public const int CommandTimeout = 180;
        public const int BatchCommandTimeout = 900;

        public static class BatchProcessStatus
        {
            public const int Fail = 0;
            public const int Success = 1;
            public const int Processing = 2;
        }

        public static class BatchProcessCode
        {
            public const string ImportAFS = "A";
            public const string CreateCommPool = "M";
            public const string ExportAFS = "E";
            public const string ExportMarketing = "N";
            public const string ImportBDW = "B";
            public const string ImportCIS = "C";
            public const string ImportHP = "H";

            public const string SyncSRStatusFromReplyEmail = "R";
            public const string ReSubmitActivityToCARSystem = "S";
            public const string ReSubmitActivityToCBSHPSystem = "T";
        }

        public const string SystemUserName = "SYSTEM";

        public static class AttachmentPrefix
        {
            public const string Customer = "C";
            public const string Sr = "S";
            public const string News = "N";
            public const string Job = "J";
        }
    }
}
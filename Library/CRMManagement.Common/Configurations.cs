//-----------------------------------------------------------------------
// <copyright file="TitleConfigurations.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRMManagement.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class configurations
    /// </summary>
    public class Configurations
    {     

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static int PageSize
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static bool TestMode
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["TestMode"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string TestEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["TestEmailAddress"];
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string FromEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["FromEmailAddress"];
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        public static int CookiesValidity
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["CookiesValidity"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string ReachAssetURI
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachAssetURI"];
            }
        }

        /// <summary>
        /// Gets the reach asset user name.
        /// </summary>
        /// <value>
        /// The reach asset user name.
        /// </value>
        public static string ReachAssetUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachAssetUsername"];
            }
        }

        /// <summary>
        /// Gets the reach asset password.
        /// </summary>
        /// <value>
        /// The reach asset password.
        /// </value>
        public static string ReachAssetPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachAssetPassword"];
            }
        }

        /// <summary>
        /// Gets the order service URL.
        /// </summary>
        /// <value>
        /// The order service URL.
        /// </value>
        public static string OrderServiceURL
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderServiceURL"];
            }
        }

        /// <summary>
        /// Gets the reach elastic URI.
        /// </summary>
        /// <value>
        /// The reach elastic URI.
        /// </value>
        public static string ReachElasticURI
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachElasticURI"];
            }
        }

        /// <summary>
        /// Gets the index of the reach elastic.
        /// </summary>
        /// <value>
        /// The index of the reach elastic.
        /// </value>
        public static string ReachElasticIndex
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachElasticIndex"];
            }
        }

        /// <summary>
        /// Gets the iTunes forbidden retry count.
        /// </summary>
        /// <value>
        /// The iTunes forbidden retry count.
        /// </value>
        public static int ItunesForbiddenRetryCount
        {
            get
            {
                return System.Convert.ToInt16(ConfigurationManager.AppSettings["ItunesForbiddenRetryCount"]);
            }
        }

        public static string S3AccessKeyID
        {
            get
            {
                return ConfigurationManager.AppSettings["S3AccessKeyID"];
            }
        }

        public static string S3SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["S3SecretKey"];
            }
        }

        public static string BucketName
        {
            get
            {
                return ConfigurationManager.AppSettings["BucketName"];
            }
       }

        public static string ClientURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientURL"];
            }
        }

        public static string PublicURL
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicURL"];
            }
        }

        public static string SquareUpLocationId
        {
            get
            {
                return ConfigurationManager.AppSettings["SquareUpLocationId"];
            }
        }

        public static string SquareUpAccessToken
        {
            get
            {
                return ConfigurationManager.AppSettings["SquareUpAccessToken"];
            }
        }

        public static string SquareUpApplicationId
        {
            get
            {
                return ConfigurationManager.AppSettings["SquareUpApplicationId"];
            }
        }

        public static string Port
        {
            get
            {
                return ConfigurationManager.AppSettings["Port"];
            }
        }

        public static string EmailHost
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailHost"];
            }
        }

        public static string EnableSsl
        {
            get
            {
                return ConfigurationManager.AppSettings["EnableSsl"];
            }
        }

        public static string EmailUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailUserName"];
            }
        }

        public static string EmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailPassword"];
            }
        }

        public static string BccEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["BccEmailAddress"];
            }
        }

        public static string ToEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ToEmailAddress"];
            }
        }

        public static string ClientUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["websiteUrl"];
            }
        }

        public static string Adminurl
        {
            get
            {
                return ConfigurationManager.AppSettings["Adminurl"];
            }
        }

        public static string APIurl
        {
            get
            {
                return ConfigurationManager.AppSettings["APIurl"];
            }
        }

        public static string UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["UserName"];
            }
        }

        public static string UPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["UPassword"];
            }
        }

        public static string CCAvenue
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenue"];
            }
        }

        public static string BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["baseurl"];
            }
        }

        public static string DelhiveryBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryBaseUrl"];
            }
        }

        public static string DelhiveryTrackingUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryTrackingUrl"];
            }
        }


        public static string S3BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["S3BaseUrl"];
            }
        }

        public static string DelhiveryPickUpPin
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpPin"];
            }
        }
        public static string DelhiveryPickUpAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpAddress"];
            }
        }
        public static string DelhiveryPickUpPhone
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpPhone"];
            }
        }
        public static string DelhiveryPickUpState
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpState"];
            }
        }
        public static string DelhiveryPickUpCity
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpCity"];
            }
        }
        public static string DelhiveryPickUpCountry
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpCountry"];
            }
        }
        public static string DelhiveryPickUpName
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpName"];
            }
        }
        public static string DelhiveryCreateOrderAPIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryCreateOrderAPIURL"];
            }
        }
        public static string DelhiveryToken
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryToken"];
            }
        }

        public static string DelhiveryCreatePincodeAPIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryCreatePincodeAPIURL"];
            }
        }

        public static string ServerKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerKey"];
            }
        }

        public static string RedirectUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RedirectUrl"];
            }
        }

        public static string CancelUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["CancelUrl"];
            }
        }

        public static string Pincode
        {
            get
            {
                return ConfigurationManager.AppSettings["Pincode"];
            }
        }
        public static string CCAvenueMerchantId
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenueMerchantId"];
            }
        }
        public static string CCAvenueCode
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenueCode"];
            }
        }

        public static string CCAvenueWorkingKey
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenueWorkingKey"];
            }
        }
        public static string ImageURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ImageURL"];
            }
        }
        public static string RazorKey
        {
            get
            {
                return ConfigurationManager.AppSettings["RazorKey"];
            }
        }
        public static string RazorSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["RazorSecret"];
            }
        }
    }
}

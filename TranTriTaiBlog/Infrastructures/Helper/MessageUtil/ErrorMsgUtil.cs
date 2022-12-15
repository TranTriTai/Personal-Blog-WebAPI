using System;
namespace TranTriTaiBlog.Infrastructures.Helper.MessageUtil
{
    public static class ErrorMsgUtil
    {
        /// <summary>
        /// Get Bad Request Error Message
        /// </summary>
        /// <param name="objectName">nameof(RequestObjectName/JsonPropertyNames)</param>
        public static string GetBadRequestMsg(string objectName)
        {
            return $"be.bad.request.{objectName}.error";
        }

        /// <summary>
        /// Get Unprocessible Error Message
        /// </summary>
        /// <param name="objectName">nameof(RequestObjectName/JsonPropertyNames)</param>
        public static string GetUnprocessibleMsg(string objectName)
        {
            return $"be.Unprocessible.request.{objectName}.error";
        }

        /// <summary>
        /// Get Error When Getting Message
        /// </summary>
        /// <param name="entityName">nameof(EntityName/EntityName.FieldName)</param>
        public static string GetErrWhenGetting(string entityName)
        {
            return $"be.getting.{entityName.ToLower()}.error";
        }

        /// <summary>
        /// Get Cannot Find Error Message
        /// </summary>
        /// <param name="entityName">nameof(EntityName)</param>
        public static string GetCannotFindMsg(string entityName)
        {
            return $"be.notfound.{entityName.ToLower()}.error";
        }

        /// <summary>
        /// Get Invalid Input Message
        /// </summary>
        public static string GetInvalidInputMsg()
        {
            return "Invalid input.";
        }

        /// <summary>
        /// Get Required Field Error Message
        /// </summary>
        /// <param name="fieldName">nameof(RequestObjectName/JsonPropertyNames)</param>
        public static string GetRequiredFieldMsg(string fieldName)
        {
            return $"be.required.field.{fieldName.ToLower()}.error";
        }

        /// <summary>
        /// Get Error Message When Adding
        /// </summary>
        /// <param name="entityName">nameof(EntityName)</param>
        public static string GetErrWhenAdding(string entityName)
        {
            return $"be.adding.{entityName.ToLower()}.error";
        }

        /// <summary>
        /// Get Unauthorized Error Message
        /// </summary>
        public static string GetUnauthorizedMsg()
        {
            return "be.unauthorized.error";
        }
    }
}


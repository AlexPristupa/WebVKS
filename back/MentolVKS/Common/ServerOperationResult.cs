using MentolVKS.Common.TypeExtensions;
using MentolVKS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MentolVKS.Common
{
    /// <summary>
    ///     Базовый класс для результата операции сервера
    /// </summary>
    [DataContract]
    public abstract class ServerOperationResult
    {
        /// <summary>
        ///     TODO Comment
        /// </summary>
        [DataMember]
        public OperationResult Result { get; set; } = OperationResult.Success;

        /// <summary>
        ///     TODO Comment
        /// </summary>
        [DataMember]
        public string Message { get; set; } = OperationResult.Success.GetDisplayName();
    }

    /// <summary>
    ///     Базовый класс для результата операции сервера, возвращающей данные
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ServerOperationResult<T> : ServerOperationResult where T : class
    {
        /// <summary>
        ///     TODO Comment
        /// </summary>
        [DataMember]
        public T Data { get; set; }
    }
}

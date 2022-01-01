using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.Filters.Dto
{
    public enum OperationType
    {
        Greater = 1,
        Lower,
        GreaterOrEq,
        LowerOrEq,
        Eq,
        NotEq
    }
    public static class OperationTypeExtension
    {
        private static Dictionary<OperationType, string> _operations = new Dictionary<OperationType, string>
        {
            { OperationType.Eq, "=" },
            { OperationType.Greater, ">" },
            { OperationType.GreaterOrEq, ">=" },
            { OperationType.Lower, "<" },
            { OperationType.LowerOrEq, "<=" },
            { OperationType.NotEq, "!=" }
        };

        public static string AsString(this OperationType operationType)
        {
            if (!_operations.ContainsKey(operationType))
            {
                throw new Exception("Unknown operation type");
            }

            return _operations[operationType];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogicCore.Tasking
{

    public interface ITaskingBase { }

    public interface ITasking : ITaskingBase
    {
        void Execute(ITaskContext context);
    }


    public interface ITaskingInitialize : ITasking
    {
        void Initialize(ITaskContext context);
    }

    public interface ITaskingAsync : ITaskingBase
    {
        Task ExecuteAsync(ITaskContext context);
    }
}

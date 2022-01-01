using MentolVKS.Common.TypeExtensions;
using MentolVKS.Data.EF.Bases;
using MentolVKS.Data.EF.Classes;
using MentolVKS.Data.EF.Classes.ModelsFactory;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data.Interfaces.Repository;
using MentolVKS.Model;
using MentolVKS.Model.BaseModel;
using MentolVKS.Model.Bases;
using MentolVKS.Model.Filters.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentolVKS.Data.EF.Repository
{
    internal class AgGridVueRepository : TableBasedEntityRepositoryBase<TableBasedEntityBase>, IAgGridVueRepository
    {
        public AgGridVueRepository(DataContext context, IColumnMappingConfiguration mappings) : base(context, mappings)
        {
        }

        /// <inheritdoc />
        public async Task<dynamic> GetDataAsync(FilterQuery filters, IEnumerable<string> listFields)
        {
            // Получить тип модели из фабрики моделей на основании имени таблицы, полученной с фронта
            IModelFactory modelFactory = new ModelFactory();
            var model = modelFactory.TableNameToModels.FirstOrDefault(v => v.TableName == filters.TableName);
            if (model == null)
                throw new Exception($"Не добавлена модель {filters.TableName} в файле {nameof(ModelFactory)}.cs");

            // ReSharper disable once PossibleNullReferenceException
            var genericMethod = typeof(ModelToQuery).GetMethod(nameof(ModelToQuery.GetDataAsync)).MakeGenericMethod(model.Model.GetType());
            var modelToQuery = new ModelToQuery(Context, model, filters, listFields);
            var data = await (Task<dynamic>)genericMethod.Invoke(modelToQuery, null);

            return data;
        }

        /// <summary>
        /// Получить модели фабрики
        /// </summary>
        /// <returns>Списсок моделей</returns>
        public IEnumerable<ITableNameToModel> GetModelFactoryList()
        {
            IModelFactory modelFactory = new ModelFactory();
            return modelFactory.TableNameToModels;
        }

        /// <inheritdoc />
        public ITableNameToModel GetModelFactory(string tableName)
        {
            IModelFactory modelFactory = new ModelFactory();
            return modelFactory.TableNameToModels.FirstOrDefault(x => x.TableName.Is(tableName));
        }
    }
}

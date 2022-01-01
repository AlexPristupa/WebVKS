using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MentolVKS.Model.ViewModel
{
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }

    public static class TreeHelpers
    {
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
            this IEnumerable<T> collection,
            Func<T, K> id_selector,
            Func<T, K> parent_id_selector,
            K root_id = default(K))
        {
            foreach (var c in collection.Where(c => EqualityComparer<K>.Default.Equals(parent_id_selector(c), root_id)))
            {
                yield return new TreeItem<T>
                {
                    Item = c,
                    Children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c))
                };
            }
        }

        public static IEnumerable<K> GetFlatKeys<T, K>(this IEnumerable<TreeItem<T>> source, Func<T, K> id_selector)
        {
            if (source == null)
                yield break;
            foreach (var parent in source)
            {
                yield return id_selector(parent.Item);
                foreach (var c in GetFlatKeys(parent.Children, id_selector))
                    yield return c;
            }
        }

        /// <summary>
        /// Поиск по дереву для ролей. Если есть доступ к верхнему узлу и нет по поиску дочерних. То все дочерние доступны.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="source"></param>
        /// <param name="id_selector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<TreeItem<T>> ContainsTreeChildFull<T, K>(this IEnumerable<TreeItem<T>> source, Func<T, K> id_selector, List<K> value)
        {
            List<TreeItem<T>> result = new List<TreeItem<T>>();
            foreach (var element in source)
            {
                //if (value.Contains(id_selector(element.Item))) result.Add(element);

                IEnumerable<TreeItem<T>> childResult = new List<TreeItem<T>>();
                if (element.Children.Count() > 0)
                {
                    childResult = ContainsTreeChildFull(element.Children, id_selector, value);
                }

                if (childResult.Count() > 0)
                {
                    element.Children = childResult;
                    result.Add(element);
                }
                else
                {
                    if (value.Contains(id_selector(element.Item)))
                    {
                        result.Add(element);
                    }
                }
            }

            return result;
        }

        public static IEnumerable<TreeItem<T>> ContainsTree<T,K>(this IEnumerable<TreeItem<T>> source, Func<T,K> id_selector, List<K> value)
        {
            List<TreeItem<T>> result = new List<TreeItem<T>>();
            foreach(var element in source)
            {                
                //if (value.Contains(id_selector(element.Item))) result.Add(element);

                IEnumerable<TreeItem<T>> childResult = new List<TreeItem<T>>();
                if (element.Children.Count() > 0)
                {
                    childResult = ContainsTree(element.Children, id_selector, value);
                }

                if (childResult.Count() > 0)
                {
                    element.Children = childResult;
                    result.Add(element);
                }
                else
                {
                    if (value.Contains(id_selector(element.Item)))
                    {
                        element.Children = new List<TreeItem<T>>();
                        result.Add(element);
                    }
                }
            }

            return result;
        }
    }
}

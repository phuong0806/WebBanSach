using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model.BusinessModel
{
    public class ReflectionController
    {
        //Lấy danh sách controllers theo namespaces
        public List<Type>  getListControllers(string namespaces)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces)).OrderBy(x => x.Name);
            return types.ToList();

        }

        //Lấy danh sách actions theo controller
        public List<string> getListActions(Type controller)
        {
            List<string> listAction = new List<string>();
            IEnumerable<MemberInfo> memberInfo = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);
            foreach (MemberInfo item in memberInfo)
            {
                if (item.ReflectedType.IsPublic && !item.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(item.Name.ToString());
                }
            }
            return listAction;
        }
    }
}

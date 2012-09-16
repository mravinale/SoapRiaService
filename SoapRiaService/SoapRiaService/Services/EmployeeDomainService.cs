
namespace SoapRiaService {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using SoapRiaService.Models;


    // Implements application logic using the SoapTestDbEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class EmployeeDomainService : LinqToEntitiesDomainService<SoapTestDbEntities> {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Employees' query.
        [Invoke]
        [Query(IsDefault = true)]
        public IQueryable<Employee> GetEmployees() {
            return this.ObjectContext.Employees;
        }

        [Invoke]
        public void InsertEmployee(Employee employee) {
            if ((employee.EntityState != EntityState.Detached)) {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(employee, EntityState.Added);
            }
            else {
                this.ObjectContext.Employees.AddObject(employee);
            }
        }

        [Invoke]
        public void UpdateEmployee(Employee currentEmployee) {
            this.ObjectContext.Employees.AttachAsModified(currentEmployee, this.ChangeSet.GetOriginal(currentEmployee));
        }

        [Invoke]
        public void DeleteEmployee(Employee employee) {
            if ((employee.EntityState == EntityState.Detached)) {
                this.ObjectContext.Employees.Attach(employee);
            }
            this.ObjectContext.Employees.DeleteObject(employee);
        }
    }
}



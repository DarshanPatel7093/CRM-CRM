//-----------------------------------------------------------------------
// <copyright file="ServiceModule.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRMManagement.Services
{
    using Autofac;    
    using Data;
    using CRMManagement.Services.Contract;

    /// <summary>
    /// The Service module for dependency injection.
    /// </summary>
    public class ServiceModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {           
            builder.RegisterModule<DataModule>();
            
            builder.RegisterType<V1.UsersServices>().As<AbstractUsersServices>().InstancePerDependency();
            builder.RegisterType<V1.RolesServices>().As<AbstractRolesServices>().InstancePerDependency();
            builder.RegisterType<V1.StatusDescServices>().As<AbstractStatusDescServices>().InstancePerDependency();
            builder.RegisterType<V1.OpportunitiesServices>().As<AbstractOpportunitiesServices>().InstancePerDependency();
            builder.RegisterType<V1.Opportunities_TasksServices>().As<AbstractOpportunities_TasksServices>().InstancePerDependency();
            builder.RegisterType<V1.CompanyServices>().As<AbstractCompanyServices>().InstancePerDependency();
            builder.RegisterType<V1.CompanyContactsServices>().As<AbstractCompanyContactsServices>().InstancePerDependency();
            builder.RegisterType<V1.ProjectsServices>().As<AbstractProjectsServices>().InstancePerDependency();
            builder.RegisterType<V1.ProjectsTimeTrackingServices>().As<AbstractProjectsTimeTrackingServices>().InstancePerDependency();
            builder.RegisterType<V1.CallLogServices>().As<AbstractCallLogServices>().InstancePerDependency();
          
            base.Load(builder);
        }
    }
}

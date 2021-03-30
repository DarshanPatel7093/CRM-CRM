//-----------------------------------------------------------------------
// <copyright file="DataModule.cs" company="Dexoc Solutions">
//     Copyright Dexoc Solutions. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CRMManagement.Data
{
    using Autofac;
    using CRMManagement.Data.Contract;

    /// <summary>
    /// Contract Class for DataModule.
    /// </summary>
    public class DataModule : Module
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
            builder.RegisterType<V1.UsersDao>().As<AbstractUsersDao>().InstancePerDependency();
            builder.RegisterType<V1.RolesDao>().As<AbstractRolesDao>().InstancePerDependency();
            builder.RegisterType<V1.StatusDescDao>().As<AbstractStatusDescDao>().InstancePerDependency();
            builder.RegisterType<V1.OpportunitiesDao>().As<AbstractOpportunitiesDao>().InstancePerDependency();
            builder.RegisterType<V1.Opportunities_TasksDao>().As<AbstractOpportunities_TasksDao>().InstancePerDependency();
            builder.RegisterType<V1.CompanyContactsDao>().As<AbstractCompanyContactsDao>().InstancePerDependency();
            builder.RegisterType<V1.CompanyDao>().As<AbstractCompanyDao>().InstancePerDependency();
            builder.RegisterType<V1.ProjectsDao>().As<AbstractProjectsDao>().InstancePerDependency();
            builder.RegisterType<V1.ProjectsTimeTrackingDao>().As<AbstractProjectsTimeTrackingDao>().InstancePerDependency();
            builder.RegisterType<V1.CallLogDao>().As<AbstractCallLogDao>().InstancePerDependency();
            base.Load(builder);
        }
    }
}

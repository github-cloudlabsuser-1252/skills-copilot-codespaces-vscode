using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

public class AccountCreatePlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        // Obtain the execution context from the service provider.
        IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

        // The InputParameters collection contains all the data passed in the message request.
        if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
        {
            // Obtain the target entity from the input parameters.
            Entity entity = (Entity)context.InputParameters["Target"];

            // Verify that the target entity represents an account.
            // If not, this plug-in was not registered correctly.
            if (entity.LogicalName != "account")
                return;

            try
            {
                // Obtain the organization service reference.
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                // Example: Add a custom logic here
                // For instance, set a default value for a custom field
                if (!entity.Attributes.Contains("customfield"))
                {
                    entity["customfield"] = "DefaultValue";
                }

                // Example: Retrieve related data or perform additional operations
                // QueryExpression query = new QueryExpression("contact");
                // query.ColumnSet = new ColumnSet("firstname", "lastname");
                // query.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, entity.Id);
                // EntityCollection contacts = service.RetrieveMultiple(query);

                // Perform operations based on retrieved data
                // foreach (var contact in contacts.Entities)
                // {
                //     // Custom logic here
                // }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException($"An error occurred in the AccountCreatePlugin: {ex.Message}", ex);
            }
        }
    }
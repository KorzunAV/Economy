using System.Collections.Generic;
using System.Linq;
using CQRS.Common.Errors;
using NHibernate.Event;
using NHibernate.Validator.Engine;

namespace Economy.DataAccess.NHibernate.NHibernate
{
    public class ValidateFieldsEventListener : IPreInsertEventListener, IPreUpdateEventListener
    {
        protected ValidatorEngine ValidatorEngine { get; private set; }

        public ValidateFieldsEventListener(ValidatorEngine validatorEngine)
        {
            ValidatorEngine = validatorEngine;
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            Validate(@event.Entity);

            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            Validate(@event.Entity);

            return false;
        }

        private void Validate(object entity)
        {
            List<string> validationErrors = new List<string>();
            InvalidValue[] invalidValues = ValidatorEngine.Validate(entity);
            foreach (var invalidValue in invalidValues)
            {
                string fieldName = string.Format("{0}_{1}", entity.GetType().Name, invalidValue.PropertyName);
                string msg = string.Format("{0}: {1}", fieldName, invalidValue.Message);

                validationErrors.Add(msg);
            }

            if (validationErrors.Count > 0)
            {
                List<ErrorInfo> errors = invalidValues.Select(iv => new ErrorInfo(iv.PropertyPath, iv.Message)).ToList();
                throw new ValidationException(errors);
            }
        }
    }
}
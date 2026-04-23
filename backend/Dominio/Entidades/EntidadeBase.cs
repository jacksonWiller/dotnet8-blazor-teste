using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Clientes.Dominio.Eventos;

namespace Clientes.Dominio.Entidades
{
    public class EntidadeBase
    {

        [NotMapped]
        private readonly List<EventoBase> _domainEvents = [];

        [NotMapped] 
        public FluentValidation.Results.ValidationResult ValidationResult { get; private set; } = new FluentValidation.Results.ValidationResult();

        public bool Validar<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return ValidationResult.IsValid;
        }

        public IEnumerable<string> ObterErros()
        {
            return ValidationResult?.Errors.Select(e => e.ErrorMessage) ?? [];
        }

        protected void AddDomainEvent(EventoBase domainEvent) =>
            _domainEvents.Add(domainEvent);
    }
}

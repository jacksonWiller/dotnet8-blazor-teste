using FluentValidation;
using Clientes.Dominio.ObjetosDeValor;
using Clientes.Dominio.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Aplicacao.Commands.CreateCliente
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;

        public CreateClienteCommandValidator(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

            AdicionarValidacoesDadosBasicos();
            AdicionarValidacoesDocumento();
            AdicionarValidacoesContato();
            AdicionarValidacoesIdade();
            AdicionarValidacoesInscricaoEstadual();
            AdicionarValidacoesEndereco();
        }

        #region Grupos de Validação

        private void AdicionarValidacoesDadosBasicos()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 200).WithMessage("O nome deve ter entre 2 e 200 caracteres");

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória");
        }

        private void AdicionarValidacoesDocumento()
        {
            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório");

            RuleFor(c => c.Documento)
                .MustAsync(async (documento, cancellation) =>
                    !(await _clienteRepository.ExisteClienteComDocumentoAsync(documento)))
                .WithMessage("Já existe um cliente cadastrado com este documento")
                .When(c => !string.IsNullOrEmpty(c.Documento));

            RuleFor(c => c.Documento)
                .Must((command, documento) => DocumentoValido(command.TipoDocumento, documento))
                .WithMessage(c => c.TipoDocumento == TipoDocumento.CPF
                    ? "CPF inválido"
                    : "CNPJ inválido")
                .When(c => !string.IsNullOrEmpty(c.Documento));
        }

        private void AdicionarValidacoesContato()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail em formato inválido");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail em formato inválido")
                .MustAsync(async (email, cancellation) =>
                    !(await _clienteRepository.ExisteClienteComEmailAsync(email)))
                .WithMessage("Este e-mail já está em uso")
                .When(c => !string.IsNullOrEmpty(c.Email));

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório")
                .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter 10 ou 11 dígitos");
        }

        private void AdicionarValidacoesIdade()
        {
            When(c => c.TipoDocumento == TipoDocumento.CPF, () =>
            {
                RuleFor(c => c.DataNascimento)
                    .Must(IdadeMinima18Anos)
                    .WithMessage("Cliente deve ter pelo menos 18 anos");
            });
        }

        private void AdicionarValidacoesInscricaoEstadual()
        {
            When(c => c.TipoDocumento == TipoDocumento.CNPJ, () =>
            {
                RuleFor(c => c.InscricaoEstadual)
                    .NotEmpty()
                    .When(c => !c.Isento)
                    .WithMessage("É necessário informar a Inscrição Estadual ou marcar como Isento");
            });
        }

        private void AdicionarValidacoesEndereco()
        {
            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório")
                .Matches(@"^\d{8}$").WithMessage("CEP deve conter 8 dígitos");

            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O logradouro é obrigatório");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O número é obrigatório");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório")
                .Length(2).WithMessage("O estado deve ter 2 caracteres");
        }

        #endregion

        #region Métodos de Validação

        private bool DocumentoValido(TipoDocumento tipo, string documento)
        {
            return tipo == TipoDocumento.CPF
                ? ValidarCPF(documento)
                : ValidarCNPJ(documento);
        }

        private bool IdadeMinima18Anos(DateTime dataNascimento)
        {
            var idade = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade))
                idade--;
            return idade >= 18;
        }

        #endregion

        #region Validadores de Documentos

        protected virtual bool ValidarCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        protected virtual bool ValidarCNPJ(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            if (cnpj.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj;
            string digito;
            int soma;
            int resto;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        #endregion
    }
}
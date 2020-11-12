using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sysplan.Application.Interfaces;
using Sysplan.Application.ViewModels;
using Sysplan.Crosscutting.Domain.Bus;
using Sysplan.Crosscutting.Domain.Controller;
using Sysplan.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace Sysplan.Api.Controllers
{
    [Route("sysplan-api/v1/clientes")]
    [ApiController]
    public class ClienteController : ApiBaseController
    {
        private readonly IClienteServiceApp _clienteServiceApp;

        public ClienteController(
            IClienteServiceApp clienteServiceApp,
            IMediatorHandler mediator) : base(mediator)
        {
            _clienteServiceApp = clienteServiceApp;
        }

        /// <summary>
        /// Efetua a consulta paginada dos registros cadastrados
        /// </summary>
        /// <param name="page">Página atual</param>
        /// <param name="size">Quantidade de registros por página</param>
        /// <param name="orderProperty">Propriedade para ordenação</param>
        /// <param name="orderCrescent">Ordem dos registros</param>
        /// <param name="filterProperty">Propriedade para filtro</param>
        /// <param name="filterValue">Valor da propridade de filtro</param>
        /// <returns>Lista paginada de registros</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int size = 20,
            string orderProperty = "Id", string orderCrescent = "true",
            string filterProperty = null, string filterValue = null)
        {
            var demos = await _clienteServiceApp.GetPaged(page, size, orderProperty,
                Convert.ToBoolean(orderCrescent), filterProperty, filterValue);

            return Response(demos);
        }

        /// <summary>
        /// Efetua a consulta de um registro pelo seu código
        /// </summary>
        /// <param name="id">Código do registro</param>
        /// <returns>Registro encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var demo = await _clienteServiceApp.Get(Guid.Parse(id));

            return Response(demo);
        }

        /// <summary>
        /// Efetua o cadastro de um novo registro
        /// </summary>
        /// <param name="command">Dados do registro</param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AddClienteCommand command)
        {
            await _clienteServiceApp.Add(command);

            return Response();
        }

        /// <summary>
        /// Efetua a atualização de um registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command">Dados do registro</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateClienteCommand command)
        {
            command.Id = id;
            await _clienteServiceApp.Update(command);

            return Response();
        }

        /// <summary>
        /// Efetua a exclusão de um registro
        /// </summary>
        /// <param name="id">Código do registro</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _clienteServiceApp.Remove(Guid.Parse(id));

            return Response();
        }
    }
}
﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UPTEAM.AutoMapper.Parses.Interfaces;
using UPTEAM.Domain.ServiceInterfaces;
using UPTEAM.Models;

namespace UPTEAM.Presentation.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class MensagemController : BaseController
    {
        private IMensagemService _mensagemService;
        private IUsuarioService _usuarioService;
        private ITbMensagemToMensagemModelParse _parseTbMensagemToMensagemModel;
        private IMensagemModelToTbMensagemParse _parseMensagemModelToTbMensagem;
        public MensagemController(IMensagemService mensagemService, IUsuarioService usuarioService,
                                  ITbMensagemToMensagemModelParse parseTbMensagemToMensagemModel,
                                  IMensagemModelToTbMensagemParse parseMensagemModelToTbMensagem)
        {
            _mensagemService = mensagemService;
            _usuarioService = usuarioService;
            _parseTbMensagemToMensagemModel = parseTbMensagemToMensagemModel;
            _parseMensagemModelToTbMensagem = parseMensagemModelToTbMensagem;
        }

        [HttpPost]
        [Authorize]
        [Route("mensagem")]
        public Task<HttpResponseMessage> Post([FromBody]dynamic body)
        {
            try
            {
                var usuario = _usuarioService.ObterUsuarioPorLogin(ObterUsuarioLogado());
                var mensagemModel = new MensagemModel()
                {
                    TextoMensagem = (string)body.textomensagem,
                    Equipe = (int)body.equipe,
                    IdUsuario = usuario.idt_usuario
                    
                };

                var mensagemTb = _parseMensagemModelToTbMensagem.Parse(mensagemModel);

                var novaMensagem = _mensagemService.EnviarMensagem(mensagemTb);

                if (novaMensagem != null)
                {
                    return CreateResponse(HttpStatusCode.OK, null, null);
                }

                return CreateResponse(HttpStatusCode.BadRequest, null, null);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.BadRequest, null, null);
            }

        }

        [HttpGet]
        [Authorize]
        [Route("equipe/{idEquipe:int}/mensagem")]
        public Task<HttpResponseMessage> BuscarPorEquipe(int idEquipe)
        {
            try
            {
                var mensagemTb = _mensagemService.BuscarPorEquipe(idEquipe).ToList();
                if (mensagemTb != null)
                {
                    var mensagemVM = _parseTbMensagemToMensagemModel.Parse(mensagemTb);
                    mensagemVM.ToList().ForEach(x => x.IdUsuario = null);
                    return CreateResponse(HttpStatusCode.OK, mensagemVM, null);
                }
                return CreateResponse(HttpStatusCode.NotFound, null, null);
            }
            catch (Exception ex)
            {
                return CreateResponse(HttpStatusCode.BadRequest, null, null);
            }
        }
    }
}
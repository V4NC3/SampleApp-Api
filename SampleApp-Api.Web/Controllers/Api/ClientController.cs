using SampleApp.Model.Request;
using SampleApp.Model.Responses;
using SampleApp.Model.ViewModel;
using SampleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleApp_Api.Web.Controllers.Api
{
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {   
        [Route("register"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Register(ClientAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClientServices svc = new ClientServices();
                    int? id = svc.Insert(model);

                    ItemResponse<int?> resp = new ItemResponse<int?>();
                    resp.Item = id;

                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                throw;
            }
        }

        [Route("login"), HttpPost, AllowAnonymous]
        public HttpResponseMessage Login(LoginAddRequest model)
        {
            try
            {
                ClientServices svc = new ClientServices();
                LoginViewModel success = svc.Login(model);
                if (success != null)
                {
                    LoginResponse<LoginViewModel> resp = new LoginResponse<LoginViewModel>();
                    resp.Item = success;
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                else
                {
                    ErrorResponse resp = new ErrorResponse("Unsuccessful Login Attempt");
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [Route("logout"), HttpGet, AllowAnonymous]
        public HttpResponseMessage Logout()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new SampleApp.Model.Responses.SuccessResponse());
        }
    }
}

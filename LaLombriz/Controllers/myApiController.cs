﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LaLombriz.Clases;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

namespace LaLombriz.Controllers
{
    public class myApiController : ApiController
    {
        [HttpPost]
        [ActionName("Pago")]
        public async System.Threading.Tasks.Task<IHttpActionResult> PostPagoAsync(Informacion pago)
        {
            try
            {
                MercadoPagoConfig.AccessToken = "TEST-3969616877780286-040702-dea28e2430645874fedab6a0223727c6-169461027";
                pago.transactionAmount = HttpUtility.HtmlEncode(pago.transactionAmount);
                pago.correo = HttpUtility.HtmlEncode(pago.correo);
                pago.description = HttpUtility.HtmlEncode(pago.description);
                pago.token = HttpUtility.HtmlEncode(pago.token);
                pago.installments = HttpUtility.HtmlEncode(pago.installments);
                pago.payment_method_id = HttpUtility.HtmlEncode(pago.payment_method_id);
                pago.issuer_id = HttpUtility.HtmlEncode(pago.issuer_id);

                var paymentRequest = new PaymentCreateRequest
                {
                    TransactionAmount = Convert.ToDecimal(pago.transactionAmount),
                    Token = pago.token,
                    Description = pago.description,
                    Installments = Convert.ToInt32(pago.installments),
                    PaymentMethodId = pago.payment_method_id,
                    Payer = new PaymentPayerRequest
                    {
                        Email = pago.correo,
                        Identification = new IdentificationRequest
                        {
                            Number = pago.issuer_id,
                        },
                    },
                };
                var client = new PaymentClient();
                Payment payment = await client.CreateAsync(paymentRequest);
                Console.WriteLine(payment.Status);
                return Redirect("https://localhost:44393//Formularios/Menu.aspx");
                //return Request.CreateResponse(HttpStatusCode.OK, payment.Status);
            }
            catch
            {
                return Redirect("https://localhost:44393//Formularios/Inicio.aspx");
                //return Request.CreateResponse(HttpStatusCode.OK, "Algo salió mal dentro del controlador :u");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AutoMapper;
using Data;
using Data.Contracts;
using Data.Models;
using Web.Models;

namespace Web.Controllers
{

    //[EnableCors(origins: "http://localhost:54231", headers: "*", methods: "*")]
    public class DomainsController : ApiController
    {
        private readonly IRepository<Domain> _repository;


        public DomainsController(IRepository<Domain> repository)
        {
            _repository = repository;
        }

        // GET: api/Domains
        [ResponseType(typeof(DomainViewModel))]
        public IHttpActionResult Get()
        {
            try
            {
                var domains = new List<DomainViewModel>();
                var result = _repository.GetAll().Select(Mapper.Map<DomainViewModel>);
                for (int i = 0; i < 20; i++)
                {
                    domains.AddRange(result);
                }
                return Ok(domains);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET: api/Domains/5
        [ResponseType(typeof(DomainViewModel))]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Domain domain;
                if (id > 0)
                {

                    domain = _repository.GetById(id);

                    if (domain == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    domain = new Domain();
                }
                return Ok(Mapper.Map<DomainViewModel>(domain));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Domains
        [ResponseType(typeof(DomainViewModel))]
        public IHttpActionResult Post([FromBody]DomainViewModel domain)
        {
            try
            {
                if (domain == null)
                {
                    return BadRequest("Domain cannot be null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newDomain = _repository.Add(Mapper.Map<Domain>(domain));

                if (newDomain == null)
                {
                    return Conflict();
                }
                return Created(Request.RequestUri + newDomain.Id.ToString(), Mapper.Map<DomainViewModel>(newDomain));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Domains/5
        [ResponseType(typeof(DomainViewModel))]
        public IHttpActionResult Put(int id, [FromBody]DomainViewModel domain)
        {
            try
            {
                if (domain == null)
                {
                    return BadRequest("Domain cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedDomain = _repository.Update(Mapper.Map<Domain>(domain));
                if (updatedDomain == null)
                {
                    return NotFound();
                }
                return Ok(updatedDomain);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Domains/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
